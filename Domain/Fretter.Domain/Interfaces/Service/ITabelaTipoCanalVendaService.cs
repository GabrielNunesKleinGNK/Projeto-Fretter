using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ITabelaTipoCanalVendaService<TContext> : IServiceBase<TContext, TabelaTipoCanalVenda>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
