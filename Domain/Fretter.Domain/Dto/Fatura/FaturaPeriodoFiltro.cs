using System;
namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaPeriodoFiltro
    {
        public FaturaPeriodoFiltro()
        {
        }

        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int? TransportadorId { get; set; }
        public int? StatusConciliacaoId { get; set; }
    }
}
