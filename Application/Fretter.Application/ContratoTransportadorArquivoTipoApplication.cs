
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ContratoTransportadorArquivoTipoApplication<TContext> : ApplicationBase<TContext, ContratoTransportadorArquivoTipo>, IContratoTransportadorArquivoTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        IContratoTransportadorArquivoTipoService<TContext> _service;
        public ContratoTransportadorArquivoTipoApplication(IUnitOfWork<TContext> context, IContratoTransportadorArquivoTipoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public bool Save(List<ContratoTransportadorArquivoTipo> model)
        {
            return _service.Save(model);
        }
    }
}
