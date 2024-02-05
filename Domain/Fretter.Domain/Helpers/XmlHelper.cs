using Fretter.Domain.Extensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Fretter.Domain.Helpers
{
    public static class XmlHelper
    {
        public static T Deserialize<T>(string xmlString)
        {
            if (xmlString == null) return default;
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        public static string ObterXmlElement(string classe, string propriedade)
        {
            Type type = Type.GetType(classe);

            PropertyInfo propertyInfo = type?.GetProperty(propriedade);
            XmlElementAttribute xmlElementAttr = propertyInfo?.GetCustomAttribute<XmlElementAttribute>();

            if (xmlElementAttr?.ElementName != null)
                return xmlElementAttr.ElementName;

            XmlAttributeAttribute xmlAnyAttr = propertyInfo?.GetCustomAttribute<XmlAttributeAttribute>();

            return xmlAnyAttr?.AttributeName ?? string.Empty;
        }

        #region EPPlus

        public static bool TryFill(this ExcelRange cellRange, Action<bool> a, ListImportacaoExcelMsg ret, bool? vazio = true, bool erro = true, string fileName = null)
        {
            if (!cellRange.TryFill(a, vazio))
            {
                if (erro)
                    ret.Add($"Erro na célula: {cellRange?.FullAddress} não foi possivel interpreta-lo com booleano {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            return true;
        }

        public static bool TryFill(this ExcelRange cellRange, Action<byte> a, ListImportacaoExcelMsg ret, byte? vazio = default(byte), bool erro = true, string fileName = null)
        {
            if (!byte.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: {cellRange?.FullAddress} não foi possivel interpreta-lo como inteiro {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static void Fill(this ExcelRange cellRange, Action<int?> a)
        {
            if (int.TryParse(cellRange?.Text?.Trim(), out var v))
                a.Invoke(v);
        }

        public static bool TryFill(this ExcelRange cellRange, Action<int> a, ListImportacaoExcelMsg ret, int? vazio = default(int), bool erro = true, string fileName = null)
        {
            if (!int.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: {cellRange?.FullAddress} não foi possivel interpreta-lo como inteiro {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static bool TryFill(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, decimal? vazio = default(decimal), bool erro = true, string fileName = null)
        {
            if (!decimal.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: {cellRange?.FullAddress} não foi possivel interpreta-lo como numérico {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static bool TryFill(this
            ExcelRange cellRange,
            Action<string> a,
            ListImportacaoExcelMsg ret,
            string[] remove = null,
            int? minLen = null,
            int? maxLen = null,
            bool obrigatoria = true,
            string fileName = null)
        {
            var v = cellRange?.Text?.Trim();
            if (remove != null)
                foreach (var re in remove)
                    v = v?.Replace(re, "");
            if (obrigatoria && string.IsNullOrEmpty(v))
            {
                ret.Add($"Erro na célula: obrigatória {cellRange?.FullAddress} esta vazia. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            if (minLen.HasValue && v.Length < minLen.Value)
            {
                ret.Add($"Erro na célula: {cellRange?.FullAddress} comprimento menor que o mínomo de {minLen}. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            if (maxLen.HasValue && v.Length > maxLen.Value)
            {
                ret.Add($"Erro na célula: {cellRange?.FullAddress} comprimento maior que o máximo de {maxLen}. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }


        private static bool TryFill(this ExcelRange cellRange, Action<bool> a, bool? vazio = true)
        {
            var t = cellRange?.Text?.ToLower()?.Trim();
            if (bool.TryParse(t, out var ret))
            {
                a.Invoke(ret);
                return true;// True, False, true, false
            }
            if (string.IsNullOrEmpty(t) && vazio.HasValue)//Vazio é verdadeiro
            {
                a.Invoke(vazio.Value);
                return true;
            }
            if (t.StartsWith("s") || t.StartsWith("v"))
            {
                a.Invoke(true);
                return true;
            }
            if (t.StartsWith("n") || t.StartsWith("f"))
            {
                a.Invoke(false);
                return true;
            }
            return false;
        }

        #endregion

        #region EPPlus 2
        public static bool TryFill(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, bool erro = true)
        {
            if (!decimal.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo com numérico", true, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }
        public static bool TryFill2(this ExcelRange cellRange, Action<bool> a, ListImportacaoExcelMsg ret, bool? vazio = true, bool erro = true)
        {
            if (!cellRange.TryFill2(a, vazio))
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo com booleano", true, cellRange);
                return false;
            }
            return true;
        }

        public static bool TryFill2(this ExcelRange cellRange, Action<byte> a, ListImportacaoExcelMsg ret, byte? vazio = default(byte), bool erro = true)
        {
            if (!byte.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo como inteiro", true, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static void Fill2(this ExcelRange cellRange, Action<int?> a)
        {
            if (int.TryParse(cellRange?.Text?.Trim(), out var v))
                a.Invoke(v);
        }

        public static bool TryFill2(this ExcelRange cellRange, Action<int> a, ListImportacaoExcelMsg ret, int? vazio = default(int), bool erro = true)
        {
            if (!int.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo como inteir", true, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static bool TryFill2(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, decimal? vazio = default(decimal), bool erro = true)
        {
            if (!decimal.TryParse(cellRange?.Text?.Trim(), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo como numérico", true, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static bool TryFillDecimal(this ExcelRange cellRange, Action<decimal> a, ListImportacaoExcelMsg ret, decimal? vazio = default(decimal), bool erro = true)
        {
            if (!decimal.TryParse(cellRange?.Text?.Trim().Replace(".", ","), out var v))
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo como numérico", true, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }

        public static bool TryFill2(this
            ExcelRange cellRange,
            Action<string> a,
            ListImportacaoExcelMsg ret,
            string[] remove = null,
            int? minLen = null,
            int? maxLen = null,
            bool obrigatoria = true,
            string fileName = null)
        {
            var v = cellRange?.Text?.Trim();
            if (remove != null)
                foreach (var re in remove)
                    v = v?.Replace(re, "");
            if (obrigatoria && string.IsNullOrEmpty(v))
            {
                ret.Add($"Erro na célula: obrigatória esta vazia.", true, cellRange);
                return false;
            }
            if (minLen.HasValue && v.Length < minLen.Value)
            {
                ret.Add($"Erro na célula: comprimento menor que o mínomo de {minLen}.", true, cellRange);
                return false;
            }
            if (maxLen.HasValue && v.Length > maxLen.Value)
            {
                ret.Add($"Erro na célula: comprimento maior que o máximo de {maxLen}.", true, cellRange);
                return false;
            }
            a.Invoke(v);
            return true;
        }


        private static bool TryFill2(this ExcelRange cellRange, Action<bool> a, bool? vazio = true)
        {
            var t = cellRange?.Text?.ToLower()?.Trim();
            if (bool.TryParse(t, out var ret))
            {
                a.Invoke(ret);
                return true;// True, False, true, false
            }
            if (string.IsNullOrEmpty(t) && vazio.HasValue)//Vazio é verdadeiro
            {
                a.Invoke(vazio.Value);
                return true;
            }
            if (t.StartsWith("s") || t.StartsWith("v"))
            {
                a.Invoke(true);
                return true;
            }
            if (t.StartsWith("n") || t.StartsWith("f"))
            {
                a.Invoke(false);
                return true;
            }
            return false;
        }

        #endregion

        #region ExcelDataReader
        public static decimal ExcelDataToDecimal(this string valor, int linha, int coluna, ListImportacaoExcelMsg ret, bool erro = true)
        {
            try
            {
                if (CultureInfo.CurrentCulture.Name == "en-US" && valor.Contains(","))
                {
                    return Convert.ToDecimal(valor.Replace(',', '.'));
                }
                else if (CultureInfo.CurrentCulture.Name == "pt-BR" && valor.Contains("."))
                {
                    return Convert.ToDecimal(valor.Replace('.', ','));
                }
                else
                {
                    return Convert.ToDecimal(valor);
                }
            }
            catch
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo como numérico", true, linha, coluna, valor);

                return 0;
            }
        }

        public static int ExcelDataToInt(this string valor, int linha, int coluna, ListImportacaoExcelMsg ret)
        {
            try
            {
                return Convert.ToInt32(valor);
            }
            catch
            {
                ret.Add($"Erro na célula: não foi possivel interpreta-lo como inteiro", true, linha, coluna, valor);
                return 0;
            }
        }
        public static byte ExcelDataToByte(this string valor, int linha, int coluna, ListImportacaoExcelMsg ret, bool erro = true)
        {
            try
            {
                return Convert.ToByte(valor);
            }
            catch
            {
                if (erro)
                    ret.Add($"Erro na célula: não foi possivel interpreta-lo como inteiro", true, linha, coluna, valor);

                return 0;
            }
        }

        public static bool ExcelDataToBoolean(this string valor, int linha, int coluna, ListImportacaoExcelMsg ret)
        {
            string t = valor.ToLower();
            try
            {
                if (bool.TryParse(t, out bool retorno))
                {
                    return retorno;// True, False, true, false
                }
                if (string.IsNullOrEmpty(t))//Vazio é verdadeiro
                {
                    return true;
                }
                if (t.StartsWith("1"))
                {
                    return true;
                }
                if (t.StartsWith("0"))
                {
                    return false;
                }
                if (t.StartsWith("s") || t.StartsWith("v"))
                {
                    return true;
                }
                if (t.StartsWith("n") || t.StartsWith("f"))
                {
                    return false;
                }
                return false;
            }
            catch
            {
                ret.Add($"Erro na célula: não foi possivel interpreta-lo como inteiro", true, linha, coluna, valor);
                return false;
            }
        }

        public static bool ValidateExcelString(this string str, int linha, int coluna, string nomePlanilha, ListImportacaoExcelMsg ret, string[] remove = null, int? minLen = null, int? maxLen = null, bool obrigatoria = true, string fileName = null)
        {
            var v = str.Trim();

            if (remove != null)
                v = v.ReplaceChars(remove, string.Empty);

            if (obrigatoria && string.IsNullOrEmpty(v))
            {
                ret.Add($"Erro na célula: obrigatória {nomePlanilha} {ret.GeraLetraColunaExcel(coluna) + linha} esta vazia. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, linha, coluna, v);
                return false;
            }
            if (minLen.HasValue && v.Length < minLen.Value)
            {
                ret.Add($"Erro na célula: {nomePlanilha} {ret.GeraLetraColunaExcel(coluna) + linha}  comprimento menor que o mínimo de {minLen}. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, linha, coluna, v);
                return false;
            }
            if (maxLen.HasValue && v.Length > maxLen.Value)
            {
                ret.Add($"Erro na célula: {nomePlanilha} {ret.GeraLetraColunaExcel(coluna) + linha}  comprimento maior que o máximo de {maxLen}. {(string.IsNullOrEmpty(fileName) ? "" : $"arquivo:{fileName}")}", false, linha, coluna, v);
                return false;
            }

            return true;
        }
        #endregion

        public class ImportacaoExcelMsg
        {
            public string Msg { get; set; }
            public string Cel { get; set; }
            public string Vlr { get; set; }
            public bool Error { get; set; }
        }

        public class ListImportacaoExcelMsg : List<ImportacaoExcelMsg>
        {
            public void Add(string msg, bool error, ExcelRange cellRange)
            {
                var vlr = string.Empty;
                if (cellRange?.Value is object[,] a)
                {
                    for (int i = 0; i < a.GetLength(0); i++)
                    {
                        for (int j = 0; j < a.GetLength(1); j++)
                        {
                            vlr += a[i, j] + " | ";
                        }
                    }
                    vlr = vlr.Substring(0, vlr.Length - 3);
                }
                else
                    vlr = cellRange?.Text;


                Add(new ImportacaoExcelMsg { Msg = msg, Error = error, Cel = cellRange?.Address, Vlr = vlr });
            }

            public void Add(string msg, bool error, string line, string vlr)
            {
                Add(new ImportacaoExcelMsg { Msg = msg, Error = error, Cel = line, Vlr = vlr });
            }

            public void Add(string msg, bool error, int line, int column, string vlr)
            {
                string excelColumn = GeraLetraColunaExcel(column) + line;

                Add(new ImportacaoExcelMsg { Msg = msg, Error = error, Cel = excelColumn + line, Vlr = vlr });
            }

            public void Add(string msg, bool error, int line, int[] columns, string vlr)
            {
                string[] letrasColunaExcel = new string[columns.Length];

                for (int i = 0; i < columns.Length; i++)
                {
                    string colunaExcel = GeraLetraColunaExcel(columns[i]) + line;
                    letrasColunaExcel.SetValue(colunaExcel, i);
                }

                string excelColumn = string.Join(":", letrasColunaExcel);

                Add(new ImportacaoExcelMsg { Msg = msg, Error = error, Cel = excelColumn, Vlr = vlr });
            }

            public string GeraLetraColunaExcel(int numeroColuna)
            {
                string letraColuna = string.Empty;
                numeroColuna++;

                do
                {
                    letraColuna = (char)(65 + (numeroColuna - 1) % 26) + letraColuna;
                    numeroColuna = (numeroColuna - (numeroColuna - 1) % 26) / 26;
                }
                while (numeroColuna > 0);

                return letraColuna;
            }
        }
    }
}
