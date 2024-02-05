using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository.Webhook
{
    public interface ITrackingIntegracaoRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
