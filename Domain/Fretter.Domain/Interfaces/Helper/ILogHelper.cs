using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Fretter.Domain.Interfaces.Helper
{
    public interface ILogHelper
    {
        void LogInfo(string processName, string descricao, object objeto = null, DateTime? dataExecucao = null, long tempoExecucao = 0, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void LogWarn(string processName, string descricao, object objeto = null, DateTime? dataExecucao = null, long tempoExecucao = 0, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
        void LogError(string processName, string descricao, object objeto = null, DateTime? dataExecucao = null, long tempoExecucao = 0, Exception exception = null, [CallerMemberName] string memberName = "", [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0);
    }
}
