using Fretter.Api.Models;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IFaturaStatusAcaoService<TContext> : IServiceBase<TContext, FaturaStatusAcao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
