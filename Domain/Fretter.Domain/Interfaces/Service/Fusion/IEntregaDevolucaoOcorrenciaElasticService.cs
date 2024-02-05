using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service.Fusion
{
    public interface IEntregaDevolucaoOcorrenciaElasticService<TContext> : IServiceBase<TContext, EntregaDevolucao>
        where TContext : IUnitOfWork<TContext>
    {
        Task GravarTrackingReversa(List<DevolucaoCorreioOcorrencia> listDevolucaoCorreioOcorrencia);
    }
}
