using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IIndicadorConciliacaoRepository<TContext> : IRepositoryBase<TContext, IndicadorConciliacao> 
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessaIndicadorConciliacao();
    }
}	
	
