using System.Globalization;
using System.Linq;
using Fretter.Domain.Entities;

namespace System
{
    public static class QueryFilterExtension
    {
        public static string ToString(this QueryFilter filter, string coluna)
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null) ? null : parametro.Value.ToString();
        }
        public static Int16? ToShort(this QueryFilter filter, string coluna)
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null || string.IsNullOrEmpty(parametro.Value.ToString())) ? (short?)null : short.Parse(parametro.Value.ToString());
        }
        public static int? ToInt(this QueryFilter filter, string coluna)
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null || string.IsNullOrEmpty(parametro.Value.ToString())) ? (int?)null : Convert.ToInt32(parametro.Value.ToString());
        }
        public static long? ToLong(this QueryFilter filter, string coluna)
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null) ? (long?)null : Convert.ToInt64(parametro.Value.ToString());
        }
        public static decimal? ToDecimal(this QueryFilter filter, string coluna)
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null || string.IsNullOrEmpty(parametro.Value.ToString())) ? (decimal?)null : Convert.ToDecimal(parametro.Value.ToString());
        }

        public static bool? ToBool(this QueryFilter filter, string coluna)
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null || string.IsNullOrEmpty(parametro.Value.ToString())) ? (bool?)null : Convert.ToBoolean(parametro.Value.ToString());
        }

        public static DateTime? ToDateTime(this QueryFilter filter, string coluna, string formato = "MM/dd/yyyy")
        {
            var parametro = filter.Filters.FirstOrDefault(f => f.Property.ToLower() == coluna.ToLower());
            return (parametro == null || string.IsNullOrEmpty(parametro.Value.ToString())) ? (DateTime?)null : DateTime.ParseExact(parametro.Value.ToString(), formato, CultureInfo.InvariantCulture);
        }

        public static TEnum? ToEnum<TEnum>(this QueryFilter filter, string coluna) where TEnum : struct
        {
            var parametro = filter.ToString(coluna);
            return (string.IsNullOrWhiteSpace(parametro)) ? (TEnum?)null : (TEnum)Enum.Parse(typeof(TEnum), parametro);
        }
    }
}
