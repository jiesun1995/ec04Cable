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

namespace EC04铁壳熔接焊
{
    public partial class FrmFixture : Form
    {
        private Func<string, string, string, bool> _codeCallBack;
        private readonly RFIDHelper _RFIDHelper1;
        private readonly RFIDHelper _RFIDHelper2;
        private readonly RFIDHelper _RFIDHelper3;
        private readonly int _channelId1;
        private readonly int _channelId2;
        private readonly int _channelId3;
        private bool _tp1 = false;
        private bool _tp2 = false;
        private bool _tp3 = false;
        private string _codeFixtrueL = string.Empty;
        private string _codeFixtrueR = string.Empty;
        private string _codeCable = string.Empty;

        public FrmFixture(RFIDHelper RFIdHelp1, RFIDHelper RFIdHelp2, RFIDHelper RFIdHelp3, int channelId1, int channelId2, int channelId3, Func<string, string, string, bool>  codeCallBack)
        {
            _codeCallBack = codeCallBack;
            InitializeComponent();
            _RFIDHelper1 = RFIdHelp1;
            _RFIDHelper2 = RFIdHelp2;
            _RFIDHelper3 = RFIdHelp3;
            _channelId1 = channelId1;
            _channelId2 = channelId2;
            _channelId3 = channelId3;
        }
        private void FrmFixture_Load(object sender, EventArgs e)
        {
            _RFIDHelper1.ChannelStateCallback = state =>
            {
                if (state.Tp != _tp1 && state.ChannelId == _channelId1)
                {
                    _tp1 = state.Tp;
                    if (state.Tp)
                    {
                        _RFIDHelper1.Read(_channelId1);
                        Invoke((EventHandler)delegate
                        {
                            tbxFixtureL.BackColor = System.Drawing.Color.Yellow;
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            tbxFixtureL.BackColor = SystemColors.Control;
                        });
                    }
                }
            };
            _RFIDHelper1.ReadCallback = (channelId, content) =>
            {
                if (channelId != _channelId1) return;
                LogManager.Info($"读取电子标签:{content};");
                if (_codeFixtrueL == content)
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxFixtureL.BackColor = System.Drawing.Color.Green;
                    });
                }
                else
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxFixtureL.Text = content;
                        tbxFixtureL.BackColor = System.Drawing.Color.Green;
                    });
                    _codeFixtrueL = content;
                }
                SaveData();
            };

            _RFIDHelper2.ChannelStateCallback = state =>
            {
                if (state.Tp != _tp2 && state.ChannelId == _channelId2)
                {
                    _tp2 = state.Tp;
                    if (state.Tp)
                    {
                        _RFIDHelper2.Read(_channelId2);
                        Invoke((EventHandler)delegate
                        {
                            tbxFixtureR.BackColor = System.Drawing.Color.Yellow;
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            tbxFixtureR.BackColor = SystemColors.Control;
                        });
                    }
                }
            };
            _RFIDHelper2.ReadCallback = (channelId, content) =>
            {
                if (channelId != _channelId2) return;
                LogManager.Info($"读取电子标签:{content};");
                if (_codeFixtrueR == content)
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxFixtureR.BackColor = System.Drawing.Color.Green;
                    });
                }
                else
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxFixtureR.Text = content;
                        tbxFixtureR.BackColor = System.Drawing.Color.Green;
                    });
                    _codeFixtrueR = content;
                }
                SaveData();
            };

            _RFIDHelper3.ChannelStateCallback = state =>
            {
                if (state.Tp != _tp2 && state.ChannelId == _channelId2)
                {
                    _tp3 = state.Tp;
                    if (state.Tp)
                    {
                        _RFIDHelper3.Read(_channelId3);
                        Invoke((EventHandler)delegate
                        {
                            tbxCable.BackColor = System.Drawing.Color.Yellow;
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            tbxCable.BackColor = SystemColors.Control;
                        });
                    }
                }
            };
            _RFIDHelper3.ReadCallback = (channelId, content) =>
            {
                if (channelId != _channelId3) return;
                LogManager.Info($"读取电子标签:{content};");
                if (_codeCable == content)
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxCable.BackColor = System.Drawing.Color.Green;
                    });
                }
                else
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxCable.Text = content;
                        tbxCable.BackColor = System.Drawing.Color.Green;
                    });
                    _codeCable = content;
                }
                SaveData();
            };
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
                    tbxFixtureL.BackColor= System.Drawing.SystemColors.Control;
                    tbxFixtureR.BackColor = System.Drawing.SystemColors.Control;
                    tbxCable.BackColor = System.Drawing.SystemColors.Control;
                });
            });
        }
        private void SaveData()
        {
            Task.Factory.StartNew(() =>
            {
                //Invoke((EventHandler)delegate
                //{
                //if (string.IsNullOrWhiteSpace(_codeFixtrueL))
                //{
                //    tbxFixtureL.Focus();
                //}
                //else if (string.IsNullOrWhiteSpace(_codeFixtrueR))
                //{
                //    tbxFixtureR.Focus();
                //}
                //else if (string.IsNullOrWhiteSpace(_codeCable))
                //{
                //    tbxCable.Focus();
                //}
                //else
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
                //});
            });
        }
    }
}
