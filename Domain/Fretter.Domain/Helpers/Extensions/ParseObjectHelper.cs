using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Helpers.Extensions
{
    public static class ParseObjectHelper
    {
        public static object ConvertObjectToLayout(string layoutData, dynamic data)
        {
            var objRetorno = new ExpandoObject() as IDictionary<string, Object>;

            if (!string.IsNullOrEmpty(layoutData))
            {
                var layoutArray = layoutData.Split(",");

                foreach (var layout in layoutArray)
                {
                    var ltAtributo = layout.Substring(0, layout.IndexOf(':')).TrimEnd().TrimStart();
                    var ltValorCampo = Regex.Match(layout, @"\$(.*?)\$").Groups[1].Value;
                    string ltValor;

                    if (string.IsNullOrEmpty(ltValorCampo))
                    {
                        ltValor = layout.Split(":")[1].TrimEnd().TrimStart();
                    }
                    else
                    {
                        if (ltValorCampo.Split("|").Length > 1)
                        {
                            var valorArray = ltValorCampo.Split("|");
                            ltValor = FormatValue(GetPropValue(data, valorArray[0]), valorArray[1]);
                        }
                        else ltValor = GetPropValue(data, ltValorCampo).ToString();
                    }

                    objRetorno.Add(ltAtributo.Replace("\"", ""), ltValor.Replace("\"", ""));
                }
            }

            return objRetorno;
        }
        public static string ConvertObjectToLayoutString(string layoutData, dynamic data)
        {
            var stringRetorno = string.Empty;

            if (!string.IsNullOrEmpty(layoutData))
            {
                var layoutArray = new string[] { };
                string splitChar = string.Empty;

                if (layoutData.Contains("&"))
                {
                    layoutArray = layoutData.Split("&");
                    splitChar = "&";
                }
                else if (layoutData.Contains("/"))
                {
                    layoutArray = layoutData.Split("/");
                    splitChar = "/";
                }

                foreach (var layout in layoutArray)
                {
                    var ltValorCampo = Regex.Match(layout, @"\$(.*?)\$").Groups[1].Value;
                    string ltValor = string.Empty;

                    if (!string.IsNullOrEmpty(ltValorCampo))
                    {
                        try
                        {
                            if (ltValorCampo.Split("|").Length > 1)
                            {
                                var valorArray = ltValorCampo.Split("|");
                                ltValor = FormatValue(GetPropValue(data, valorArray[0]), valorArray[1]);
                            }
                            else ltValor = GetPropValue(data, ltValorCampo).ToString();
                        }
                        catch (Exception)
                        {
                            ltValor = "";
                        }

                        if (string.IsNullOrEmpty(stringRetorno))
                            stringRetorno = layout.Replace("$" + ltValorCampo + "$", ltValor);
                        else stringRetorno += $"{splitChar}{layout.Replace("$" + ltValorCampo + "$", ltValor)}";
                    }
                    else stringRetorno += string.IsNullOrEmpty(stringRetorno) ? layout : $"{splitChar}{layout}";
                }
            }

            return stringRetorno;
        }

        //public static string ConvertObjectToLayoutString(string layoutData, dynamic data, List<string> fieldsDynamics)
        //{
        //    if (!string.IsNullOrEmpty(layoutData))
        //    {
        //        foreach (var field in fieldsDynamics)
        //        {
        //            string ltValor = string.Empty;
        //            try
        //            {
        //                if (field.Split("|").Length > 1)
        //                {
        //                    var valorArray = field.Split("|");
        //                    var formato = valorArray[1];
        //                    ltValor = FormatValue(GetPropValue(data, valorArray[0]), formato);

        //                    switch (formato)
        //                    {
        //                        case ("n0"):
        //                        case ("n1"):
        //                        case ("n2"):
        //                        case ("c2"):
        //                            ltValor = ltValor.Replace(",", ".");
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                else ltValor = GetPropValue(data, field).ToString();
        //            }
        //            catch (Exception)
        //            {
        //                ltValor = "";
        //            }

        //            if (!string.IsNullOrEmpty(ltValor))
        //                layoutData = layoutData.Replace("$" + field + "$", string.Format("'{0}'", ltValor));
        //        }
        //    }

        //    //valida se ainda existe campos sem replace
        //    var camposDinamicos = Regex.Matches(layoutData, @"\$(.*?)\$").Select(s => s.Groups.Values.Where(w => !w.Value.Contains("$")).FirstOrDefault().Value).ToList();
        //    if (camposDinamicos.Count > 0)
        //        foreach (var campo in camposDinamicos)
        //            layoutData = layoutData.Replace("$" + campo + "$", "''");

        //    return layoutData;
        //}

        private static string FormatValue(object obj, string format)
        {
            string ltValor = string.Empty;
            switch (format)
            {
                case ("dd/MM/yyyy"):
                case ("dd/MM/yyyy HH:mm:ss"):
                case ("dd/MM/yyyy HH:mm:ssss"):
                    DateTime dataParseFormat;
                    CultureInfo cultureDateFormat = CultureInfo.CreateSpecificCulture("pt-BR");
                    try
                    {
                        if (DateTime.TryParse(((Newtonsoft.Json.Linq.JValue)obj).ToString(null, cultureDateFormat), out dataParseFormat))
                            ltValor = Convert.ToDateTime(dataParseFormat).ToString(format);
                        else ltValor = Convert.ToDateTime(obj).ToString(format);
                    }
                    catch (InvalidCastException)
                    {
                        ltValor = Convert.ToDateTime(obj).ToString(format);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case ("n0"):
                case ("n1"):
                case ("n2"):
                case ("c2"):
                    decimal number;
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("pt-BR");
                    try
                    {
                        if (Decimal.TryParse(obj.ToString(), NumberStyles.Number, culture, out number))
                            ltValor = Convert.ToDecimal(obj.ToString()).ToString(format);
                        else ltValor = obj.ToString();
                    }
                    catch (InvalidCastException)
                    {
                        ltValor = obj.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case ("System.DateTime"):
                    DateTime dataParseFormatDt;
                    CultureInfo cultureDateFormatDt = CultureInfo.CreateSpecificCulture("pt-BR");
                    try
                    {
                        if (DateTime.TryParse(((Newtonsoft.Json.Linq.JValue)obj).ToString(null, cultureDateFormatDt), out dataParseFormatDt))
                            ltValor = ((DateTime)dataParseFormatDt).ToString(format);
                        else ltValor = ((DateTime)obj).ToString(format);
                    }
                    catch (InvalidCastException)
                    {
                        ltValor = ((DateTime)obj).ToString(format);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                case ("UnixEpoch"):
                    DateTime dataParse;
                    TimeSpan t;
                    CultureInfo cultureDate = CultureInfo.CreateSpecificCulture("pt-BR");

                    try
                    {
                        if (DateTime.TryParse(((Newtonsoft.Json.Linq.JValue)obj).ToString(null, cultureDate), out dataParse))
                            t = (dataParse) - new DateTime(1970, 1, 1);
                        else t = ((DateTime)obj) - new DateTime(1970, 1, 1);
                    }
                    catch (InvalidCastException)
                    {
                        t = ((DateTime)obj) - new DateTime(1970, 1, 1);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    ltValor = ((long)t.TotalSeconds).ToString();
                    break;
                case ("UTCTime"):
                    DateTime dataParseUtc;
                    CultureInfo cultureDateUtc = CultureInfo.CreateSpecificCulture("pt-BR");

                    try
                    {
                        if (DateTime.TryParse(((Newtonsoft.Json.Linq.JValue)obj).ToString(null, cultureDateUtc), out dataParseUtc))
                            ltValor = Convert.ToDateTime(dataParseUtc).ToString("yyyy-MM-ddTHH:mm:sszzz");
                        else ltValor = Convert.ToDateTime(obj).ToString("yyyy-MM-ddTHH:mm:sszzz");
                    }
                    catch (InvalidCastException)
                    {
                        ltValor = Convert.ToDateTime(obj).ToString("yyyy-MM-ddTHH:mm:sszzz");
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    break;
                default:
                    break;
            }

            return ltValor;
        }
        public static object GetPropValue(object src, string propName)
        {
            if (src.GetType().GetProperty(propName) == null)
            {
                try
                {
                    return ((Newtonsoft.Json.Linq.JObject)src)[propName];
                }
                catch (InvalidCastException)
                {
                    return src.GetType().GetProperty(propName).GetValue(src, null);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
