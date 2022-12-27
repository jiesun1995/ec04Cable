using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;

namespace EC0404铁壳熔接焊
{
    public partial class FrmFixture : Form
    {
        private Func<string, string, string, bool> _codeCallBack;
        private string _codeFixtrueL = string.Empty;
        private string _codeFixtrueR = string.Empty;
        private string _codeCable = string.Empty;

        private readonly MesService _mesService;
        private readonly RFIDChannel _RFIDChannelL;
        private readonly RFIDChannel _RFIDChannelR;
        private readonly RFIDChannel _RFIDChannelCable;
        private readonly OMRHelper _omrHelper;
        private readonly string _address = "";

        public FrmFixture(RFIDChannel RFIDChannelL, RFIDChannel RFIDChannelR, RFIDChannel RFIDChannelCable, MesService mesService, OMRHelper omrHelper, Func<string, string, string, bool> codeCallBack)
        {
            _omrHelper = omrHelper;
            _mesService = mesService;
            _codeCallBack = codeCallBack;
            InitializeComponent();
            _RFIDChannelL = RFIDChannelL;
            _RFIDChannelR = RFIDChannelR;
            _RFIDChannelCable = RFIDChannelCable;
        }
        private void FrmFixture_Load(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_omrHelper.Read(_address) == 1)
                    {
                        try
                        {

                            _codeFixtrueL = _RFIDChannelL.Read();
                            _codeFixtrueR = _RFIDChannelR.Read();
                            SaveData();
                            _omrHelper.Write(_address, 2);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                            _omrHelper.Write(_address, 3);
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
            //            tbxFixtureL.BackColor = System.Drawing.Color.Yellow;
            //            var content = _RFIDChannelL.Read();
            //            if (_codeFixtrueL == content)
            //            {
            //                tbxFixtureL.BackColor = System.Drawing.Color.Green;
            //            }
            //            else
            //            {
            //                tbxFixtureL.Text = content;
            //                tbxFixtureL.BackColor = System.Drawing.Color.Green;
            //                _codeFixtrueL = content;
            //            }
            //            SaveData();
            //        }
            //        else
            //        {
            //            tbxFixtureL.BackColor = SystemColors.Control;
            //        }
            //    });
            //});
            //_RFIDChannelR.SetChannelState(state =>
            //{
            //    Invoke((EventHandler)delegate
            //    {
            //        if (state)
            //        {
            //            tbxFixtureR.BackColor = System.Drawing.Color.Yellow;
            //            var content = _RFIDChannelR.Read();
            //            if (_codeFixtrueR == content)
            //            {
            //                tbxFixtureR.BackColor = System.Drawing.Color.Green;
            //            }
            //            else
            //            {
            //                tbxFixtureR.Text = content;
            //                tbxFixtureR.BackColor = System.Drawing.Color.Green;
            //                _codeFixtrueR = content;
            //            }
            //            SaveData();
            //        }
            //        else
            //        {
            //            tbxFixtureR.BackColor = SystemColors.Control;
            //        }
            //    });
            //});
            _RFIDChannelCable.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (state)
                    {
                        var result = false;
                        tbxCable.BackColor = System.Drawing.Color.Yellow;
                        var content = _RFIDChannelCable.Read();
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
                    tbxFixtureL.Text = string.Empty;
                    tbxFixtureR.Text = string.Empty;
                    tbxCable.Text = string.Empty;
                    tbxFixtureL.BackColor = System.Drawing.SystemColors.Control;
                    tbxFixtureR.BackColor = System.Drawing.SystemColors.Control;
                    tbxCable.BackColor = System.Drawing.SystemColors.Control;
                });
            });
        }
        private void SaveData()
        {
            Task.Factory.StartNew(() =>
            {
                if (!string.IsNullOrWhiteSpace(_codeFixtrueL) && !string.IsNullOrWhiteSpace(_codeFixtrueR) && !string.IsNullOrWhiteSpace(_codeCable))
                {
                    var result = _codeCallBack(_codeFixtrueL, _codeFixtrueR, _codeCable);
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
