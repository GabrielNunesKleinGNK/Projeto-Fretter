using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class DashboardTransportadorQuantidade
    {
        public DashboardTransportadorQuantidade()
        {
        }

        public string Transportador { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }

    }
}
