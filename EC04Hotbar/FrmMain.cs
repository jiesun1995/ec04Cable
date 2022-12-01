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
        private readonly OMRHelper _omrHelper;
        private readonly IFixtureCableBindService _fixtureCableBindService;
        private readonly RFIDChannel _RFIDChannel1;
        private readonly RFIDChannel _RFIDChannel1P;
        private readonly RFIDChannel _RFIDChannel2;
        private readonly RFIDChannel _RFIDChannel2P;
        public FrmMain()
        {
            InitializeComponent();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            timer1.Start();
            
            try
            {
                LogManager.Init(lvLogs);
                _fixtureCableBindService = WCFHelper.CreateClient();
                if (DataContent.SystemConfig.ScannerCode > 0)
                    return;
                _omrHelper = new OMRHelper(DataContent.SystemConfig.PLCIp, DataContent.SystemConfig.PLCPort);

                _RFIDChannel1 = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                _RFIDChannel1P = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[1].IP, DataContent.SystemConfig.RFIDConfigs[1].Channel, DataContent.SystemConfig.RFIDConfigs[1].Port);
                _RFIDChannel2 = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[2].IP, DataContent.SystemConfig.RFIDConfigs[2].Channel, DataContent.SystemConfig.RFIDConfigs[2].Port);
                _RFIDChannel2P = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[3].IP, DataContent.SystemConfig.RFIDConfigs[3].Channel, DataContent.SystemConfig.RFIDConfigs[3].Port);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"流道1子RFID:{(_RFIDChannel1 != null && _RFIDChannel1.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"流道1母RFID:{(_RFIDChannel1P != null && _RFIDChannel1P.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"流道2子RFID:{(_RFIDChannel2 != null && _RFIDChannel2.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"流道2母RFID:{(_RFIDChannel2P != null && _RFIDChannel2P.IsConnect ? "已连接" : "未连接")}; ");
            tslRIDFStatus.Text = "RFID：" + stringBuilder.ToString();
            if(_RFIDChannel1==null || !_RFIDChannel1.IsConnect || _RFIDChannel1P == null || !_RFIDChannel1P.IsConnect
                || _RFIDChannel2 == null || !_RFIDChannel2.IsConnect || _RFIDChannel2P == null || !_RFIDChannel2P.IsConnect
                )
            {
                tslRIDFStatus.BackColor = Color.Red;
            }
            else
            {
                tslRIDFStatus.BackColor = Color.Green;
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
                var fixtureCableBindService = WCFHelper.CreateClient();
                while (true)
                {
                    if (_omrHelper.Read("D11") == 1)
                    {
                        LogManager.Info($"读取到启动信号：{{D11:1}}");
                        try
                        {
                            var content = _RFIDChannel1.Read();
                            var contentP = _RFIDChannel1P.Read();
                            fixtureCableBindService.FixtureBind(content, contentP);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                        finally
                        {
                            _omrHelper.Write("D11", 3);
                        }
                    }
                    await Task.Delay(100);
                }
            }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(async () =>
            {
                var fixtureCableBindService = WCFHelper.CreateClient();
                while (true)
                {
                    if (_omrHelper.Read("D13") == 1)
                    {
                        LogManager.Info($"读取到启动信号：{{D13:1}}");
                        try
                        {
                            var content = _RFIDChannel2.Read();
                            var contentP = _RFIDChannel2P.Read();
                            fixtureCableBindService.FixtureBind(content, contentP);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                        finally
                        {
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
