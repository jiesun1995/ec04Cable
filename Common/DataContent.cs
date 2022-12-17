using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class DataContent
    {
        public static SystemConfig SystemConfig { private set; get; } = new SystemConfig();

        public static string User { set; get; }

        public static void SetConfig(SystemConfig systemConfig)
        {
            SystemConfig = systemConfig;
            var json = JsonConvert.SerializeObject(SystemConfig);
            File.WriteAllText("System.Config", json);
        }

        public static void LoadConfig()
        {
            if (File.Exists("System.Config"))
            {
                var result = File.ReadAllText("System.Config");
                var config = JsonConvert.DeserializeObject<SystemConfig>(result);
                if (config.RFIDConfigs.Count < 5)
                {
                    for (int i = config.RFIDConfigs.Count -1; i < 5; i++)
                    {
                        config.RFIDConfigs.Add(new RFIDConfig());
                    }
                }
                DataContent.SetConfig(config);
            }
            else
            {
                SystemConfig = new SystemConfig
                {
                    RFIDConfigs = new List<RFIDConfig>
                    {
                        new RFIDConfig{ },
                        new RFIDConfig{ },
                        new RFIDConfig{ },
                        new RFIDConfig{ },
                        new RFIDConfig{ },
                    }
                };
            }
        }
    }

    public class SystemConfig
    {
        public List<RFIDConfig> RFIDConfigs { get; set; } = new List<RFIDConfig>();
        public string CSVPath { get; set; } = "D:\\data";
        public string Model { get; set; }
        public string FixtureID { get; set; }
        public string TestStation { get; set; }
        public string PassWord { get; set; } = "888888";
        public string SystemPassWord { get; set; } = "jajqr168";

        public int ScannerCode { set; get; } = 1;
        public string ConfirmStation { set; get; }
        public string MESUrl { set; get; } = "http://192.168.16.30/Bobcat/sfc_response.aspx";

        public string PLCIp { get; set; }
        public int PLCPort { get; set; }

        public string WCFSeverIp { set; get; } = "0.0.0.0";
        public string WCFSeverPort { set; get; } = "4000";
        public string WCFClinetIp { set; get; } = "127.0.0.1";
        public string WCFClinetPort { set; get; } = "4000";
    }

    public class RFIDConfig
    {
        public string IP { get; set; }
        public int Channel { get; set; }
        public int Port { get; set; } = 502;
        public int DataLength { get; set; } = 100;
        public int StartAddress { get; set; } = 0;
    }

}
