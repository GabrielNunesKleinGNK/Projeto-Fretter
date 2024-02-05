    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaTransporteServicoApplication<TContext> : ApplicationBase<TContext, EntregaTransporteServico>, IEntregaTransporteServicoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaTransporteServicoApplication(IUnitOfWork<TContext> context, IEntregaTransporteServicoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
