    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class UsuarioTipoApplication<TContext> : ApplicationBase<TContext, UsuarioTipo>, IUsuarioTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public UsuarioTipoApplication(IUnitOfWork<TContext> context, IUsuarioTipoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
