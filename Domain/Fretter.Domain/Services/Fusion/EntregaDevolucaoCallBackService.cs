using Fretter.Domain.Config;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Fretter.Domain.Helpers;
using Microsoft.Extensions.Options;
using Fretter.Domain.Dto.Carrefour.Mirakl;
using System.Reflection;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Dto.LogisticaReversa.Enum;

namespace Fretter.Domain.Services
{
    public class EntregaDevolucaoCallBackService<TContext> : ServiceBase<TContext, EntregaDevolucaoLog>, IEntregaDevolucaoCallBackService<TContext>
      where TContext : IUnitOfWork<TContext>
    {
        private readonly IMessageBusService<EntregaDevolucaoCallBackService<TContext>> _bus;
        private readonly MessageBusConfig _config;
        private readonly IEntregaDevolucaoLogRepository<TContext> _Repository;
        private readonly IEntregaConfiguracaoService<TContext> _serviceConfiguracao;

        public EntregaDevolucaoCallBackService(IEntregaDevolucaoLogRepository<TContext> Repository,
                                            IEntregaConfiguracaoService<TContext> serviceConfiguracao,
                                            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user,
                                            IMessageBusService<EntregaDevolucaoCallBackService<TContext>> bus,
                                            IOptions<MessageBusConfig> config) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
            _serviceConfiguracao = serviceConfiguracao;
            _config = config.Value;
            _bus = bus;
            _bus.InitReceiver(Enum.ReceiverType.Queue, _config.ConnectionString, _config.EntregaDevolucaoCallbackQueue, "", _config.PreFetchCount);
        }

        public async Task<int> ProcessaCallbackEntregaDevolucao()
        {
            IList<MessageData<DevolucaoCorreioRetorno>> entregas;

            try
            {
                entregas = await _bus.Receive<DevolucaoCorreioRetorno>(_config.ConsumeCount, 10);
            }
            catch (Exception ex)
            {
                var messageException = ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty);

                Thread.Sleep(60000);
                return 0;
            }

            if (entregas.Any())
            {
                var configs = _serviceConfiguracao.GetConfiguracoesPorIdTipo(Enum.EnumEntregaConfiguracaoTipo.CallBackUrl.GetHashCode());
                WebApiClient webApiClient = new WebApiClient(configs.Caminho, configs.ApiKey);
                EntregaDevolucaoLog devolucaoLog = new EntregaDevolucaoLog();

                foreach (var entrega in entregas)
                {
                    try
                    {
                        var jsonDados = JsonConvert.SerializeObject(entrega.Body);
                        List<OrderAdditionalFieldsDTO> orderAdditionals = ObterCamposAdicionais(entrega.Body);

                        var json = JsonConvert.SerializeObject(new { order_additional_fields = orderAdditionals });
                        var result = await webApiClient.Put<object>(configs.URLEtiquetaCallBack.Replace(":order_id", entrega.Body.Cd_Pedido), new { order_additional_fields = orderAdditionals });

                        if (result.StatusCode != System.Net.HttpStatusCode.Accepted && result.StatusCode != System.Net.HttpStatusCode.OK)
                            devolucaoLog = await PreparaLog(entrega.Body, json, result, false);
                        else devolucaoLog = await PreparaLog(entrega.Body, json, result, true);

                        entrega.Body = null;
                    }
                    catch (Exception ex)
                    {
                        devolucaoLog = await PreparaLog(entrega.Body, ($"Mensagem: {ex.Message} - Inner Exception: {ex.InnerException} - StackTrace: {ex.StackTrace}").Truncate(1024), null, false);
                    }

                    _Repository.Save(devolucaoLog);
                    _unitOfWork.Commit();
                }
                await _bus.Commit(entregas);
            }
            return entregas.Count();
        }

        private async Task<EntregaDevolucaoLog> PreparaLog(DevolucaoCorreioRetorno correioRetorno, string jsonEnvio, HttpResponseMessage response, bool sucesso)
        {
            string responseApi = response.Content != null ? await response.Content.ReadAsStringAsync() : "";
            string jsonRetorno = JsonConvert.SerializeObject(new { StatusCode = $"{response.StatusCode.GetHashCode()} - {response.StatusCode}", Response = responseApi });

            return new EntregaDevolucaoLog
            (
                Id: 0,
                EntregaDevolucaoTipoId: correioRetorno.Id_DevolucaoCorreioRetornoTipo,
                EntregaDevolucaoId: correioRetorno.Id_EntregaDevolucao,
                JsonEnvio: response == null ? null : jsonEnvio,
                JsonRetorno: jsonRetorno,
                Observacao: response == null ? jsonEnvio : null,
                Sucesso: sucesso
            );
        }

        private List<OrderAdditionalFieldsDTO> ObterCamposAdicionais(DevolucaoCorreioRetorno devolucaoCorreioRetorno)
        {
            List<OrderAdditionalFieldsDTO> list = new List<OrderAdditionalFieldsDTO>();

            if (devolucaoCorreioRetorno.Id_DevolucaoCorreioRetornoTipo == EnumDevolucaoCorreioRetornoTipo.Solicitacao.GetHashCode())
            {
                list.Add(new OrderAdditionalFieldsDTO()
                {
                    code = "codigo-postagem-reversa",
                    value = devolucaoCorreioRetorno.Cd_CodigoColeta
                });                

                list.Add(new OrderAdditionalFieldsDTO()
                {
                    code = "validadeetiquetaenvios-reversa",
                    value = Convert.ToDateTime(devolucaoCorreioRetorno.Dt_Validade).ToString("yyyy-MM-ddTHH:mm:ss.fff")
                });
            }
            else //Ocorrencia
            {
                list.Add(new OrderAdditionalFieldsDTO()
                {
                    code = "status-reversa",
                    value = devolucaoCorreioRetorno.Ds_Ocorrencia
                });

                list.Add(new OrderAdditionalFieldsDTO()
                {
                    code = "envios-tracking-reversa",
                    value = devolucaoCorreioRetorno.Ds_UrlReversa
                });

                list.Add(new OrderAdditionalFieldsDTO()
                {
                    code = "historico-tracking-envios-reversa",
                    value = devolucaoCorreioRetorno.Ds_OcorrenciaHistorico
                });

                list.Add(new OrderAdditionalFieldsDTO()
                {
                    code = "sro-reversa-envios",
                    value = devolucaoCorreioRetorno.Cd_CodigoRastreio
                });
            }

            return list;
        }
    }
}
