using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Dto.Webhook.Entrega.Entrada;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers.Webhook;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service.Webhook
{
    public interface IEntregaService<TContext> : IServiceBase<TContext, Entrega>
        where TContext : IUnitOfWork<TContext>
    {
        Task<RetornoWs<EEntregaErro>> EntregaPadraoAsync(entradaEntrega entrega, string requestString, Guid token, List<Tuple<string, string>> headers);
        Task<RetornoWs<EEntregaErro>> EntregaCustomAsync(EntradaEntregaDirect entrega);
        Task<RetornoWs<EEntregaErro>> EntregaCustomRestoqueAsync(Shipment entrega, string requestString, Guid token, List<Tuple<string, string>> headers);
    }
}	
