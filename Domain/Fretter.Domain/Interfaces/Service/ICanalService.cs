
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ICanalService<TContext> : IServiceBase<TContext, Canal>
        where TContext : IUnitOfWork<TContext>
    {
        List<Dto.Dashboard.Canal> GetCanalPorEmpresa();
    }
}
