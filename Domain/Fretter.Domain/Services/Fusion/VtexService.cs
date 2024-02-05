
using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour;
using Fretter.Domain.Dto.Carrefour.Mirakl;
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
using Newtonsoft.Json;
using Fretter.Domain.Dto.Vtex;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers.Extensions;
using System.Text;

namespace Fretter.Domain.Services
{
    public class VtexService<TContext> : ServiceBase<TContext, EntregaConfiguracao>, IVtexService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaConfiguracaoRepository<TContext> _entregaConfiguracaoRepository;
        private readonly IEmpresaRepository<TContext> _empresaRepository;
        private readonly FusionApiSkuConfig _fusionApiSkuConfig;

        public VtexService(IEntregaConfiguracaoRepository<TContext> entregaConfiguracaoRepository,
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
                                                                .GetAll(x => x.EntregaConfiguracaoTipo == EnumEntregaConfiguracaoTipo.Vtex.GetHashCode())
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
                    await ImportarSkus(entregaConfig);
                    _unitOfWork.Commit();
                }
        }

        private async Task ImportarSkus(EntregaConfiguracao entregaConfig)
        {
            string result = string.Empty;
            DateTime dataInicial = DateTime.Now;
            string cnpjSeller = string.Empty;
            List<Sku> skusEnvio = new List<Sku>();
            MontarLog(entregaConfig, 0, 0, 0, "Inicio da importação de skus", DateTime.Now);

            List<int> listaSkus = await ObterListaSkus(entregaConfig);

            foreach (var sku in listaSkus)
            {
                VtexSkuDto cadastro = await ObterCadastroDoSku(entregaConfig, sku);
                cadastro.SkuSellersComplete = new List<SkuSellerComplete>();

                //cadastro.SkuSellers.ForEach(seller =>
                //    cadastro.SkuSellersComplete.Add(ObterDadosSeller(entregaConfig, seller.SellerId).Result));

                Sku skuEnvio = new Sku(0, cadastro.Id.ToString(), null, cadastro.ProductName, cadastro.ComplementName, cadastro.MeasurementUnit, null,
                                       0, 0, (decimal)cadastro.RealDimension.RealWeight, (decimal)cadastro.RealDimension.RealCubicWeight, null, cadastro.BrandName, cadastro.BrandName,
                                       null, null, null, true, false);

                skuEnvio.Medidas = new SkuMedidas(0, (decimal)cadastro.RealDimension.RealWidth, (decimal)cadastro.RealDimension.RealHeight, (decimal)cadastro.RealDimension.RealLength);

                skusEnvio.Add(skuEnvio);

                if (skusEnvio.Count < 100)
                    skusEnvio.Add(skuEnvio);
                else
                {
                    result = EnviarSkuEndpointFusion(skusEnvio).Result;
                    MontarLog(entregaConfig, long.Parse(skusEnvio?.FirstOrDefault()?.Codigo), long.Parse(skusEnvio?.LastOrDefault()?.Codigo), skusEnvio.Count, result.Truncate(2050), dataInicial);
                    skusEnvio.Clear();
                }
            }

            result = EnviarSkuEndpointFusion(skusEnvio).Result;
            MontarLog(entregaConfig, long.Parse(skusEnvio?.FirstOrDefault()?.Codigo), long.Parse(skusEnvio?.LastOrDefault()?.Codigo), skusEnvio.Count, result.Truncate(2050), dataInicial);
        }

        private async Task<List<int>> ObterListaSkus(EntregaConfiguracao entregaConfig)
        {
            DateTime dataInicial = DateTime.Now;
            int page = 1;
            int[] result = null;
            bool processamento = true;
            StringBuilder pagesError = new StringBuilder();
            List<int> listaIdsSkus = new List<int>();
            WebApiClient webApiClient = new WebApiClient(entregaConfig.Caminho);

            var configItem = entregaConfig.Itens.FirstOrDefault(x => x.EntregaConfiguracaoItemTipoId == EnumEntregaConfiguracaoItemTipo.ListaSkuVtex.GetHashCode());
            configItem.AtualizarProcessamento(DateTime.Now);
            var listHeaderItem = PreparaHeader(string.IsNullOrEmpty(configItem.LayoutHeader) ? entregaConfig.LayoutHeader : configItem.LayoutHeader);

            while (processamento)
            {
                var data = new
                {
                    registro = entregaConfig.Registro,
                    page = page
                };
                var url = ParseObjectHelper.ConvertObjectToLayoutString(configItem.Url, data);

                try
                {
                    result = await webApiClient.GetWithHeader<int[]>(url, listHeaderItem);
                    listaIdsSkus.AddRange(result.Select(x => x).ToList());
                }
                catch (Exception ex)
                {
                    pagesError.Append(page + ", ");
                    configItem.AtualizarProcessamentoSucesso(false);
                    MontarLog(entregaConfig, listaIdsSkus.FirstOrDefault(), listaIdsSkus.LastOrDefault(), listaIdsSkus.Count,
                        $"Falha ao obter a lista de Skus da página { page } que esta sendo consultado em lotes de {entregaConfig.Registro} na vtex. Message: {ex.Message} ", dataInicial);
                }
                page += 1;

                if (result.Count() <= 0)
                    processamento = false;
            }

            MontarLog(entregaConfig, listaIdsSkus.FirstOrDefault(), listaIdsSkus.LastOrDefault(), listaIdsSkus.Count,
                $"Finalização da recuperação da lista de skus na vtex. {listaIdsSkus?.Count} skus encontados. {(pagesError.Length > 0 ? $"Tiveram erros nas páginas {pagesError}" : string.Empty)}.", dataInicial);
            configItem.AtualizarProcessamentoSucesso(true);
            return listaIdsSkus;
        }

        private async Task<VtexSkuDto> ObterCadastroDoSku(EntregaConfiguracao entregaConfig, int id)
        {
            DateTime dataInicial = DateTime.Now;
            VtexSkuDto result = new VtexSkuDto();
            WebApiClient webApiClient = new WebApiClient(entregaConfig.Caminho);
            var configItem = entregaConfig.Itens.FirstOrDefault(x => x.EntregaConfiguracaoItemTipoId == EnumEntregaConfiguracaoItemTipo.CadastroSkuVtex.GetHashCode());
            configItem.AtualizarProcessamento(DateTime.Now);
            var data = new { id = id };
            var url = ParseObjectHelper.ConvertObjectToLayoutString(configItem.Url, data);
            var listHeaderItem = PreparaHeader(string.IsNullOrEmpty(configItem.LayoutHeader) ? entregaConfig.LayoutHeader : configItem.LayoutHeader);

            try
            {
                result = await webApiClient.GetWithHeader<VtexSkuDto>(url, listHeaderItem);
            }
            catch (Exception ex)
            {
                configItem.AtualizarProcessamentoSucesso(false);
                MontarLog(entregaConfig, data.id, data.id, 1, $"Falha ao consultar cadastro do Sku { id } na vtex. Message: {ex.Message}", dataInicial);
            }

            MontarLog(entregaConfig, data.id, data.id, 1, $"Cadastro do Sku { id } consultado com sucesso na vtex.", dataInicial);
            configItem.AtualizarProcessamentoSucesso(true);
            return result;
        }

        private async Task<SkuSellerComplete> ObterDadosSeller(EntregaConfiguracao entregaConfig, string sellerId)
        {
            DateTime dataInicial = DateTime.Now;
            SkuSellerComplete result = new SkuSellerComplete();
            var configItem = entregaConfig.Itens.FirstOrDefault(x => x.EntregaConfiguracaoItemTipoId == EnumEntregaConfiguracaoItemTipo.DadosSellerVtex.GetHashCode());
            configItem.AtualizarProcessamento(DateTime.Now);
            WebApiClient webApiClient = new WebApiClient(entregaConfig.Caminho);
            var data = new { sellerId = sellerId };
            var url = ParseObjectHelper.ConvertObjectToLayoutString(configItem.Url, data);
            var listHeaderItem = PreparaHeader(string.IsNullOrEmpty(configItem.LayoutHeader) ? entregaConfig.LayoutHeader : configItem.LayoutHeader);

            try
            {
                result = await webApiClient.GetWithHeader<SkuSellerComplete>(url, listHeaderItem);
            }
            catch (Exception ex)
            {
                configItem.AtualizarProcessamentoSucesso(false);
                MontarLog(entregaConfig, long.Parse(data.sellerId), long.Parse(data.sellerId), 1, $"Falha ao consultar cadastro do seller { sellerId } na vtex. Message: {ex.Message}", dataInicial);
            }

            MontarLog(entregaConfig, long.Parse(data.sellerId), long.Parse(data.sellerId), 1, $"Cadastro do seller { sellerId } consultado com sucesso na vtex.", dataInicial);
            configItem.AtualizarProcessamentoSucesso(true);
            return result;
        }

        private async Task<string> EnviarSkuEndpointFusion(List<Sku> skusEnvio)
        {
            Guid? token = _empresaRepository.GetAll(x => x.NomeFantasia.ToUpper().Contains("DECATHLON"))?.FirstOrDefault()?.TokenId;
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
