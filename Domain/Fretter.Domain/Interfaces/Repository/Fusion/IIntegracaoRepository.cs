using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.EmpresaIntegracao;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IIntegracaoRepository<TContext> : IRepositoryBase<TContext, Integracao> 
        where TContext : IUnitOfWork<TContext>
    {
        List<DeParaEmpresaIntegracao> BuscaCamposDePara(string proc);
    }
}	
	
