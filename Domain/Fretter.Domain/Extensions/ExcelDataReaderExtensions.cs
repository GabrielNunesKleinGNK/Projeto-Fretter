using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Extensions
{
    public static class ExcelDataReaderExtension
    {
        /// <summary>
        /// Avança no Reader a quantidade de linhas especificadas
        /// </summary>
        public static IExcelDataReader AdvanceLines(this IExcelDataReader reader, int line)
        {
            int controlLine = 0;

            do
            {
                reader.Read();
                controlLine++;
            } while (controlLine < line);

            return reader;
        }
    }
}
