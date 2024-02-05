using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IAgendamentoRegraApplication<TContext> : IApplicationBase<TContext, AgendamentoRegra>
        where TContext : IUnitOfWork<TContext>
    {
        List<RegraTipoOperador> ObtemRegraTiposOperadores();
        List<RegraTipoItem> ObtemRegraTipoItem();
        List<RegraTipo> ObtemRegraTipo();
        int GravaRegra(AgendamentoRegra entidade);
        int InativarRegra(int id);
    }
}
