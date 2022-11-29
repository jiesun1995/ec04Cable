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

namespace EC04编织喷胶
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
                    var rfidHelperFixtrue = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[i * 2].IP, DataContent.SystemConfig.RFIDConfigs[i * 2].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                    rfidHelperFixtrue.DataLength_Ch0 = DataContent.SystemConfig.RFIDConfigs[0].DataLength;
                    rfidHelperFixtrue.StartAddress_Ch0 = DataContent.SystemConfig.RFIDConfigs[0].StartAddress;

                    var rfidHelperCable = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[i * 2 + 1].IP, DataContent.SystemConfig.RFIDConfigs[i * 2 + 1].Channel, DataContent.SystemConfig.RFIDConfigs[1].Port);
                    rfidHelperCable.DataLength_Ch1 = DataContent.SystemConfig.RFIDConfigs[1].DataLength;
                    rfidHelperCable.StartAddress_Ch1 = DataContent.SystemConfig.RFIDConfigs[1].StartAddress;

                    rfidHelperFixtrue.DataLength_Ch2 = DataContent.SystemConfig.RFIDConfigs[2].DataLength;
                    rfidHelperFixtrue.StartAddress_Ch2 = DataContent.SystemConfig.RFIDConfigs[2].StartAddress;
                    rfidHelperCable.DataLength_Ch3 = DataContent.SystemConfig.RFIDConfigs[3].DataLength;
                    rfidHelperCable.StartAddress_Ch3 = DataContent.SystemConfig.RFIDConfigs[3].StartAddress;
                    Form frmcode;
                    frmcode = new FrmFixture(rfidHelperFixtrue, rfidHelperCable,
                            DataContent.SystemConfig.RFIDConfigs[i * 2].Channel, DataContent.SystemConfig.RFIDConfigs[i * 2 + 1].Channel,
                        (fixture, cable) => { return ScannerCodeByPeopleSaveCSV(fixture, new List<string> { cable }); });
                    frmcode.TopLevel = false;
                    frmcode.Dock = DockStyle.None;
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
                    var cable = newCables[i];
                    var dt = new DataTable();
                    dt.Columns.Add(new DataColumn("SN"));
                    dt.Columns.Add(new DataColumn("model"));
                    dt.Columns.Add(new DataColumn("fixtureID"));
                    dt.Columns.Add(new DataColumn("test_station"));
                    dt.Columns.Add(new DataColumn("start_time"));
                    dt.Columns.Add(new DataColumn("finish_time"));
                    dt.Columns.Add(new DataColumn("status"));
                    dt.Columns.Add(new DataColumn("error_code"));
                    dt.Columns.Add(new DataColumn("FAI1_A"));
                    dt.Columns.Add(new DataColumn("FAI1_B"));
                    dt.Columns.Add(new DataColumn("FAI1_C"));
                    dt.Columns.Add(new DataColumn("FAI1_D"));
                    dt.Columns.Add(new DataColumn("FAI1_E"));
                    dt.Columns.Add(new DataColumn("FAI1_F"));
                    dt.Columns.Add(new DataColumn("FAI1_G"));
                    dt.Columns.Add(new DataColumn("FAI1_H"));
                    dt.Columns.Add(new DataColumn("Station"));
                    var row = dt.NewRow();
                    row[0] = cable;
                    row[1] = DataContent.SystemConfig.Model;
                    row[2] = DataContent.SystemConfig.FixtureID;
                    row[3] = DataContent.SystemConfig.TestStation;
                    row[4] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    row[5] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    row[6] = "PASS";
                    row[8] = fixture;
                    row[15] = i + 1;
                    dt.Rows.Add(row);
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
            if (DataContent.User == "管理员")
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
                if (DataContent.User == "管理员")
                {
                    btnLogin.Text = "退出权限";
                }
            }
            
        }
    }
}
