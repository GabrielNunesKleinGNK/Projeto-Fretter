using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Fretter.Api.Models
{
    public class ContratoTransportadorRegraViewModel : IViewModel<ContratoTransportadorRegra>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("ocorrenciaId")]
        public int? OcorrenciaId { get; set; }
        [NotMapped]
        [JsonPropertyName("Ocorrencia")]
        public virtual string Ocorrencia { get; set; }

        [JsonPropertyName("tipo")]
        public byte TipoCondicao { get; set; }

        [JsonPropertyName("operacao")]
        public bool Operacao { get; set; }

        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        [JsonPropertyName("transportadorId")]
        public int TransportadorId { get; set; }

        [JsonPropertyName("conciliacaoTipoId")]
        public int? ConciliacaoTipoId { get; set; }        

        [JsonPropertyName("ativo")]
        public bool Ativo { get; set; }

        public ContratoTransportadorRegra Model()
        {
            return new ContratoTransportadorRegra(Id, TipoCondicao, Operacao, Valor, TransportadorId, Ativo, OcorrenciaId, Ocorrencia, ConciliacaoTipoId);
        }
    }
}
