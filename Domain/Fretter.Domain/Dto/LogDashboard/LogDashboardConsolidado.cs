using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.Dashboard
{
    public class LogDashboardConsolidado 
    {
        public string Type { get; set; }
        public string Color { get; set; }
        public long Total { get; set; }
        public decimal Percent { get; set; }
        public DateTime Date { get; set; }
        public List<LogDashboardConsolidado> Subs { get; set; }
        
    }

}
