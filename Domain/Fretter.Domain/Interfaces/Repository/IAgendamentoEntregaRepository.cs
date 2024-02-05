using Fretter.Domain.Dto.EntregaAgendamento;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IAgendamentoEntregaRepository<TContext> : IRepositoryBase<TContext, AgendamentoEntrega>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
