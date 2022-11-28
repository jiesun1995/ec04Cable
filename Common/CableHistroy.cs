using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace Common
{
    [Table("CableHistroies")]
    public class CableHistroy
    {
        public CableHistroy() { }
        public CableHistroy(Cable cable)
        {
            Id = cable.Id;
            Sn = cable.Sn;
            Error_code = cable.Error_code;
            FAI1_A = cable.FAI1_A;
            FAI1_B = cable.FAI1_B;
            FAI1_C = cable.FAI1_C;
            FAI1_D = cable.FAI1_D;
            FAI1_E = cable.FAI1_E;
            FAI1_F = cable.FAI1_F;
            FAI1_G = cable.FAI1_G;
            FAI1_H = cable.FAI1_H;
            Finish_time = DateTime.Now;
            Start_time = cable.Start_time.Value;
            FixtureID = cable.FixtureID;
            Model = cable.Model;
            Station = cable.Station;
            Status = cable.Status;
            Test_station = cable.Test_station;
        }
        [ExplicitKey]
        public long Id { get; set; }
        public string Sn { get; set; }
        public string Model { get; set; }
        public string FixtureID { get; set; }
        public string Test_station { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime Finish_time { get; set; }
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
    }
}
