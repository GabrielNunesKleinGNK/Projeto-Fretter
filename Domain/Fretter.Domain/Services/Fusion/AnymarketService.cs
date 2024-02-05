
using Fretter.Domain.Config;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers.Extensions;
using System.Text;
using Fretter.Domain.Dto.Anymarket;

namespace Fretter.Domain.Services
{
    public class AnymarketService<TContext> : ServiceBase<TContext, EntregaConfiguracao>, IAnymarketService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaConfiguracaoRepository<TContext> _entregaConfiguracaoRepository;
        private readonly IEmpresaRepository<TContext> _empresaRepository;
        private readonly FusionApiSkuConfig _fusionApiSkuConfig;

        public AnymarketService(IEntregaConfiguracaoRepository<TContext> entregaConfiguracaoRepository,
                                          IEmpresaRepository<TContext> empresaRepository,
                                          IUnitOfWork<TContext> unitOfWork,
                                          IOptions<FusionApiSkuConfig> fusionApiSkuConfig,
                                          IUsuarioHelper user) : base(entregaConfiguracaoRepository, unitOfWork, user)
        {
            _empresaRepository = empresaRepository;
            _entregaConfiguracaoRepository = entregaConfiguracaoRepository;
            _fusionApiSkuConfig = fusionApiSkuConfig.Value;
        }

        public async Task ImportarSku()
        {
            List<EntregaConfiguracao> entregasConfig = _entregaConfiguracaoRepository
                                                                .GetAll(x => x.EntregaConfiguracaoTipo == EnumEntregaConfiguracaoTipo.Anymarket.GetHashCode())
                                                                .ToList();
            foreach (var entregaConfig in entregasConfig)
                if (DateTime.Now >= entregaConfig.DataProximaExecucao || entregaConfig.DataProximaExecucao == null)
                {
                    switch (entregaConfig.IntervaloExecucaoTipo)
                    {
                        case (int)EnumEntregaConfiguracaoIntervaloExecucaoTipo.Dias:
                            entregaConfig.AtualizarDataProximaExecucao(DateTime.Now.AddDays((int)entregaConfig.IntervaloExecucao));
                            break;
                        case (int)EnumEntregaConfiguracaoIntervaloExecucaoTipo.Horas:
                            entregaConfig.AtualizarDataProximaExecucao(DateTime.Now.AddHours((int)entregaConfig.IntervaloExecucao));
                            break;
                        case (int)EnumEntregaConfiguracaoIntervaloExecucaoTipo.Minuto:
                            entregaConfig.AtualizarDataProximaExecucao(DateTime.Now.AddMinutes((int)entregaConfig.IntervaloExecucao));
                            break;
                        default:
                            entregaConfig.AtualizarDataProximaExecucao(null);
                            break;
                    }

                    _entregaConfiguracaoRepository.Update(entregaConfig);
                    MontarLog(entregaConfig, 0, 0, 0, "Inicio da importação de skus", DateTime.Now);
                    await ObterListaSkus(entregaConfig);
                    _unitOfWork.Commit();
                }
        }

        private async Task ObterListaSkus(EntregaConfiguracao entregaConfig)
        {
            int contadorDeSkus = 0;
            long primeiroSku = 0, ultimoSku = 0; 
            bool processamento = true;
            DateTime dataInicial = DateTime.Now;
            StringBuilder pagesError = new StringBuilder();
            AnymarketSkuDto result = new AnymarketSkuDto();
            List<ContentAnymarket> listaDeSkus = new List<ContentAnymarket>();

            var configItem = entregaConfig.Itens.FirstOrDefault(x => x.EntregaConfiguracaoItemTipoId == EnumEntregaConfiguracaoItemTipo.SkusAnymarket.GetHashCode());
            configItem.AtualizarProcessamento(DateTime.Now);

            WebApiClient webApiClient = new WebApiClient(entregaConfig.Caminho);
            var listHeaderItem = PreparaHeader(string.IsNullOrEmpty(configItem.LayoutHeader) ? entregaConfig.LayoutHeader : configItem.LayoutHeader);
            var url = ParseObjectHelper.ConvertObjectToLayoutString(configItem.Url, new { limit = 100 });

            while (processamento)
            {
                try
                {
                    result = await webApiClient.GetWithHeader<AnymarketSkuDto>(url, listHeaderItem);
                    if (result.Content != null)
                    {
                        listaDeSkus.AddRange(result.Content);
                        url = result.Links.FirstOrDefault(x => x.Rel.ToUpper() == "NEXT").Href.ToString();
                        int skusProcessados = await ProcessaEnvia(entregaConfig, listaDeSkus);

                        if (result.Page.Number == 0)
                            primeiroSku = result.Content.FirstOrDefault().Id;
                        ultimoSku = result.Content.LastOrDefault().Id;
                        contadorDeSkus += skusProcessados;
                        listaDeSkus.Clear();
                    }
                }
                catch (Exception ex)
                {
                    pagesError.Append(((int)(result?.Page?.Number) + 1) + ", ");
                    configItem.AtualizarProcessamentoSucesso(false);
                    MontarLog(entregaConfig, 0, 0, listaDeSkus.Count,
                        $"Falha ao obter a lista de Skus da página { (result?.Page?.Number) } que esta sendo consultado em lotes de " +
                        $"{entregaConfig.Registro} na anymarket. Message: {ex.Message} ", dataInicial);

                    var tratativaUrl = url.Split("offset=");
                    if (tratativaUrl.Count() > 1)
                        url = $"{tratativaUrl[0]}offset={(Convert.ToInt64(tratativaUrl[1]) + 100)}";
                    else
                        break;
                }

                if (result?.Content == null)
                    processamento = false;
            }

            MontarLog(entregaConfig, primeiroSku, ultimoSku, (int)contadorDeSkus,
                $"Finalização da leitura e processamento dos skus da anymarket. " +
                $"{contadorDeSkus} skus encontados. {(pagesError.Length > 0 ? $"Tiveram erros nas páginas {pagesError}" : string.Empty)}.", dataInicial);
            configItem.AtualizarProcessamentoSucesso(true);
        }

