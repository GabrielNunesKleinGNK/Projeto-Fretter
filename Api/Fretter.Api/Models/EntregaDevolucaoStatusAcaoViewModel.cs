using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class EntregaDevolucaoStatusAcaoViewModel : IViewModel<EntregaDevolucaoStatusAcao>
    {
        public int Id { get; set; }
        public int EntregaTransporteTipoId { get; set; }
        public int EntregaDevolucaoStatusId { get; set; }
        public int EntregaDevolucaoAcaoId { get; set; }
        public int EntregaDevolucaoResultadoId { get; set; }
        public bool? InformaMotivo { get; set; }
        public bool? Visivel { get; set; }

        public EntregaDevolucaoAcaoViewModel Acao { get; set; }

        public EntregaDevolucaoStatusAcao Model()
        {
            return new EntregaDevolucaoStatusAcao(Id, EntregaTransporteTipoId, EntregaDevolucaoStatusId, EntregaDevolucaoAcaoId, EntregaDevolucaoResultadoId, InformaMotivo, Visivel);
        }
    }
}
