using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Application
{
    public class AgendamentoRegraApplication<TContext> : ApplicationBase<TContext, AgendamentoRegra>, IAgendamentoRegraApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private new readonly IAgendamentoRegraService<TContext> _service;

        public AgendamentoRegraApplication(IUnitOfWork<TContext> context, IAgendamentoRegraService<TContext> service) : base(context, service) 
        {
            _service = service;
        }

        public List<RegraTipoOperador> ObtemRegraTiposOperadores()
        {
            return _service.ObtemRegraTiposOperadores();
        }

        public List<RegraTipoItem> ObtemRegraTipoItem()
        {
            return _service.ObtemRegraTipoItem();
        }
        public List<RegraTipo> ObtemRegraTipo()
        {
            return _service.ObtemRegraTipo();
        }

        public int GravaRegra(AgendamentoRegra entidade)
        {
            return _service.GravaRegra(entidade);
        }

        public int InativarRegra(int id)
        {
            return _service.InativarRegra(id);
        }
        
    }
}
