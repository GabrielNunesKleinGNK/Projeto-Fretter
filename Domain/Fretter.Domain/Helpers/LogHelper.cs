using Fretter.Domain.Interfaces.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Fretter.Domain.Helpers
{
    public class LogHelper : ILogHelper
    {
        private readonly Serilog.ILogger _log;

        public LogHelper(Serilog.ILogger log)
        {
            _log = log;
        }
        public void LogInfo(string processName, string descricao, object objeto = null, DateTime? dataExecucao = null, long tempoExecucao = 0, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (dataExecucao == null)
                dataExecucao = DateTime.Now;
            _log.Information("{ClassName} => {MethodName} => Nome do Processo: {ProcessName} => Descrição: {Descricao} => Linha Numero: {LineNumber} - Termino Execucao: {Termino} - Duracao Execucao: {Duracao} - Objeto: {Objeto}", fileName, memberName, processName, descricao, lineNumber, ((DateTime)dataExecucao).ToString("yyyy-MM-dd HH:mm:ssss"), tempoExecucao, JsonConvert.SerializeObject(objeto, Formatting.Indented, new JsonSerializerSettings{ ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
        public void LogWarn(string processName, string descricao, object objeto = null, DateTime? dataExecucao = null, long tempoExecucao = 0, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (dataExecucao == null)
                dataExecucao = DateTime.Now;
            _log.Warning(exception, "{ClassName} => {MethodName} => Nome do Processo: {ProcessName} => Descrição: {Descricao} => Linha Numero: {LineNumber} - Termino Execucao: {Termino} - Duracao Execucao: {Duracao} - Objeto: {Objeto}", fileName, memberName, processName, descricao, lineNumber, ((DateTime)dataExecucao).ToString("yyyy-MM-dd HH:mm:ssss"), tempoExecucao, JsonConvert.SerializeObject(objeto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
        public void LogError(string processName, string descricao, object objeto = null, DateTime? dataExecucao = null, long tempoExecucao = 0, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0)
        {
            if (dataExecucao == null)
                dataExecucao = DateTime.Now;
            _log.Error(exception, "{ClassName} => {MethodName} => Nome do Processo: {ProcessName} => Descrição: {Descricao} => Linha Numero: {LineNumber} - Termino Execucao: {Termino} - Duracao Execucao: {Duracao} - Objeto: {Objeto}", fileName, memberName, processName, descricao, lineNumber, ((DateTime)dataExecucao).ToString("yyyy-MM-dd HH:mm:ssss"), tempoExecucao, JsonConvert.SerializeObject(objeto, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
        }
    }
}
