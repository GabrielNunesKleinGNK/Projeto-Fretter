using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.EntregaDevolucao
{
    public class EntregaDevolucaoAcaoDto
    {
        public int EntregaDevolucaoId { get;  set; }
        public int AcaoId { get; set; }
        public string Motivo { get; set; }
    }
}
