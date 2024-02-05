using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IAgendamentoExpedicaoApplication<TContext> : IApplicationBase<TContext, AgendamentoExpedicao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
