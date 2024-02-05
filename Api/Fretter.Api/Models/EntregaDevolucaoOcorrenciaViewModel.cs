using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class EntregaDevolucaoOcorrenciaViewModel : IViewModel<EntregaDevolucaoOcorrencia>
    {
	    public int Id { get; set; }
        public int EntregaDevolucao { get;  set; }
        public int? OcorrenciaEmpresaItem { get;  set; }
        public string CodigoIntegracao { get;  set; }
        public string Ocorrencia { get;  set; }
        public DateTime? DataOcorrencia { get; set; }
        public string Observacao { get;  set; }
        public string Sigla { get;  set; }
        public DateTime? Inclusao { get;  set; }


        public EntregaDevolucaoOcorrencia Model()
        {
            return new EntregaDevolucaoOcorrencia(Id, EntregaDevolucao, OcorrenciaEmpresaItem,
                CodigoIntegracao, Ocorrencia,  DataOcorrencia, Sigla, Inclusao);
        }
    }
}      
