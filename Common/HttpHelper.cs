using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class HttpHelper
    {
        public static string PostHandle(string url, Dictionary<string,string> dict)
        {
            var result = string.Empty;
            HttpContent httpContent = new FormUrlEncodedContent(dict);
            httpContent.Headers.ContentType.CharSet = "UTF-8";
            HttpClient httpClient = new HttpClient();
            try
            {
                result = httpClient.PostAsync(url, httpContent).Result.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex.InnerException.InnerException;
            }
            return result;
        }
    }
}
