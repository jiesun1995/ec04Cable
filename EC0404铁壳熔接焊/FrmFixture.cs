using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
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
        private readonly IPLCReadWrite _plcHelper;
        private readonly int _address = 4035;

        public FrmFixture(RFIDChannel RFIDChannelL, RFIDChannel RFIDChannelR, RFIDChannel RFIDChannelCable, MesService mesService, IPLCReadWrite plcHelper, Func<string, string, string, bool> codeCallBack)
        {
            _plcHelper = plcHelper;
            _mesService = mesService;
            _codeCallBack = codeCallBack;
            InitializeComponent();
            _RFIDChannelL = RFIDChannelL;
            _RFIDChannelR = RFIDChannelR;
            _RFIDChannelCable = RFIDChannelCable;
        }
        private void FrmFixture_Load(object sender, EventArgs e)
        {
            if (_RFIDChannelL == null || _RFIDChannelR == null || _RFIDChannelCable == null) return;
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (_plcHelper.Read(_address) == 1)
                    {
                        LogManager.Info($"读取地址【D{_address}:1】");
                        try
                        {

                            _plcHelper.Write(_address, 9);
                            _codeFixtrueL = _RFIDChannelL.Read();
                            _codeFixtrueR = _RFIDChannelR.Read();
                            Invoke((EventHandler)delegate
                            {
                                tbxFixtureL.Text = _codeFixtrueL;
                                tbxFixtureR.Text = _codeFixtrueR;
                                groupBox1.BackColor= SystemColors.Control;
                                tbxFixtureL.BackColor = Color.Yellow;
                                tbxFixtureR.BackColor = Color.Yellow;
                            });
                            SaveData();
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                            _plcHelper.Write(_address, 3);
                        }
                    }

                    Thread.Sleep(100);
                }
            },TaskCreationOptions.LongRunning);

            _RFIDChannelCable.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (state)
                    {
                        var result = false;
                        tbxCable.BackColor = System.Drawing.Color.Yellow;
                        groupBox1.BackColor = SystemColors.Control;
                        var content = _RFIDChannelCable.Read();
                        try
                        {
                            result = _mesService.GetCurrStation(content) == DataContent.SystemConfig.ConfirmStation;
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                        if (!string.IsNullOrWhiteSpace(DataContent.SystemConfig.CableStr) && content.IndexOf(DataContent.SystemConfig.CableStr) <= 0)
                        {
                            LogManager.Error($"请输入正确的线材码：{content}");
                            return;
                        }
                        if (_codeCable == content)
                        {
                            tbxCable.BackColor = result ? System.Drawing.Color.Yellow : Color.Yellow;
                        }
                        else
                        {
                            tbxCable.Text = content;
                            tbxCable.BackColor = result ? System.Drawing.Color.Yellow : Color.Yellow;
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
                    groupBox1.BackColor = Color.Green;
                    //tbxFixtureL.Text = string.Empty;
                    //tbxFixtureR.Text = string.Empty;
                    //tbxCable.Text = string.Empty;
                    _codeFixtrueL = string.Empty;
                    _codeFixtrueR = string.Empty;
                    _codeCable = string.Empty;
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
                    _plcHelper.Write(_address, 2);

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
