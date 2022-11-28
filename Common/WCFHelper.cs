using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class WCFHelper
    {
        public static void CreateServer()
        {
            Task.Factory.StartNew(() =>
            {
                ServiceHost server = null;
                while (true)
                {
                    if (server == null || server.State != CommunicationState.Opened)
                    {
                        server = WCFHelper.BindServer();
                        if (server.State == CommunicationState.Opened)
                            LogManager.Info($"WCF服务启动成功：{server.BaseAddresses[0].OriginalString}");
                    }
                    Task.Delay(1000).Wait();
                }
            });
        }

        private static ServiceHost BindServer()
        {
            CableSqliteDal.CreateTable();

            string uri = $"http://{DataContent.SystemConfig.WCFSeverIp}:{DataContent.SystemConfig.WCFSeverPort}";

            ServiceHost host = new ServiceHost(typeof(FixtureCableBindService), new Uri(uri));
 
            host.AddServiceEndpoint(typeof(IFixtureCableBindService), new BasicHttpBinding(), "BindService");
 
            //host.AddServiceEndpoint(typeof(IFixtureCableBindService), new NetTcpBinding(), "net.tcp://127.0.0.1:1921/HomeServieTcp");
 
            host.Description.Behaviors.Add(new ServiceMetadataBehavior() { HttpGetEnabled = true });
 
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
 
            host.Open();
 
            return host;
        }
        public static IFixtureCableBindService CreateClient()
        {
            string uri = $"http://{DataContent.SystemConfig.WCFClinetIp}:{DataContent.SystemConfig.WCFClinetPort}/BindService";
            ChannelFactory<IFixtureCableBindService> factory = new ChannelFactory<IFixtureCableBindService>(new BasicHttpBinding(),
                                                                                    new EndpointAddress("http://127.0.0.1:1920/BindService"));
            
            var client = factory.CreateChannel();

            return client;
        }
    }
}
