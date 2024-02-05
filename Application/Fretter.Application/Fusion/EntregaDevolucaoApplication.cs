using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Interfaces.Service.Fusion;

namespace Fretter.Application
{
    public class EntregaDevolucaoApplication<TContext> : ApplicationBase<TContext, EntregaDevolucao>, IEntregaDevolucaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaDevolucaoService<TContext> _service;
        public EntregaDevolucaoApplication(IUnitOfWork<TContext> context,
            IEntregaDevolucaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public async Task<int> ProcessaEntregaDevolucaoIntegracao()
        {
            return await _service.ProcessaEntregaDevolucaoIntegracao();
        }
        public async Task<int> ProcessaEntregaDevolucaoCancelamento()
        {
            return await _service.ProcessaEntregaDevolucaoCancelamento();
        }
        public async Task<int> ProcessaEntregaDevolucaoOcorrencia()
        {
            var listEntregaDevolucao = await _service.ProcessaEntregaDevolucaoOcorrencia();
            return listEntregaDevolucao.Count();
        }

        public List<EntregaDevolucao> GetEntregasDevolucoes(EntregaDevolucaoFiltro filtro)
        {
            return _service.GetEntregasDevolucoes(filtro);
        }
        public byte[] Download(List<EntregaDevolucaoDto> entregas)
        {
            return _service.Download(entregas);
        }

        public void RealizarAcao(EntregaDevolucaoAcaoDto acao)
        {
            _service.RealizarAcao(acao);
            _unitOfWork.Commit();
        }
    }
}
