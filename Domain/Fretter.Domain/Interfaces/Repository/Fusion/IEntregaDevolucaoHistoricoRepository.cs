using  Fretter.Domain.Entities;
using System.Collections.Generic;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaDevolucaoHistoricoRepository<TContext> : IRepositoryBase<TContext, EntregaDevolucaoHistorico> 
        where TContext : IUnitOfWork<TContext>
    {
        List<EntregaDevolucaoHistorico> ObterHistoricoEntregaDevolucao(int entregaDevolucaoId);
    }
}	
	
