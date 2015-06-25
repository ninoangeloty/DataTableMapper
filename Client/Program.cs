using DataTableMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = Mapper.Map<Model>(InitDataTable());
        }

        private static DataTable InitDataTable()
        {
            var table = new DataTable();

            table.Columns.Add("Column1", typeof(string));
            table.Columns.Add("Column2", typeof(int));
            table.Columns.Add("Column3", typeof(DateTime));
            table.Columns.Add("Column4", typeof(long));
            table.Columns.Add("Column5", typeof(bool));

            var rowOne = table.NewRow();
            rowOne[0] = "asdf";
            rowOne[1] = 1;
            rowOne[2] = DateTime.Now;
            rowOne[3] = 2;
            rowOne[4] = false;

            var rowTwo = table.NewRow();
            rowTwo[0] = "qwerty";
            rowTwo[1] = 10;
            rowTwo[2] = DateTime.Now;
            rowTwo[3] = 20;
            rowTwo[4] = true;

            table.Rows.Add(rowOne);
            table.Rows.Add(rowTwo);

            return table;
        }
    }

    public class Model
    {
        //[Map("Column1")]
        public string Column1 { get; set; }
        [Map("Column2")]
        public int Prop2 { get; set; }
        [Map("Column3")]
        public DateTime Prop3 { get; set; }
        [Map("Column4")]
        public long Prop4 { get; set; }
        [Map("Column5")]
        public bool Prop5 { get; set; }
    }
}
