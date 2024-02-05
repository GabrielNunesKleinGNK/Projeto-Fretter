using Fretter.Domain.Dto.LogElasticSearch;
using System;

namespace Fretter.Domain.Dto.Dashboard
{
    public class LogDashboardLista
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ProcessName { get; set; }
        public int LineNumber { get; set; }
        public string Termino { get; set; }
        public int Duracao { get; set; }
        public string ApplicationName { get; set; }
        public string ExceptionDetail { get; set; }
        public string MachineName { get; set; }
        public string EnvironmentName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string ObjectJson { get; set; }

    }

}
