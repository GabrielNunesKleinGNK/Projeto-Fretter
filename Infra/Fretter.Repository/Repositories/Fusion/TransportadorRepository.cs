using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Entities.Fusion;

namespace Fretter.Repository.Repositories.Fusion
{
    public class TransportadorRepository<TContext> :  RepositoryBase<TContext, Transportador>, ITransportadorRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {

        protected IUnitOfWork<TContext> UnitOfWork { get; }
        private DbSet<Transportador> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Transportador>();

        public TransportadorRepository(IUnitOfWork<TContext> context) : base(context)
        {
            UnitOfWork = context;
        }

        public List<Transportador> ObterTransportadores()
        {
           return _dbSet.Where(x => x.Ativo).ToList();
        }
    }
}	
