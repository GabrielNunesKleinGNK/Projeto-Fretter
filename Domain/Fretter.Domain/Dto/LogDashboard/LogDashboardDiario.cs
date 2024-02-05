using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class LogDashboardDiario
    {
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public long Quantidade { get; set; }
        public double Valor { get; set; }

    }
}
