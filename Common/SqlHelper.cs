using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;
using System.Configuration;

namespace Common
{
    public static class SqlHelper
    {
        //public static string connectionString = "Server=10.194.64.8;Database=ECo4;uid=sa;pwd=Pass@word1";
        public static string connectionString = ConfigurationManager.ConnectionStrings["ec04"].ToString();
        public static int Insert<T>(T obj) where T : class
        {
            var connection=new SqlConnection(connectionString); 
            var rows = connection.Insert(obj);
            return Convert.ToInt32(rows);
        }
        public static int Update<T>(T obj) where T : class
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Update(obj);
            return Convert.ToInt32(rows);
        }
        public static int Delete<T>(T obj) where T : class
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Delete(obj);
            return Convert.ToInt32(rows);
        }
        public static int Execute(string sql,object obj)
        {
            var connection = new SqlConnection(connectionString);
            var rows = connection.Execute(sql, obj);
            return Convert.ToInt32(rows);
        }
        public static IEnumerable<T> Query<T>(string sql, object obj) where T : class
        {
            var connection = new SqlConnection(connectionString);
            var list = connection.Query<T>(sql, obj);
            return list;
        }
        public static DataTable Query(string sql, object obj=null)
        {
            var connection = new SqlConnection(connectionString);
            var dataReader = connection.ExecuteReader(sql, obj);
            var dt = new DataTable();
            dt.Load(dataReader);
            return dt;
        }
    }
}
