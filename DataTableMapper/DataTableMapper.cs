using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper
{
    public class Mapper
    {
        public static IEnumerable<T> Map<T>(DataTable table) where T : new()
        {
            if (table.Rows.Count == 0)
            {
                return new List<T>();
            }

            var modelProperties = Activator.CreateInstance<T>().GetType().GetProperties();
            var columns = GetDataTableColumns(table);

            return table.Rows.Cast<DataRow>().Select(row => 
            {
                var obj = new T();

                foreach (var prop in modelProperties)
                {
                    var fieldName = GetFieldName(prop);
                    var rowIdentifier = !string.IsNullOrEmpty(fieldName) ? fieldName : prop.Name;

                    if (columns.Contains(fieldName) || columns.Contains(rowIdentifier))
                    {
                        var value = row[rowIdentifier] != DBNull.Value ? row[rowIdentifier] : Convert.ChangeType(null, prop.PropertyType);
                        prop.SetValue(obj, value);
                    }
                }

                return obj;
            });
        }

        private static string GetFieldName(PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<Map>();
            return attribute != null ? ((Map)attribute).Field : (string)null;
        }

        private static IEnumerable<string> GetDataTableColumns(DataTable table)
        {
            return table.Columns.Cast<DataColumn>().Select(_ => _.ColumnName);
        }
    }
}
