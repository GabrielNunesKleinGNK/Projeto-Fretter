using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class DashboardTransportadorValor
    {
        public DashboardTransportadorValor()
        {
        }

        public string Transportador { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }

    }
}
