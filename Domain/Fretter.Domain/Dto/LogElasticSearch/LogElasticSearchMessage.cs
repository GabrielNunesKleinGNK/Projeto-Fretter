using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Fretter.Domain.Dto.LogElasticSearch
{
    public class LogElasticSearchMessage
    {
        [JsonProperty("@timestamp")]
        public DateTime timestamp { get; set; }
        public string level { get; set; }
        public string messageTemplate { get; set; }
        public string message { get; set; }
        public List<Exception> exceptions { get; set; }

        public Fields fields { get; set; }
    }
   
    public class Fields
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ProcessName { get; set; }
        public int LineNumber { get; set; }
        public string Termino { get; set; }
        public int Duracao { get; set; }
        public string Environment { get; set; }
        public string ApplicationName { get; set; }
        public ExceptionDetail ExceptionDetail { get; set; }
        public string MachineName { get; set; }
        public string EnvironmentName { get; set; }

        [JsonProperty("@timestamp")]
        public List<DateTime> Timestamp { get; set; }
    }

    public class Exception
    {
        public int Depth { get; set; }
        public string ClassName { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTraceString { get; set; }
        public object RemoteStackTraceString { get; set; }
        public int RemoteStackIndex { get; set; }
        public int HResult { get; set; }
        public string HelpURL { get; set; }
    }

    public class ExceptionDetail
    {
        public string Type { get; set; }
        public int HResult { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string TargetSite { get; set; }
    }
}
