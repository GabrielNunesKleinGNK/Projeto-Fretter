
using System.Collections.Generic;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class SistemaMenuApplication<TContext> : ApplicationBase<TContext, SistemaMenu>, ISistemaMenuApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private new readonly ISistemaMenuService<TContext> _service;

        public SistemaMenuApplication(IUnitOfWork<TContext> context, ISistemaMenuService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }


        public IEnumerable<SistemaMenu> GetMenusUsuarioLogado()
        {
            return _service.GetMenusUsuarioLogado();
        }
    }
}	
