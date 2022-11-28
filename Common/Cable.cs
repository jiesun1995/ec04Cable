using stdole;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Cable
    {
        public int Id { get; set; }
        public string Sn { get; set; }
        public string Model { get; set; }
        public string FixtureID { get; set; }
        public string Test_station { get; set; }
        public DateTime? Start_time { get; set; }
        public DateTime? Finish_time { get; set; }
        public string Status { get; set; }
        public string Error_code { get; set; }
        /// <summary>
        /// 子治具
        /// </summary>
        public string FAI1_A { get; set; }
        /// <summary>
        /// 母治具
        /// </summary>
        public string FAI1_B { get; set; }
        /// <summary>
        /// hotbar机
        /// </summary>
        public string FAI1_C { get; set; }
        public string FAI1_D { get; set; }
        public string FAI1_E { get; set; }
        public string FAI1_F { get; set; }
        public string FAI1_G { get; set; }
        /// <summary>
        /// 第一个还是第二个
        /// </summary>
        public string FAI1_H { get; set; }
        public string Station { get; set; }
        public DataTable ToTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("SN"));
            dt.Columns.Add(new DataColumn("model"));
            dt.Columns.Add(new DataColumn("fixtureID"));
            dt.Columns.Add(new DataColumn("test_station"));
            dt.Columns.Add(new DataColumn("start_time"));
            dt.Columns.Add(new DataColumn("finish_time"));
            dt.Columns.Add(new DataColumn("status"));
            dt.Columns.Add(new DataColumn("error_code"));
            dt.Columns.Add(new DataColumn("FAI1_A"));
            dt.Columns.Add(new DataColumn("FAI1_B"));
            dt.Columns.Add(new DataColumn("FAI1_C"));
            dt.Columns.Add(new DataColumn("FAI1_D"));
            dt.Columns.Add(new DataColumn("FAI1_E"));
            dt.Columns.Add(new DataColumn("FAI1_F"));
            dt.Columns.Add(new DataColumn("FAI1_G"));
            dt.Columns.Add(new DataColumn("FAI1_H"));
            dt.Columns.Add(new DataColumn("Station"));
            var row = dt.NewRow();
            row[0] = Sn;
            row[1] = Model;
            row[2] = FixtureID;
            row[3] = Test_station;
            row[4] = Start_time;
            row[5] = Finish_time;
            row[6] = Status;
            row[7] = Error_code;
            row[8] = FAI1_A;
            row[9] = FAI1_B;
            row[10] = FAI1_C;
            row[11] = FAI1_D;
            row[12] = FAI1_E;
            row[13] = FAI1_F;
            row[14] = FAI1_G;
            row[15] = FAI1_H;
            row[15] = Station;
            dt.Rows.Add(row);
            return dt;
        }
    }
}
