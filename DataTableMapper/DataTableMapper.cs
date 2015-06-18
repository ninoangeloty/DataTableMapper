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

                    if (columns.Contains(fieldName))
                    {
                        prop.SetValue(obj, row[fieldName]);
                    }
                }

                return obj;
            });
        }

        private static string GetFieldName(PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<Map>();
            return attribute != null ? ((Map)attribute).Field : string.Empty;
        }

        private static IEnumerable<string> GetDataTableColumns(DataTable table)
        {
            return table.Columns.Cast<DataColumn>().Select(_ => _.ColumnName);
        }
    }
}
