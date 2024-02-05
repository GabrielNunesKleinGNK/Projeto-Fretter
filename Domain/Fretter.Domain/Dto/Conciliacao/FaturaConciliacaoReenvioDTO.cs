using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class FaturaConciliacaoReenvioDTO
    {
        public long FaturaConciliacaoId { get; set; }
        public int FaturaId { get; set; }
        public long? ConciliacaoId { get; set; }
        public int UsuarioCadastro { get; set;}
    }
}
