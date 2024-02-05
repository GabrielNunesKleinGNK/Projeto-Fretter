using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IConciliacaoTransportadorApplication<TContext> : IApplicationBase<TContext, EntregaConciliacao>
        where TContext : IUnitOfWork<TContext>
    {       
        Task<int> ProcessaConciliacaoTransportador();
    }
}
	