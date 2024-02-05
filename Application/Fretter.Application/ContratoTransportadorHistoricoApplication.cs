
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ContratoTransportadorHistoricoApplication<TContext> : ApplicationBase<TContext, ContratoTransportadorHistorico>, IContratoTransportadorHistoricoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        IContratoTransportadorHistoricoService<TContext> _service;
        public ContratoTransportadorHistoricoApplication(IUnitOfWork<TContext> context, IContratoTransportadorHistoricoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }
    }
}	
