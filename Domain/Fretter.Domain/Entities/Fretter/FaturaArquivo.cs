using Fretter.Domain.Dto.Fusion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class FaturaArquivo : EntityBase
    {
        public int EmpresaId { get; set; }
        public string NomeArquivo { get; set; }
        public string UrlBlobStorage { get; set; }
        public int QtdeRegistros { get; set; }
        public int QtdeCriticas { get; set; }
        public decimal ValorTotal { get; set; }
        public int TransportadorId { get; set; }
        public bool Faturado { get; set; }
    }
}
