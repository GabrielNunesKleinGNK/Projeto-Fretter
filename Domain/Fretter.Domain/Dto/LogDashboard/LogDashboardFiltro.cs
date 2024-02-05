using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class LogDashboardFiltro
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }

        public string Application { get; set; }
        public string Process { get; set; }
        public string Method { get; set; }
        public string Level { get; set; }

    }
}
