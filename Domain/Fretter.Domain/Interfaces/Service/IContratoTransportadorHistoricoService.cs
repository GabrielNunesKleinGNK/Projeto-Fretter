using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IContratoTransportadorHistoricoService<TContext> : IServiceBase<TContext, ContratoTransportadorHistorico>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	