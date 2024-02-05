using Fretter.Domain.Config;
using Fretter.Domain.Dto.PedidoPendenteBSeller;
using Fretter.Domain.Dto.PedidoPendenteTransportador.Rodonaves;
using Fretter.Domain.Dto.PedidoPendenteTransportador.SSW;
using Fretter.Domain.Dto.PedidoPendenteTransportador.UxDelivery;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Helpers.PedidoPendenteTransportador;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Fretter.Domain.Services
{
    public class PedidoPendenteTransportadorService<TContext> : ServiceBase<TContext, PedidoPendenteTransportador>, IPedidoPendenteTransportadorService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IPedidoPendenteTransportadorRepository<TContext> _Repository;
        private readonly IMessageBusService<PedidoPendenteTransportadorService<TContext>> _messageBusService;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly PedidoIntegracaoConfig _pedidoIntegracaoConfig;
        private const int ThreadSleep = 60000;
        private const int TimeoutCorreios = 20000;


        public PedidoPendenteTransportadorService(IPedidoPendenteTransportadorRepository<TContext> Repository,
            IUnitOfWork<TContext> unitOfWork,
            IUsuarioHelper user,
            IOptions<MessageBusConfig> messageBusConfig,
            IOptions<PedidoIntegracaoConfig> pedidoIntegracaoConfig,
            IMessageBusService<PedidoPendenteTransportadorService<TContext>> messageBusService) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
            _messageBusService = messageBusService;
            _messageBusConfig = messageBusConfig.Value;
            _pedidoIntegracaoConfig = pedidoIntegracaoConfig.Value;
            _messageBusService.InitReceiver(ReceiverType.Queue, _messageBusConfig.ConnectionString, _messageBusConfig.PedidoPendenteBatimentoQueue, "", _messageBusConfig.PreFetchCountPedidoPendenteBatimento);
        }

        public async Task<int> ProcessarTransportadoraPedidoStatus()
        {
            IList<MessageData<PedidoPendenteDTO>> listaMessagePedidoPendenteTransportador;
            try
            {
                listaMessagePedidoPendenteTransportador = await _messageBusService.Receive<PedidoPendenteDTO>(_messageBusConfig.ConsumeCount, _messageBusConfig.SecondsToTimeout);
            }
            catch (Exception ex)
            {
                var messageException = ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty);
                Console.WriteLine($"Erro ao buscar itens da fila: {_messageBusConfig.PedidoPendenteBatimentoQueue} / Finalizado- {DateTime.Now.ToString("HH:mm:ss")} - Erro {messageException} ");
                Thread.Sleep(ThreadSleep);
                return 0;
            }

            if (listaMessagePedidoPendenteTransportador?.FirstOrDefault() != null)
            {
                var listaPedidoPendenteTransportador = new List<PedidoPendenteTransportador>();
                var listCommit = new List<MessageData<PedidoPendenteDTO>>();
                await RunWithMaxDegreeOfConcurrency(10, listaMessagePedidoPendenteTransportador, async message =>
                {
                    var item = await BuscarTrackingTransportador(message.Body);
                    listaPedidoPendenteTransportador.Add(item);
                    listCommit.Add(message);
                });
                _Repository.SalvarPedidoPendenteTransportador(CollectionHelper.ConvertTo(listaPedidoPendenteTransportador));
                await _messageBusService.Commit(listCommit);
            }

            return listaMessagePedidoPendenteTransportador == null ? 0 : listaMessagePedidoPendenteTransportador.Count();
        }

        public async Task RunWithMaxDegreeOfConcurrency<T>(int maxDegreeOfConcurrency, IEnumerable<T> collection, Func<T, Task> taskFactory)
        {
            var activeTasks = new List<Task>(maxDegreeOfConcurrency);
            foreach (var task in collection.Select(taskFactory))
            {
                activeTasks.Add(task);
                if (activeTasks.Count == maxDegreeOfConcurrency)
                {
                    await Task.WhenAny(activeTasks.ToArray());
                    //observe exceptions here
                    activeTasks.RemoveAll(t => t.IsCompleted);
                }
            }

            await Task.WhenAll(activeTasks.ToArray());
        }

        private async Task<PedidoPendenteTransportador> BuscarTrackingTransportador(PedidoPendenteDTO pedido)
        {
            var pedidoPendenteTransportador = new PedidoPendenteTransportador(pedido.EmpresaId, pedido.EntregaId, pedido.TransportadorId);
            await EnviarBuscaTracking(pedido, pedidoPendenteTransportador);
            return pedidoPendenteTransportador;
        }

        private async Task EnviarBuscaTracking(PedidoPendenteDTO pedido, PedidoPendenteTransportador pedidoPendenteTransportador)
        {
            var danfe = string.IsNullOrEmpty(pedido.DanfeDRS) ? pedido.Danfe : pedido.DanfeDRS;
            var notaFiscal = string.IsNullOrEmpty(pedido.NotaFiscalDRS) ? pedido.NotaFiscal : pedido.NotaFiscalDRS;
            var cnpjTransportador = string.IsNullOrEmpty(pedido.CnpjCanalDRS) ? pedido.CnpjCanal : pedido.CnpjCanalDRS;

            switch (pedido.PedidoPendenteIntegracaoId)
            {
                case (int)EnumPedidoIntegracao.UxDelivery:
                    await BuscaTrackingUXDelivery(danfe, pedidoPendenteTransportador);
                    break;
                case (int)EnumPedidoIntegracao.Rodonaves:
                    await BuscaTrackingRodonaves(cnpjTransportador, notaFiscal, pedidoPendenteTransportador);
                    break;
                case (int)EnumPedidoIntegracao.Correios:
                    BuscaTrackingCorreios(pedido.Sro, pedidoPendenteTransportador);
                    break;
                case (int)EnumPedidoIntegracao.SSW:
                default:
                    await BuscaTrackingSSW(danfe, pedidoPendenteTransportador);
                    pedido.PedidoPendenteIntegracaoId = (int)EnumPedidoIntegracao.SSW;
                    break;
            }

            var listaIntegracao = pedido.ListaIntegracaoEnviada ?? new List<EnumPedidoIntegracao>();
            listaIntegracao.Add((EnumPedidoIntegracao)pedido.PedidoPendenteIntegracaoId);
            pedido.ListaIntegracaoEnviada = listaIntegracao;

            if (!pedidoPendenteTransportador.Sucesso)
            {
                await BuscarTransportadorDiferente(pedido, pedidoPendenteTransportador);
            }
            pedidoPendenteTransportador.AtualizarIntegracaoEnviada(string.Join(",", pedido.ListaIntegracaoEnviada.Select(s => s.ToString())));
        }

        private async Task BuscarTransportadorDiferente(PedidoPendenteDTO pedido, PedidoPendenteTransportador pedidoPendenteTransportador)
        {
            var listaIntegracaoGeral = new List<EnumPedidoIntegracao>()
            {
                EnumPedidoIntegracao.Rodonaves,
                EnumPedidoIntegracao.SSW,
                EnumPedidoIntegracao.UxDelivery,
                EnumPedidoIntegracao.Correios
            };

            if (listaIntegracaoGeral.All(a => pedido.ListaIntegracaoEnviada.Contains(a)))
            {
                pedidoPendenteTransportador.AtualizarStatusTransportadora("Objeto não encontrado");
                return;
            }

            pedido.PedidoPendenteIntegracaoId = (int)listaIntegracaoGeral.First(f => !pedido.ListaIntegracaoEnviada.Contains(f));
            await EnviarBuscaTracking(pedido, pedidoPendenteTransportador);
        }



        #region Transportadora
        public async Task<PedidoPendenteTransportador> BuscaTrackingSSW(string chaveNfe, PedidoPendenteTransportador pedidoPendenteTransportador)
        {
            var integracao = _pedidoIntegracaoConfig.Itens.FirstOrDefault(f => f.PedidoPendenteIntegracaoId == EnumPedidoIntegracao.SSW);
            if (integracao == null)
                return null;
            try
            {
                var webApiClient = new WebApiClient(integracao.Endpoint);
                var resquest = new RequestTrackingSSW() { chave_nfe = chaveNfe };
                var objResponse = await webApiClient.Post(integracao.Endpoint, resquest);
                var response = await objResponse.Content.ReadAsStringAsync();
                var objSsw = JsonConvert.DeserializeObject<ResponseTrackingSSW>(response);
                return pedidoPendenteTransportador.PopulatePedidoPendenteTransportador(objSsw);
            }
            catch
            {
                return null;
            }   
        }

        public async Task<PedidoPendenteTransportador> BuscaTrackingUXDelivery(string chaveNfe, PedidoPendenteTransportador pedidoPendenteTransportador)
        {
            var integracao = _pedidoIntegracaoConfig?.Itens?.FirstOrDefault(f => f.PedidoPendenteIntegracaoId == EnumPedidoIntegracao.UxDelivery);
            if (integracao == null)
                return null;
            try
            {
                var webApiClient = new WebApiClient(integracao.Endpoint);
                webApiClient.IsAnonymous = true;

                var header = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>(integracao.User,integracao.Password)
                };

                var resquest = new RequestTrackingUxDelivery()
                {
                    ChaveNFe = chaveNfe
                };
                var response = await webApiClient.PostWithHeader<ResponseTrackingUxDelivery>(integracao.Endpoint, resquest, string.Empty, header);
                return pedidoPendenteTransportador.PopulatePedidoPendenteTransportador(response);
            }
            catch
            {
                return null;
            }
        }

        public async Task<PedidoPendenteTransportador> BuscaTrackingRodonaves(string cnpj, string notaFiscal, PedidoPendenteTransportador pedidoPendenteTransportador)
        {
            var integracao = _pedidoIntegracaoConfig.Itens.FirstOrDefault(f => f.PedidoPendenteIntegracaoId == EnumPedidoIntegracao.Rodonaves);
            if (integracao == null)
                return null;
            try
            {
                var webApiClientRodonaves = new WebApiClient(integracao.Endpoint);

                await webApiClientRodonaves.AuthenticateGrantType(integracao.Endpoint + "token", integracao.User, integracao.Password);

                var urlRastreio = string.Format("{0}api/v1/rastreio-mercadoria?taxIdRegistration={1}&invoiceNumber={2}",
                    integracao.Endpoint, cnpj, notaFiscal);

                var response = await webApiClientRodonaves.Get<ResponseTrackingRodonaves>(urlRastreio);
                return pedidoPendenteTransportador.PopulatePedidoPendenteTransportador(response);
            }
            catch
            {
                return null;
            }

        }

        public PedidoPendenteTransportador BuscaTrackingCorreios(string sro, PedidoPendenteTransportador pedidoPendenteTransportador)
        {
            var integracao = _pedidoIntegracaoConfig.Itens.FirstOrDefault(f => f.PedidoPendenteIntegracaoId == EnumPedidoIntegracao.Correios);
            if (integracao == null)
                return null;

            var hash_log = Guid.NewGuid().ToString();
            var xmlEnvio = new XmlDocument();
            var xmlRetorno = new XmlDocument();
            try
            {
                string envelope = $@"
                    <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:res=""http://resource.webservice.correios.com.br/"">
                    <soapenv:Header />
                    <soapenv:Body>
                    <res:buscaEventos>                               
                        <usuario>{integracao.User}</usuario>                             
                        <senha>{integracao.Password}</senha>                            
                        <tipo>L</tipo>                           
                        <resultado>T</resultado>                           
                        <lingua>101</lingua >                          
                        <objetos>{sro}</objetos>
                    </res:buscaEventos>
                    </soapenv:Body>
                    </soapenv:Envelope>";

                var request = (HttpWebRequest)WebRequest.Create(integracao.Endpoint);
                request.Headers.Add("SOAPAction", "buscaEventos");
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";
                request.Host = "webservice.correios.com.br";
                request.Timeout = TimeoutCorreios;
                request.ContinueTimeout = TimeoutCorreios;
                request.ReadWriteTimeout = TimeoutCorreios;
                request.KeepAlive = true;
                xmlEnvio.LoadXml(envelope);

                using (var stream = request.GetRequestStream())
                    xmlEnvio.Save(stream);
                using (var response = request.GetResponse())
                using (var st = response.GetResponseStream())
                    if (st != null)
                        using (var rd = new StreamReader(st))
                        {
                            var soapResult = rd.ReadToEnd();
                            xmlRetorno.LoadXml(soapResult);
                        }

                return pedidoPendenteTransportador.PopulatePedidoPendenteTransportador(xmlRetorno);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }

}
