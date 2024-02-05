using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class EntregaOcorrenciaViewModel : IViewModel<EntregaOcorrencia>
    {
        public int Id { get; set; }
        public int EntregaId { get;  set; }
        public int? OcorrenciaId { get;  set; }
        public string Ocorrencia { get;  set; }
        public DateTime? DataOcorrencia { get;  set; }
        public DateTime? DataInclusao { get;  set; }
        public DateTime? DataOriginal { get;  set; }
        public bool? OcorrenciaValidada { get;  set; }
        public Guid? EdiId { get;  set; }
        public string ArquivoImportacao { get;  set; }
        public string UsuarioInclusao { get;  set; }
        public bool? OcorrenciaValidadaDe { get;  set; }
        public int? OcorrenciaDeId { get;  set; }
        public int? TransportadorId { get;  set; }
        public int? OrigemImportacaoId { get;  set; }
        public DateTime? DataInclusaoAnterior { get;  set; }
        public DateTime? DataOcorrenciaAnterior { get;  set; }
        public string CodigoBaseTransportador { get;  set; }
        public string Complementar { get;  set; }
        public int? ArquivoId { get;  set; }
        public DateTime? DataPostagemAtualiza { get;  set; }
        public string Sigla { get;  set; }
        public bool? Finalizar { get;  set; }
        public string Dominio { get;  set; }
        public string Cidade { get;  set; }
        public string Uf { get;  set; }
        public string Latitude { get;  set; }
        public string Longitude { get;  set; }
        public EntregaOcorrencia Model()
        {
            return new EntregaOcorrencia(Id, EntregaId, OcorrenciaId, Ocorrencia, (DataOcorrencia ?? DateTime.Now), TransportadorId, Sigla, Finalizar, Uf, Cidade, Dominio, Complementar);

        }
    }
}
