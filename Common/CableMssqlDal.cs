using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Configuration;

namespace Common
{
    public class CableMssqlDal
    {

        public static string connectionString = ConfigurationManager.ConnectionStrings["ec04"].ToString();
        //public static string connectionString = "Server=10.194.64.8;Database=ECo4;uid=sa;pwd=Pass@word1"; 
        public long Insert(Cable cable)
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Insert(cable);
            return rows;
        }
        public long InsertHistroy(CableHistroy cableHistroy)
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Insert(cableHistroy);
            return rows;
        }
        public bool DeleteHistroy(CableHistroy cableHistroy)
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Delete(cableHistroy);
            return rows;
        }

        public long DeleteByFixture(string fixture, string fixturePat = null)
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Execute("Delete from Cables where FAI1_A = @fixture or FAI1_A = @fixturePat", new { fixture, fixturePat });
            return rows;
        }

        public bool Update(Cable cable)
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Update(cable);
            return rows;
        }
        public IEnumerable<Cable> QueryByFixture(string fixture, string fixturePat=null)
        {
            var sql = new StringBuilder("select * from Cables where 1=1");
            if (!string.IsNullOrWhiteSpace(fixture))
            {
                sql.Append(" and FAI1_A = @fixture  or FAI1_B = @fixture");
            }
            if (!string.IsNullOrWhiteSpace(fixturePat))
            {
                sql.Append(" or FAI1_B = @fixturePat or FAI1_A = @fixturePat");
            }
            var cables = SqlHelper.Query<Cable>(sql.ToString(), new { fixture, fixturePat });
            return cables;
        }
        public DataTable Query(string cable, string fixture, string fixturePat)
        {
            var sql = new StringBuilder(@"SELECT [Sn] as 线材序号
      ,[Model]  as 模块
      ,[FixtureID] as 治具ID
      ,[Test_station] as 测试工站
      ,[Start_time] as 开始时间
      ,[Finish_time] as 结束时间
      ,[Status] as 状态
      ,[Error_code] as 错误码
      ,[FAI1_A] as 子治具Id
      ,[FAI1_B] as 母治具Id
      ,[FAI1_C] as HotBar号
      ,[FAI1_H] as 扫码顺序
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

            var dt = SqlHelper.Query(sql.ToString(), new { cable, fixture, fixturePat });
            return dt;
        }

        public DataTable QueryHistroyById(long Id)
        {
            var dt = SqlHelper.Query(@"SELECT [Sn] as SN
      ,[Model] as model
      ,[FixtureID] as fixtureID
      ,[Test_station] as test_station
      ,[Start_time] as start_time
      ,[Finish_time] as finish_time
      ,[Status] as status
      ,[Error_code] as error_code
      ,[FAI1_A]
      ,[FAI1_B]
      ,[FAI1_C]
      ,[FAI1_D]
      ,[FAI1_E] as FE
      ,[FAI1_F]
      ,[FAI1_G]
      ,[FAI1_H]
      ,case when [FAI1_H] = 1 then 'Left' else 'Right' end as [Station] 
  FROM [CableHistroies] where Id = @Id", new { Id });
            return dt;
        }

        public DataTable QueryHistroy(string cable, string fixture, string fixturePat, DateTime? startDate, DateTime? endDate)
        {
            var sql = new StringBuilder(@"SELECT [Sn] as 线材序号
      ,[Model]  as 模块
      ,[FixtureID] as 治具ID
      ,[Test_station] as 测试工站
      ,[Start_time] as 开始时间
      ,[Finish_time] as 结束时间
      ,[Status] as 状态
      ,[Error_code] as 错误码
      ,[FAI1_A] as 子治具Id
      ,[FAI1_B] as 母治具Id
      ,[FAI1_C] as HotBar号
      ,[FAI1_H] as 扫码顺序
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
            if (startDate!=null)
            {
                sql.Append(" and Start_time >= @startDate ");
            }
            if (endDate !=null)
            {
                sql.Append(" and Start_time < @endDate ");
            }

            var dt = SqlHelper.Query(sql.ToString(), new { cable, fixture, fixturePat, startDate, endDate });
            return dt;
        }
    }
}
