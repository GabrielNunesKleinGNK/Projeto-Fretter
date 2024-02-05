using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IContratoTransportadorArquivoTipoService<TContext> : IServiceBase<TContext, ContratoTransportadorArquivoTipo>
        where TContext : IUnitOfWork<TContext>
    {
        bool Save(List<ContratoTransportadorArquivoTipo> model);
    }
}
