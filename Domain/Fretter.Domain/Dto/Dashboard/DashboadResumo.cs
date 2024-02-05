using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class DashboardResumo
    {
        public DashboardResumo()
        {
        }

        public int EntregasConciliadas { get; set; }
        public int EntregasComProblema { get; set; }
        public int EntregasComDivergencia { get; set; }
        public int EntregasSemCte { get; set; }
        public int CteSemEntregas { get; set; }
        public int TotalGeral { get { return EntregasConciliadas + EntregasComDivergencia + EntregasSemCte; } }

    }
}
