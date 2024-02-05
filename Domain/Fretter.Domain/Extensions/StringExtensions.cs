using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Use the current thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
            }
            else return str;
        }

        /// <summary>
        /// Overload which uses the culture info with the specified name
        /// </summary>
        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var cultureInfo = new CultureInfo(cultureInfoName);
                return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
            }
            else return str;
        }

        /// <summary>
        /// Overload which uses the specified culture info
        /// </summary>
        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string ToLettersOnly(this string str)
        {
            if (!string.IsNullOrEmpty(str))
                return Regex.Replace(str.ToUpper(), @"[^A-Z]", "");

            return str;
        }

        public static string ReplaceChars(this string str, string[] oldChars, string newChar)
        {
            for (int i = 0; i < oldChars.Length; i++)
            {
                str = str?.Replace(oldChars[i], newChar);
            }

            return str;
        }
    }
}
