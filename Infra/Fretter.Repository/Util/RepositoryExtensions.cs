using FastMember;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Fretter.Repository.Util
{
    public static class RepositoryExtensions 
    {
        public static List<T> ToList<T>(this IDataReader reader) //where T : class//, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var list = new List<T>();

            if (((SqlDataReader)reader).HasRows)
            {
                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (!reader.IsDBNull(i))
                        {
                            string fieldName = reader.GetName(i);

                            if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                            {
                                accessor[item, fieldName] = reader.GetValue(i);
                            }
                        }
                    }

                    list.Add(item);
                }
            }

            return list;
        }

        public static void MapDataToObject<T>(this SqlDataReader dataReader, T newObject)
        {
            if (newObject == null) throw new ArgumentNullException(nameof(newObject));

            // Fast Member Usage
            var objectMemberAccessor = TypeAccessor.Create(newObject.GetType());
            var propertiesHashSet =
                    objectMemberAccessor
                    .GetMembers()
                    .Select(mp => mp.Name)
                    .ToHashSet(StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                var name = propertiesHashSet.FirstOrDefault(a => a.Equals(dataReader.GetName(i), StringComparison.InvariantCultureIgnoreCase));
                if (!String.IsNullOrEmpty(name))
                {
                    var defaultTypeValue = dataReader.GetFieldType(i).IsValueType ? Activator.CreateInstance(dataReader.GetFieldType(i)) : null;
                    objectMemberAccessor[newObject, name] = dataReader.IsDBNull(i) ? defaultTypeValue : dataReader.GetValue(i);
                }
            }
        }
    }

    public class DbReader
    {
    }
}
