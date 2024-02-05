using Fretter.Domain.Dto.Webhook.Tracking.Response;
using Fretter.Domain.Dto.Webhook.TrackingIntegracao;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service.Webhook
{
    public interface ITrackingIntegracaoService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        Task<ResponseEspecifico> ProcessaSucesso(List<TrackingIntegracaoEntradaDto> entrada, Guid hashProcessamento, Guid token);

        Task<ResponseEspecifico> ProcessaInsucesso(List<TrackingIntegracaoEntradaDto> entrada, Guid hashProcessamento, Guid token);
    }
}
