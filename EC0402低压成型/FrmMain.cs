using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC0402低压成型
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch;
        //private static int _peopleScannerCodeCount = 1;
        public FrmMain()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            InitializeComponent();
            timer1.Start();
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
                Form frmcode;
                MesService mesService = new MesService();
                if (ConfigurationManager.AppSettings["Input"] != null)
                {
                    RFIDChannel channel=null;
                    try
                    {
                        channel = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                    }
                    catch (Exception ex)
                    {
                        LogManager.Error(ex);
                    }
                    frmcode = new FrmRFIDGetWayFixture(channel, (fixture, cable1, cable2) => { return ScannerCodeByPeopleSaveCSV(fixture, new List<string> { cable1, cable2 }); }, mesService);
                }
                else
                {
                    frmcode = new FrmFixture((fixture, cable1, cable2) => { return ScannerCodeByPeopleSaveCSV(fixture, new List<string> { cable1, cable2 }); }, mesService);
                }
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
            tabPage1.Controls.Add(tableLayoutPanel);
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
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxRFID5, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Visible = true;
                    gbxRFID2.Visible = false;
                    gbxRFID3.Visible = false;
                    gbxRFID4.Visible = false;
                    gbxRFID5.Visible = false;
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
