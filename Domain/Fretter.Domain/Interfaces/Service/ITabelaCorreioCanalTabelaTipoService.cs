using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ITabelaCorreioCanalTabelaTipoService<TContext> : IServiceBase<TContext, TabelaCorreioCanalTabelaTipo>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
