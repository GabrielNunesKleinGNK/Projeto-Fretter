using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Fretter.Util;

namespace System
{
    public static class StringExtentions
    {
        public static string RemoveNonNumeric(this string self)
        {
            Throw.IfIsNullOrEmpty(self);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < self.Length; i++)
                if (Char.IsNumber(self[i]))
                    sb.Append(self[i]);
            return sb.ToString();
        }

        public static string RemoveNumeric(this string self)
        {
            Throw.IfIsNullOrEmpty(self);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < self.Length; i++)
                if (!Char.IsNumber(self[i]))
                    sb.Append(self[i]);
            return sb.ToString();
        }
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        public static string RemoveSpecialChars(this string self)
        {
            char[] buffer = new char[self.Length];
            int idx = 0;

            foreach (char c in self)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z') || (c == '.') || (c == '_'))
                {
                    buffer[idx] = c;
                    idx++;
                }
            }

            return new string(buffer, 0, idx);
        }

        public static string ReplaceSpecialChars(this string self)
        {
            var specialChars = new List<char> { 'ã', 'õ', 'ñ', 'ê', 'û', 'î', 'ô', 'â', 'ç', 'ä', 'ü', 'ï', 'ö', 'Ã', 'Õ', 'Ñ', 'Ê', 'Û', 'Î', 'Ô', 'Â', 'Ç', 'Ä', 'Ü', 'Ï', 'Ö' };
            var normalChars = new List<char> { 'a', 'o', 'n', 'e', 'u', 'i', 'o', 'a', 'c', 'a', 'u', 'i', 'o', 'A', 'O', 'N', 'E', 'U', 'I', 'O', 'A', 'C', 'A', 'U', 'I', 'O' };

            var buffer = new char[self.Length];
            var index = 0;

            foreach (char c in self)
            {
                var indexOf = specialChars.IndexOf(c);
                if (indexOf >= 0)
                    buffer[index] = normalChars[indexOf];
                else
                    buffer[index] = c;

                index++;
            }

            var newString = new string(buffer, 0, self.Length);
            return newString;
        }

        public static string ReplaceAccented(this string self)
        {
            var normalizedString = self.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static string Truncate(this string self, int length, string suffix = "")
        {
            Throw.IfIsNullOrEmpty(self);
            Throw.IfLessThanOrEqZero(length);

            if (self.Length <= length) return self;
            var fragment = self.Left(length);

            return string.Format("{0}{1}", fragment, suffix);
        }

        public static string Right(this string self, int length)
        {
            Throw.IfIsNullOrEmpty(self);
            Throw.IfLessThanZero(length);

            return self.Length > length ? self.Substring(self.Length - length) : self;
        }

        public static string Left(this string self, int length)
        {
            Throw.IfIsNullOrEmpty(self);
            Throw.IfLessThanZero(length);

            return self.Length > length ? self.Substring(0, length) : self;
        }

        public static bool In(this string self, params string[] items)
        {
            Throw.IfIsNullOrEmpty(self);
            Throw.IfIsNull(items);
            Throw.IfEqZero(items.Length);

            return items.Contains(self);
        }

        public static string RemoveLastChars(this string self, int length = 1)
        {
            Throw.IfIsNullOrEmpty(self);
            return self.Left(self.Length - length);
        }

        public static string RemoveMaskCPFOrCNPJ(this string self)
        {
            return self.Replace(".", string.Empty)
                       .Replace("-", string.Empty)
                       .Replace("/", string.Empty);
        }
        public static bool IsPhoneNumber(this string self) => Regex.IsMatch(self, "^([1-9]{2}9[0-9][0-9]{7})|([1-9]{2}7[0-9]{7})$");

        public static string SafeSubstring(this string text, int start, int length)
        {
            return text.Length <= start ? ""
                : text.Length - start <= length ? text.Substring(start)
                : text.Substring(start, length);
        }

        public static T FromJson<T>(this string str) => JsonConvert.DeserializeObject<T>(str);
    }
}
