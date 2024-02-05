using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.Fatura;
using System.Threading.Tasks;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IArquivoCobrancaRepository<TContext> : IRepositoryBase<TContext, ArquivoCobranca> 
        where TContext : IUnitOfWork<TContext>
    {
        List<ArquivoCobranca> ObterArquivoCobrancas (int faturaId);
    }
}	
	
