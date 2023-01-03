using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC0406喷胶保压
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch;
        private readonly IPLCReadWrite _plcHelper;
        private readonly RFIDChannel _RFIDChannel1;
        private readonly RFIDChannel _RFIDChannel1P;
        private readonly RFIDChannel _RFIDChannel2;
        private readonly RFIDChannel _RFIDChannel2L;
        private readonly RFIDChannel _RFIDChannel2R;
        private readonly MesService _mesService;
        public FrmMain()
        {
            _mesService = new MesService();
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            InitializeComponent();
            timer1.Start();

            try
            {
                //_plcHelper = new InovanceHelper(DataContent.SystemConfig.PLCIp, DataContent.SystemConfig.PLCPort);
                _plcHelper = PLCFactory.Instance(DataContent.SystemConfig.PLCConfigs[0].IP, DataContent.SystemConfig.PLCConfigs[0].Port, DataContent.SystemConfig.PLCConfigs[0].Type);
                _RFIDChannel1 = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                _RFIDChannel1P = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[1].IP, DataContent.SystemConfig.RFIDConfigs[1].Channel, DataContent.SystemConfig.RFIDConfigs[1].Port);
                _RFIDChannel2 = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[2].IP, DataContent.SystemConfig.RFIDConfigs[2].Channel, DataContent.SystemConfig.RFIDConfigs[2].Port);
                _RFIDChannel2L = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[3].IP, DataContent.SystemConfig.RFIDConfigs[3].Channel, DataContent.SystemConfig.RFIDConfigs[3].Port);
                _RFIDChannel2R = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[4].IP, DataContent.SystemConfig.RFIDConfigs[4].Channel, DataContent.SystemConfig.RFIDConfigs[4].Port);
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
            if (_plcHelper == null)
            {
                tslPLCStatus.Text = $"PLC: 连接失败";
                tslPLCStatus.BackColor = Color.Red;
            }
            else
            {
                tslPLCStatus.Text = $"PLC:{(_plcHelper.IsConnect() ? "已连接" : "连接失败")}";
                tslPLCStatus.BackColor = _plcHelper.IsConnect() ? Color.Green : Color.Red;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"前段线材RFID:{(_RFIDChannel1 != null && _RFIDChannel1.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"前段载具RFID:{(_RFIDChannel1P != null && _RFIDChannel1P.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"后段载具RFID:{(_RFIDChannel2 != null && _RFIDChannel2.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"后段保压块RFID左:{(_RFIDChannel2L != null && _RFIDChannel2L.IsConnect ? "已连接" : "未连接")}; ");
            stringBuilder.Append($"后段保压块RFID右:{(_RFIDChannel2R != null && _RFIDChannel2R.IsConnect ? "已连接" : "未连接")}; ");
            tslRIDFStatus.Text = "RFID：" + stringBuilder.ToString();
            if (_RFIDChannel1 == null || !_RFIDChannel1.IsConnect || _RFIDChannel1P == null || !_RFIDChannel1P.IsConnect
                || _RFIDChannel2 == null || !_RFIDChannel2.IsConnect || _RFIDChannel2L == null || !_RFIDChannel2L.IsConnect
                || _RFIDChannel2R == null || !_RFIDChannel2R.IsConnect
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
            LogManager.Init(lvLogs);
            tabPage1.Controls.Clear();
            tabPage2.Controls.Clear();
            tabPage3.Controls.Clear();
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
            Task.Factory.StartNew(async () =>
            {
                var fixtureCableBindService = WCFHelper.CreateClient();
                while (true)
                {
                    if (_plcHelper == null) { await Task.Delay(1000); continue; }
                    if (_plcHelper.Read(5100) == 1)
                    {
                        LogManager.Info($"读取到启动信号：{{D5100:1}}");
                        try
                        {
                            var sn = _RFIDChannel1.Read();
                            var fixture = _RFIDChannel1P.Read();
                            var snStation = _mesService.GetCurrStation(sn);
                            if (_mesService.GetCurrStation(sn) != DataContent.SystemConfig.ConfirmStation)
                                LogManager.Warn($"读取到线材：{sn}，但站点不符：{snStation}");
                            if (!string.IsNullOrEmpty(sn) && !string.IsNullOrEmpty(fixture))
                            {
                                var cable = new Cable
                                {
                                    Sn = sn,
                                    Start_time = DateTime.Now,
                                    Test_station = DataContent.SystemConfig.TestStation,
                                    FAI1_A = fixture,
                                    //FixtureID = fixture,
                                    Model = DataContent.SystemConfig.Model,
                                };
                                fixtureCableBindService.FixtureCableBind(new List<Cable> { cable });
                            }
                            else if (!string.IsNullOrEmpty(sn) && string.IsNullOrEmpty(fixture))
                            {
                                var cable = new Cable
                                {
                                    Sn = sn,
                                    Start_time = DateTime.Now,
                                    Test_station = DataContent.SystemConfig.TestStation,
                                    FAI1_A = "NG",
                                    FixtureID = DataContent.SystemConfig.FixtureID,
                                    Model = DataContent.SystemConfig.Model,
                                };
                                fixtureCableBindService.FixtureCableBind(new List<Cable> { cable });
                            }
                            _plcHelper.Write(5100, 2);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                            _plcHelper.Write(5100, 3);
                        }
                    }
                    await Task.Delay(10);
                }
            }, TaskCreationOptions.LongRunning);
            Task.Factory.StartNew(async () =>
            {
                var fixtureCableBindService = WCFHelper.CreateClient();
                while (true)
                {
                    if (_plcHelper == null) { await Task.Delay(1000); continue; }
                    if (_plcHelper.Read(5102) == 1)
                    {
                        LogManager.Info($"读取到启动信号：{{D5102:1}}");
                        try
                        {
                            var val = _RFIDChannel2.Read();
                            var valL = _RFIDChannel2L.Read();
                            var valR = _RFIDChannel2R.Read();
                            if (!string.IsNullOrEmpty(val) && !string.IsNullOrEmpty(valL))
                            {
                                fixtureCableBindService.FixtureBind(val, valL,valR);
                            }
                            else if (string.IsNullOrEmpty(val))
                            {
                                fixtureCableBindService.FixtureBind("NG", string.IsNullOrEmpty(valL) ? "NG" : valL,valR);
                            }
                            else if (string.IsNullOrEmpty(valL))
                            {
                                fixtureCableBindService.FixtureBind(val, "NG", valR);
                            }
                            _plcHelper.Write(5102, 2);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                            _plcHelper.Write(5102, 3);
                        }
                    }
                    await Task.Delay(10);
                }
            }, TaskCreationOptions.LongRunning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxRFID5, gbxRFID6, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "前段线材RFID";
                    gbxRFID2.Text = "前段载具RFID";
                    gbxRFID3.Text = "后段载具RFID";
                    gbxRFID4.Text = "后段保压块RFID左";
                    gbxRFID5.Text = "后段保压块RFID右";
                    gbxRFID6.Visible= false;
                    gbxWCF.Visible = false;
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
            if (btnLogin.Text == "退出权限")
            {
                DataContent.User = string.Empty;
                btnLogin.Text = "权限登陆";
            }
            else
            {
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();
                if (!string.IsNullOrEmpty(DataContent.User))
                {
                    btnLogin.Text = "退出权限";
                }
            }

        }
    }
}
