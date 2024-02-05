using System;
namespace Fretter.Domain.Dto.EntregaDevolucao
{
    public class OcorrenciaEmbarcadorDto
    {
        public OcorrenciaEmbarcadorDto()
        {
        }

        public int Id { get; set; }
        public int OcorrenciaEmpresaId { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public bool? Finalizadora { get; set; }
    }
}
