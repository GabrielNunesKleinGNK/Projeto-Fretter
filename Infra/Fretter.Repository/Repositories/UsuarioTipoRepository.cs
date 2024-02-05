using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Repository.Repositories
{
    public class UsuarioTipoRepository<TContext> : RepositoryBase<TContext, UsuarioTipo>, IUsuarioTipoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<UsuarioTipo> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<UsuarioTipo>();

        public UsuarioTipoRepository(IUnitOfWork<TContext> context) : base(context) { }
    }
}	
