using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Entities.Fusion.SKU;
using System.Linq.Expressions;
using System;
using Fretter.Domain.Interfaces;

namespace Fretter.Repository.Repositories.Fusion
{
    public class GrupoRepository<TContext> : RepositoryBase<TContext, Grupo>, IGrupoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Grupo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Grupo>();
        public readonly IUsuarioHelper _user;

        public GrupoRepository(IUnitOfWork<TContext> context,
                                      IUsuarioHelper user) : base(context) {
            _user = user;
        }

        public List<Grupo> GetGruposPorEmpresa()
        {
            var list = base.GetQueryable()
                        .Where(x => x.EmpresaId == _user.UsuarioLogado.EmpresaId)
                        .ToList();
            return list;
        }
    }
}	
