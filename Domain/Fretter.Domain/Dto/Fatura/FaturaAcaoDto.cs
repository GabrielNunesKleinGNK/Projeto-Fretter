using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaAcaoDto
    {
        public int FaturaId { get; set; }
        public int AcaoId { get; set; }
        public string Motivo { get; set; }
    }
}
