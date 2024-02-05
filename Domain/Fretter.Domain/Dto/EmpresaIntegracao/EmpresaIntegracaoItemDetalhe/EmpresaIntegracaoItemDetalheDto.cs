using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe
{
    public class EmpresaIntegracaoItemDetalheDto
    {
        public int Id { get; set; }
        public string EntregaId { get; set; }
        public long EntregaOcorrenciaId { get; set; }
        public int OcorrenciaId { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public DateTime DataEnvio { get; set; }
        public bool Sucesso { get; set; }
        public string StatusCode { get; set; }
        public int Total { get; set; }
    }
}
