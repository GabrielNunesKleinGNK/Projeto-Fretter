using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Entities.Fusion;

namespace Fretter.Repository.Repositories.Fusion
{
    public class TransportadorCnpjRepository<TContext> : ITransportadorCnpjRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {

        protected IUnitOfWork<TContext> UnitOfWork { get; }
        private DbSet<TransportadorCnpj> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<TransportadorCnpj>();

        public TransportadorCnpjRepository(IUnitOfWork<TContext> context)
        {
            UnitOfWork = context;
        }

        public List<TransportadorCnpj> ObterTransportadorCnpj(int tranportadorId)
        {
            if(tranportadorId == default)
                return _dbSet.Where(x => x.Ativo).ToList();
            else
                return _dbSet.Where(x => x.Ativo && x.TransportadorId == tranportadorId).ToList();
        }
    }
}	
