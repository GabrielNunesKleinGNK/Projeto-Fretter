using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ICnpjDetalheService<TContext> : IServiceBase<TContext, CnpjDetalhe>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
