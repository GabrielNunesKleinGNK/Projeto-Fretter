using System;
using System.Collections.Generic;
using Fretter.Domain.Dto;
using Fretter.Domain.Dto.Menu;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class SisMenuService<TContext> : ServiceBase<TContext, Entities.SisMenu>, ISisMenuService<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        private readonly IRepositoryBase<TContext, Entities.SisMenu> _repository;
        private readonly IUsuarioHelper _user;
        private const string ProcGetSisMenu = "[Fretter].[GetSisMenu]";


        public SisMenuService(IRepositoryBase<TContext, Entities.SisMenu> repository,
                            IUsuarioHelper user,
                            IUnitOfWork<TContext> unitOfWork)
            : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _user = user;
        }

        public List<SisMenu> GetSisMenu()
        {
            try
            {
                var filtro = new FiltroSisMenu(_user.UsuarioLogado.Id, _user.UsuarioLogado.Administrador, true);
                return _repository.ExecuteStoredProcedure<SisMenu, FiltroSisMenu>(filtro, ProcGetSisMenu);
            }
            catch (Exception e)
            {
                var a = e;
            }
            return new List<SisMenu>();
        }
    }
}
