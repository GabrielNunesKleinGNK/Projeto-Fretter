using Fretter.Domain.Entities.Fusion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository.Fusion
{
    public interface ITransportadorCnpjRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        List<TransportadorCnpj> ObterTransportadorCnpj(int transportadorId);
    }
}
