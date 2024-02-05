using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IAgendamentoExpedicaoService<TContext> : IServiceBase<TContext, AgendamentoExpedicao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
