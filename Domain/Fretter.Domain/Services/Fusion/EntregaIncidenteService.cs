using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Dto.LogisticaReversa.Enum;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static WSLogisticaReversa.logisticaReversaWSClient;

namespace Fretter.Domain.Services
{
    public class EntregaIncidenteService<TContext> : ServiceBase<TContext, EntregaStage>, IEntregaIncidenteService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IEntregaStageRepository<TContext> _repository;
        private readonly IEntregaDevolucaoRepository<TContext> _repositoryDevolucao;
        private readonly IEntregaRepository<TContext> _entregaRepository;
        private readonly IEntregaStageErroRepository<TContext> _entregaStageErroRepository;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly IMessageBusService<EntregaDevolucaoService<TContext>> _messageBusService;
        
        public EntregaIncidenteService(IEntregaStageRepository<TContext> repository,
                                       IUnitOfWork<TContext> unitOfWork,
                                       IMessageBusService<EntregaDevolucaoService<TContext>> messageBusService,
                                       IOptions<MessageBusConfig> messageBusConfig,
                                       IEntregaRepository<TContext> entregaRepository,
                                       IEntregaStageErroRepository<TContext> entregaStageErroRepository,
                                       IEntregaDevolucaoRepository<TContext> repositoryDevolucao,
                                    IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _entregaRepository = entregaRepository;
            _entregaStageErroRepository = entregaStageErroRepository;
            _repositoryDevolucao = repositoryDevolucao;
            _user = user;

            _messageBusConfig = messageBusConfig.Value;
            _messageBusService = messageBusService;
            _messageBusService.InitReceiver(Enum.ReceiverType.Queue, _messageBusConfig.ConnectionString, _messageBusConfig.EntregaIncidenteQueue);
        }
        public async Task ProcessarFilaIncidentes()
        {
            var messages = await _messageBusService.Receive<EntregaStageFilaDTO>(500, _messageBusConfig.SecondsToTimeout);
            if (messages.Count > 0)
            {
                List<EntregaStageFilaDTO> entregasIncidentes = messages.Select(x => x.Body).ToList();
                List<EntregaStageFilaDTO> entregasExtravio = new List<EntregaStageFilaDTO>();

                foreach (var entregaIncidente in entregasIncidentes.Where(x => x.Itens.Where( y => y.IncidenteCodigo == "91").Any()))
                {
                    bool incidenteReversa = entregaIncidente.Itens.Any(x => x.IncidenteCodigo == "91");
                    if (incidenteReversa)
                        ProcessaIncidenteReversa(entregaIncidente);
                }

                _unitOfWork.Commit();
                await _messageBusService.Commit(messages);
            }
        }
        private void ProcessaIncidenteReversa(EntregaStageFilaDTO entregaIncidente)
        {
            int entregaId = _entregaRepository.ObterEntregaPorCodigoPedido(entregaIncidente.EntregaEntrada);
            if (entregaId > 0)
            {
                entregaIncidente.Itens.Where(w => !string.IsNullOrEmpty(w.IncidenteCodigo)).ForEach(item =>
                 {
                     bool existeDevolucao = _repositoryDevolucao.GetQueryable(entregaDevolucao => entregaDevolucao.EntregaId == entregaId &&
                            ((entregaDevolucao.EntregaDevolucaoStatus != EnumEntregaDevolucaoStatus.Cancelado.GetHashCode() && entregaDevolucao.EntregaDevolucaoStatus != EnumEntregaDevolucaoStatus.Erro.GetHashCode() && entregaDevolucao.EntregaDevolucaoStatus != EnumEntregaDevolucaoStatus.AgCancelamento.GetHashCode())
                            && (entregaDevolucao.CodigoEntregaSaidaItem == item.EntregaSaida || entregaDevolucao.CodigoEntregaSaidaItem == null)
                            && entregaDevolucao.Ativo)).ToList().Any();

                     if (!existeDevolucao)
                     {
                         EntregaDevolucao entregaDevolucao = new EntregaDevolucao(0, entregaId, EnumEntregaTransporteTipo.AutorizacaoPostagem.GetHashCode(), null, null, null, null, DateTime.Now, EnumEntregaDevolucaoStatus.AgProcessamento.GetHashCode(), false, false, string.Empty, item.EntregaSaida);
                         _repositoryDevolucao.Save(entregaDevolucao);
                     }
                 });
            }
            else
            {
                string json = System.Text.Json.JsonSerializer.Serialize(entregaIncidente);
                EntregaStageErro entregaStageErro = new EntregaStageErro(0, DateTime.Now, null,
                    $"ProcessarFilaIncidentes - Erro no pedido de reversa. Entrega não localizada (cd_pedido {entregaIncidente.EntregaEntrada})", null, json);
                _entregaStageErroRepository.Save(entregaStageErro);
            }
        }
    }
}
