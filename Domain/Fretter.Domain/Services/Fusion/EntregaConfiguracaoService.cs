
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
using Fretter.Domain.Dto.Whirlpool;
using Fretter.Domain.Interfaces.Helper;
using System.Net.Http;

namespace Fretter.Domain.Services
{
    public class EntregaConfiguracaoService<TContext> : ServiceBase<TContext, EntregaConfiguracao>, IEntregaConfiguracaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaConfiguracaoRepository<TContext> _entregaConfiguracaoRepository;
        private readonly IEntregaStageReprocessamentoRepository<TContext> _entregaStageReprocessamentoRepository;
        private readonly IMessageBusService<EntregaConfiguracaoService<TContext>> _messageBusService;
        private readonly IMessageBusService<MessageBusShipNConfig> _messageBusServiceDanfe;
        private readonly IMessageBusService<EntregaIncidenteService<TContext>> _messageBusServiceIncidente;
        private readonly IMessageBusService<MessageBusConfig> _messageBusServiceEtiqueta;
        private readonly IMessageBusService<MiraklConfig> _messageBusServiceOndetah;
        private readonly MessageBusShipNConfig _messageBusShipNConfig;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly MiraklConfig _miraklConfig;
        private readonly ImportacaoEntregaConfig _importacaoEntregaConfig;
        private readonly ILogHelper _logHelper;

