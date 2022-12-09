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

namespace EC0405编织喷胶
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch ;
        public FrmMain()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            InitializeComponent();
            timer1.Start();
            LogManager.Init(lvLogs);
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
                    var RFIDChannelFixtrue = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i * 2].IP, DataContent.SystemConfig.RFIDConfigs[i * 2].Channel, DataContent.SystemConfig.RFIDConfigs[i * 2].Port);
                    var RFIDChannelCable = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i * 2 + 1].IP, DataContent.SystemConfig.RFIDConfigs[i * 2 + 1].Channel, DataContent.SystemConfig.RFIDConfigs[i * 2 + 1].Port);

                    Form frmcode;
                    frmcode = new FrmFixture(RFIDChannelFixtrue, RFIDChannelCable,
                        (fixture, cable) => { return ScannerCodeByPeopleSaveCSV(fixture, new List<string> { cable }); });
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
        private bool ScannerCodeByPeopleSaveCSV(string fixture, List<string> newCables)
        {
            try
            {
                for (int i = 0; i < newCables.Count; i++)
                {
                    var cable = new Cable
                    {
                        Sn = newCables[i],
                        Start_time = DateTime.Now,
                        Finish_time = DateTime.Now,
                        Station = "PASS",
                        Model = DataContent.SystemConfig.Model,
                        Test_station = DataContent.SystemConfig.TestStation,
                        FixtureID = DataContent.SystemConfig.FixtureID,
                        FAI1_A = fixture,
                    };
                    var dt = cable.ToTable();
                    CSVHelper.SaveCSV(dt, DataContent.SystemConfig.CSVPath + "//" + Guid.NewGuid() + ".csv");
                }
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
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "前工站载具扫码RFID";
                    gbxRFID2.Text = "前工站线材扫码RFID";
                    gbxRFID3.Text = "后工站载具扫码RFID";
                    gbxRFID4.Text = "后工站线材扫码RFID";
                    gbxPLC.Visible = false;
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
