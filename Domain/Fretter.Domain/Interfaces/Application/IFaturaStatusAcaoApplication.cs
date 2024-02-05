using Fretter.Api.Models;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IFaturaStatusAcaoApplication<TContext> : IApplicationBase<TContext, FaturaStatusAcao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
