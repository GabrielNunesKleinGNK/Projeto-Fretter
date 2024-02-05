using Fretter.Domain.Config;
using Fretter.Domain.Dto.LogisticaReversa;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service.Fusion;
using System;
using Nest;
using Newtonsoft.Json;

namespace Fretter.Domain.Services.Fusion
{
    public class EntregaDevolucaoOcorrenciaElasticService<TContext> : ServiceBase<TContext, EntregaDevolucao>, IEntregaDevolucaoOcorrenciaElasticService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IElasticSearchRepository _elasticSearchService;
        private readonly ElasticSearchConfig _elasticSearchConfig;
        private readonly IEntregaDevolucaoRepository<TContext> _repository;
        private readonly IOcorrenciaTransportadorTipoRepository<TContext> _repositoryOcorrenciaTransportador;

        private const int CorreiosId = 3;
        public EntregaDevolucaoOcorrenciaElasticService(IElasticSearchRepository elasticSearchService,
                                                 IEntregaDevolucaoRepository<TContext> repository,
                                                 IOcorrenciaTransportadorTipoRepository<TContext> repositoryOcorrenciaTransportador,
                                                 IUnitOfWork<TContext> unitOfWork,
                                                 IOptions<ElasticSearchConfig> elasticSearchConfig,
                                                 IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _elasticSearchConfig = elasticSearchConfig.Value;
            _elasticSearchService = elasticSearchService;
            _elasticSearchService.InitElasticSearch(_elasticSearchConfig, Enum.EnumElasticConexaoTipo.PorUri);

            _repository = repository;
            _repositoryOcorrenciaTransportador = repositoryOcorrenciaTransportador;
        }

        public async Task GravarTrackingReversa(List<DevolucaoCorreioOcorrencia> listDevolucaoCorreioOcorrencia)
        {
            var listCodigoRastreio = listDevolucaoCorreioOcorrencia.Select(s => s.Cd_CodigoColeta).Distinct().ToList();
            var listEntregaDevolucao = _repository.GetAll(w => listCodigoRastreio.Contains(w.CodigoColeta), x => x.Entrega).ToList();
            PreencherOcorrenciaTipoId(ref listEntregaDevolucao);
            await GravarElastic(listEntregaDevolucao);
        }

        private async Task GravarElastic(List<EntregaDevolucao> listEntregaDevolucao)
        {
            foreach (var entregaDevolucao in listEntregaDevolucao)
            {
                await _elasticSearchService.CreateDocument(_elasticSearchConfig.TrackingReversaIndex, entregaDevolucao, entregaDevolucao.EntregaId.ToString());
            }
        }

        private void PreencherOcorrenciaTipoId(ref List<EntregaDevolucao> listEntregaDevolucao)
        {
            var listOcorrenciaTipo = _repositoryOcorrenciaTransportador.GetAll(x => x.TransportadorId == CorreiosId && x.Ativo);
            if (listOcorrenciaTipo?.FirstOrDefault() == null)
                return;
            listEntregaDevolucao.ForEach(entregaDevolucao =>
            {
                entregaDevolucao.Ocorrencias?.ForEach(entregaDevolucaoOcorrencia =>
                {
                    var ocorrenciaTipoId = listOcorrenciaTipo.FirstOrDefault(f => f.Sigla == entregaDevolucaoOcorrencia.Sigla);
                    if (ocorrenciaTipoId != null)
                        entregaDevolucaoOcorrencia.AtualizarOcorrenciaTipoId(ocorrenciaTipoId.OcorrenciaTipoId);
                });

                var ultimoOcorrenciaTipoId = entregaDevolucao.Ocorrencias?.OrderByDescending(o => o.DataOcorrencia)?.FirstOrDefault();
                if (ultimoOcorrenciaTipoId?.OcorrenciaTipoId != null)
                    entregaDevolucao.AtualizarUltimaOcorrenciaTipo(ultimoOcorrenciaTipoId.OcorrenciaTipoId.Value);
            });
        }
    }
}
