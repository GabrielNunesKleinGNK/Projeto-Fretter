using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaDevolucaoService<TContext> : IServiceBase<TContext, EntregaDevolucao>
        where TContext : IUnitOfWork<TContext>
    {
        Task<int> ProcessaEntregaDevolucaoIntegracao();
        Task<int> ProcessaEntregaDevolucaoCancelamento();
        Task<List<DevolucaoCorreioOcorrencia>> ProcessaEntregaDevolucaoOcorrencia();
        List<EntregaDevolucao> GetEntregasDevolucoes(EntregaDevolucaoFiltro filtro);
        byte[] Download(List<EntregaDevolucaoDto> entregas);
        void RealizarAcao(EntregaDevolucaoAcaoDto acao);
    }
}
