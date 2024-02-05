using Fretter.Domain.Config;
using Fretter.Domain.Dto.PedidoPendenteBSeller;
using Fretter.Domain.Dto.PedidoPendenteBSeller.BSeller;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class PedidoPendenteBSellerService<TContext> : ServiceBase<TContext, PedidoPendenteBSeller>, IPedidoPendenteBSellerService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IPedidoPendenteBSellerRepository<TContext> _Repository;
        private readonly IMessageBusService<PedidoPendenteBSellerService<TContext>> _messageBusService;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly PedidoIntegracaoConfig _pedidoIntegracaoConfig;


        private const string BSellerStatusPendente = "N";

        public PedidoPendenteBSellerService(IPedidoPendenteBSellerRepository<TContext> Repository,
            IUnitOfWork<TContext> unitOfWork,
            IUsuarioHelper user,
            IOptions<MessageBusConfig> messageBusConfig,
            IOptions<PedidoIntegracaoConfig> pedidoIntegracaoConfig,
            IMessageBusService<PedidoPendenteBSellerService<TContext>> messageBusService) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
            _messageBusService = messageBusService;
            _messageBusConfig = messageBusConfig.Value;
            _pedidoIntegracaoConfig = pedidoIntegracaoConfig.Value;
            _messageBusService.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.PedidoPendenteBatimentoQueue);
        }

        #region BSeller
        public async Task<int> ProcessarPedidoPendenteBSeller()
        {
            var listaEmpresaProcessamento = _Repository.BuscarEmpresaProcessamento();

            if (listaEmpresaProcessamento?.FirstOrDefault() != null)
            {
                var listaPedidoPendenteBseller = await BuscarPedidoPendenteBSeller(listaEmpresaProcessamento);
                if (listaPedidoPendenteBseller?.FirstOrDefault() != null)
                    EnviarLote(listaPedidoPendenteBseller);
            }

            return listaEmpresaProcessamento.Count;
        }

        private async void EnviarLote(List<PedidoPendenteBSeller> listaPedidoPendenteBseller)
        {
            var qtdLote = 3000;
            var lote = listaPedidoPendenteBseller;

            if (listaPedidoPendenteBseller.Count() > qtdLote)
                lote = listaPedidoPendenteBseller.Take(qtdLote).ToList();

            await _messageBusService.SendRange<PedidoPendenteBSeller>(lote);

            if (listaPedidoPendenteBseller.Skip(qtdLote).Any())
                EnviarLote(listaPedidoPendenteBseller.Skip(qtdLote).ToList());
        }

        private async Task<List<PedidoPendenteBSeller>> ProcessarPedidos(List<PedidoPendenteBSeller> listaPedidoPendenteBseller)
        {
            PopularStatusFusion(listaPedidoPendenteBseller);
            _Repository.SalvarPedidoPendenteBSeller(CollectionHelper.ConvertTo(listaPedidoPendenteBseller));

            return listaPedidoPendenteBseller.Where(w => w.EntregaId != null).ToList();

        }

        private void PopularStatusFusion(List<PedidoPendenteBSeller> listaPedidoPendenteBseller)
        {
            var listaPedidoStatus = _Repository.BuscarPedidoStatus((int)EnumPedidoIntegracao.BSeller);

            var listaPedidoStatusBSeller = listaPedidoPendenteBseller
                .Select(s => new { StatusBSellerTratado = s.NomePontoBSeller.ToUpper().Trim(), s.NomePontoBSeller }).Distinct()
                .Where(w => listaPedidoStatus.Select(s => s.Status.ToUpper().Trim()).Contains(w.StatusBSellerTratado))
                .ToList();

            listaPedidoStatusBSeller.ForEach(pedidoStatusBseller =>
            {
                var pedidoStatus = listaPedidoStatus.FirstOrDefault(f => f.Status.ToUpper().Trim() == pedidoStatusBseller.StatusBSellerTratado);
                if (pedidoStatus != null)
                {
                    listaPedidoPendenteBseller.Where(w => w.NomePontoBSeller == pedidoStatusBseller.NomePontoBSeller).ForEach(pedidoPendente =>
                    {
                        pedidoPendente.AtualizarStatusTratado(pedidoStatus.StatusFusion);
                    });
                }
            });
        }

        private async Task<List<PedidoPendenteBSeller>> BuscarPedidoPendenteBSeller(List<EmpresaProcessamento> listaClientesBatimento)
        {
            var listaPedidoBSeller = new List<PedidoPendenteBSeller>();

            foreach (var cliente in listaClientesBatimento)
            {
                var listaPedidoPendente = await BuscaTrackingBSeller(cliente);

                var listaPedido = GerarListaPedidoPendenteBSeller(listaPedidoPendente, cliente.EmpresaId);

                if (listaPedido?.FirstOrDefault() != null)
                    listaPedido = await ProcessarPedidos(listaPedido);

                listaPedidoBSeller.AddRange(listaPedido);
            }
            return listaPedidoBSeller;
        }

        private async Task<List<ResponseTrackingBSeller>> BuscaTrackingBSeller(EmpresaProcessamento cliente)
        {
            try
            {
                var integracao = _pedidoIntegracaoConfig?.Itens?.FirstOrDefault(f => f.PedidoPendenteIntegracaoId == EnumPedidoIntegracao.BSeller);
                if (integracao == null)
                    return new List<ResponseTrackingBSeller>();
                var webApiClient = new WebApiClient(integracao.Endpoint, cliente.TimeoutRequest);

                webApiClient.IsAnonymous = true;

                var header = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("X-Auth-Token",integracao.Password)
                };

                var resquest = new RequestTrackingBSeller()
                {
                    parametros = new Parametro()
                    {
                        P_ID_CONTRATO = null,
                        P_ID_ESTABELECIMENTO = cliente.EstabelecimentoBSellerId,
                        P_ID_FILIAL = null,
                        P_ID_TRANSP = null,
                        P_STATUS = BSellerStatusPendente,
                        P_DT_INI = DateTime.Now.AddDays(-(int)cliente.PeriodoEmDias).ToString("dd/MM/yyyy"),
                        P_DT_FIM = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy")
                    }
                };
                var result = await webApiClient.PostWithHeader<List<ResponseTrackingBSeller>>(integracao.Endpoint, resquest, string.Empty, header);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao buscar pedidos BSeller -{cliente.EstabelecimentoBSellerId} Finalizado- {DateTime.Now.ToString("HH:mm:ss")} - Erro {e.Message} ");
                return new List<ResponseTrackingBSeller>();
            }
        }

        private List<PedidoPendenteBSeller> GerarListaPedidoPendenteBSeller(List<ResponseTrackingBSeller> listaPedidoBSeller, int empresaId)
        {
            var listaPedidoPendenteBSeller = new List<PedidoPendenteBSeller>();

            var listaEntregaBSeller = listaPedidoBSeller.Select(s => new ItemEntrega(s.Entrega)).ToList();
            var listaEntregaPedido = _Repository.BuscarEntregaPedido(CollectionHelper.ConvertTo(listaEntregaBSeller));
            if (listaPedidoBSeller.Any())
            {
                listaPedidoBSeller.ForEach(pedidoBSeller =>
                {
                    EntregaPedido entregaPedido = listaEntregaPedido.FirstOrDefault(f => f.CdEntrega == pedidoBSeller.Entrega);

                    listaPedidoPendenteBSeller.Add(new PedidoPendenteBSeller(pedidoBSeller, empresaId, entregaPedido));
                });
            }

            return listaPedidoPendenteBSeller;
        }
        #endregion

    }

}
