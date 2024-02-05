using Fretter.Domain.Config;
using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.CTe;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Helper;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Interfaces.Service.Util;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Fretter.Domain.Services
{
    public class ConciliacaoTransportadorService<TContext> : ServiceBase<TContext, EntregaConciliacao>, IConciliacaoTransportadorService<TContext>
 where TContext : IUnitOfWork<TContext>
    {
        private readonly IConciliacaoTransportadorRepository<TContext> _repository;
        private readonly IMessageBusService<ConciliacaoService<TContext>> _bus;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly ICorreioService _correiosService;
        private readonly ILogHelper _logHelper;

        public ConciliacaoTransportadorService(IConciliacaoTransportadorRepository<TContext> repository,
                                  IMessageBusService<ConciliacaoService<TContext>> bus,
                                  ICorreioService correiosService,
                                  IOptions<MessageBusConfig> messageBusConfig,
                                  IUnitOfWork<TContext> unitOfWork,
                                  ILogHelper logHelper,
                                  IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _correiosService = correiosService;
            _logHelper = logHelper;

            _messageBusConfig = messageBusConfig.Value;
            _bus = bus;
            _bus.InitReceiver(Enum.ReceiverType.Queue, _messageBusConfig.ConnectionString, _messageBusConfig.EntregaConciliacaoQueue, "", _messageBusConfig.PreFetchCount);
        }

        public async Task<int> ProcessaConciliacaoTransportador()
        {
            IList<MessageData<EntregaConciliacaoDTO>> entregas;

            try
            {
                entregas = await _bus.Receive<EntregaConciliacaoDTO>(_messageBusConfig.ConsumeCount, _messageBusConfig.SecondsToTimeout);
            }
            catch (Exception ex)
            {
                var messageException = ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                Thread.Sleep(60000);
                return 0;
            }

            if (entregas.Any())
            {
                List<EntregaConciliacaoInsereDTO> listEntrega = new List<EntregaConciliacaoInsereDTO>();

                await entregas.ForEachAsync(async ent =>
                {
                    var entrega = ent.Body;
                    EntregaConciliacaoInsereDTO entregaInsere = new EntregaConciliacaoInsereDTO();
                    List<ItemComposicaoDto> listComposicao = new List<ItemComposicaoDto>();

                    try
                    {
                        entregaInsere.Id_Entrega = entrega.EntregaId;
                        entregaInsere.Id_EntregaConciliacao = entrega.EntregaConciliacaoId;
                        entregaInsere.Dt_Processamento = DateTime.Now;

                        var retorno = await _correiosService.ConsultaXmlPLP(entrega.CodigoPLP, entrega.SistemaUsuario, entrega.SistemaSenha, true);

                        try
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(retorno.@return);

                            if (!string.IsNullOrEmpty(retorno.@return) && retorno.@return.Contains("id_plp"))
                            {
                                entregaInsere.Vl_Cubagem = (decimal.TryParse(xml.GetElementsByTagName("cubagem")[0]?.InnerText, out var vlCubagem) ? vlCubagem / 100 : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Cubagem", (decimal)entregaInsere.Vl_Cubagem, Enum.EnumCteComposicaoTipo.Peso));
                                entregaInsere.Vl_Peso = (decimal.TryParse(xml.GetElementsByTagName("peso")[0]?.InnerText, out var vlPeso) ? vlPeso / 1000 : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Peso", (decimal)entregaInsere.Vl_Peso, Enum.EnumCteComposicaoTipo.Peso));
                                entregaInsere.Vl_Altura = (decimal.TryParse(xml.GetElementsByTagName("dimensao_altura")[0]?.InnerText.Replace(",", "."), out var vlAltura) ? vlAltura / 100 : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Altura", (decimal)entregaInsere.Vl_Altura, Enum.EnumCteComposicaoTipo.Peso));
                                entregaInsere.Vl_Largura = (decimal.TryParse(xml.GetElementsByTagName("dimensao_largura")[0]?.InnerText.Replace(",", "."), out var vlLargura) ? vlLargura / 100 : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Largura", (decimal)entregaInsere.Vl_Largura, Enum.EnumCteComposicaoTipo.Peso));
                                entregaInsere.Vl_Comprimento = (decimal.TryParse(xml.GetElementsByTagName("dimensao_comprimento")[0]?.InnerText.Replace(",", "."), out var vlComprimento) ? vlComprimento / 100 : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Comprimento", (decimal)entregaInsere.Vl_Comprimento, Enum.EnumCteComposicaoTipo.Peso));
                                entregaInsere.Vl_Diametro = (decimal.TryParse(xml.GetElementsByTagName("dimensao_diametro")[0]?.InnerText.Replace(",", "."), out var vlDiametro) ? vlDiametro / 100 : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Diametro", (decimal)entregaInsere.Vl_Diametro, Enum.EnumCteComposicaoTipo.Peso));
                                entregaInsere.Vl_Cobrado = (decimal.TryParse(xml.GetElementsByTagName("valor_cobrado")[0]?.InnerText.Replace(".", ","), out var vlCobrado) ? vlCobrado : 0);
                                listComposicao.Add(new ItemComposicaoDto("Vl_Cobrado", (decimal)entregaInsere.Vl_Cobrado, Enum.EnumCteComposicaoTipo.Valor));
                            }

                            entregaInsere.Ds_RetornoProcessamento = "Conciliacao processada com Sucessso !";
                            entregaInsere.Ds_Json = JsonConvert.SerializeObject(listComposicao);
                            entregaInsere.Flg_Sucesso = true;
                        }
                        catch (Exception ex)
                        {
                            entregaInsere.Ds_RetornoProcessamento = ($"Erro ao realizar o parse dos dados: {ex.Message} - {ex.InnerException}").Truncate(2040);
                            entregaInsere.Flg_Sucesso = false;
                            _logHelper.LogError("ProcessaConciliacaoTransportador", "Erro ao realizar o parse dos dados", retorno, DateTime.Now, 0, ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        entregaInsere.Ds_RetornoProcessamento = ($"Erro ao realizar consulta: {ex.Message} - {ex.InnerException}").Truncate(2040);
                        entregaInsere.Flg_Sucesso = false;
                        _logHelper.LogError("ProcessaConciliacaoTransportador", "Erro ao realizar consulta", entrega, DateTime.Now, 0, ex);
                    }
                    finally
                    {
                        listEntrega.Add(entregaInsere);
                    }
                });

                _repository.ProcessaConciliacaoTransportador(CollectionHelper.ConvertTo<EntregaConciliacaoInsereDTO>(listEntrega));
            }

            _unitOfWork.Commit();
            if (entregas.Count() > 0)
                await _bus.Commit(entregas);

            return entregas.Count();
        }
    }
}
