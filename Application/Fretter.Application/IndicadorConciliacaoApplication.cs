using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Application
{    
    public class IndicadorConciliacaoApplication<TContext> : ApplicationBase<TContext, IndicadorConciliacao>, IIndicadorConciliacaoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IIndicadorConciliacaoService<TContext> _service;
        public IndicadorConciliacaoApplication(IUnitOfWork<TContext> context, IIndicadorConciliacaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public int ProcessaIndicadorConciliacao()
        {
            return this._service.ProcessaIndicadorConciliacao();
        }
    }
}
