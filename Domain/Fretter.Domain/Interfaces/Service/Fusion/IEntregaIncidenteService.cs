using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaIncidenteService<TContext> : IServiceBase<TContext, EntregaStage>
        where TContext : IUnitOfWork<TContext>
    {
        Task ProcessarFilaIncidentes();
    }
}
