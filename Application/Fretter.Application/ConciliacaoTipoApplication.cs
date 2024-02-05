
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConciliacaoTipoApplication<TContext> : ApplicationBase<TContext, ConciliacaoTipo>, IConciliacaoTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ConciliacaoTipoApplication(IUnitOfWork<TContext> context, IConciliacaoTipoService<TContext> service)
            : base(context, service)
        {
        }
    }
}
