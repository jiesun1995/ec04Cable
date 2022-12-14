using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC0401前处理
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch ;
        private Stopwatch _resetStopwatch;
        public FrmMain()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            _resetStopwatch=new Stopwatch();
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tslSysTime.Text = $"系统时间:{DateTime.Now.ToString("yyyyMMdd HH:mm:ss")}";
            tslRunTime.Text = $"运行时间:{_stopwatch.Elapsed}";

            if (tabControl1.TabPages.Count > 1 && _resetStopwatch.Elapsed >= TimeSpan.FromMinutes(10))
            {
                tabControl1.TabPages.RemoveAt(1);
                _resetStopwatch.Stop();
            }

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
                    Form frmcode;
                    var mesService = new MesService();
                    var channel = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i].IP, DataContent.SystemConfig.RFIDConfigs[i].Channel, (ushort)DataContent.SystemConfig.RFIDConfigs[i].Port);
                    frmcode = new FrmRFIDInputCable(channel, mesService);

                    frmcode.TopLevel = false;
                    frmcode.Dock = DockStyle.Top;
                    frmcode.Width = tabPage1.Width;
                    frmcode.FormBorderStyle = FormBorderStyle.None;

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
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(DataContent.User))
            {
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxRFID5, gbxRFID6, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "感应区读写RFID1";
                    gbxRFID2.Text = "感应区读写RFID2";
                    gbxRFID3.Visible = false;
                    gbxRFID4.Visible = false;
                    gbxRFID5.Visible = false;
                    gbxRFID6.Visible = false;
                    gbxStation.Visible= true;
                    gbxPLC.Visible= false;
                    gbxWCF.Visible= false;
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
                if (tabControl1.TabPages.Count > 1)
                {
                    tabControl1.TabPages.RemoveAt(1);
                }
            }
            else
            {
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();
                if (!string.IsNullOrEmpty(DataContent.User))
                {
                    btnLogin.Text = "退出权限";
                }
                if (DataContent.User == "管理员")
                {
                    Form frmcode;
                    var mesService = new MesService();
                    var channel = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, (ushort)DataContent.SystemConfig.RFIDConfigs[0].Port);
                    frmcode = new FrmResetRFID(channel, mesService, () => _resetStopwatch.Restart());

                    frmcode.TopLevel = false;
                    frmcode.Dock = DockStyle.Fill;
                    frmcode.FormBorderStyle = FormBorderStyle.None;
                    
                    TabPage tabPage=new TabPage();
                    tabPage.Text = "RFID重置";
                    tabPage.Controls.Add(frmcode);
                    frmcode.Show();
                    tabControl1.TabPages.Add(tabPage);
                    _resetStopwatch.Restart();
                }
            }
        }
    }
}
