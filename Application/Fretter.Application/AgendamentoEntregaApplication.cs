using Fretter.Domain.Dto.EntregaAgendamento;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class AgendamentoEntregaApplication<TContext> : ApplicationBase<TContext, AgendamentoEntrega>, IAgendamentoEntregaApplication<TContext>
    where TContext : IUnitOfWork<TContext>
    {
        private readonly IAgendamentoEntregaService<TContext> _service;

        public AgendamentoEntregaApplication(IUnitOfWork<TContext> context, IAgendamentoEntregaService<TContext> service) : base(context, service)
        {
            _service = service;
        }

        public SkuDetalhesDto ObterProdutoPorCodigo(string codigo)
        {
            var result = _service.ObterProdutoPorCodigo(codigo);
            return result;
        }

        public List<DisponibilidadeDto> ObterDisponibilidade(AgendamentoDisponibilidadeFiltro filtro)
        {
            var result = _service.ObterDisponibilidade(filtro);
            return result;
        }

        public async Task<ConsultaCepDto> ObterCep(string cep)
        {
            var result =  await _service.ObterCep(cep);
            return result;
        }
    }
}
