using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IFaturaConciliacaoRepository<TContext> : IRepositoryBase<TContext, FaturaConciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        List<FaturaConciliacaoIntegracaoDTO> GetAllFaturaConciliacaoIntegracao(int faturaId, int empresaId);

        FaturaConciliacaoIntegracaoDTO ExisteAlgumaReincidencia(DataTable faturaConciliacaoIds);

        void InserirTabelaReenvio(DataTable conciliacoesReenvio);
        JsonIntegracaoFaturaConciliacaoDTO GetJsonIntegracaoFaturaConciliacao(int empresaIntegracaoItemDetalheId);
    }
}
