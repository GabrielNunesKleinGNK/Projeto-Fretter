using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Application
{
    public class AgendamentoExpedicaoApplication<TContext> : ApplicationBase<TContext, AgendamentoExpedicao>, IAgendamentoExpedicaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public AgendamentoExpedicaoApplication(IUnitOfWork<TContext> context, IAgendamentoExpedicaoService<TContext> service) : base(context, service)
        {
        }
    }
}
