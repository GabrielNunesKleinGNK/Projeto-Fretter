using System;
namespace Fretter.Domain.Dto.EntregaDevolucao
{
    public class OcorrenciaEnvioDto
    {
        public OcorrenciaEnvioDto()
        {
        }

        public int Cd_Id { get; set; }
        public int Id_Entrega { get; set; }
        public int? Id_Ocorrencia { get; set; }
        public string Ds_Ocorrencia { get; set; }
        public DateTime Dt_Ocorrencia { get; set; }
        public DateTime? Dt_Inclusao { get; set; }
        public DateTime? Dt_Original { get; set; }
        public bool? Flg_OcorrenciaValidada { get; set; }
        public string Ds_ArquivoImportacao { get; set; }
        public string Ds_UsuarioInclusao { get; set; }
        public bool Flg_OcorrenciaValidadaDe { get; set; }
        public int? Id_OcorrenciaDe { get; set; }
        public int? Id_Transportador { get; set; }
        public int? Id_OrigemImportacao { get; set; }
        public DateTime? Dt_InclusaoAnterior { get; set; }
        public DateTime? Dt_OcorrenciaAnterior { get; set; }
    }
}
