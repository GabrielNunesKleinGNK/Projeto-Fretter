using Fretter.Domain.Config;
using Fretter.Domain.Config.WebHook;
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Dto.Webhook.Tracking.Entrada;
using Fretter.Domain.Dto.Webhook.Tracking.Message;
using Fretter.Domain.Dto.Webhook.Tracking.Response;
using Fretter.Domain.Dto.Webhook.TrackingIntegracao;
using Fretter.Domain.Entities.Fusion;
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
    public class TrackingIntegracaoService<TContext> : ITrackingIntegracaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ILogHelper _logger;
        private readonly ICacheService<TContext> _cache;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly IMessageBusService<EntradaTrackingEspecifico> _busEspecifico;
        private readonly IMessageBusService<TrackingIntegracaoService<TContext>> _busInsucesso;
        private readonly ITrackingIntegracaoRepository<TContext> _repository;
        private readonly TrackingIntegracaoConfig _trackingIntegracaoConfig;
        private readonly CultureInfo _culture = new CultureInfo("pt-BR");

        public TrackingIntegracaoService(ILogHelper logger,
                               ICacheService<TContext> cache,
                               IOptions<MessageBusConfig> messageBusConfig,
                               IMessageBusService<EntradaTrackingEspecifico> busEspecifico,
                               IMessageBusService<TrackingIntegracaoService<TContext>> busInsucesso,
                               ITrackingIntegracaoRepository<TContext> repository,
                               IOptions<TrackingIntegracaoConfig> trackingIntegracaoConfig)
        {
            _cache = cache;
            _logger = logger;
            _repository = repository;
            _trackingIntegracaoConfig = trackingIntegracaoConfig.Value;
            _messageBusConfig = messageBusConfig.Value;
            _busEspecifico = busEspecifico;
            _busInsucesso = busInsucesso;
            _busEspecifico.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.TrackingEspecificoTopic);
            _busInsucesso.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.TrackingIntegracaoInsucessoQueue);
        }

        public Task<ResponseEspecifico> ProcessaSucesso(List<TrackingIntegracaoEntradaDto> entrada, Guid hashProcessamento, Guid token)
        {
            string processName = "TrakingIntegracao_Sucesso", requestString = string.Empty;
            var dtAtual = DateTime.Now;
            Transportador transportador = null;
            TransportadorCnpj transportadorCnpj = null;

            if (token.ToString().ToUpper() != _trackingIntegracaoConfig.TrackingIntegracaoToken.ToUpper())
                return Task.FromResult(MontarRetornoApi("ERROR", "Você não tem permissão para executar esta ação.", dtAtual, hashProcessamento, "401"));

            try
            {
                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hashProcessamento });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hashProcessamento, entrada, requestString });

                requestString = JsonConvert.SerializeObject(entrada);
                if (!entrada.Any())
                {
                    _logger.LogError(processName, "Entrada de ocorrências vazia.", new { hashProcessamento, requestString });
                    return Task.FromResult(MontarRetornoApi("ERROR", $"Pacote de entrada de ocorrências vazio.", dtAtual, hashProcessamento, "400"));
                }

                List<TrackingEspecificoMessage> lstOcorrenciaEnvioFila = new List<TrackingEspecificoMessage>();
                var listaLogKibanaOcorrencias = new List<dynamic>() { };

                int linha = 0;
                foreach (var ocorrencia in entrada)
                {
                    Guid.TryParse(ocorrencia.ProtocoloOcorrencia, out Guid hashOcorrencia);
                    var stmOcorrencia = JsonConvert.SerializeObject(ocorrencia); 

                    transportadorCnpj = _cache.ObterLista<TransportadorCnpj>(x => x.CNPJ == ocorrencia.TransportadorCnpj).OrderBy(x => x.Id).FirstOrDefault();
                    if (transportadorCnpj == null)
                    {
                        _logger.LogError(processName, $"Transportador {ocorrencia.TransportadorCnpj} não encontrado.", new { hashProcessamento, hashOcorrencia, ocorrencia, requestString, stmOcorrencia });
                        return Task.FromResult(MontarRetornoApi("ERROR", $"Transportador {ocorrencia.TransportadorCnpj} não encontrado.", dtAtual, hashProcessamento, "400"));
                    }
                    else
                        transportador = _cache.ObterLista<Transportador>(x => x.Id == transportadorCnpj.TransportadorId).OrderBy(x => x.Id).FirstOrDefault();

                    if (string.IsNullOrEmpty(ocorrencia.NotaFiscal) || string.IsNullOrEmpty(ocorrencia.Serie) || string.IsNullOrEmpty(ocorrencia.Danfe))
                    {
                        _logger.LogError(processName, $"Danfe, Nota ou Série não preenchida para a entrega {ocorrencia.CodigoIntegracaoEntrega}.", new { hashProcessamento, hashOcorrencia, ocorrencia, requestString, stmOcorrencia });
                        return Task.FromResult(MontarRetornoApi("ERROR", $"Danfe, Nota ou Série não preenchida para a entrega {ocorrencia.CodigoIntegracaoEntrega}.", dtAtual, hashProcessamento, "400"));
                    }

                    else
                    {
                        linha++;                        

                        lstOcorrenciaEnvioFila.Add(new TrackingEspecificoMessage
                        {
                            Ds_Hash = hashOcorrencia,
                            Cd_Linha = linha,
                            Dt_Ocorrencia = ocorrencia.DataOcorrencia,
                            Cd_Danfe = ocorrencia.Danfe,
                            Cd_SerieNotaFiscal = ocorrencia.Serie,
                            Cd_NotaFiscal = ocorrencia.NotaFiscal,
                            Cd_Sro = ocorrencia.Sro,
                            Ds_Ocorrencia = ocorrencia.Descricao,
                            Ds_Complementar = ocorrencia.DescricaoComplementar,
                            Cd_Ocorrencia = ocorrencia.CodigoOcorrencia,
                            Cd_CnpjTransportador = transportadorCnpj.CNPJ,
                            Id_Transportador = transportador?.Id,
                            Ds_Dominio = ocorrencia.Dominio,
                            Cd_Latitude = ocorrencia.Latitude,
                            Cd_Longitude = ocorrencia.Longitude,
                            Nm_Cidade = ocorrencia.Cidade,
                            Cd_Uf = ocorrencia.Uf
                            //Id_OrigemImportacao = (byte)Enums.OrigemImportacao.Sequoia.GetHashCode()
                        });

                        listaLogKibanaOcorrencias.Add(new
                            {
                                mensagem = $"A ocorrência {ocorrencia.Descricao} da danfe {ocorrencia.Danfe} foi enviada para fila de Específico.",
                                complementares = new { hashProcessamento, hashOcorrencia, ocorrencia, requestString, stmOcorrencia }
                            });
                    }
                }

                _busEspecifico.SendRange<TrackingEspecificoMessage>(lstOcorrenciaEnvioFila);

                foreach (var log in listaLogKibanaOcorrencias)
                    _logger.LogInfo(processName, log.mensagem, log.complementares);

                return Task.FromResult(MontarRetornoApi("OK", $"Ocorrências enviadas com sucesso para fila Específica.", dtAtual, hashProcessamento, "200"));
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro ao processamento as ocorrências", new { hashProcessamento }, exception: ex);
                return Task.FromResult(MontarRetornoApi("ERROR", $"Falha no processamento das ocorrências. Message: {ex.Message} -> InnerException: {ex.InnerException}", dtAtual, hashProcessamento, "500"));
            }
        }

        public Task<ResponseEspecifico> ProcessaInsucesso(List<TrackingIntegracaoEntradaDto> entrada, Guid hashProcessamento, Guid token)
        {
            string processName = "TrakingIntegracao_Insucesso";
            var dtAtual = DateTime.Now;

            if (token.ToString().ToUpper() != _trackingIntegracaoConfig.TrackingIntegracaoToken.ToUpper())
                return Task.FromResult(MontarRetornoApi("ERROR", "Você não tem permissão para executar esta ação.", dtAtual, hashProcessamento, "401"));

            try
            {
                var requestString = JsonConvert.SerializeObject(entrada);

                _logger.LogInfo(processName, "Entrada de Ocorrencia", new { hashProcessamento });
                _logger.LogInfo(processName, "Arquivo de Ocorrencia", new { hashProcessamento, entrada, requestString });

                if (!entrada.Any())
                {
                    //_logger.LogError(processName, "Entrada de ocorrências vazia.", new { hashProcessamento, requestString });
                    return Task.FromResult(MontarRetornoApi("ERROR", $"Pacote de entrada de ocorrências vazio.", dtAtual, hashProcessamento, "400"));
                }

                _busInsucesso.SendRange<TrackingIntegracaoEntradaDto>(entrada);
                _logger.LogInfo(processName, "Ocorrências enviadas com sucesso para fila de Insucesso", new { hashProcessamento, entrada, requestString });

                return Task.FromResult(MontarRetornoApi("OK", $"Ocorrências enviadas com sucesso para fila de Insucesso.", dtAtual, hashProcessamento, "200"));
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro ao enviar as ocorrências para fila de Insucesso.", new { hashProcessamento }, exception: ex);
                return Task.FromResult(MontarRetornoApi("ERROR", $"Erro ao enviar as ocorrências para fila de Insucesso. Message: {ex.Message} -> InnerException: {ex.InnerException}", dtAtual, hashProcessamento, "500"));
            }
        }

        private ResponseEspecifico MontarRetornoApi(string status, string mensagem, DateTime dataAtual, Guid hashProcessamento, string statusCode)
        {
            return  new ResponseEspecifico
            {
                status = status,
                messages = new List<MessageEspecifico>()
                {
                    new MessageEspecifico 
                    {
                        type = status,
                        text = mensagem,
                        key = statusCode
                    }
                 },
                 time = $"{DateTime.Now.Subtract(dataAtual).TotalMilliseconds.ToString("00.0", _culture)} ms",
                 timezone = "America/Sao_Paulo",
                 locale = "pt_BR",
                 hash = hashProcessamento.ToString()
            };
        }
    }
}
