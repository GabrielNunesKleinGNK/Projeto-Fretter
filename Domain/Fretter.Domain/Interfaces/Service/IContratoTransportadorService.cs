using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IContratoTransportadorService<TContext> : IServiceBase<TContext, ContratoTransportador>
        where TContext : IUnitOfWork<TContext>
    {
        List<MicroServicosDTO> ObterMicroServicoPorEmpresa();
        List<OcorrenciasDTO> ObterOcorrenciasPorEmpresa();
        int ProcessaContratoTransportadorRegra(List<ContratoTransportadorRegra> contratoTransportadorRegra);
        List<ContratoTransportadorRegra> ObterContratoTransportadorRegra(ContratoTransportadorRegraFiltroDTO regra);
    }
}
