using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;

namespace System
{
    public static class ObjectExtentions
    {
        /// <summary>
        /// Get a value from a property of an object by property name.
        /// </summary>
        /// <param name="object">Object</param>
        /// <param name="propertyName">Property Name</param>
        /// <returns>If property not exists, returns null otherwise returns the property value</returns>
        public static object GetPropertyValue(this object @object, string propertyName)
        {
            return @object.GetType().GetProperty(propertyName)?.GetValue(@object, null);
        }

        public static bool TryCast<T>(this object obj, out T result)
        {
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }

            result = default(T);
            return false;
        }

        public static string ToJson(this object obj)
        {
            if (obj == null) return string.Empty;
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static long ToInt64(this object obj)
        {
            Int64.TryParse(obj?.ToString(), out long result);
            return result;
        }

        public static int ToInt32(this object obj)
        {
            Int32.TryParse(obj?.ToString(), out int result);
            return result;
        }

        public static DateTime? ToDateTime(this object obj)
        {
            DateTime.TryParse(obj?.ToString(), out var result);
            return result == DateTime.MaxValue || result == DateTime.MinValue ? default(DateTime?) : result;
        }

        public static DateTime? ToDateTimeCulture(this object obj)
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentCulture;

            DateTime dateValue;
            DateTime.TryParse(obj?.ToString(), out dateValue);

            if (dateValue == DateTime.MinValue && currentCulture.ToString() != "pt-BR")
            {
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR", false);
                System.Globalization.DateTimeStyles styles = System.Globalization.DateTimeStyles.None;
                DateTime.TryParse(obj?.ToString(), culture, styles, out dateValue);
            }

            return dateValue;
        }
    }
}
