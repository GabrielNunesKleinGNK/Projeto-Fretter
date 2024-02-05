using Newtonsoft.Json;
using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class LogCotacaoFreteLista
    {
        public string Id { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string ProcessName { get; set; }
        public int LineNumber { get; set; }
        public string Termino { get; set; }
        public string MessageURI { get; set; }
        public string MessageAction { get; set; }
        public string MessageBody { get; set; }
        public string MessageHeader { get; set; }
        public string CotacaoCepInvalido { get; set; }
        public string CotacaoPeso { get; set; }
        public string Instancia { get; set; }
        public string EmpresaId { get; set; }
        public string MachineName { get; set; }
        public string EnvironmentName { get; set; }
        public string Level { get; set; }
        public string HostName { get; set; }
        public string Message { get; set; }
        public string ObjectJson { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
