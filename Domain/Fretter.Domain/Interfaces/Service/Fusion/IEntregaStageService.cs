using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaStageService<TContext> : IServiceBase<TContext, EntregaStage>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> PopulaFilaReclicagemEtiquetas();
    }
}	