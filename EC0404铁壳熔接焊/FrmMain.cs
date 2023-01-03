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

namespace EC0404铁壳熔接焊
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
            LogManager.Init(lvLogs);
            tabPage1.Controls.Clear();
            var zhanbi = 1.00 / DataContent.SystemConfig.ScannerCode;
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.RowCount = DataContent.SystemConfig.ScannerCode % 2 > 0 ? DataContent.SystemConfig.ScannerCode / 2 + 1 : DataContent.SystemConfig.ScannerCode / 2;
            tableLayoutPanel.ColumnCount = DataContent.SystemConfig.ScannerCode > 1 ? 2 : 1;
            tableLayoutPanel.Dock = DockStyle.Fill;
            ///动态加载人工扫码位显示界面
            for (int i = 0; i < DataContent.SystemConfig.ScannerCode; i++)
            {
                try
                {
                    _RFIDChannelL = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i / 3 + 0].IP, DataContent.SystemConfig.RFIDConfigs[i / 3 + 0].Channel, DataContent.SystemConfig.RFIDConfigs[i / 3 + 0].Port);
                    _RFIDChannelR = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i / 3 + 1].IP, DataContent.SystemConfig.RFIDConfigs[i / 3 + 1].Channel, DataContent.SystemConfig.RFIDConfigs[i / 3 + 1].Port);
                    _RFIDChannelCable = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i / 3 + 2].IP, DataContent.SystemConfig.RFIDConfigs[i / 3 + 2].Channel, DataContent.SystemConfig.RFIDConfigs[i / 3 + 2].Port);
                    MesService mesService = new MesService();
                    //OMRHelper omrHelper = new OMRHelper(DataContent.SystemConfig.PLCIp, DataContent.SystemConfig.PLCPort);
                    var _plcHelper = PLCFactory.Instance(DataContent.SystemConfig.PLCConfigs[i].IP, DataContent.SystemConfig.PLCConfigs[i].Port, DataContent.SystemConfig.PLCConfigs[i].Type);
                    Form frmcode;
                    frmcode = new FrmFixture(_RFIDChannelL, _RFIDChannelR, _RFIDChannelCable, mesService, _plcHelper,
                        (fixtureL, fixtureR, cable) => ScannerCodeByPeopleSaveCSV(fixtureL, fixtureR, cable));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / DataContent.SystemConfig.ScannerCode > 1 ? 2 : 1));
                    tableLayoutPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100 / DataContent.SystemConfig.ScannerCode % 2 > 0 ? DataContent.SystemConfig.ScannerCode / 2 + 1 : DataContent.SystemConfig.ScannerCode / 2));

                    var panel = new Panel();
                    panel.Dock = DockStyle.Fill;
                    frmcode.TopLevel = false;
                    frmcode.Dock = DockStyle.Fill;
                    frmcode.Width = tabPage1.Width;
                    frmcode.FormBorderStyle = FormBorderStyle.None;

                    //frmcode.Height = Convert.ToInt32(tabPage1.Height * zhanbi);
                    //var y = Convert.ToInt32(tabPage1.Height * zhanbi * i);
                    //frmcode.Location = new Point(0, y);
                    //tabPage1.Controls.Add(frmcode);
                    panel.Controls.Add(frmcode);
                    frmcode.Show();
                    tableLayoutPanel.Controls.Add(panel, i % 2, i / 2);
                }
                catch (Exception ex)
                {
                    LogManager.Error(ex);
                }
            }
            tabPage1.Controls.Add(tableLayoutPanel);
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
                    //Station = "PASS",
                    Status = "PASS",
                    Model = DataContent.SystemConfig.Model,
                    Test_station = DataContent.SystemConfig.TestStation,
                    //FixtureID = fixtureL,
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
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxRFID5, gbxRFID6, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "左治具RFID1";
                    gbxRFID2.Text = "右治具RFID1";
                    gbxRFID3.Text = "线材RFID1";
                    gbxRFID4.Text = "左治具RFID2";
                    gbxRFID5.Text = "右治具RFID2";
                    gbxRFID6.Text = "线材RFID2";
                    gbxWCF.Visible = false;
                    gbxPLC.Visible = true;
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
                if (!string.IsNullOrEmpty(DataContent.User))
                {
                    btnLogin.Text = "退出权限";
                }
            }
            
        }
    }
}
