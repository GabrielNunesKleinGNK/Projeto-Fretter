using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
namespace Fretter.Domain.Interfaces.Service
{
    public interface IFaturaStatusService<TContext> : IServiceBase<TContext, FaturaStatus>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
