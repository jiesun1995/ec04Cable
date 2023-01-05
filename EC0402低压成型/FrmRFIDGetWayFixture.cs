using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace EC0402低压成型
{
    public partial class FrmRFIDGetWayFixture : Form
    {
        private string _fixture = string.Empty;
        private string _cable1 = string.Empty;
        private string _cable2 = string.Empty;
        private string _code = string.Empty;
        private Func<string, string, string, bool> _codeCallBack;
        private readonly MesService _mesService;
        private readonly RFIDChannel _RFIDChannel;
        private int _indnex = 0;
        public FrmRFIDGetWayFixture(RFIDChannel channel, Func<string, string, string, bool> codeCallBack, MesService mesService)
        {
            InitializeComponent();
            _RFIDChannel = channel;
            _codeCallBack = codeCallBack;
            _mesService= mesService;
        }
        private void ClearUI()
        {
            Task.Factory.StartNew(() =>
            {
                Invoke(new Action(() => 
                {
                    //groupBox1.BackColor = System.Drawing.SystemColors.Control;
                    _fixture = string.Empty;
                    _cable1 = string.Empty;
                    _cable2 = string.Empty;
                    tbxFixture.BackColor = SystemColors.Control;
                    tbxCable1.BackColor = SystemColors.Control;
                    tbxCable2.BackColor = SystemColors.Control;
                    tbxFixture.Focus();
                    _indnex = 0;
                }));
            });
        }

        private void FrmRFIDGetWayFixture_Load(object sender, EventArgs e)
        {
            if (_RFIDChannel == null) return;
            _RFIDChannel.SetChannelState(state =>
            {
                this.Invoke(new Action(() =>
                {
                    if (state)
                    {
                        groupBox1.BackColor = SystemColors.Control;
                        var code= _RFIDChannel.Read();
                        if (_code != code)
                        {
                            _code = code;
                        }
                        else
                        {
                            LogManager.Info($"重复数据，{code}");
                            return;
                        }
                        switch (_indnex)
                        {
                            case 0:
                                {
                                    tbxCable1.Text = _cable1;
                                    tbxCable2.Text = _cable2;
                                    _fixture = _code;
                                    tbxFixture.Text = _fixture;
                                }
                                 break;
                            case 1: _cable1 = _code; tbxCable1.Text = _cable1; break;
                            case 2: _cable2 = _code; tbxCable2.Text = _cable2; break;
                            default:
                                break;
                        }
                        _indnex++;
                        if (_indnex > 2)
                        {
                            var result = _codeCallBack(_fixture,_cable1,_cable2);
                            if (result)
                            {
                                groupBox1.BackColor = System.Drawing.Color.Green;
                            }
                            else
                            {
                                groupBox1.BackColor = System.Drawing.Color.Red;
                            }
                            ClearUI();
                        }
                    }
                }));

            });
        }
    }
}
