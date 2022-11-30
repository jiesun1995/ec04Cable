using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Channels;

namespace EC04Hotbar
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch ;
        private IFixtureCableBindService _fixtureCableBindService;
        private readonly OMRHelper _omrHelper;
        private ConcurrentDictionary<int, string> vals = new ConcurrentDictionary<int, string>();
        private readonly RFIDHelper _rfidHelper1;
        private readonly RFIDHelper _rfidHelper1P;
        private readonly RFIDHelper _rfidHelper2;
        private readonly RFIDHelper _rfidHelper2P;
        public FrmMain()
        {
            InitializeComponent();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            timer1.Start();
            
            vals.TryAdd(0, string.Empty);
            vals.TryAdd(1, string.Empty);
            vals.TryAdd(2, string.Empty);
            vals.TryAdd(3, string.Empty);
            try
            {
                LogManager.Init(lvLogs);
                _fixtureCableBindService = WCFHelper.CreateClient();
                if (DataContent.SystemConfig.ScannerCode > 0)
                    return;
                _omrHelper = new OMRHelper(DataContent.SystemConfig.PLCIp, DataContent.SystemConfig.PLCPort);

                _rfidHelper1 = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                _rfidHelper1.DataLength_Ch0 = DataContent.SystemConfig.RFIDConfigs[0].DataLength;
                _rfidHelper1.StartAddress_Ch0 = DataContent.SystemConfig.RFIDConfigs[0].StartAddress;

                _rfidHelper1.ReadCallback = (channel, content) =>
                {
                    if(DataContent.SystemConfig.RFIDConfigs[0].Channel == channel)
                    {
                        vals.TryUpdate(channel, content,content);
                        DataSaveRunner1();
                    }
                };

                _rfidHelper1P = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[1].IP, DataContent.SystemConfig.RFIDConfigs[1].Channel, DataContent.SystemConfig.RFIDConfigs[1].Port);
                _rfidHelper1P.DataLength_Ch1 = DataContent.SystemConfig.RFIDConfigs[1].DataLength;
                _rfidHelper1P.StartAddress_Ch1 = DataContent.SystemConfig.RFIDConfigs[1].StartAddress;

                _rfidHelper1P.ReadCallback = (channel, content) =>
                {
                    if (DataContent.SystemConfig.RFIDConfigs[1].Channel == channel)
                    {
                        vals.TryUpdate(channel, content, content);
                        DataSaveRunner1();
                    }
                };

                _rfidHelper2 = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[2].IP, DataContent.SystemConfig.RFIDConfigs[2].Channel, DataContent.SystemConfig.RFIDConfigs[2].Port);
                _rfidHelper2.DataLength_Ch2 = DataContent.SystemConfig.RFIDConfigs[2].DataLength;
                _rfidHelper2.StartAddress_Ch2 = DataContent.SystemConfig.RFIDConfigs[2].StartAddress;
                _rfidHelper1P.ReadCallback = (channel, content) =>
                {
                    if (DataContent.SystemConfig.RFIDConfigs[2].Channel == channel)
                    {
                        vals.TryUpdate(channel, content, content);
                        DataSaveRunner2();
                    }
                };

                _rfidHelper2P = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[3].IP, DataContent.SystemConfig.RFIDConfigs[3].Channel, DataContent.SystemConfig.RFIDConfigs[3].Port);
                _rfidHelper2P.DataLength_Ch3 = DataContent.SystemConfig.RFIDConfigs[3].DataLength;
                _rfidHelper2P.StartAddress_Ch3 = DataContent.SystemConfig.RFIDConfigs[3].StartAddress;
                _rfidHelper1P.ReadCallback = (channel, content) =>
                {
                    if (DataContent.SystemConfig.RFIDConfigs[3].Channel == channel)
                    {
                        vals.TryUpdate(channel, content, content);
                        DataSaveRunner2();
                    }
                };
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
            }
        }

        public void DataSaveRunner1()
        {
            vals.TryGetValue(0, out string val);
            vals.TryGetValue(1, out string valP);
            if (!string.IsNullOrEmpty(val) && !string.IsNullOrEmpty(valP))
            {
                _fixtureCableBindService.FixtureBind(val, valP);
                _omrHelper.Write("D11", 3);
            }
        }
        public void DataSaveRunner2()
        {
            vals.TryGetValue(2, out string val);
            vals.TryGetValue(3, out string valP);
            if (!string.IsNullOrEmpty(val) && !string.IsNullOrEmpty(valP))
            {
                _fixtureCableBindService.FixtureBind(val, valP);
                _omrHelper.Write("D13", 3);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslSysTime.Text = $"系统时间:{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}";
            tslRunTime.Text = $"运行时间:{_stopwatch.Elapsed}";
            if (_omrHelper == null)
            {
                tslPLCStatus.Text = $"PLC: 未连接";
                tslPLCStatus.BackColor = Color.Red;
            }
            else
            {
                tslPLCStatus.Text = $"PLC:{(_omrHelper.IsConnect ? "已连接" : "未连接")}";
                tslPLCStatus.BackColor = _omrHelper.IsConnect ? Color.Green : Color.Red;
            }
            if (_rfidHelper1 != null)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"流道1子RFID:{(_rfidHelper1.channel0_connect ? "已连接" : "未连接")}; ");
                stringBuilder.Append($"流道1母RFID:{(_rfidHelper1P.channel1_connect ? "已连接" : "未连接")}; ");
                stringBuilder.Append($"流道2子RFID:{(_rfidHelper2.channel2_connect ? "已连接" : "未连接")}; ");
                stringBuilder.Append($"流道2母RFID:{(_rfidHelper2P.channel3_connect ? "已连接" : "未连接")}; ");
                tslRIDFStatus.Text = "RFID：" + stringBuilder.ToString();
                tslRIDFStatus.BackColor= Color.Green;
            }
            else
            {
                tslRIDFStatus.Text = "RFID：未连接;";
                tslRIDFStatus.BackColor = Color.Red;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            tabPage1.Controls.Clear();
            tabPage2.Controls.Clear();
            tabPage3.Controls.Clear();
            var zhanbi = 1.00 / DataContent.SystemConfig.ScannerCode;
            ///动态加载人工扫码位显示界面
            for (int i = 0; i < DataContent.SystemConfig.ScannerCode; i++)
            {
                Form frmcode;
                frmcode = new FrmFixture((fixture, cable1, cable2) => ScannerCodeByPeople(fixture, new List<string> { cable1, cable2 }));
                frmcode.TopLevel = false;
                frmcode.Dock = DockStyle.Top;
                frmcode.Width = tabPage1.Width;
                frmcode.FormBorderStyle = FormBorderStyle.None;

                frmcode.Height = Convert.ToInt32(tabPage1.Height * zhanbi);
                var y = Convert.ToInt32(tabPage1.Height * zhanbi * i);
                frmcode.Location = new Point(0, y);
                tabPage1.Controls.Add(frmcode);
                frmcode.Show();
            }

            Form frmRunFixture;
            frmRunFixture = new FrmRunFixture();
            frmRunFixture.TopLevel = false;
            frmRunFixture.Dock = DockStyle.Fill;
            frmRunFixture.FormBorderStyle = FormBorderStyle.None;
            tabPage2.Controls.Add(frmRunFixture);
            frmRunFixture.Show();

            Form frmCableHistroies;
            frmCableHistroies = new FrmCableHistroies();
            frmCableHistroies.TopLevel = false;
            frmCableHistroies.Dock = DockStyle.Fill;
            frmCableHistroies.FormBorderStyle = FormBorderStyle.None;
            tabPage3.Controls.Add(frmCableHistroies);
            frmCableHistroies.Show();

            if (DataContent.SystemConfig.ScannerCode > 0)
                return;
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (_omrHelper.Read("D11") == 1)
                    {
                        LogManager.Info($"读取到启动信号：{{D11:1}}");
                        try
                        {

                            _rfidHelper1.Read(DataContent.SystemConfig.RFIDConfigs[0].Channel);
                            _rfidHelper1P.Read(DataContent.SystemConfig.RFIDConfigs[1].Channel);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                        finally
                        {
                            await Task.Delay(1000);
                            _omrHelper.Write("D11", 3);
                        }
                    }
                    await Task.Delay(100);
                }
            }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    if (_omrHelper.Read("D13") == 1)
                    {
                        LogManager.Info($"读取到启动信号：{{D13:1}}");
                        try
                        {

                            _rfidHelper2.Read(DataContent.SystemConfig.RFIDConfigs[2].Channel);
                            _rfidHelper2P.Read(DataContent.SystemConfig.RFIDConfigs[3].Channel);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                        finally
                        {
                            await Task.Delay(1000);
                            _omrHelper.Write("D13", 3);
                        }
                    }
                    await Task.Delay(100);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private bool ScannerCodeByPeople(string fixture, List<string> list)
        {
            var cables= new List<Cable>();
            foreach (var item in list)
            {
                var cable = new Cable
                {
                    Sn=item,
                    Start_time=DateTime.Now,
                    Test_station=DataContent.SystemConfig.TestStation,
                    FAI1_A=fixture,
                    FixtureID=fixture,
                    Model=DataContent.SystemConfig.Model,
                    
                };
                cables.Add(cable);
            }
            return _fixtureCableBindService.FixtureCableBind(cables);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataContent.User == "管理员")
            {
                FrmSetting frmSetting = new FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "流道1子载具RFID";
                    gbxRFID2.Text = "流道1母载具RFID";
                    gbxRFID3.Text = "流道2子载具RFID";
                    gbxRFID4.Text = "流道2母载具RFID";
                });
                frmSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先登录");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(btnLogin.Text == "退出权限")
            {
                DataContent.User = string.Empty;
                btnLogin.Text = "权限登陆";
            }
            else
            {
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();
                if (DataContent.User == "管理员")
                {
                    btnLogin.Text = "退出权限";
                }
            }
            
        }
    }
}
