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

namespace EC04编织喷胶
{
    public partial class FrmFixture : Form
    {
        private Func<string, string, bool> _codeCallBack;
        private readonly RFIDHelper _RFIDHelper1;
        private readonly RFIDHelper _RFIDHelper2;
        private readonly int _channelId1;
        private readonly int _channelId2;
        private bool _tp1 = false;
        private bool _tp2 = false;
        private string _codeFixtrue = string.Empty;
        private string _codeCable = string.Empty;

        public FrmFixture(RFIDHelper RFIdHelp1, RFIDHelper RFIdHelp2, int channelId1, int channelId2, Func<string, string, bool>  codeCallBack)
        {
            _codeCallBack = codeCallBack;
            InitializeComponent();
            _RFIDHelper1 = RFIdHelp1;
            _RFIDHelper2 = RFIdHelp2;
            _channelId1 = channelId1;
            _channelId2 = channelId2;
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
                            tbxFixture.BackColor = System.Drawing.Color.Yellow;
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            tbxFixture.BackColor = SystemColors.Control;
                        });
                    }
                }
            };
            _RFIDHelper1.ReadCallback = (channelId, content) =>
            {
                if (channelId != _channelId1) return;
                LogManager.Info($"读取电子标签:{content};");
                if (_codeFixtrue == content)
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxFixture.BackColor = System.Drawing.Color.Green;
                    });
                }
                else
                {
                    Invoke((EventHandler)delegate
                    {
                        tbxFixture.Text = content;
                        tbxFixture.BackColor = System.Drawing.Color.Green;
                    });
                    _codeFixtrue = content;
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
            _RFIDHelper2.ReadCallback = (channelId, content) =>
            {
                if (channelId != _channelId2) return;
                LogManager.Info($"读取电子标签:{content};");
                if (_codeFixtrue == content)
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
                    _codeFixtrue = content;
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
                //});
            });
        }
    }
}
