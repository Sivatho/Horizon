using System.Data.Common;
using System.Reflection;
using ClientServicing.Main.Resources.Helper;

namespace ClientServicing.Main.DataAccess.SQL
{
    public static class SqlDataMapper
    {
        public static void MapReaderToObject<T>(DbDataReader reader, T obj) where T : class, new()
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);
                var prop = typeof(T).GetProperty(fieldName,
                    BindingFlags.IgnoreCase
                    | BindingFlags.Public
                    | BindingFlags.Instance
                );
                try {
                    var fieldValue = reader.GetValue(i);
                    if (fieldValue != null && !reader.IsDBNull(i))
                        prop.SetValue(obj, Convert.ChangeType(reader.GetValue(i), prop.PropertyType));
                }
                catch (Exception ex) {
                    DocumentTemplate.DisplayBody($"Warning: Could Not Map Field '{fieldName}' to property '{prop.Name}': {ex.Message}");
                }
            }
        }
    }
}
