using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.TabelaArquivo
{
    public class RegiaoCEPCapacidadeModel
    {
        public int Cd_Id { get; set; }
        public int Id_RegiaoCEP { get; set;}
        public int Id_Periodo { get; set; }
        public byte Nr_Dia { get; set; }
        public int Vl_Quantidade { get; set; }
        public int Vl_QuantidadeDisponivel { get; set; }
        public decimal Nr_Valor { get; set; }
    }
}
