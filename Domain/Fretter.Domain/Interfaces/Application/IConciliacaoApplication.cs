using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IConciliacaoApplication<TContext> : IApplicationBase<TContext, Conciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        List<RelatorioDetalhadoDTO> ObterRelatorioDetalhado(RelatorioDetalhadoFiltroDTO filtro);
        List<RelatorioDetalhadoArquivoDTO> ObterRelatorioDetalhadoArquivo(RelatorioDetalhadoFiltroDTO filtro);
        List<ConciliacaoStatus> ObterStatus();
        void ProcessaConciliacao();
        void ProcessaFatura();
        Task<int> ProcessaConciliacaoControle();
        Task<int> ProcessaConciliacaoRecotacao();
        string ObterArquivoPorConciliacao(int conciliacaoId);
        void EnviarParaRecalculoFrete(List<int> listaConciliacoes);
        void EnviarParaRecalculoFreteMassivo(RelatorioDetalhadoFiltroDTO filtro);
    }
}
	