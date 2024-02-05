    
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Fretter.Application
{
    public class GrupoApplication<TContext> : ApplicationBase<TContext, Grupo>, IGrupoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        IGrupoService<TContext> _service;
        public GrupoApplication(IUnitOfWork<TContext> context, IGrupoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public List<Grupo> GetGruposPorEmpresa()
        {
            return _service.GetGruposPorEmpresa();
        }
    }
}	
