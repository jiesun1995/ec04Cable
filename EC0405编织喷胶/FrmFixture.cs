using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;

namespace EC0405编织喷胶
{
    public partial class FrmFixture : Form
    {
        private Func<string, string, bool> _codeCallBack;
        private readonly RFIDChannel _RFIDChannelL;
        private readonly RFIDChannel _RFIDChannelR;
        private string _codeFixtrue = string.Empty;
        private string _codeCable = string.Empty;
        private readonly MesService _mesService;
        private readonly InovanceHelper _inovanceHelper;
        private readonly int _dAddress;

        public FrmFixture(RFIDChannel RFIDChannelL, RFIDChannel RFIDChannelR, MesService mesService, InovanceHelper inovanceHelper, int dAddress, Func<string, string, bool>  codeCallBack)
        {
            _mesService = mesService;
            _codeCallBack = codeCallBack;
            InitializeComponent();
            _RFIDChannelL = RFIDChannelL;
            _RFIDChannelR = RFIDChannelR;
            _inovanceHelper = inovanceHelper;
            _dAddress = dAddress;
        }
        private void FrmFixture_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_inovanceHelper.read_D_Address(_dAddress) == 1)
                    {
                        try
                        {
                            _codeFixtrue = _RFIDChannelR.Read();
                            SaveData();
                            _inovanceHelper.write_D_Address(_dAddress, 2);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                            _inovanceHelper.write_D_Address(_dAddress, 3);
                        }
                    }
                }
            });


            //_RFIDChannelL.SetChannelState(state =>
            //{
            //    Invoke((EventHandler)delegate
            //    {
            //        if (state)
            //        {
            //            tbxFixture.BackColor = System.Drawing.Color.Yellow;
            //            var content = _RFIDChannelL.Read();
            //            LogManager.Info($"读取治具:{content};");
            //            if (_codeFixtrue == content)
            //            {
            //                tbxFixture.BackColor = System.Drawing.Color.Green;
            //            }
            //            else
            //            {
            //                tbxFixture.Text = content;
            //                tbxFixture.BackColor = System.Drawing.Color.Green;
            //                _codeFixtrue = content;
            //            }
            //            SaveData();
            //        }
            //        else
            //        {
            //            tbxFixture.BackColor = SystemColors.Control;
            //        }
            //    });
            //});

            _RFIDChannelR.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (state)
                    {
                        var result = false;
                        tbxCable.BackColor = System.Drawing.Color.Yellow;
                        var content = _RFIDChannelR.Read();
                        LogManager.Info($"读取线材:{content};");
                        try
                        {
                            result = _mesService.GetCurrStation(content) == DataContent.SystemConfig.ConfirmStation;
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                        if (_codeCable == content)
                        {
                            tbxCable.BackColor = result ? System.Drawing.Color.Green : Color.Yellow;
                        }
                        else
                        {
                            tbxCable.Text = content;
                            tbxCable.BackColor = result ? System.Drawing.Color.Green : Color.Yellow;
                            _codeCable = content;
                        }
                        SaveData();
                    }
                    else
                    {
                        tbxCable.BackColor = SystemColors.Control;
                    }
                });
            });
        }

        public void ClearUI()
        {
            Task.Factory.StartNew(() =>
            {
                Invoke((EventHandler)delegate
                {
                    groupBox1.BackColor = System.Drawing.SystemColors.Control;
                    tbxFixture.Text = string.Empty;
                    tbxCable.Text = string.Empty;
                    tbxFixture.BackColor= System.Drawing.SystemColors.Control;
                    tbxCable.BackColor = System.Drawing.SystemColors.Control;
                });
            });
        }
        private void SaveData()
        {
            Task.Factory.StartNew(() =>
            {
                if (!string.IsNullOrWhiteSpace(_codeFixtrue) && !string.IsNullOrWhiteSpace(_codeCable))
                {
                    var result = _codeCallBack(_codeFixtrue, _codeCable);
                    if (result)
                    {
                        groupBox1.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        groupBox1.BackColor = System.Drawing.Color.Red;
                    }
                    Task.Delay(500).Wait();
                    ClearUI();
                }
            });
        }
    }
}
