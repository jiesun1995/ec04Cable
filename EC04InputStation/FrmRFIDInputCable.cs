using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EC04InputStation
{
    public partial class FrmRFIDInputCable : Form
    {

        private int _channelId = 0;
        private readonly MesService _mesService;
        private string code;
        public bool result = false;
        private bool _tp = false;

        private RFIDHelper _RFIDHelper;
        public FrmRFIDInputCable(int channelId, RFIDHelper RFIDHelper, MesService mesService)
        {
            _RFIDHelper = RFIDHelper;
            _channelId = channelId;
            _mesService = mesService;
            InitializeComponent();
        }
        private void FrmRFIDInputCable_Load(object sender, EventArgs e)
        {
            _RFIDHelper.ReadCallback = (channel, content) =>
            {
                if (channel != _channelId) return;
                LogManager.Info($"读取电子标签:{content};");
                if (code == content)
                {
                    Invoke((EventHandler)delegate
                    {
                        groupBox1.BackColor = System.Drawing.Color.Green;
                    });
                    return;
                }

                if (string.IsNullOrWhiteSpace(content))
                    code = _mesService.FetchCode();
                else
                    code = _mesService.FetchCode(content);
                if (content != code)
                {
                    switch (_channelId)
                    {
                        case 0: _RFIDHelper.DataLength_Ch0 = code.Length; break;
                        case 1: _RFIDHelper.DataLength_Ch1 = code.Length; break;
                        case 2: _RFIDHelper.DataLength_Ch2 = code.Length; break;
                        case 3: _RFIDHelper.DataLength_Ch3 = code.Length; break;
                        default:
                            _RFIDHelper.DataLength_Ch0 = code.Length;
                            break;
                    }
                    try
                    {
                        _RFIDHelper.Write(_channelId, code);
                    }
                    catch (Exception ex)
                    {
                        LogManager.Warn($"电子标签写入失败，请重试。{ex.Message}");
                        Invoke((EventHandler)delegate
                        {
                            groupBox1.BackColor = System.Drawing.Color.Red;
                        });
                        return;
                    }
                    LogManager.Info($"写入电子标签:{code};");
                    Invoke((EventHandler)delegate { tbxCable.Text = code; });
                    _tp = false;
                    return;
                }
            };
            _RFIDHelper.ChannelStateCallback = (state) =>
            {
                if (state.Tp != _tp && state.ChannelId == _channelId)
                {
                    _tp = state.Tp;
                    if (state.Tp)
                    {
                        _RFIDHelper.Read(_channelId);
                        Invoke((EventHandler)delegate
                        {
                            groupBox1.BackColor = System.Drawing.Color.Yellow;
                        });
                    }
                    else
                    {
                        Invoke((EventHandler)delegate
                        {
                            groupBox1.BackColor = System.Drawing.SystemColors.Control;
                        });
                    }
                }
            };
        }
    }
}
