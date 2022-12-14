using Dapper;
using Dapper.Contrib.Extensions;
using stdole;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        private Cable QueryByCable(string cable)
        {
            Cable cables = new Cable();
            var sql = new StringBuilder("select * from Cables where 1=1");
            if (!string.IsNullOrWhiteSpace(cable))
            {
                sql.Append(" and Sn = @cable ");
            }
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                cables = connection.QueryFirstOrDefault<Cable>(sql.ToString(), new { cable });
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
                    var cables = QueryByFixture(fixture);
                    foreach (var cable in cables)
                    {
                        connection.Delete<Cable>(cable);
                    }
                }
                foreach (var cable in Cables)
                {
                    var cables = QueryByCable(cable.Sn);
                    if (cables != null)
                        connection.Delete<Cable>(cables);
                }
                foreach (var cable in Cables)
                {
                    connection.Insert<Cable>(cable);
                }
            }
        }

        public void AddHistroy(string fixture, string fixturePat, string[] fixtures)
        {
            var cables = QueryByFixture(fixture);
            foreach (var cable in cables)
            {
                try
                {
                    cable.FAI1_B = fixturePat;
                    for (int i = 0; i < fixtures.Count(); i++)
                    {
                        switch (i)
                        {
                            case 0: cable.FAI1_C = fixtures[i]; break;
                            case 1: cable.FAI1_D = fixtures[i]; break;
                            case 2: cable.FAI1_E = fixtures[i]; break;
                            case 3: cable.FAI1_F = fixtures[i]; break;
                            case 4: cable.FAI1_G = fixtures[i]; break;
                            default:
                                break;
                        }
                    }
                    cable.Status = "PASS";
                    var dt = cable.ToTable();
                    CSVHelper.SaveCSV(dt, $"{DataContent.SystemConfig.CSVPath}\\{Guid.NewGuid().ToString()}.csv");
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Delete<Cable>(cable);
                        CableHistroy cableHistroy = new CableHistroy(cable);
                        connection.Insert<CableHistroy>(cableHistroy);
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(ex);
                }
            }
        }

        public DataTable QueryHistroy(string cable = null, string fixture = null, string fixturePat = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var dt = new DataTable();
            var sql = new StringBuilder(@"SELECT [Sn] as 线材序号
      ,[Model]  as 模块
      ,[FixtureID] as 治具ID
      ,[Test_station] as 测试工站
      ,[Start_time] as 开始时间
      ,[Finish_time] as 结束时间
      ,[Status] as 状态
      ,[Error_code] as 错误码
      ,[FAI1_A] as 治具A
      ,[FAI1_B] as 治具B
      ,[FAI1_C] as 治具C
  FROM [CableHistroies] where 1=1 ");
            if (!string.IsNullOrWhiteSpace(cable))
            {
                sql.Append(" and Sn = @cable ");
            }
            if (!string.IsNullOrWhiteSpace(fixture))
            {
                sql.Append(" and FAI1_A = @fixture ");
            }
            if (!string.IsNullOrWhiteSpace(fixturePat))
            {
                sql.Append(" and FAI1_B = @fixturePat ");
            }
            if (startDate != null)
            {
                sql.Append(" and Start_time >= @startDate ");
            }
            if (endDate != null)
            {
                sql.Append(" and Start_time < @endDate ");
            }
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var dataReader = connection.ExecuteReader(sql.ToString(), new { cable, fixture, fixturePat, startDate, endDate });
                dt.Load(dataReader);
            }
            return dt;
        }

        public DataTable Query(string cable = null, string fixture = null, string fixturePat = null)
        {
            var dt = new DataTable();
            var sql = new StringBuilder(@"SELECT [Sn] as 线材序号
      ,[Model]  as 模块
      ,[FixtureID] as 治具ID
      ,[Test_station] as 测试工站
      ,[Start_time] as 开始时间
      ,[Finish_time] as 结束时间
      ,[Status] as 状态
      ,[Error_code] as 错误码
      ,[FAI1_A] as 治具A
      ,[FAI1_B] as 治具B
      ,[FAI1_C] as 治具C
  FROM [Cables] where 1=1 ");
            if (!string.IsNullOrWhiteSpace(cable))
            {
                sql.Append(" and Sn = @cable ");
            }
            if (!string.IsNullOrWhiteSpace(fixture))
            {
                sql.Append(" and FAI1_A = @fixture ");
            }
            if (!string.IsNullOrWhiteSpace(fixturePat))
            {
                sql.Append(" and FAI1_B = @fixturePat ");
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                var dataReader = connection.ExecuteReader(sql.ToString(), new { cable, fixture, fixturePat });
                dt.Load(dataReader);
            }
            return dt;
        }
    }
}
