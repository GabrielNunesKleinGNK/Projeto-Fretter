using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Helpers.Extensions
{
    public static class FileHelper
    {
        public static string FormataTemplatePosicional(this string layout, object data)
        {
            string rowFormat = string.Empty;

            if (!string.IsNullOrEmpty(layout))
            {
                string[] layoutArray = layout.Split(',');

                foreach (var layoutItem in layoutArray)
                {
                    //{ TipoDeRegistro | Number | 1 | 9} Exemplo
                    var arrayItem = layoutItem.Replace("{", "").Replace("}", "").Split('|');
                    string propName = arrayItem[0].ToLower();
                    string typeValue = arrayItem[1].ToLower();
                    string lenghtProp = arrayItem[2].ToString();
                    string defaultValue = arrayItem[3].ToLower();


                    switch (defaultValue)
                    {
                        case ("column"):
                            string propNamePascalCase = data.GetType().GetProperties().ToList().Where(t => t.Name.ToLower() == propName).Select(t => t.Name).FirstOrDefault().ToString();
                            string valueData = data.GetType().GetProperty(propNamePascalCase).GetValue(data, null).ToString();

                            Regex regex = new Regex(@"[^a-zA-Z0-9 ]");
                            valueData = regex.Replace(valueData, "").ToUpper();

                            rowFormat = String.Concat(rowFormat, FormataLinha(typeValue, lenghtProp, valueData.ToString()).Substring(0, lenghtProp.ToInt32()));
                            break;
                        case ("empty"):
                            rowFormat = String.Concat(rowFormat, ' '.Repeat(lenghtProp.ToInt32()));
                            break;
                        default:
                            rowFormat = String.Concat(rowFormat, FormataLinha(typeValue, lenghtProp, defaultValue));
                            break;
                    }
                }
            }

            return rowFormat.ToUpperInvariant();
        }

        public static string FormataTemplateDelimitado(this string layout, object data, string delimitador)
        {
            string rowFormat = string.Empty;

            if (!string.IsNullOrEmpty(layout))
            {
                string[] layoutArray = layout.Split(',');

                foreach (var layoutItem in layoutArray)
                {
                    var arrayItem = layoutItem.Replace("{", "").Replace("}", "").Split('|');
                    string propName = arrayItem[0].ToLower();
                    string defaultValue = arrayItem[1].ToLower();

                    switch (defaultValue)
                    {
                        case ("column"):
                            string propNamePascalCase = data.GetType().GetProperties().ToList().Where(t => t.Name.ToLower() == propName).Select(t => t.Name).FirstOrDefault().ToString();
                            string valueData = data.GetType().GetProperty(propNamePascalCase).GetValue(data, null).ToString();

                            Regex regex = new Regex(@"[^a-zA-Z0-9 /]");
                            valueData = regex.Replace(valueData, "").ToUpper();

                            rowFormat = String.Concat(rowFormat, valueData, delimitador);
                            break;
                        case ("empty"):
                            rowFormat = String.Concat(rowFormat, " ", delimitador);
                            break;
                        default:
                            rowFormat = String.Concat(rowFormat, defaultValue, delimitador);
                            break;
                    }
                }
            }

            return rowFormat.ToUpperInvariant();
        }

        private static string FormataLinha(string typeValue, string lenghtProp, string valueData)
        {
            string valorFormatado = string.Empty;
            switch (typeValue)
            {
                case ("number"):
                    valorFormatado = valueData.PadRight(lenghtProp.ToInt32(), '0');
                    break;
                case ("string"):
                    valorFormatado = valueData.PadRight(lenghtProp.ToInt32(), ' ');
                    break;
                case ("decimal"):
                    valorFormatado = valueData.PadRight(lenghtProp.ToInt32(), '0');
                    break;
                default:
                    break;
            }

            return valorFormatado;
        }
        private static string Repeat(this char charToRepeat, int repeat)
        {

            return new string(charToRepeat, repeat);
        }
        private static string Repeat(this string stringToRepeat, int repeat)
        {
            var builder = new StringBuilder(repeat * stringToRepeat.Length);
            for (int i = 0; i < repeat; i++)
            {
                builder.Append(stringToRepeat);
            }
            return builder.ToString();
        }
    }
}
