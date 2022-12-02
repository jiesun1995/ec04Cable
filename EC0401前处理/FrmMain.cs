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

namespace EC0401前处理
{
    public partial class FrmMain : Form
    {
        private Stopwatch _stopwatch ;
        private static int _peopleScannerCodeCount = 1;
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
            var zhanbi = 1.00 / _peopleScannerCodeCount;
            ///动态加载人工扫码位显示界面
            for (int i = 0; i < _peopleScannerCodeCount; i++)
            {
                try
                {
                    Form frmcode;
                    var mesService = new MesService();
                    var channel = RFIDFactory.Instance(DataContent.SystemConfig.RFIDConfigs[i].IP, DataContent.SystemConfig.RFIDConfigs[i].Channel, (ushort)DataContent.SystemConfig.RFIDConfigs[i].Port);
                    frmcode = new FrmRFIDInputCable(DataContent.SystemConfig.RFIDConfigs[i].Channel, channel, mesService);

                    frmcode.TopLevel = false;
                    frmcode.Dock = DockStyle.Top;
                    frmcode.Width = tabPage1.Width;
                    frmcode.FormBorderStyle = FormBorderStyle.None;

                    var a = Convert.ToInt32((tabPage1.Height * zhanbi));
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (DataContent.User == "管理员")
            {
                Common.FrmSetting frmSetting = new Common.FrmSetting((gbxRFID1, gbxRFID2, gbxRFID3, gbxRFID4, gbxStation, gbxPLC, gbxWCF) =>
                {
                    gbxRFID1.Text = "感应区读写RFID";
                    gbxRFID2.Visible= false;
                    gbxRFID3.Visible= false;
                    gbxRFID4.Visible= false;
                    gbxStation.Visible= false;
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
