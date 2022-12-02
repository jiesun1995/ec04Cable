using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EC0401前处理
{
    public partial class FrmRFIDInputCable : Form
    {

        private readonly MesService _mesService;
        private string code;
        public bool result = false;
        private RFIDChannel _RFIDChannel;
        public FrmRFIDInputCable(int channelId, RFIDChannel RFIDChannel, MesService mesService)
        {
            _RFIDChannel = RFIDChannel;
            _mesService = mesService;
            InitializeComponent();
        }
        private void FrmRFIDInputCable_Load(object sender, EventArgs e)
        {
            _RFIDChannel.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (state)
                    {
                        groupBox1.BackColor = System.Drawing.Color.Yellow;
                        var content = _RFIDChannel.Read();
                        LogManager.Info($"读取电子标签:{content};");
                        if (code == content)
                        {
                            groupBox1.BackColor = System.Drawing.Color.Green;
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(content))
                            code = _mesService.FetchCode();
                        else
                            code = _mesService.FetchCode(content);

                        if (code != content)
                        {
                            try
                            {
                                var result = _RFIDChannel.Wirte(code);
                                if (result)
                                    groupBox1.BackColor = System.Drawing.Color.Green;
                                else
                                    groupBox1.BackColor = System.Drawing.Color.Red;
                            }
                            catch (Exception ex)
                            {
                                LogManager.Warn($"电子标签写入失败，请重试。{ex.Message}");
                                groupBox1.BackColor = System.Drawing.Color.Red;
                                return;
                            }
                            LogManager.Info($"写入电子标签:{code};");
                            tbxCable.Text = code;
                            return;
                        }
                    }
                    else
                    {
                        groupBox1.BackColor = System.Drawing.SystemColors.Control;
                    }
                });
            });
        }
    }
}
