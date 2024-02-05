using Fretter.Domain.Helpers.Attributes;
using System;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class VtexModel
    {
        public int Cd_Id { get; set; }
        public int Id_Tabela { get; set; }
        public int Id_Empresa { get; set; }
        public int Id_Transportador { get; set; }
        public bool Flg_Ativo { get; set; }


        public string Cd_CepInicio { get; set; }
        public string Cd_CepFim { get; set; }
        public string Ds_NomeGeolicazacao { get; set; }

        public decimal Nr_PesoInicial { get; set; }
        public decimal Nr_PesoFim { get; set; }

        public decimal Nr_CustoAbsoluto { get; set; }
        public decimal Nr_ProcentagemDePrecoAdicional { get; set; }
        public decimal Nr_PrecoPesoExtra { get; set; }
        public decimal Nr_VolumeMaximo { get; set; }
        public decimal Nr_TaxaAdicionalDeSeguro { get; set; }

        public string Ds_PrazoEntrega { get; set; }
        public string Ds_Pais { get; set; }
        [DataTableColumnIgnore]
        public DateTime Dt_Inclusao { get; set; }
    }
}
