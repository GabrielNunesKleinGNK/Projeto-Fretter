using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class DashboardEntregasGrafico
    {
        public DashboardEntregasGrafico()
        {
        }

        public DateTime Data { get; set; }
        public string Status { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }

    }
}
