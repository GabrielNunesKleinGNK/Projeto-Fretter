using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;
using System.Data;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEmpresaIntegracaoItemRepository<TContext> : IRepositoryBase<TContext, EmpresaIntegracaoItem> 
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
	