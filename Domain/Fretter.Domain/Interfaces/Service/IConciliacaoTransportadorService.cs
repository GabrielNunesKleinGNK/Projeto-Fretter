
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IConciliacaoTransportadorService<TContext> : IServiceBase<TContext, EntregaConciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessaConciliacaoTransportador();
    }
}
