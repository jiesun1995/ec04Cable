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

namespace EC04铁壳熔接焊
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch ;
        private IFixtureCableBindService _fixtureCableBindService;
        ConcurrentDictionary<int, string> vals = new ConcurrentDictionary<int, string>();
        private RFIDChannel _RFIDChannelL;
        private RFIDChannel _RFIDChannelR;
        private RFIDChannel _RFIDChannelCable;
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
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            tabPage1.Controls.Clear();
            var zhanbi = 1.00 / DataContent.SystemConfig.ScannerCode;
            ///动态加载人工扫码位显示界面
            for (int i = 0; i < DataContent.SystemConfig.ScannerCode; i++)
            {
                try
                {
                    _RFIDChannelL = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                    _RFIDChannelR = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[1].IP, DataContent.SystemConfig.RFIDConfigs[1].Channel, DataContent.SystemConfig.RFIDConfigs[1].Port);
                    _RFIDChannelCable = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[2].IP, DataContent.SystemConfig.RFIDConfigs[2].Channel, DataContent.SystemConfig.RFIDConfigs[2].Port);
                    Form frmcode;
                    frmcode = new FrmFixture(_RFIDChannelL, _RFIDChannelR, _RFIDChannelCable,
                        (fixtureL, fixtureR, cable) => ScannerCodeByPeopleSaveCSV(fixtureL, fixtureR, cable));
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
                catch (Exception ex)
                {
                    LogManager.Error(ex);
                }
            }
        }

        private bool ScannerCodeByPeopleSaveCSV(string fixtureL, string fixtureR, string cableSn)
        {
            try
            {
                var cable = new Cable
                {
                    Sn = cableSn,
                    Start_time = DateTime.Now,
                    Finish_time = DateTime.Now,
                    Station = "PASS",
                    Model = DataContent.SystemConfig.Model,
                    Test_station = DataContent.SystemConfig.TestStation,
                    FixtureID = fixtureL,
                    FAI1_A = fixtureL,
                    FAI1_B = fixtureR,
                };
                var dt = cable.ToTable();
                CSVHelper.SaveCSV(dt, DataContent.SystemConfig.CSVPath + "//" + Guid.NewGuid() + ".csv");
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataContent.User == "管理员")
            {
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "左治具RFID";
                    gbxRFID2.Text = "右治具RFID";
                    gbxRFID3.Text = "线材RFID";
                    gbxRFID4.Visible= false;
                    gbxWCF.Visible = false;
                    gbxPLC.Visible = false;
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
