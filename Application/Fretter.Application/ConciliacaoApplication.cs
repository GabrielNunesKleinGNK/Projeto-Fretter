using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ConciliacaoApplication<TContext> : ApplicationBase<TContext, Conciliacao>, IConciliacaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        readonly IConciliacaoService<TContext> _service;
        public ConciliacaoApplication(IUnitOfWork<TContext> context, IConciliacaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public List<RelatorioDetalhadoDTO> ObterRelatorioDetalhado(RelatorioDetalhadoFiltroDTO filtro)
        {
            return _service.ObterRelatorioDetalhado(filtro);
        }
        public List<RelatorioDetalhadoArquivoDTO> ObterRelatorioDetalhadoArquivo(RelatorioDetalhadoFiltroDTO filtro)
        {
            return _service.ObterRelatorioDetalhadoArquivo(filtro);
        }
        public List<ConciliacaoStatus> ObterStatus()
        {
            return _service.ObterStatus();
        }
        public void ProcessaConciliacao() => this._service.ProcessaConciliacao();        
        public void ProcessaFatura() => this._service.ProcessaFatura();
        public Task<int> ProcessaConciliacaoControle() => this._service.ProcessaConciliacaoControle();
        public Task<int> ProcessaConciliacaoRecotacao() => this._service.ProcessaConciliacaoRecotacao();
        public string ObterArquivoPorConciliacao(int conciliacaoId) => this._service.ObterArquivoPorConciliacao(conciliacaoId);
        public void EnviarParaRecalculoFrete(List<int> listaConciliacoes) => _service.EnviarParaRecalculoFrete(listaConciliacoes);
        public void EnviarParaRecalculoFreteMassivo(RelatorioDetalhadoFiltroDTO filtro) => _service.EnviarParaRecalculoFreteMassivo(filtro);
    }
}
