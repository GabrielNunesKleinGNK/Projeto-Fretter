using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository.Webhook
{
    public interface ITrackingRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {

    }
}
