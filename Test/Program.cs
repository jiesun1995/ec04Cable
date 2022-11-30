using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogManager.Init(null);

            TestWCFQueryDt();
            Console.ReadLine();

        }

        public static void TestWCFQueryDt()
        {
            WCFHelper.CreateServer();
            Task.Delay(1000).Wait();
            Task.Factory.StartNew(() => {
                //var kvs =new Dictionary<int, string>();
                var clinet = WCFHelper.CreateClient();
                var dt = clinet.QueryHistroy();
            });
        }
        public static void TestHotbarWCF()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            WCFHelper.CreateServer();

            Task.Delay(1000).Wait();

            //ConcurrentDictionary<int, string> kvs = new ConcurrentDictionary<int, string>();
            ConcurrentQueue<string> fixtures = new ConcurrentQueue<string>();
            for (int i = 0; i < 8; i++)
            {
                Task.Factory.StartNew(obj =>
                {
                    var index = Convert.ToInt32(obj) + 1;
                    //var kvs =new Dictionary<int, string>();
                    var clinet = WCFHelper.CreateClient();
                    Random random = new Random();

                    while (true)
                    {
                        List<Cable> list = new List<Cable>(2);
                        Dictionary<int, string> kvs = new Dictionary<int, string>();
                        for (int j = 0; j < 2; j++)
                        {
                            var fix = random.Next(index * 10, index * 10 + 10);
                            if (!kvs.ContainsKey(fix))
                            {
                                var fixture = $"fix-000{fix}";
                                var cable = new Cable
                                {
                                    Sn = Guid.NewGuid().ToString(),
                                    Start_time = DateTime.Now,
                                    Test_station = DataContent.SystemConfig.TestStation,
                                    FAI1_A = fixture,
                                    FixtureID = fixture,
                                    Model = DataContent.SystemConfig.Model,
                                };
                                list.Add(cable);
                                kvs.Add(fix, string.Empty);
                                fixtures.Enqueue(fixture);

                            }
                            else
                            {
                                j--;
                            }
                        }
                        clinet.FixtureCableBind(list);
                        Console.WriteLine($"写入治具和线材：{list[0].FixtureID}，{list[0].Sn}，{list[1].Sn}");
                        list.Clear();
                        Task.Delay(random.Next(1, 10) * 1000).Wait();
                    }
                }, i);
            }
            Task.Delay(5 * 1000).Wait();

            Task.Factory.StartNew(() => {
                //var kvs =new Dictionary<int, string>();
                var clinet = WCFHelper.CreateClient();
                Random random = new Random();
                while (true)
                {
                    if (fixtures.Count > 0)
                    {
                        fixtures.TryDequeue(out string fixture);
                        var fixturePat = $"fixtruepat-00000{random.Next(1000, 9999).ToString()}";
                        clinet.FixtureBind(fixture, fixturePat);
                        Console.WriteLine($"写入治具和和母治具：{fixture}，{fixturePat}");
                    }
                    Task.Delay(10).Wait();
                }
            }, cancellationToken);
        }

        public static void TestRFID()
        {
            for (int i = 0; i < 4; i++)
            {
                Task.Factory.StartNew(obj =>
                {
                    var index=Convert.ToInt32(obj);
                    RFIDGetway rfidhelper = new RFIDGetway("192.168.1.10", 0);
                    rfidhelper.SetChannelState(tp =>
                    {
                        if (tp)
                        {
                            var result = rfidhelper.Read();
                            Console.WriteLine(result);
                        }
                    });
                },i);
            }
            Task.Factory.StartNew(() =>
            {
                RFIDGetway rfidhelper = new RFIDGetway("192.168.1.10", 0);
                rfidhelper.SetChannelState(tp =>
                {
                    if (tp)
                    {
                        var result = rfidhelper.Read();
                        Console.WriteLine(result);
                    }
                });
            });

        }
    }
}
