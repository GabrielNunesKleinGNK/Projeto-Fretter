
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Fretter.Domain.Services
{
    public class GrupoService<TContext> : ServiceBase<TContext, Grupo>, IGrupoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IGrupoRepository<TContext> _Repository;

        public GrupoService(IGrupoRepository<TContext> Repository, 
                                    IUnitOfWork<TContext> unitOfWork,
                                    IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public List<Grupo> GetGruposPorEmpresa()
        {
            return _Repository.GetGruposPorEmpresa();
        }
    }
}	