        private async Task<int> ProcessaEnvia(EntregaConfiguracao entregaConfig, List<ContentAnymarket> contentAnymarket)
        {
            int contadorDeSkus = 0;
            DateTime dataInicial = DateTime.Now;
            List<Sku> skusEnvio = new List<Sku>();
            foreach (var produto in contentAnymarket)
                foreach (var sku in produto.Skus)
                {
                    string fornecedor = produto.Brand?.Name ?? "Fornecedor não informado";
                    Sku skuEnvio = new Sku(0, sku?.PartnerId?.ToString(), null, produto.Title, "", "UN", null,
                                           (decimal)sku?.SellPrice, (decimal)sku?.Price, (decimal)produto.Weight, 
                                           (decimal)produto.Weight, null, fornecedor, fornecedor,
                                            null, null, null, true, false);
                    skuEnvio.Medidas = new SkuMedidas(0, (decimal)produto.Width, (decimal)produto.Height, (decimal)produto.Length, 100);
                    
                    skusEnvio.Add(skuEnvio);
                    contadorDeSkus++;
                }

            var result = EnviarSkuEndpointFusion(skusEnvio, entregaConfig.EmpresaId).Result;
            MontarLog(entregaConfig, long.Parse(skusEnvio?.FirstOrDefault()?.Codigo), long.Parse(skusEnvio?.LastOrDefault()?.Codigo), skusEnvio.Count, result.Truncate(2050), dataInicial);

            return contadorDeSkus;
        }

        private async Task<string> EnviarSkuEndpointFusion(List<Sku> skusEnvio, int empresaId)
        {
            Guid? token = _empresaRepository.GetAll(x => x.Id == empresaId)?.FirstOrDefault()?.TokenId;
            WebApiClient webApiClient = new WebApiClient(_fusionApiSkuConfig.Url);
            var listHeaderItem = PreparaHeader($"Token={token}|api-key={token}");

            try
            {
                var objResponse = await webApiClient.PostListWithHeader<object>(null, skusEnvio, null, listHeaderItem);
                var response = await objResponse.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private List<KeyValuePair<string, string>> PreparaHeader(string layoutHeader)
        {
            var listHeaderItem = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(layoutHeader))
            {
                var listHeader = layoutHeader.Split("|").ToList();
                foreach (var itemHeader in listHeader)
                {
                    if (itemHeader.Contains("="))
                    {
                        var listHeaderValue = itemHeader.Split("=").ToList();
                        listHeaderItem.Add(new KeyValuePair<string, string>(listHeaderValue[0], listHeaderValue[1]));
                    }
                }
            }
            return listHeaderItem;
        }

        private void MontarLog(EntregaConfiguracao entregaConfig, long inicial, long final, int quantidade, string log, DateTime dataInicial)
        {
            EntregaConfiguracaoHistorico historico = new EntregaConfiguracaoHistorico(0, entregaConfig.Id, quantidade, null, null,
                                                                 dataInicial, DateTime.Now, inicial, final, log, true);
            entregaConfig.AdicionarHistorico(historico);
            _entregaConfiguracaoRepository.Update(entregaConfig);
        }
    }
}
