using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class ContratoTransportadorArquivoTipoViewModel : IViewModel<ContratoTransportadorArquivoTipo>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("empresaId")]
        public int? EmpresaId { get; set; }
        [JsonPropertyName("transportadorId")]
        public int TransportadorId { get; set; }
        [JsonPropertyName("importacaoArquivoTipoItemId")]
        public int ImportacaoArquivoTipoItemId { get; set; }
        [JsonPropertyName("alias")]
        public string Alias { get; set; }
        [JsonPropertyName("dataCadastro")]
        public DateTime DataCadastro { get; set; }
        [NotMapped]
        public ImportacaoArquivoTipoItemViewModel ImportacaoArquivoTipoItem { get; set; }

        public ContratoTransportadorArquivoTipo Model()
        {
            return new ContratoTransportadorArquivoTipo(Id, EmpresaId, TransportadorId, ImportacaoArquivoTipoItemId, Alias);
        }
    }
}
