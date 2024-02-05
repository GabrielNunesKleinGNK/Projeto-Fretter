using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface ITabelaTipoCanalVendaRepository<TContext> : IRepositoryBase<TContext, TabelaTipoCanalVenda> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
