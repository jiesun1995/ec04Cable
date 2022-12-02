using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC0401前处理
{
    public class MesService
    {
        /// <summary>
        /// 获取Mes标签条码接口
        /// </summary>
        /// <returns></returns>
        public string FetchCode()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999).ToString();
        }

        public string FetchCode(string code)
        {
            Random random = new Random();
            var num = random.Next(100000000, 999999999);

            if(int.TryParse(code,out int newcode))
            {
                //if (num % 2 == 0)
                return num.ToString();
                //else
                //    return code;
            }
            return num.ToString();
        }
    }
}
