using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Fretter.Domain.Services
{
    public class EmpresaTransporteConfiguracaoItemService<TContext> : ServiceBase<TContext, EmpresaTransporteConfiguracaoItem>, IEmpresaTransporteConfiguracaoItemService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaTransporteConfiguracaoItemRepository<TContext> _Repository;

        public EmpresaTransporteConfiguracaoItemService(IEmpresaTransporteConfiguracaoItemRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user)
            : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public void SaveRange(List<EmpresaTransporteConfiguracaoItem> items)
        {
            _repository.SaveRange(items); 
        }
    }
}
