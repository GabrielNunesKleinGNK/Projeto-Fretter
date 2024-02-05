using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class PropertyInfoExtensions
    {
        public static string GetDisplayValue(this PropertyInfo prop)
        {
            if (prop.CustomAttributes == null || prop.CustomAttributes.Count() == 0)
                return prop.Name;

            var displayNameAttribute = prop.CustomAttributes.Where(x => x.AttributeType == typeof(DisplayAttribute)).FirstOrDefault();

            return displayNameAttribute.NamedArguments[0].TypedValue.Value.ToString() ?? prop.Name;
        }

        public static bool IsMarkedWith<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttributes<T>().Any();
        }
    }
}
