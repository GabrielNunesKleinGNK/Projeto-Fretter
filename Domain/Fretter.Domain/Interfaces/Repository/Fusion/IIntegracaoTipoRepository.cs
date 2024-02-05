using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.EmpresaIntegracao;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IIntegracaoTipoRepository<TContext> : IRepositoryBase<TContext, IntegracaoTipo> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	
