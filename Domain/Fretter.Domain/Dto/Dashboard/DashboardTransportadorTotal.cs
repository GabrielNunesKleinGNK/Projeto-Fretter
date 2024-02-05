using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class DashboardTransportadorTotal
    {
        public DashboardTransportadorTotal()
        {
        }

        public string Transportador { get; set; }
        public int QtdEntrega { get; set; }
        public int QtdCte { get; set; }
        public int QtdSucesso { get; set; }
        public int QtdErro { get; set; }
        public int QtdDivergencia { get; set; }
        public int QtdDivergenciaPeso { get; set; }
        public int QtdDivergenciaTarifa { get; set; }

    }
}
