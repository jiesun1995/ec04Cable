using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Common
{
    public class MesService
    {
        /// <summary>
        /// 通过SN获取当前站点
        /// </summary>
        /// <param name="SN">产品sn</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetCurrStation(string SN)
        {
            var result = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("c", "QUERY_HISTORY");
            dict.Add("sn", SN);
            dict.Add("p", "Get_CurrStation");
            LogManager.Info($"获取当前站点:{JsonHelper.SerializeObject(dict)}");
            var data = HttpHelper.PostHandle(DataContent.SystemConfig.MESUrl, dict);
            LogManager.Info($"当前站点结果：{data}");
            if (!data.StartsWith("0 SFC_OK"))
                throw new Exception($"处理Mes结果异常 :{data}");
            var ss = data.Split('\n');
            foreach (var s in ss)
            {
                var start = "Get_CurrStation=";
                var startIndex = s.IndexOf(start);
                if (startIndex >= 0)
                {
                    result = s.Substring(startIndex + start.Length, s.Length - (startIndex + start.Length));
                }
            }
            return result;
        }
        /// <summary>
        /// 通过站点和工单获取新的SN
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="invoices"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetNewSN(string stationId,string invoices)
        {
            var result = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("c", "QUERY_HISTORY");
            dict.Add("p", "Auto_SN");
            dict.Add("tsid", stationId);
            dict.Add("sn", invoices);
            LogManager.Info($"获取SN:{JsonHelper.SerializeObject(dict)}");
            var data = HttpHelper.PostHandle(DataContent.SystemConfig.MESUrl, dict);
            LogManager.Info($"获取SN结果：{data}");
            if (!data.StartsWith("0 SFC_OK"))
                throw new Exception($"处理Mes结果异常 :{data}");
            var ss = data.Split('\n');
            foreach (var s in ss)
            {
                var start = "SN=";
                var startIndex = s.IndexOf(start);
                if (startIndex >= 0)
                {
                    result = s.Substring(startIndex + start.Length, s.Length - (startIndex + start.Length));
                }
            }
            return result;
        }
        /// <summary>
        /// 验证工单
        /// </summary>
        /// <param name="invoices">工单号</param>
        /// <returns></returns>
        public bool ValidationInvoices(string invoices)
        {
            if(string.IsNullOrEmpty(invoices)) return false;
            var result = string.Empty;
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("c", "QUERY_RECORD");
            dict.Add("p", "Auto_Wo");
            dict.Add("sn", invoices);
            LogManager.Info($"验证工单:{JsonHelper.SerializeObject(dict)}");
            var data = HttpHelper.PostHandle(DataContent.SystemConfig.MESUrl, dict);
            LogManager.Info($"验证结果：{data}");
            if (!data.StartsWith("0 SFC_OK"))
                throw new Exception($"处理Mes结果异常 :{data}");
            var ss = data.Split('\n');
            foreach (var s in ss)
            {
                var start = "Auto_Wo=";
                var startIndex = s.IndexOf(start);
                if (startIndex >= 0)
                {
                    result = s.Substring(startIndex + start.Length, s.Length - (startIndex + start.Length));
                }
            }
            return result==invoices;
        }
    }
}
