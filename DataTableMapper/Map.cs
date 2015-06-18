using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableMapper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class Map : Attribute
    {
        public string Field { get; set; }

        public Map(string field)
        {
            this.Field = field;
        }
    }
}
