using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Fretter.Domain.Services
{
    public class SistemaMenuService<TContext> : ServiceBase<TContext, SistemaMenu>, ISistemaMenuService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ISistemaMenuPermissaoRepository<TContext> _sistemaMenuPermissaoRepository;
        private readonly IUsuarioHelper _user;

        public SistemaMenuService(ISistemaMenuRepository<TContext> repository,
                                  ISistemaMenuPermissaoRepository<TContext> sistemaMenuPermissaoRepository,
                                  IUnitOfWork<TContext> unitOfWork,
                                  IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _sistemaMenuPermissaoRepository = sistemaMenuPermissaoRepository;
            _user = user;
        }


        public IEnumerable<SistemaMenu> GetMenusUsuarioLogado()
        {
            return _sistemaMenuPermissaoRepository.GetMenus(_user.UsuarioLogado.Id);
        }
    }
}
