using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class ReenvioReincidenciaDTO
    {
        public long FaturaConciliacaoId { get; set; }
        public bool Reincidente { get; set; }
    }
}
