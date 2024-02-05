using Fretter.Domain.Dto.EntregaAgendamento;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IAgendamentoEntregaApplication<TContext> : IApplicationBase<TContext, AgendamentoEntrega>
        where TContext : IUnitOfWork<TContext>
    {
       SkuDetalhesDto ObterProdutoPorCodigo(string codigo);
       List<DisponibilidadeDto> ObterDisponibilidade(AgendamentoDisponibilidadeFiltro filtro);
       Task<ConsultaCepDto> ObterCep(string cep);
    }
}
