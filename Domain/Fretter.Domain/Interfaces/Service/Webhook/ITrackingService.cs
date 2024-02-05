
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Dto.Webhook.Tracking.Entrada;
using Fretter.Domain.Dto.Webhook.Tracking.Response;
using Fretter.Domain.Helpers.Webhook;
using Fretter.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service.Webhook
{
    public interface ITrackingService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        Task<RetornoWs<ECriticaArquivo>> TrackingPadraoAsync(List<EntradaTracking> listaOcorrencias, string requestString, Guid token);
        Task<ResponseEspecifico> TrackingEspecificoAsync(EntradaTrackingEspecifico entradaEspecifico, string requestString, Guid token);
        Task<ResponseSequoia> TrackingSequoiaAsync(List<EntradaTrackingSequoia> listaOcorrencias, string requestString, Guid token);
        Task<ResponseEspecifico> TrackingFreteRapidoAsync(EntradaTrackingFreteRapido tracking, string requestString, Guid token);
        Task<ResponseEuEntrego> TrackingEuEntregoAsync(EntradaTrackingEuEntrego tracking, string requestString, Guid token);
        Task<ResponseLoggi> TrackingLoggiAsync(EntradaTrackingLoggi tracking, string requestString, Guid token);
        Task<ResponseAllPost> TrackingAllPostAsync(EntradaTrackingAllPost tracking, string requestString, Guid token);
        Task<string> WebhookSyncAsync(EntradaSync tracking, string requestString, Guid token);
    }
}
