using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class EntregaTransporteServicoService<TContext> : ServiceBase<TContext, EntregaTransporteServico>, IEntregaTransporteServicoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaTransporteServicoRepository<TContext> _Repository;

        public EntregaTransporteServicoService(IEntregaTransporteServicoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}	
