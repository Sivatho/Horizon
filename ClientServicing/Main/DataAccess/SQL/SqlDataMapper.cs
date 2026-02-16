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

                // If no matching property was found, log and continue
                if (prop == null)
                {
                    DocumentTemplate.DisplayBody($"Warning: Could Not Map Field '{fieldName}' - no matching property on type '{typeof(T).FullName}'.");
                    continue;
                }

                if (!prop.CanWrite)
                {
                    DocumentTemplate.DisplayBody($"Warning: Property '{prop.Name}' is read-only; skipping mapping for field '{fieldName}'.");
                    continue;
                }

                try
                {
                    if (reader.IsDBNull(i))
                        continue;

                    var fieldValue = reader.GetValue(i);

                    if (fieldValue == null)
                        continue;

                    // Unwrap nullable target type if needed
                    var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    object? converted;
                    if (targetType.IsEnum)
                    {
                        // handle enums from string or numeric values
                        if (fieldValue is string s)
                            converted = Enum.Parse(targetType, s, ignoreCase: true);
                        else
                            converted = Enum.ToObject(targetType, fieldValue);
                    }
                    else if (targetType == typeof(Guid))
                    {
                        converted = fieldValue is Guid g ? g : Guid.Parse(fieldValue.ToString()!);
                    }
                    else
                    {
                        converted = Convert.ChangeType(fieldValue, targetType);
                    }

                    prop.SetValue(obj, converted);
                }
                catch (Exception ex)
                {
                    // Avoid dereferencing prop in the message (prop is known non-null here)
                    DocumentTemplate.DisplayBody($"Warning: Could Not Map Field '{fieldName}' to property '{prop.Name}': {ex.Message}");
                }
            }
        }
    }
}
