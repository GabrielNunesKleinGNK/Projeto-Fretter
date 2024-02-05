using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IFaturaStatusApplication<TContext> : IApplicationBase<TContext, FaturaStatus>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