        public EntregaConfiguracaoService(IEntregaConfiguracaoRepository<TContext> entregaConfiguracaoRepository,
                                          IEntregaStageReprocessamentoRepository<TContext> entregaStageReprocessamentoRepository,
                                          IUnitOfWork<TContext> unitOfWork,
                                          IMessageBusService<EntregaConfiguracaoService<TContext>> messageBusService,
                                          IMessageBusService<MessageBusShipNConfig> messageBusServiceDanfe,
                                          IMessageBusService<EntregaIncidenteService<TContext>> messageBusServiceIncidente,
                                          IMessageBusService<MessageBusConfig> messageBusServiceEtiqueta,
                                          IMessageBusService<MiraklConfig> messageBusServiceOndetah,
                                          IOptions<MessageBusConfig> messageBusConfig,
                                          IOptions<MessageBusShipNConfig> messageBusShipNConfig,
                                          IOptions<MiraklConfig> miraklConfig,
                                          IOptions<ImportacaoEntregaConfig> importacaoEntregaConfig,
                                          ILogHelper logHelper,
                                          IUsuarioHelper user) : base(entregaConfiguracaoRepository, unitOfWork, user)
        {
            _messageBusService = messageBusService;
            _messageBusServiceDanfe = messageBusServiceDanfe;
            _messageBusServiceOndetah = messageBusServiceOndetah;
            _messageBusServiceEtiqueta = messageBusServiceEtiqueta;
            _messageBusServiceIncidente = messageBusServiceIncidente;
            _entregaConfiguracaoRepository = entregaConfiguracaoRepository;
            _entregaStageReprocessamentoRepository = entregaStageReprocessamentoRepository;

            _logHelper = logHelper;
            _miraklConfig = miraklConfig.Value;
            _messageBusShipNConfig = messageBusShipNConfig.Value;
            _messageBusConfig = messageBusConfig.Value;
            _importacaoEntregaConfig = importacaoEntregaConfig.Value;

            _messageBusService.InitSender(_messageBusConfig.ConnectionString, _messageBusShipNConfig.EntregaStageTopic);
            _messageBusServiceDanfe.InitSender(_messageBusConfig.ConnectionString, _messageBusShipNConfig.EntregaStageDanfeTopic);
            _messageBusServiceIncidente.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.EntregaIncidenteTopic);
            _messageBusServiceEtiqueta.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.EtiquetaStageShipNTopic);
            _messageBusServiceOndetah.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.EntregaMiraklOndetahTopic);
        }

        public EntregaConfiguracao GetConfiguracoesPorIdTipo(int Id_Tipo)
        {
            return _entregaConfiguracaoRepository.GetConfiguracoesPorIdTipo(Id_Tipo);
        }
        public List<EntregaConfiguracao> GetListaConfiguracoesPorIdTipo(int Id_Tipo)
        {
            return _entregaConfiguracaoRepository.GetListaConfiguracoesPorIdTipo(Id_Tipo);
        }
        public async Task ProcessaEntregaConfiguracaoAtivo()
        {
            List<EntregaConfiguracao> entregasConfig = _entregaConfiguracaoRepository
                                                                            .GetAll(x => x.EntregaConfiguracaoTipo == 1)
                                                                            .ToList();
            foreach (var entregaConfig in entregasConfig)
            {
                if (entregaConfig.IntervaloExecucao != null)
                    await ImportarEntregaPorIntervalo(entregaConfig);
            }
        }
        public async Task ReprocessaEntregaMirakl()
        {
            List<EntregaConfiguracao> entregasConfig = _entregaConfiguracaoRepository
                                                                .GetAll(x => x.EntregaConfiguracaoTipo == 1 && x.EmpresaId == _importacaoEntregaConfig.IntegracaoCRF.EmpresaId)
                                                                .ToList();
            List<EntregaStageReprocessamento> lstEntregasReprocessamento = _entregaStageReprocessamentoRepository
                                                                           .GetAll(x => x.Processado == false).ToList();

            foreach (var entregaConfig in entregasConfig)
            {
                if (lstEntregasReprocessamento.Any())
                    await ImportarEntregaPorEntrega(entregaConfig, lstEntregasReprocessamento);
            }
        }
        private async Task ImportarEntregaPorIntervalo(EntregaConfiguracao entregaConfig)
        {
            if (entregaConfig.DataProximaExecucao == null)
            {
                var dateNow = DateTime.Now;
                DateTime novaData = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, dateNow.Hour, 0, 0);
                entregaConfig.AtualizarDataProximaExecucao(novaData);
                _repository.Update(entregaConfig);
            }

            if (DateTime.Now >= ((DateTime)entregaConfig.DataProximaExecucao).AddMinutes((int)entregaConfig.IntervaloExecucao))
            {
                int qtdProcessada = 0;
                DateTime dataInicial = ((DateTime)entregaConfig.DataProximaExecucao);
                DateTime dataFinal = ((DateTime)entregaConfig.DataProximaExecucao).AddMinutes((int)entregaConfig.IntervaloExecucao).AddSeconds(-1);
                try
                {
                    if (entregaConfig.EmpresaId == _importacaoEntregaConfig.IntegracaoCRF.EmpresaId || entregaConfig.EmpresaId == _importacaoEntregaConfig.IntegracaoLRY.EmpresaId)
                        qtdProcessada = await ProcessaEntregaMirakl(entregaConfig, dataInicial, dataFinal);
                    else if (entregaConfig.EmpresaId == _importacaoEntregaConfig.IntegracaoWP.EmpresaId)
                        qtdProcessada = await ProcessaEntregaWhirlpool(entregaConfig, dataInicial, dataFinal);

                    entregaConfig.AtualizarProcessamento(DateTime.Now);
                    entregaConfig.AtualizarDataProximaExecucao(dataFinal.AddSeconds(1).ToDateTime());
                    entregaConfig.AtualizarRegistro(qtdProcessada);

                    EntregaConfiguracaoHistorico historico = new EntregaConfiguracaoHistorico(0, 0, qtdProcessada, null, null,
                                                                 dataInicial, dataFinal, null, null, null, true);
                    entregaConfig.AdicionarHistorico(historico);
                }
                catch (Exception ex)
                {
                    string erroTratado = $"Erro: Message->{ex.Message.Truncate(512)} Inner->{ex?.InnerException?.Message?.Truncate(1048) ?? ex?.Message ?? ""}";
                    EntregaConfiguracaoHistorico historicoErr = new EntregaConfiguracaoHistorico(0, 0, qtdProcessada, null, null,
                                                                 dataInicial, dataFinal, null, null, erroTratado, false);

                    entregaConfig.AdicionarHistorico(historicoErr);
                }

                _repository.Update(entregaConfig);
            }
            _unitOfWork.Commit();
        }
        private async Task ImportarEntregaPorEntrega(EntregaConfiguracao entregaConfig, List<EntregaStageReprocessamento> lstEntregasReprocessamento)
        {
            var ordersStage = new List<OrderDTO>();
            var ordersIncidente = new List<OrderDTO>();
            var stageFilaList = new List<EntregaStageFilaDTO>();
            var incidenteFilaList = new List<EntregaStageFilaDTO>();

            foreach (var entrega in lstEntregasReprocessamento)
            {
                try
                {
                    ordersStage.AddRange(await ObterEntregasMiraklPorOrderId(entregaConfig.Caminho, entregaConfig.ApiKey, entrega.Entrega));
                    ordersIncidente.AddRange(await ObterEntregasMiraklPorOrderId(entregaConfig.Caminho, entregaConfig.ApiKey, entrega.Entrega, true));

                    if (ordersStage.Any())
                    {
                        EntregaStageFilaDTO entregaFila = new EntregaStageFilaDTO(ordersStage.First(x => x.order_id == entrega.Entrega), entregaConfig.EmpresaId);
                        entregaFila.TipoServico = "Entrega";
                        stageFilaList.Add(entregaFila);
                    }

                    if (ordersIncidente.Any())
                    {
                        EntregaStageFilaDTO entregaFila = new EntregaStageFilaDTO(ordersIncidente.First(x => x.order_id == entrega.Entrega), entregaConfig.EmpresaId);
                        incidenteFilaList.Add(entregaFila);
                    }

                    entrega.AtualizarProcessado(true);
                    entrega.AtualizarDataProcessamento(DateTime.Now);
                    entrega.AtualizarJsonEnviadoParaFila(JsonConvert.SerializeObject(
                    ordersStage != null ? ordersStage.Where(x => x.order_id == entrega.Entrega) : ordersIncidente.Where(x => x.order_id == entrega.Entrega)));
                    entrega.AtualizarEntregaStageStatusProcessamentoId(Enum.EnumEntregaStageStatusProcessamento.Reprocessado);
                }
                catch (Exception ex)
                {
                    entrega.AtualizarJsonEnviadoParaFila(@$"Falha na consulta da entrega {entrega.Entrega}. Message: {ex.Message} - StackTrace : {ex.StackTrace}");
                    entrega.AtualizarEntregaStageStatusProcessamentoId(Enum.EnumEntregaStageStatusProcessamento.Erro);
                }
            }
            try
            {
                if (stageFilaList.Count > 0)
                    await _messageBusService.SendRange<EntregaStageFilaDTO>(stageFilaList);

                if (incidenteFilaList.Count > 0)
                    await _messageBusServiceIncidente.SendRange<EntregaStageFilaDTO>(incidenteFilaList);
            }
            catch (Exception ex)
            {
                var primeiraEntregaDoLote = string.Empty;
                var ultimaEntregaDoLote = string.Empty;
                for (var index = 0; index < lstEntregasReprocessamento.Count; index++)
                {
                    if (index == 0)
                        primeiraEntregaDoLote = lstEntregasReprocessamento[index].Entrega;
                    else if (index == lstEntregasReprocessamento.Count - 1)
                        ultimaEntregaDoLote = lstEntregasReprocessamento[index].Entrega;

                    lstEntregasReprocessamento[index].AtualizarJsonEnviadoParaFila(
                        @$"Falha ao enviar lote da entrega {primeiraEntregaDoLote} à {ultimaEntregaDoLote}. Message: {ex.Message} - StackTrace : {ex.StackTrace}");
                    lstEntregasReprocessamento[index].AtualizarEntregaStageStatusProcessamentoId(Enum.EnumEntregaStageStatusProcessamento.Erro);
                }
            }

            _entregaStageReprocessamentoRepository.UpdateRange(lstEntregasReprocessamento);

            _unitOfWork.Commit();
        }

        #region Mirakl
        private async Task<int> ProcessaEntregaMirakl(EntregaConfiguracao entregaConfig, DateTime dataInicial, DateTime dataFinal)
        {
            bool eCarrefour = _importacaoEntregaConfig.IntegracaoCRF.EmpresaId == entregaConfig.EmpresaId;

            var stageFilaList = new List<EntregaStageFilaDTO>();
            var incidenteFilaList = new List<EntregaStageFilaDTO>();
            var novaEtiquetaFilaList = new List<EntregaStageFilaDTO>();
            var listOrders = new List<OrderDTO>();

            listOrders = await ObterTodasEntregasMirakl(entregaConfig, dataInicial, dataFinal);

            var statusStage = _miraklConfig.StatusStage.Split(",");

            List<OrderDTO> ordersStage = new List<OrderDTO>();
            List<OrderDTO> ordersIncidente = new List<OrderDTO>();

            if (eCarrefour)
            {
                ordersStage = listOrders.Where(o => o.is_envios
                                                     && statusStage.Contains(o.order_state_clean))
                                            .ToList();

                var statusIncidentes = _miraklConfig.StatusIncidentes.Split(",");
                ordersIncidente = listOrders.Where(o => o.is_envios
                                                         && statusIncidentes.Contains(o.order_state_clean)
                                                         && o.has_incident)
                                                .ToList();
            }
            else
            {
                ordersStage = listOrders.Where(o => o.order_additional_fields.Any(a => a.code == "shop-id" && a.value == $"{_importacaoEntregaConfig.IntegracaoLRY.IdLojista}")).ToList();
                ordersStage = ordersStage.Where(o =>
                    o.order_lines.Any(ol =>
                        ol.order_line_additional_fields.Any(ola =>
                            ola.code == "carrier-service" && ola.value == $"{_importacaoEntregaConfig.IntegracaoLRY.TransportadorNome}"))).ToList();

            }


            foreach (OrderDTO order in ordersStage)
            {

                EntregaStageFilaDTO entregaFila = new EntregaStageFilaDTO(order, entregaConfig.EmpresaId, eCarrefour);
                entregaFila.TipoServico = "Entrega";
                stageFilaList.Add(entregaFila);
            }

            foreach (OrderDTO order in ordersIncidente)
            {
                var orderline = order.order_lines;
                EntregaStageFilaDTO entregaFila = new EntregaStageFilaDTO(order, entregaConfig.EmpresaId, eCarrefour);
                if (orderline.Any(x => x.order_line_state_reason_code != "124"))
                    incidenteFilaList.Add(entregaFila);
                else
                    novaEtiquetaFilaList.Add(entregaFila);
            }

            if (stageFilaList.Count > 0)
            {
                var listComDanfe = stageFilaList.Where(t => !string.IsNullOrEmpty(t.Danfe)).ToList();
                await _messageBusServiceDanfe.SendRange(listComDanfe);

                var listSemDanfe = stageFilaList.Where(t => string.IsNullOrEmpty(t.Danfe)).ToList();
                await _messageBusService.SendRange(listSemDanfe);
            }

            if (incidenteFilaList.Count > 0)
                await _messageBusServiceIncidente.SendRange(incidenteFilaList);

            if (novaEtiquetaFilaList.Count > 0)
                await _messageBusServiceEtiqueta.SendRange(novaEtiquetaFilaList);

            if (eCarrefour)
                await _messageBusServiceOndetah.SendRange(listOrders.Select(o => new EntregaMiraklDTO() { order_id = o.order_id, order_state = o.order_state_clean }).ToList());

            return stageFilaList.Count;
        }
        private async Task<List<OrderDTO>> ObterEntregasMirakl(string caminho, string apikey, DateTime dataInicial, DateTime dataFinal, bool incident = false)
        {
            string formatDate = "yyyy-MM-ddTHH:mm:ss";
            int offset = 0;
            var filter = $"?start_update_date={dataInicial.ToString(formatDate)}" +
                            $"&end_update_date={dataFinal.ToString(formatDate)}" +
                            $"&max=2500" +
                            $"&offset={offset.ToString()}";

            if (!incident)
                filter += $"&order_state_codes={_miraklConfig.StatusStage}";
            else
                filter += $"&order_state_codes={_miraklConfig.StatusIncidentes}";

            if (incident)
                filter += "&has_incident=true";

            WebApiClient webApiClient = new WebApiClient(caminho, apikey);
            ResultMiraklDTO result = await webApiClient.Get<ResultMiraklDTO>(filter);
            List<OrderDTO> orders = result.orders;

            while (orders.Count < result.total_count)
            {
                filter = filter.Replace($"offset={offset}", $"offset={offset + 100}");
                ResultMiraklDTO resultOfsset = await webApiClient.Get<ResultMiraklDTO>(filter);
                if (resultOfsset.orders.Count == 0)
                    break;
                orders.AddRange(resultOfsset.orders);
                offset += 100;
            }
            var list = orders.Where(x => x.order_additional_fields.Any(y => y.code == "shipping-priority-name" && y.value.ToUpper() == "ENVIOS")).ToList();
            return list;
        }
        private async Task<List<OrderDTO>> ObterTodasEntregasMirakl(EntregaConfiguracao entregaConfig, DateTime dataInicial, DateTime dataFinal)
        {
            string caminho = entregaConfig.Caminho;
            string apikey = entregaConfig.ApiKey;

            string formatDate = "yyyy-MM-ddTHH:mm:ss";
            int offset = 0;
            var filter = $"?start_update_date={dataInicial.ToString(formatDate)}" +
                            $"&end_update_date={dataFinal.ToString(formatDate)}" +
                            $"&max=2500" +
                            $"&offset={offset.ToString()}";

            ResultMiraklDTO result = new ResultMiraklDTO();
            WebApiClient webApiClient = new WebApiClient(caminho, apikey);

            if (entregaConfig.LayoutHeader != null && entregaConfig.LayoutHeader?.Length > 0)
                result = await webApiClient.GetWithHeader<ResultMiraklDTO>(filter, entregaConfig.LayoutHeader);
            else
                result = await webApiClient.Get<ResultMiraklDTO>(filter);

            List<OrderDTO> orders = new List<OrderDTO>();
            orders.AddRange(result.orders);

            while (orders.Count < result.total_count)
            {
                filter = filter.Replace($"offset={offset}", $"offset={offset + 100}");
                ResultMiraklDTO resultOfsset = await webApiClient.Get<ResultMiraklDTO>(filter);
                if (resultOfsset.orders.Count == 0)
                    break;
                orders.AddRange(resultOfsset.orders);
                offset += 100;
            }

            var list = orders.ToList();
            return list;
        }
        private async Task<List<OrderDTO>> ObterEntregasMiraklPorOrderId(string caminho, string apikey, string orderId, bool incident = false)
        {
            int offset = 0;
            var filter = $"?order_ids={orderId}";
            if (incident)
                filter += "&has_incident=true";

            WebApiClient webApiClient = new WebApiClient(caminho, apikey);
            ResultMiraklDTO result = await webApiClient.Get<ResultMiraklDTO>(filter);
            List<OrderDTO> orders = result.orders;

            while (orders.Count < result.total_count)
            {
                filter = filter.Replace($"offset={offset}", $"offset={offset + 100}");
                ResultMiraklDTO resultOfsset = await webApiClient.Get<ResultMiraklDTO>(filter);
                if (resultOfsset.orders.Count == 0)
                    break;
                orders.AddRange(resultOfsset.orders);
                offset += 100;
            }
            var list = orders.Where(x => x.order_additional_fields.Any(y => y.code == "shipping-priority-name" && y.value.ToUpper() == "ENVIOS")).ToList();
            return list;
        }
        #endregion

        #region Whirlpool
        private async Task<int> ProcessaEntregaWhirlpool(EntregaConfiguracao entregaConfig, DateTime dataInicial, DateTime dataFinal)
        {
            int qtdProcessada = 0;
            var transportadoras = _importacaoEntregaConfig.IntegracaoWP.TransportadorasCNPJ;
            var entregaFilaList = new List<Dto.Fusion.entradaEntrega>();
            var tokenAuthWP = await ObterTokenAuthWhirlpool();

            foreach (var cnpj in transportadoras)
            {
                try
                {
                var entregasWP = await ObterEntregasWhirlpool(entregaConfig.Caminho, tokenAuthWP, dataInicial, dataFinal, cnpj);
                if (entregasWP.notfis != null)
                {
                    foreach (var ent in entregasWP.notfis)
                    {
                        var entregaFila = new Dto.Fusion.entradaEntrega(ent);
                        if (entregaFila != null)
                            entregaFilaList.Add(entregaFila);
                    }
                    }
                } catch(Exception ex)
                {
					_logHelper.LogError("EntregaConfiguracaoService.ProcessaEntregaWhirlpool", $"Houve um erro ao Buscar Entrega na API.URL: {entregaConfig.Caminho} DataInicial: {dataInicial.ToString("yyyy-MM-dd HH:mm:ss")} DataFinal: {dataFinal.ToString("yyyy-MM-dd HH:mm:ss")} CNPJ Transportador: {cnpj}", ex);
                }
            }

            await entregaFilaList.AsParallel().WithDegreeOfParallelism(5).ForEachAsync(async entrega =>
            {
                try
                {
                    WebApiClient webApiClient = new WebApiClient(_importacaoEntregaConfig.URLImportacaoEntrega);
                    webApiClient.AddHeader("Token", _importacaoEntregaConfig.IntegracaoWP.EmpresaEntregaToken.ToString());
                    var response = await webApiClient.PostWithHeader<object>(_importacaoEntregaConfig.URLImportacaoEntregaRota, entrega);
                    var msgLog = $"Entrega enviada com sucesso. URL: {_importacaoEntregaConfig.URLImportacaoEntrega}/{_importacaoEntregaConfig.URLImportacaoEntregaRota} Token: {_importacaoEntregaConfig.IntegracaoWP.EmpresaEntregaToken}";
                    _logHelper.LogInfo("EntregaConfiguracaoService.ProcessaEntregaWhirlpool", msgLog, entrega);

                    qtdProcessada += entrega.tipos[0].filiais[0].transportadores[0].entregas.Count();
                }
                catch (Exception ex)
                {
                    _logHelper.LogError("EntregaConfiguracaoService.ProcessaEntregaWhirlpool", $"Houve um erro ao Enviar Entrega para API.URL: {_importacaoEntregaConfig.URLImportacaoEntrega}/{_importacaoEntregaConfig.URLImportacaoEntregaRota} Token: {_importacaoEntregaConfig.IntegracaoWP.EmpresaEntregaToken}", ex);
                }
            });

            return qtdProcessada;
        }
        private async Task<string> ObterTokenAuthWhirlpool()
        {
            WebApiClient webApiClient = new WebApiClient(_importacaoEntregaConfig.IntegracaoWP.Credenciais.AuthURL);
            webApiClient.AddHeader("Authorization", _importacaoEntregaConfig.IntegracaoWP.Credenciais.AuthToken);
            var body = new List<KeyValuePair<string, string>>();
            body.Add(new KeyValuePair<string, string>("grant_type", _importacaoEntregaConfig.IntegracaoWP.Credenciais.GrantType));
            body.Add(new KeyValuePair<string, string>("username", _importacaoEntregaConfig.IntegracaoWP.Credenciais.UserName));
            body.Add(new KeyValuePair<string, string>("password", _importacaoEntregaConfig.IntegracaoWP.Credenciais.Password));

            var data = await webApiClient.AuthenticateGrantTypeOAuth2(_importacaoEntregaConfig.IntegracaoWP.Credenciais.AuthURLRoute, body);
            return data.access_token;
        }
        private async Task<NotfisDTO> ObterEntregasWhirlpool(string caminho, string apikey, DateTime dataInicial, DateTime dataFinal, string cnpjTransportadora)
        {
            string formatDate = "yyyy-MM-dd";
            string formatTime = "HH:mm:ss";
            string filterURL = $"?cnpjCarrier={cnpjTransportadora}&dataStart={dataInicial.ToString(formatDate)}" +
                                $"&dataEnd={dataFinal.ToString(formatDate)}&timeStart={dataInicial.ToString(formatTime)}&timeEnd={dataFinal.ToString(formatTime)}";

            WebApiClient webApiClient = new WebApiClient(caminho, $"Bearer {apikey}");
            NotfisDTO result = await webApiClient.Get<NotfisDTO>(filterURL);

            return result;
        }
        #endregion
    }
}
