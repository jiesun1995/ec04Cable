using Dapper;
using Dapper.Contrib.Extensions;
using stdole;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CableSqliteDal
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ec04"].ToString();
        //public static string connectionString = "Server=10.194.64.8;Database=ECo4;uid=sa;pwd=Pass@word1"; 
        
        public static void CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var count = connection.QueryFirstOrDefault<int>("select Count(*) from sqlite_master where type = 'table' and name = @tableName ", new { tableName = "Cables" });
                if (count <= 0)
                {
                    connection.Execute(@"CREATE TABLE Cables (
                                            Id           INTEGER    PRIMARY KEY AUTOINCREMENT,
                                            Sn           TEXT (50),
                                            Model        TEXT (50),
                                            FixtureID    TEXT (50),
                                            Test_station TEXT (50),
                                            Start_time   TEXT (100),
                                            Finish_time  TEXT (100),
                                            Status       TEXT (50),
                                            Error_code   TEXT (50),
                                            FAI1_A       TEXT (50),
                                            FAI1_B       TEXT (50),
                                            FAI1_C       TEXT (50),
                                            FAI1_D       TEXT (50),
                                            FAI1_E       TEXT (50),
                                            FAI1_F       TEXT (50),
                                            FAI1_G       TEXT (50),
                                            FAI1_H       TEXT (50),
                                            Station      TEXT (50) 
                                        );

                                        CREATE TABLE CableHistroies (
                                            Id           INTEGER,
                                            Sn           TEXT (50),
                                            Model        TEXT (50),
                                            FixtureID    TEXT (50),
                                            Test_station TEXT (50),
                                            Start_time   TEXT (100),
                                            Finish_time  TEXT (100),
                                            Status       TEXT (50),
                                            Error_code   TEXT (50),
                                            FAI1_A       TEXT (50),
                                            FAI1_B       TEXT (50),
                                            FAI1_C       TEXT (50),
                                            FAI1_D       TEXT (50),
                                            FAI1_E       TEXT (50),
                                            FAI1_F       TEXT (50),
                                            FAI1_G       TEXT (50),
                                            FAI1_H       TEXT (50),
                                            Station      TEXT (50) 
                                        );
                     ");
                }
            }
        }

        private IEnumerable<Cable> QueryByFixture(string fixture)
        {
            List<Cable> cables=new List<Cable>();
            var sql = new StringBuilder("select * from Cables where 1=1");
            if (!string.IsNullOrWhiteSpace(fixture))
            {
                sql.Append(" and FAI1_A = @fixture ");
            }
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                cables = connection.Query<Cable>(sql.ToString(), new { fixture }).ToList();
            }
            //var cables = SqlHelper.Query<Cable>(sql.ToString(), new {  });
            return cables;
        }

        //private IEnumerable<Cable> Delete(string id)
        //{
        //    var sql = new StringBuilder("select * from Cables where 1=1");
        //    if (!string.IsNullOrWhiteSpace(fixture))
        //    {
        //        sql.Append(" and FAI1_A = @fixture ");
        //    }
        //    var cables = SqlHelper.Query<Cable>(sql.ToString(), new { fixture });
        //    return cables;
        //}
        public void AddCable(List<Cable> Cables)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var fixtures = Cables.GroupBy(x => x.FAI1_A).Select(x => x.Key);
                foreach (var fixture in fixtures)
                {
                    //connection.q
                    var cables = QueryByFixture(fixture);
                    foreach (var cable in cables)
                    {
                        connection.Delete<Cable>(cable);
                    }
                }
                foreach (var cable in Cables)
                {
                    connection.Insert<Cable>(cable);
                }
            }
        }

        public void AddHistroy(string fixture, string fixturePat)
        {
            var cables = QueryByFixture(fixture);
            foreach (var cable in cables)
            {
                try
                {
                    cable.FAI1_B = fixturePat;
                    cable.Status = "PASS";
                    var dt = cable.ToTable();
                    CSVHelper.SaveCSV(dt, $"{DataContent.SystemConfig.CSVPath}\\{Guid.NewGuid().ToString()}.csv");
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Delete<Cable>(cable);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(ex);
                }
                finally
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        CableHistroy cableHistroy = new CableHistroy(cable);
                        connection.Insert<CableHistroy>(cableHistroy);
                    }
                }
            }
        }
    }
}
