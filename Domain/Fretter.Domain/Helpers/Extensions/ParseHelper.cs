using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain
{
    public static class ParseHelper
    {
        public static decimal ToDecimal(this string input)
        {
            decimal data = 0;
            decimal.TryParse(input, out data);
            return data;
        }

        public static double ToDouble(this string input)
        {
            double data = 0;
            double.TryParse(input, out data);
            return data;
        }

        public static int ToInt(this string input)
        {
            int data = 0;
            int.TryParse(input, out data);
            return data;
        }

        public static string MatchReplace(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        public static List<T> CopyToList<T>(this IList<T> data)
        {
            List<T> list = (List<T>)Activator.CreateInstance<List<T>>();

            foreach (var r in data)
            {
                var item = r.CopyTo<T>();

                if (item != null)
                    list.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, list, new object[] { item });
            }
            return list;
        }

        public static T CopyTo<T>(this object source)
        {

            //if (item == null)
            T item = (T)Activator.CreateInstance(typeof(T));

            if (item != null)
            {
                foreach (var prop in item.GetType().GetProperties())
                {
                    PropertyInfo sourceAttr = source.GetType().GetProperty(prop.Name);

                    if (sourceAttr != null && prop.CanWrite)
                        prop.SetValue(item, Convert.ChangeType(sourceAttr.GetValue(source, null), prop.PropertyType), null);
                }
            }

            return item;
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            if (str == null)
                return str;

            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
       
    }
}
