using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IFaturaHistoricoService<TContext> : IServiceBase<TContext, FaturaHistorico>
        where TContext : IUnitOfWork<TContext>
    {
        Task<List<FaturaHistorico>> GetHistoricoDeFaturasPorEmpresa(int faturaId);
    }
}
