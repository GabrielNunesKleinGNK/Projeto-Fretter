using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class FaturaStatusApplication<TContext> : ApplicationBase<TContext, FaturaStatus>, IFaturaStatusApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public FaturaStatusApplication(IUnitOfWork<TContext> context, IFaturaStatusService<TContext> service)
            : base(context, service)
        {
        }
    }
}
