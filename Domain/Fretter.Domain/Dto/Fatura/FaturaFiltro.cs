using System;
namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaFiltro
    {
        public FaturaFiltro()
        {
        }

        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int? TransportadorId { get; set; }
        public int? CanalId { get; set; }
        public int? FaturaStatusId { get; set; }
        public int? EmpresaId { get; set; }
    }
}
