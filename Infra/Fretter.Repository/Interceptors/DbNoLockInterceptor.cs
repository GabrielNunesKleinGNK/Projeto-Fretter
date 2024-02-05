using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;


namespace Fretter.Repository.Interceptors
{
    public class DbNoLockInterceptor : DbCommandInterceptor
    {
        private static readonly Regex _tableAliasRegex =
         new Regex(@"(?<tableAlias>FROM +(\[.*\]\.)?(\[.*\]) AS (\[.*\])(?! WITH \(NOLOCK\)))",
             RegexOptions.Multiline |
             RegexOptions.IgnoreCase |
             RegexOptions.Compiled);

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            if (!command.CommandText.Contains("WITH (NOLOCK)"))
            {
                command.CommandText =
                    _tableAliasRegex.Replace(command.CommandText,
                    "${tableAlias} WITH (NOLOCK)");
            }

            return base.ReaderExecuting(command, eventData, result);
        }
    }
}
