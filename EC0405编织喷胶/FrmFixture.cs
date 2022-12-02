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

namespace EC0405编织喷胶
{
    public partial class FrmFixture : Form
    {
        private Func<string, string, bool> _codeCallBack;
        private readonly RFIDChannel _RFIDChannelL;
        private readonly RFIDChannel _RFIDChannelR;
        private string _codeFixtrue = string.Empty;
        private string _codeCable = string.Empty;

        public FrmFixture(RFIDChannel RFIDChannelL, RFIDChannel RFIDChannelR, Func<string, string, bool>  codeCallBack)
        {
            _codeCallBack = codeCallBack;
            InitializeComponent();
            _RFIDChannelL = RFIDChannelL;
            _RFIDChannelR = RFIDChannelR;
        }
        private void FrmFixture_Load(object sender, EventArgs e)
        {
            _RFIDChannelL.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (state)
                    {
                        tbxFixture.BackColor = System.Drawing.Color.Yellow;
                        var content = _RFIDChannelL.Read();
                        LogManager.Info($"读取电子标签:{content};");
                        if (_codeFixtrue == content)
                        {
                            tbxFixture.BackColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            tbxFixture.Text = content;
                            tbxFixture.BackColor = System.Drawing.Color.Green;
                            _codeFixtrue = content;
                        }
                        SaveData();
                    }
                    else
                    {
                        tbxFixture.BackColor = SystemColors.Control;
                    }
                });
            });

            _RFIDChannelR.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (state)
                    {
                        tbxCable.BackColor = System.Drawing.Color.Yellow;
                        var content = _RFIDChannelR.Read();
                        LogManager.Info($"读取电子标签:{content};");
                        if (_codeCable == content)
                        {
                            tbxCable.BackColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            tbxCable.Text = content;
                            tbxCable.BackColor = System.Drawing.Color.Green;
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
