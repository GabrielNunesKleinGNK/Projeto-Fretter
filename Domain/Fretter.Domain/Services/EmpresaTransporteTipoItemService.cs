using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Linq;

namespace Fretter.Domain.Services
{
    public class EmpresaTransporteTipoItemService<TContext> : ServiceBase<TContext, EmpresaTransporteTipoItem>, IEmpresaTransporteTipoItemService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEmpresaTransporteTipoItemRepository<TContext> _Repository;

        public EmpresaTransporteTipoItemService(IEmpresaTransporteTipoItemRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) 
            : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public IEnumerable<EmpresaTransporteTipoItem> GetEmpresaTransporteItemPorTipo(int transporteTipoId)
        {
            return _Repository.GetAll(t => t.EmpresaTransporteTipoId == transporteTipoId).ToList();
        }

        public override EmpresaTransporteTipoItem Save(EmpresaTransporteTipoItem entidade)
        {
            foreach (var item in entidade.EmpresaTransporteConfiguracoes)
                item.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);

            return base.Save(entidade);
        }

        public override EmpresaTransporteTipoItem Update(EmpresaTransporteTipoItem entidade)
        {
            foreach (var item in entidade.EmpresaTransporteConfiguracoes)
                item.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);

            return base.Update(entidade);
        }
    }
}	
