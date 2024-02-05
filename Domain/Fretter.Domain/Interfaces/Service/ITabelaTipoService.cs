using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ITabelaTipoService<TContext> : IServiceBase<TContext, TabelaTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
