using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository.Fusion
{
    public interface ITransportadorRepository<TContext> : IRepositoryBase<TContext, Transportador>
        where TContext : IUnitOfWork<TContext>
    {
        List<Transportador> ObterTransportadores();
    }
}
