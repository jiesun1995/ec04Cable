﻿using Common;
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
                    var rfidHelperL = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[0].IP, DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[0].Port);
                rfidHelperL.DataLength_Ch0 = DataContent.SystemConfig.RFIDConfigs[0].DataLength;
                rfidHelperL.StartAddress_Ch0 = DataContent.SystemConfig.RFIDConfigs[0].StartAddress;

                var rfidHelperR = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[1].IP, DataContent.SystemConfig.RFIDConfigs[1].Channel, DataContent.SystemConfig.RFIDConfigs[1].Port);
                rfidHelperR.DataLength_Ch1 = DataContent.SystemConfig.RFIDConfigs[1].DataLength;
                rfidHelperR.StartAddress_Ch1 = DataContent.SystemConfig.RFIDConfigs[1].StartAddress;

                var rfidHelperCable = new RFIDHelper(DataContent.SystemConfig.RFIDConfigs[2].IP, DataContent.SystemConfig.RFIDConfigs[2].Channel, DataContent.SystemConfig.RFIDConfigs[2].Port);
                rfidHelperCable.DataLength_Ch2 = DataContent.SystemConfig.RFIDConfigs[2].DataLength;
                rfidHelperCable.StartAddress_Ch2 = DataContent.SystemConfig.RFIDConfigs[2].StartAddress;

                
                    Form frmcode;
                    frmcode = new FrmFixture(rfidHelperL, rfidHelperR, rfidHelperCable,
                        DataContent.SystemConfig.RFIDConfigs[0].Channel, DataContent.SystemConfig.RFIDConfigs[1].Channel, DataContent.SystemConfig.RFIDConfigs[2].Channel,
                        (fixture, cable1, cable2) => ScannerCodeByPeople(fixture, new List<string> { cable1, cable2 }));
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
                catch (Exception ex )
                {
                    LogManager.Error(ex);
                }
            }

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
                FrmSetting frmSetting = new FrmSetting();
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
