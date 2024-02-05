using Fretter.Domain.Config;
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Dto.Webhook.Tracking.Entrada;
using Fretter.Domain.Dto.Webhook.Tracking.Message;
using Fretter.Domain.Dto.Webhook.Tracking.Response;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Enum.Webhook;
using Fretter.Domain.Helpers.Webhook;
using Fretter.Domain.Interfaces.Helper;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Webhook;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Interfaces.Service.Webhook;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Domain.Services.Webhook
{
    public class TrackingService<TContext> : ITrackingService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ILogHelper _logger;
        private readonly ICacheService<TContext> _cache;
        private readonly IMessageBusService<EntradaTracking> _bus;
        private readonly IMessageBusService<EntradaTrackingEspecifico> _busEspecifico;
        private readonly IMessageBusService<EntradaSync> _busWebhookSync;
        private readonly MessageBusConfig _messageBusConfig;

        public TrackingService(ILogHelper logger,
                               ICacheService<TContext> cache,
                               IMessageBusService<EntradaTracking> bus,
                               IMessageBusService<EntradaTrackingEspecifico> busEspecifico,
                               IMessageBusService<EntradaSync> busWebhookSync,
                               IOptions<MessageBusConfig> messageBusConfig)
        {
            _messageBusConfig = messageBusConfig.Value;
            _bus = bus;
            _busEspecifico = busEspecifico;
            _busWebhookSync = busWebhookSync;
            _cache = cache;
            _logger = logger;
            _bus.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.TrackingPadraoTopic);
            _busEspecifico.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.TrackingEspecificoTopic);
            _busWebhookSync.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.WebhookSyncTopic);
        }

        public Task<ResponseEspecifico> TrackingEspecificoAsync(EntradaTrackingEspecifico entradaEspecifico, string requestString, Guid token)
        {
            var hash = Guid.NewGuid();
            var ret = new ResponseEspecifico();
            var dt = DateTime.Now;
            string processName = "TrackingEspecifico_Fila";

            Empresa empresa = _cache.ObterLista<Empresa>(x => x.TokenId == token).FirstOrDefault();
            Transportador transportador = _cache.ObterLista<Transportador>(x => x.TokenId == token).FirstOrDefault();

            try
            {
                if (string.IsNullOrEmpty(requestString))
                    requestString = JsonConvert.SerializeObject(entradaEspecifico);

                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hash });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hash, entradaEspecifico, requestString });


                if (token.Equals(Guid.Empty) || ((empresa == null) && (transportador == null)))
                {
                    _logger.LogError(processName, "Você não tem permissão para executar esta ação", new { hash, entradaEspecifico, requestString, token });
                    return Task.FromResult(new ResponseEspecifico
                    {
                        status = "ERROR",
                        messages = new List<MessageEspecifico>()
                        {
                            new MessageEspecifico {
                                type = "ERROR",
                                text = "Você não tem permissão para executar esta ação.",
                                key = "error.403"
                            }
                        },
                        time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                        timezone = "America/Sao_Paulo",
                        locale = "pt_BR",
                        hash = hash.ToString()
                    });
                }

                _logger.LogInfo(processName, "Token do responsável", new { hash, entradaEspecifico, requestString, token });

                if (entradaEspecifico == null)
                {
                    _logger.LogError(processName, "Não foi possível obter as informações do tracking", new { hash });
                    return Task.FromResult(new ResponseEspecifico
                    {
                        status = "ERROR",
                        messages = new List<MessageEspecifico>()
                        {
                            new MessageEspecifico {
                                type = "ERROR",
                                text = "Não foi possível obter as informações do tracking.",
                                key = "error.400"
                            }
                        },
                        time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                        timezone = "America/Sao_Paulo",
                        locale = "pt_BR",
                        hash = hash.ToString()
                    });
                }

                if (empresa != null && string.IsNullOrEmpty(entradaEspecifico.shipper_federal_tax_id))
                {
                    var lsCanal = _cache.ObterLista<Canal>(c => c.EmpresaId == empresa.Id && c.Ativo);
                    entradaEspecifico.shipper_federal_tax_id = lsCanal.Count == 1 ? lsCanal.First().Cnpj : entradaEspecifico.shipper_federal_tax_id;
                }

                if (transportador != null && string.IsNullOrEmpty(entradaEspecifico.logistic_provider_federal_tax_id))
                {
                    var tCnpj = _cache.ObterLista<TransportadorCnpj>(t => t.TransportadorId == transportador.Id).FirstOrDefault();
                    entradaEspecifico.logistic_provider_federal_tax_id = tCnpj.CNPJ;
                }

                if ((string.IsNullOrEmpty(entradaEspecifico.invoice_key) || entradaEspecifico.invoice_key.Length != 44) &&
                   (string.IsNullOrEmpty(entradaEspecifico.invoice_number) || string.IsNullOrEmpty(entradaEspecifico.invoice_series) || string.IsNullOrEmpty(entradaEspecifico.shipper_federal_tax_id)))
                {

                    _logger.LogError(processName, "Identificador da entrega não localizado (danfe ou cnpj+nf+serie)", new { hash, entradaEspecifico, requestString, token });

                    return Task.FromResult(new ResponseEspecifico
                    {
                        status = "ERROR",
                        messages = new List<MessageEspecifico>()
                        {
                            new MessageEspecifico {
                                type = "ERROR",
                                text = "Identificador da entrega não localizado (danfe ou cnpj+nf+serie).",
                                key = "error.400"
                            }
                        },
                        time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                        timezone = "America/Sao_Paulo",
                        locale = "pt_BR",
                        hash = hash.ToString()
                    });
                }

                int linha = 0;
                foreach (var e in entradaEspecifico.events)
                {
                    linha++;
                    _busEspecifico.Send(new TrackingEspecificoMessage
                    {
                        Ds_Hash = hash,
                        Cd_Linha = linha,
                        Dt_Ocorrencia = e.event_date.DateTime,
                        Cd_Danfe = entradaEspecifico.invoice_key,
                        Cd_SerieNotaFiscal = entradaEspecifico.invoice_series,
                        Cd_NotaFiscal = entradaEspecifico.invoice_number,
                        Cd_Sro = entradaEspecifico.tracking_code,
                        Cd_CnpjCanal = entradaEspecifico.shipper_federal_tax_id,
                        Ds_Ocorrencia = e.original_message,
                        Cd_Ocorrencia = e.original_code,
                        Cd_BaseTransportador = e.original_base,
                        Cd_CnpjTransportador = entradaEspecifico.logistic_provider_federal_tax_id,
                        Id_Empresa = empresa?.Id,
                        Id_Transportador = transportador?.Id
                    });
                }
                _logger.LogInfo(processName, "Enviado com sucesso para o processamento", new { hash, entradaEspecifico, requestString, token, empresa, transportador });
                return Task.FromResult(new ResponseEspecifico
                {
                    status = "OK",
                    messages = new List<MessageEspecifico>()
                    {
                        new MessageEspecifico {
                            type = "INFO",
                            text = "Operação realizada com sucesso.",
                            key = "info.message"
                        }
                    },
                    time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                    client_id = null,
                    logistics_provider = 0,
                    logistics_provider_name = null,
                    timezone = "America/Sao_Paulo",
                    locale = "pt_BR",
                    hash = hash.ToString()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro ao preparar o envelope para o processamento", new { hash });
                return Task.FromResult(new ResponseEspecifico
                {
                    status = "ERROR",
                    messages = new List<MessageEspecifico>()
                        {
                            new MessageEspecifico {
                                type = "ERROR",
                                text = ex.Message,
                                key = "error.400"
                            }
                        },
                    time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                    timezone = "America/Sao_Paulo",
                    locale = "pt_BR",
                    hash = hash.ToString()
                });
            }
        }
        public Task<ResponseEspecifico> TrackingFreteRapidoAsync(EntradaTrackingFreteRapido tracking, string requestString, Guid token)
        {
            var dt = DateTime.Now;
            var hash = Guid.NewGuid();
            Transportador transportador = null;
            string processName = "TrackingFreteRapido_Fila";
            var ret = new ResponseEspecifico
            {
                status = "INFO",
                messages = new List<MessageEspecifico>(),
                time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                client_id = null,
                logistics_provider = 0,
                logistics_provider_name = null,
                timezone = "America/Sao_Paulo",
                locale = "pt_BR",
                hash = hash.ToString()
            };

            try
            {
                var stmString = requestString;
                if (string.IsNullOrEmpty(requestString))
                    stmString = JsonConvert.SerializeObject(tracking);


                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hash });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hash, tracking, stmString });

                if (tracking == null)
                {
                    ret.status = "INFO";
                    ret.messages.Add(new MessageEspecifico
                    {
                        type = "INFO",
                        text = "Ocorrência vazia.",
                        key = "info.message"
                    });
                    return Task.FromResult(ret);
                }

                if (token == Guid.Empty)
                {

                    _logger.LogError(processName, "Token obrigatório", new { hash, stmString });

                    ret.messages.Add(new MessageEspecifico
                    {
                        type = "INFO",
                        text = "Token obrigaório.",
                        key = "info.message"
                    });

                    return Task.FromResult(ret);
                }
                else
                {
                    transportador = _cache.ObterLista<Transportador>(x => x.TokenId == token).FirstOrDefault();
                    _logger.LogInfo(processName, "Token do responsável", new { hash, token, transportador });
                }

                TransportadorCnpj tCnpj = null;
                if (transportador != null)
                    tCnpj = _cache.ObterLista<TransportadorCnpj>(x => x.TransportadorId == transportador.Id).FirstOrDefault();
                else
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, stmString });
                    ret.messages.Add(new MessageEspecifico
                    {
                        type = "INFO",
                        text = "Token inválido.",
                        key = "info.message"
                    });
                    return Task.FromResult(ret);
                }

                try
                {
                    tracking = tracking ?? JsonConvert.DeserializeObject<EntradaTrackingFreteRapido>(stmString);
                }
                catch (Exception ex)
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, stmString }, exception: ex);

                    ret.status = "ERROR";
                    ret.messages.Add(new MessageEspecifico
                    {
                        type = "ERROR",
                        text = $"Não foi possível obter as informações do tracking. Erro: {ex.Message}",
                        key = "error.400"
                    });
                    return Task.FromResult(ret);
                }

                var listaEntregasParaKibana = new List<dynamic> { };
                int linha = 0;

                if (string.IsNullOrEmpty(tracking.Notas_Fiscais?.FirstOrDefault()?.Chave_Acesso) || string.IsNullOrEmpty(tracking.Notas_Fiscais?.FirstOrDefault()?.Numero)
                    || string.IsNullOrEmpty(tracking.Notas_Fiscais?.FirstOrDefault()?.Serie))
                {
                    ret.messages.Add(new MessageEspecifico
                    {
                        type = "INFO",
                        text = "É necessário informar a nota, serie e chave de acesso.",
                        key = "info.message"
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"Danfe, Nota ou Série não preenchida para o pedido {tracking.Numero_Pedido}.", tracking.Numero_Pedido });
                }
                else if (tracking.Notas_Fiscais?.Count == 0)
                {
                    ret.messages.Add(new MessageEspecifico
                    {
                        type = "INFO",
                        text = $"Não existe nota para o pedido { tracking.Numero_Pedido }",
                        key = "info.message"
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"Não existe nota para o pedido {tracking.Numero_Pedido}" });
                }
                else
                {
                    tracking.Notas_Fiscais.ForEach(n =>
                    {
                        linha++;
                        _busEspecifico.Send(new TrackingEspecificoMessage
                        {
                            Ds_Hash = hash,
                            Cd_Linha = linha,
                            Dt_Ocorrencia = Convert.ToDateTime(tracking.Data_Ocorrencia),
                            Cd_Danfe = n.Chave_Acesso,
                            Cd_SerieNotaFiscal = n.Serie,
                            Cd_NotaFiscal = n.Numero,
                            Cd_Sro = tracking.Id_Frete,
                            Ds_Ocorrencia = tracking.Nome,
                            Cd_Ocorrencia = tracking.Codigo.ToString(),
                            Cd_CnpjTransportador = tCnpj?.CNPJ,
                            Id_Transportador = transportador?.Id,
                            Id_OrigemImportacao = (byte)Enums.OrigemImportacao.FreteFacil.GetHashCode()
                        });
                        ret.status = "OK";
                        ret.messages.Add(new MessageEspecifico
                        {
                            type = "OK",
                            text = $"Ocorrências do pedido { tracking.Numero_Pedido } enviadas para fila de processamento.",
                            key = "info.message"
                        });
                        listaEntregasParaKibana.Add(new { dsLog = $"Nota {tracking.Notas_Fiscais.FirstOrDefault().Numero}/{tracking.Notas_Fiscais.FirstOrDefault().Serie} foi enviada para fila.", tracking.Numero_Pedido });
                    });
                }

                foreach (var log in listaEntregasParaKibana)
                    _logger.LogError(processName, log.dsLog, new { hash, transportador });

                return Task.FromResult(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro ao preparar o envelope para o processamento", new { hash }, exception: ex);
                return Task.FromResult(new ResponseEspecifico
                {
                    status = "ERROR",
                    messages = new List<MessageEspecifico>()
                        {
                            new MessageEspecifico {
                                type = "ERROR",
                                text = ex.Message,
                                key = "error.400"
                            }
                        },
                    time = $"{DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("00.0", CultureInfo.InvariantCulture)} ms",
                    timezone = "America/Sao_Paulo",
                    locale = "pt_BR",
                    hash = hash.ToString()
                });
            }
        }
        public async Task<RetornoWs<ECriticaArquivo>> TrackingPadraoAsync(List<EntradaTracking> listaOcorrencias, string requestString, Guid token)
        {
            var hash = Guid.NewGuid();
            string processName = "TrackingPadrao_Fila";
            var ret = new RetornoProcessamentoWs<ECriticaArquivo, Retorno>(new RetornoWs<ECriticaArquivo>()) { dsAppCnn = "TrackingPadrao" };
            try
            {
                if (string.IsNullOrEmpty(requestString))
                    requestString = JsonConvert.SerializeObject(listaOcorrencias);

                Empresa empresa = _cache.ObterLista<Empresa>(e => e.TokenId == token).FirstOrDefault();
                Transportador trasportador = _cache.ObterLista<Transportador>(t => t.TokenId == token).FirstOrDefault();

                var logObj = new { hash, requestString, listaOcorrencias, empresa, trasportador, token };
                _logger.LogInfo(processName, "Processo Iniciado", logObj);

                if (token.Equals(Guid.Empty))
                {
                    _logger.LogError(processName, "Token inválido", logObj);
                    return ret.ret.RetMsg("Token inválido.");
                }

                if (listaOcorrencias == null)
                {
                    _logger.LogError(processName, "Envelope sem ocorrência", logObj);
                    return ret.ret.RetMsg("É necessário informar ao menos uma ocorrência.");
                }

                _logger.LogInfo(processName, "Token do responsável", logObj);
                var i = 0;
                foreach (var ocorrencia in listaOcorrencias)
                {
                    i++;
                    ret.Set(i);
                    ret.ret.qtdRegistros++;

                    await _bus.Send(new TrackingPadraoMessage()
                    {
                        Cd_Linha = i,
                        Cd_CnpjCanal = ocorrencia.cnpjFilial,
                        Cd_CnpjTransportador = ocorrencia.cnpjTransportador,
                        Ds_Complemento = ocorrencia.complemento,
                        Cd_Danfe = ocorrencia.danfe,
                        Dt_Ocorrencia = ocorrencia.data,
                        Cd_Ocorrencia = ocorrencia.id.ToString(),
                        Ds_Token = ocorrencia.ToString(),
                        Cd_NotaFiscal = ocorrencia.notaFiscal,
                        Cd_SerieNotaFiscal = ocorrencia.serie
                    });
                }

                ret.ret.protocolo = 0;
                ret.ret.protocoloHash = hash.ToString();
                ret.Fim(null, true);

                _logger.LogInfo(processName, "Processo finalizado com sucesso");
                return ret.ret.RetMsg();
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro no processamento de ocorrência", exception: ex);
                return ret.ret.RetMsg(ex.Message);
            }
        }
        public Task<ResponseSequoia> TrackingSequoiaAsync(List<EntradaTrackingSequoia> listaOcorrencias, string requestString, Guid token)
        {
            var hash = Guid.NewGuid();
            string processName = "TrackingSequoia_Fila";
            Transportador transportador = null;
            var ret = new ResponseSequoia
            {
                Status = "Processado",
                Lista = new List<ListaEntregasErros>()
            };

            try
            {
                if (string.IsNullOrEmpty(requestString))
                    requestString = JsonConvert.SerializeObject(listaOcorrencias);

                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hash });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hash, listaOcorrencias, requestString });

                if (token.Equals(Guid.Empty))
                {
                    _logger.LogError(processName, "Token obrigatório", new { hash, requestString });
                    ret.Status = "Token obrigatório.";
                    return Task.FromResult(ret);
                }

                transportador = _cache.ObterLista<Transportador>(t => t.TokenId == token).FirstOrDefault();
                _logger.LogInfo(processName, "Token obrigatório", new { hash, transportador, requestString, token });

                if (transportador == null)
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, requestString });
                    ret.Status = "Token Inválido.";
                    return Task.FromResult(ret);
                }

                TransportadorCnpj tCnpj = null;
                tCnpj = _cache.ObterLista<TransportadorCnpj>(t => t.TransportadorId == transportador.Id).FirstOrDefault();

                try
                {
                    listaOcorrencias = listaOcorrencias ?? JsonConvert.DeserializeObject<List<EntradaTrackingSequoia>>(requestString);
                }
                catch (Exception ex)
                {
                    _logger.LogError(processName, "Não foi possível obter as informações do tracking", new { hash, requestString }, exception: ex);
                }

                var listaEntregasParaKibana = new List<dynamic> { };
                int linha = 0;
                foreach (var e in listaOcorrencias)
                {
                    if (string.IsNullOrEmpty(e.NumNfe) || string.IsNullOrEmpty(e.NumSerie) || string.IsNullOrEmpty(e.ChaveNfe_Cte))
                    {
                        ret.Lista.Add(new ListaEntregasErros() { Nrt = e.Nrt, Status = "Erro" });
                        listaEntregasParaKibana.Add(new { dsLog = $"Danfe, Nota ou Série não preenchida para a entrega de SRO {e.Ar}.", e.Nrt });
                    }
                    else if (e.Eventos?.Count == 0)
                    {
                        ret.Lista.Add(new ListaEntregasErros() { Nrt = e.Nrt, Status = "Erro" });
                        listaEntregasParaKibana.Add(new { dsLog = $"Lista de ocorrêcias da nota {e.NumNfe}/{e.NumSerie} está vazia.", e.Nrt });
                    }
                    else
                    {
                        linha++;
                        foreach (var tracking in e.Eventos)
                            _busEspecifico.Send(new TrackingEspecificoMessage
                            {
                                Ds_Hash = hash,
                                Cd_Linha = linha,
                                Dt_Ocorrencia = Convert.ToDateTime(tracking.PayLoad.Date),
                                Cd_Danfe = e.ChaveNfe_Cte,
                                Cd_SerieNotaFiscal = e.NumSerie,
                                Cd_NotaFiscal = e.NumNfe,
                                Cd_Sro = e.Ar,
                                Ds_Ocorrencia = tracking.PayLoad.Comment,
                                Cd_Ocorrencia = tracking.Code,
                                Cd_CnpjTransportador = tCnpj?.CNPJ,
                                Id_Transportador = transportador?.Id,
                                Id_OrigemImportacao = (byte)Enums.OrigemImportacao.Sequoia.GetHashCode()
                            });
                        ret.Lista.Add(new ListaEntregasErros() { Nrt = e.Nrt, Status = "Processado" });
                        listaEntregasParaKibana.Add(new { dsLog = $"Nota {e.NumNfe}/{e.NumSerie} foi enviada para fila.", e.Nrt });
                    }
                }

                foreach (var log in listaEntregasParaKibana)
                    _logger.LogError(processName, log.dsLog, new { hash, transportador, log.Nrt });
                return Task.FromResult(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro ao preparar o envelope para o processamento", new { hash }, exception: ex);
                return Task.FromResult(new ResponseSequoia
                {
                    Status = "Erro",
                    Lista = new List<ListaEntregasErros>() { }
                });
            }
        }
        public Task<string> WebhookSyncAsync(EntradaSync entrada, string requestString, Guid token)
        {

            var hash = Guid.NewGuid();
            string processName = "WebhookSync_Fila";

            try
            {
                var stmString = requestString;
                if (string.IsNullOrEmpty(requestString))
                    stmString = JsonConvert.SerializeObject(entrada);

                _logger.LogInfo(processName, "Entrada de Ocorrência", new { hash, stmString });
                _logger.LogInfo(processName, "Objeto de Ocorrência", new { hash, entrada });

                if (string.IsNullOrEmpty(entrada?.invoice?.invoice_key) || string.IsNullOrEmpty(entrada?.invoice?.invoice_number) || string.IsNullOrEmpty(entrada?.invoice?.invoice_series))
                {

                    _logger.LogError(processName, "Informações não localizadas no objeto", new { hash });
                    return Task.FromResult("Informações não localizadas no objeto.");
                }

                var cnpj = entrada?.invoice.invoice_key?.Substring(6, 14)?.CleanCnpj();
                if (string.IsNullOrEmpty(cnpj?.Replace("0", string.Empty)))
                {

                    _logger.LogError(processName, "Cnpj não localizado", new { hash, cnpj });
                    return Task.FromResult($"Cnpj não localizado: {cnpj}");
                }

                _busWebhookSync.Send(new WebhookSyncMessage
                {
                    Ds_Ocorrencia = entrada?.history?.shipment_volume_micro_state?.description,
                    Cd_Ocorrencia = entrada?.history?.shipment_volume_micro_state?.code,
                    Dt_Ocorrencia = entrada?.history?.event_date_iso?.ToLocalTime(),
                    Cd_NotaFiscal = entrada?.invoice?.invoice_number,
                    Cd_SerieNotaFiscal = entrada?.invoice?.invoice_series,
                    Cd_CnpjCanal = entrada?.invoice?.invoice_key?.Substring(6, 14),
                    Cd_Sro = entrada?.tracking_code,
                    Ds_Hash = hash
                });

                _logger.LogInfo(processName, "Ocorrencia enviada com sucesso", new { hash });
                return Task.FromResult(hash.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError("WebhookSync_Fila", "Erro ao enviar para a fila do webhookSync", new { hash }, exception: ex);
                throw ex;
            }
        }
        public Task<ResponseEuEntrego> TrackingEuEntregoAsync(EntradaTrackingEuEntrego tracking, string requestString, Guid token)
        {
            var dt = DateTime.Now;
            var hash = Guid.NewGuid();
            Transportador transportador = null;
            string processName = "TrackingEuEntrego_Fila";
            var ret = new ResponseEuEntrego()
            {
                message = string.Empty,
                errors = new List<ErrorEuEntrego>(),
            };

            try
            {
                var stmString = requestString;
                if (string.IsNullOrEmpty(requestString))
                    stmString = JsonConvert.SerializeObject(tracking);


                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hash });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hash, tracking, stmString });

                if (tracking == null)
                {
                    ret.message = "A Requisição está vazia.";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = "A Requisição está vazia.",
                    });
                    return Task.FromResult(ret);
                }

                if (token == Guid.Empty)
                {

                    _logger.LogError(processName, "Token obrigatório", new { hash, stmString });

                    ret.message = "Token não Informado.";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = "Token não Informado.",
                    });

                    return Task.FromResult(ret);
                }
                else
                {
                    transportador = _cache.ObterLista<Transportador>(x => x.TokenId == token).FirstOrDefault();
                    _logger.LogInfo(processName, "Token do responsável", new { hash, token, transportador });
                }

                TransportadorCnpj tCnpj = null;
                if (transportador != null)
                    tCnpj = _cache.ObterLista<TransportadorCnpj>(x => x.TransportadorId == transportador.Id).FirstOrDefault();
                else
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, stmString });
                    ret.message = "Token Invalido.";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = "Token Invalido.",
                    });
                    return Task.FromResult(ret);
                }

                try
                {
                    tracking = tracking ?? JsonConvert.DeserializeObject<EntradaTrackingEuEntrego>(stmString);
                }
                catch (Exception ex)
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, stmString }, exception: ex);

                    ret.message = "Tracking invalido.";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = $"Não foi possível obter as informações do tracking. Erro: {ex.Message}",
                    });
                    return Task.FromResult(ret);
                }

                var listaEntregasParaKibana = new List<dynamic> { };
                int linha = 0;
                string notaSerie = tracking.invoice?.invoiceSeries;

                if (string.IsNullOrEmpty(tracking.invoice?.invoiceKey) || string.IsNullOrEmpty(tracking.invoice?.invoiceNumber)
                    || string.IsNullOrEmpty(notaSerie))
                {
                    ret.message = "ERRO";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = $"É necessário informar a nota, serie e chave de acesso.",
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"Danfe, Nota ou Série não preenchida para o pedido {tracking.orderId}.", tracking.orderId });
                }
                else if (tracking.packages?.Count == 0)
                {
                    ret.message = "ERRO";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = $"Não existe nota para o pedido { tracking.orderId }",
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"Não existe nota para o pedido {tracking.orderId}" });
                }
                else if (!string.IsNullOrEmpty(notaSerie) && !notaSerie.All(char.IsDigit))
                {
                    ret.message = "ERRO";
                    ret.errors.Add(new ErrorEuEntrego
                    {
                        type = "ERRO",
                        message = $"A Serie da Nota Fiscal tem que ser Numérica: {tracking.invoice?.invoiceSeries}",
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"A Serie da Nota Fiscal tem que ser Numérica: {tracking.invoice?.invoiceSeries}. Pedido: {tracking.orderId}" });
                }
                else
                {
                    var cdOcorrencia = IdentificaCodigoOcorrencia(tracking.status);
                    linha++;
                    _busEspecifico.Send(new TrackingEspecificoMessage
                    {
                        Ds_Hash = hash,
                        Cd_Linha = linha,
                        Dt_Ocorrencia = Convert.ToDateTime(tracking.processedAt),
                        Cd_Danfe = tracking.invoice?.invoiceKey,
                        Cd_SerieNotaFiscal = tracking.invoice?.invoiceSeries,
                        Cd_NotaFiscal = tracking.invoice?.invoiceNumber,
                        Cd_Sro = null,
                        Ds_Ocorrencia = tracking.status,
                        Cd_Ocorrencia = cdOcorrencia,
                        Cd_CnpjTransportador = tCnpj?.CNPJ,
                        Id_Transportador = transportador?.Id,
                        Id_OrigemImportacao = (byte)Enums.OrigemImportacao.EuEntrego.GetHashCode()
                    });
                    ret.message = "OK";
                    _logger.LogInfo(processName, $"Nota {tracking.invoice?.invoiceNumber}/{tracking.invoice?.invoiceSeries} foi enviada para fila. Pedido: {tracking.orderId}", new { hash, transportador });
                }

                foreach (var log in listaEntregasParaKibana)
                    _logger.LogError(processName, log.dsLog, new { hash, transportador });

                return Task.FromResult(ret);
            }
            catch (Exception ex)
            {
                var listErroFinal = new List<ErrorEuEntrego>();
                var erroFinal = new ErrorEuEntrego()
                {
                    type = "Erro",
                    message = ex.Message,
                };
                listErroFinal.Add(erroFinal);
                _logger.LogError(processName, "Erro ao preparar o envelope para o processamento", new { hash }, exception: ex);

                return Task.FromResult(new ResponseEuEntrego
                {
                    message = "ERRO",
                    errors = listErroFinal
                });
            }
        }
        public Task<ResponseLoggi> TrackingLoggiAsync(EntradaTrackingLoggi package, string requestString, Guid token)
        {
            var dt = DateTime.Now;
            var hash = Guid.NewGuid();
            Transportador transportador = null;
            string processName = "TrackingLoggi_Fila";
            var ret = new ResponseLoggi()
            {
                message = string.Empty,
                errors = new List<ErrorLoggi>(),
            };

            try
            {
                var stmString = requestString;
                if (string.IsNullOrEmpty(requestString))
                    stmString = JsonConvert.SerializeObject(package);

                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hash });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hash, package, stmString });

                if (package == null)
                {
                    ret.message = "A Requisição está vazia.";
                    ret.errors.Add(new ErrorLoggi
                    {
                        type = "ERRO",
                        message = "A Requisição está vazia.",
                    });
                    return Task.FromResult(ret);
                }

                if (token == Guid.Empty)
                {

                    _logger.LogError(processName, "Token obrigatório", new { hash, stmString });

                    ret.message = "Token não Informado.";
                    ret.errors.Add(new ErrorLoggi
                    {
                        type = "ERRO",
                        message = "Token não Informado.",
                    });

                    return Task.FromResult(ret);
                }
                else
                {
                    transportador = _cache.ObterLista<Transportador>(x => x.TokenId == token).FirstOrDefault();
                    _logger.LogInfo(processName, "Token do responsável", new { hash, token, transportador });
                }

                TransportadorCnpj tCnpj = null;
                if (transportador != null)
                    tCnpj = _cache.ObterLista<TransportadorCnpj>(x => x.TransportadorId == transportador.Id).FirstOrDefault();
                else
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, stmString });
                    ret.message = "Token Invalido.";
                    ret.errors.Add(new ErrorLoggi
                    {
                        type = "ERRO",
                        message = "Token Invalido.",
                    });
                    return Task.FromResult(ret);
                }

                try
                {
                    package = package ?? JsonConvert.DeserializeObject<EntradaTrackingLoggi>(stmString);
                }
                catch (Exception ex)
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, stmString }, exception: ex);

                    ret.message = "Tracking invalido.";
                    ret.errors.Add(new ErrorLoggi
                    {
                        type = "ERRO",
                        message = $"Não foi possível obter as informações do tracking. Erro: {ex.Message}",
                    });
                    return Task.FromResult(ret);
                }

                var listaEntregasParaKibana = new List<dynamic> { };
                int linha = 0;

                if (package.Packages?.Count == 0)
                {
                    ret.message = "ERRO";
                    ret.errors.Add(new ErrorLoggi
                    {
                        type = "ERRO",
                        message = $"Não existem dados no evelope postado.",
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"Não existem dados no evelope postado." });
                }
                else if (package.Packages.FirstOrDefault()?.Status?.Description == null)
                {
                    ret.message = "ERRO";
                    ret.errors.Add(new ErrorLoggi
                    {
                        type = "ERRO",
                        message = $"Sem dados de tracking para o sro: {package.Packages.FirstOrDefault()?.TrackingCode}",
                    });

                    listaEntregasParaKibana.Add(new { dsLog = $"Sem dados de tracking para o sro: {package.Packages.FirstOrDefault()?.TrackingCode}" });
                }
                else
                {
                    foreach (var tracking in package.Packages)
                    {
                        linha++;
                        _busEspecifico.Send(new TrackingEspecificoMessage
                        {
                            Ds_Hash = hash,
                            Cd_Linha = linha,
                            Dt_Ocorrencia = Convert.ToDateTime(tracking.Status.UpdatedTime),
                            Cd_Danfe = null,
                            Cd_SerieNotaFiscal = null,
                            Cd_NotaFiscal = null,
                            Cd_Sro = tracking.TrackingCode,
                            Ds_Ocorrencia = tracking.Status.HighLevelStatus,
                            Cd_Ocorrencia = tracking.Status.Code,
                            Cd_CnpjTransportador = tCnpj?.CNPJ,
                            Id_Transportador = transportador?.Id,
                            Id_OrigemImportacao = (byte)Enums.OrigemImportacao.Loggi.GetHashCode()
                        }); ;
                        ret.message = "OK";
                        _logger.LogInfo(processName, $"O pedido com sro {tracking.TrackingCode} foi enviado para fila.", new { hash, transportador });
                    }
                }

                foreach (var log in listaEntregasParaKibana)
                    _logger.LogError(processName, log.dsLog, new { hash, transportador });

                return Task.FromResult(ret);
            }
            catch (Exception ex)
            {
                var listErroFinal = new List<ErrorLoggi>();
                var erroFinal = new ErrorLoggi()
                {
                    type = "Erro",
                    message = ex.Message,
                };
                listErroFinal.Add(erroFinal);
                _logger.LogError(processName, "Erro ao preparar o envelope para o processamento", new { hash }, exception: ex);

                return Task.FromResult(new ResponseLoggi
                {
                    message = "ERRO",
                    errors = listErroFinal
                });
            }
        }
        public Task<ResponseAllPost> TrackingAllPostAsync(EntradaTrackingAllPost tracking, string requestString, Guid token)
        {
            var hash = Guid.NewGuid();
            string processName = "TrackingAllPost_Fila";
            Transportador transportador = null;
            var ret = new ResponseAllPost { retorno = "sucesso", obs = "" };

            try
            {
                if (string.IsNullOrEmpty(requestString))
                    requestString = JsonConvert.SerializeObject(tracking);

                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hash });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hash, tracking, requestString });

                if (token.Equals(Guid.Empty))
                {
                    _logger.LogError(processName, "Token obrigatório", new { hash, requestString });
                    ret.retorno = "Token obrigatório.";
                    return Task.FromResult(ret);
                }

                if (tracking.tipo.ToLower() != "rastreio")
                {
                    _logger.LogError(processName, "Tipo da requisição deve ser rastreio.", new { hash, requestString });
                    ret.retorno = "Tipo da requisição deve ser rastreio.";
                    return Task.FromResult(ret);
                }

                transportador = _cache.ObterLista<Transportador>(t => t.TokenId == token).FirstOrDefault();
                _logger.LogInfo(processName, "Token obrigatório", new { hash, transportador, requestString, token });

                if (transportador == null)
                {
                    _logger.LogError(processName, "Token inválido.", new { hash, requestString });
                    ret.retorno = "Token Inválido.";
                    return Task.FromResult(ret);
                }

                TransportadorCnpj tCnpj = null;
                tCnpj = _cache.ObterLista<TransportadorCnpj>(t => t.TransportadorId == transportador.Id).FirstOrDefault();

                try
                {
                    tracking = tracking ?? JsonConvert.DeserializeObject<EntradaTrackingAllPost>(requestString);
                }
                catch (Exception ex)
                {
                    _logger.LogError(processName, "Não foi possível obter as informações do tracking", new { hash, requestString }, exception: ex);
                }

                if (tracking.dados == null)
                {
                    _logger.LogError(processName, "Pacote sem dados de rastreamento.", new { hash, requestString });
                    ret.retorno = "Pacote sem dados de rastreamento.";
                    return Task.FromResult(ret);
                }

                if (string.IsNullOrEmpty(tracking.dados.pedidoAuxiliar))
                {
                    _logger.LogError(processName, "Pedido auxiliar é obrigatório para rastreamento.", new { hash, requestString });
                    ret.retorno = "Pedido auxiliar é obrigatório para rastreamento.";
                    return Task.FromResult(ret);
                }

                _busEspecifico.Send(new TrackingEspecificoMessage
                {
                    Ds_Hash = hash,
                    Cd_Linha = 1,
                    Dt_Ocorrencia = Convert.ToDateTime(tracking.dados.data),
                    Cd_Danfe = null,
                    Cd_SerieNotaFiscal = null,
                    Cd_NotaFiscal = null,
                    Cd_Sro = null,
                    Cd_EntregaSaida = tracking.dados.pedidoAuxiliar,
                    Ds_Ocorrencia = RetornaDescricaoOcorrencia(tracking.dados),
                    Cd_Ocorrencia = tracking.dados.idOcoren.ToString(),
                    Cd_CnpjTransportador = tCnpj?.CNPJ,
                    Id_Transportador = transportador?.Id,
                    Id_OrigemImportacao = (byte)Enums.OrigemImportacao.Sequoia.GetHashCode(),
                    Cd_CnpjCanal = null
                });

                ret.obs = "Ocorrencia enviada para fila.";
                _logger.LogInfo(processName, "Ocorrencia enviada para fila.", new { hash, requestString });
                return Task.FromResult(ret);
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro ao preparar o envelope para o processamento", new { hash }, exception: ex);
                return Task.FromResult(new ResponseAllPost
                {
                    retorno = ex.Message
                });
            }
        }
        private string IdentificaCodigoOcorrencia(string status)
        {
            string cdOcorrencia = string.Empty;
            switch (status)
            {
                case "DELIVERED":
                    cdOcorrencia = "1";
                    break;
                case "DELIVECOLLECTEDRED":
                    cdOcorrencia = "2";
                    break;
                case "DELIVERED_TO_RELATIVE":
                    cdOcorrencia = "4";
                    break;
                case "DELIVERED_TO_GATEKEEPER":
                    cdOcorrencia = "5";
                    break;
                case "EN_ROUTE":
                    cdOcorrencia = "28";
                    break;
                case "REJECTED":
                    cdOcorrencia = "31";
                    break;
                case "RECIPIENT_ABSENT":
                    cdOcorrencia = "32";
                    break;
                case "ADDRESS_WRONG":
                    cdOcorrencia = "35";
                    break;
                case "LOCATION_NOT_FOUND":
                    cdOcorrencia = "36";
                    break;
                case "INVOICE_RETURNED":
                    cdOcorrencia = "37";
                    break;
                case "ORDER_RETURNED":
                    cdOcorrencia = "38";
                    break;
                case "RECIPIENT_ABSENT_COMMERCIAL":
                    cdOcorrencia = "40";
                    break;
                case "COLLECT_RECIPIENT_ABSENT":
                    cdOcorrencia = "41";
                    break;
                case "COLLECT_ADDRESS_WRONG":
                    cdOcorrencia = "45";
                    break;
                case "DELIVERY_STRAYED":
                    cdOcorrencia = "78";
                    break;
                case "COLLECT_STRAYED":
                    cdOcorrencia = "80";
                    break;
                case "DELIVERY_OTHER":
                    cdOcorrencia = "83";
                    break;
                case "TIME_LIMIT":
                    cdOcorrencia = "82";
                    break;
                case "VEHICLE_PROBLEM":
                    cdOcorrencia = "81";
                    break;
                case "NOT_READY_ORDER_CANCELLED":
                    cdOcorrencia = "85";
                    break;
                case "CLIMATE_PROBLEM":
                    cdOcorrencia = "99";
                    break;
                case "COLLECT_OTHER":
                    cdOcorrencia = "98";
                    break;
                case "COLLECT_REJECTED":
                    cdOcorrencia = "97";
                    break;
                default:
                    cdOcorrencia = null;
                    break;
            }
            return cdOcorrencia;
        }
        private string RetornaDescricaoOcorrencia(DadosAllPost dados)
        {
            if (dados.idOcoren >= 3 && dados.idOcoren <= 92)
                return dados.mensagem;
            else if (dados.idOcoren == 1 || dados.idOcoren == 12)
                return "entregue";
            else
                return dados.situacao;
        }
    }
}
