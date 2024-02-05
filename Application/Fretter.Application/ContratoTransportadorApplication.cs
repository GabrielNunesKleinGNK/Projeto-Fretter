
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ContratoTransportadorApplication<TContext> : ApplicationBase<TContext, ContratoTransportador>, IContratoTransportadorApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        IContratoTransportadorService<TContext> _service;
        public ContratoTransportadorApplication(IUnitOfWork<TContext> context, IContratoTransportadorService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public List<MicroServicosDTO> ObterMicroServicoPorEmpresa()
        {
            return _service.ObterMicroServicoPorEmpresa();
        }
        public List<OcorrenciasDTO> ObterOcorrenciasPorEmpresa()
        {
            return _service.ObterOcorrenciasPorEmpresa();
        }

        public int ProcessaContratoTransportadorRegra(List<ContratoTransportadorRegra> contratoTransportadorRegra)
        {
            return _service.ProcessaContratoTransportadorRegra(contratoTransportadorRegra);
        }

        public List<ContratoTransportadorRegra> ObterContratoTransportadorRegra(ContratoTransportadorRegraFiltroDTO model)
        {
            return _service.ObterContratoTransportadorRegra(model);
        }
    }
}
