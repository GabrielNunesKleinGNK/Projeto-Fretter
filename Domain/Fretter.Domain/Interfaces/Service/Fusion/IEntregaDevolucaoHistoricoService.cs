using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoHistoricoService<TContext> : IServiceBase<TContext, EntregaDevolucaoHistorico>
        where TContext : IUnitOfWork<TContext>
    {
        Task<List<EntregaDevolucaoHistorico>> ObterHistoricoEntregaDevolucao(int entregaDevolucaoId);
    }
}	
