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
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace EC0401前处理
{
    public partial class FrmRFIDInputCable : Form
    {
        private readonly MesService _mesService;
        private bool IsRead = false;
        private string code;
        public bool result = false;
        private RFIDChannel _RFIDChannel;
        public FrmRFIDInputCable(RFIDChannel RFIDChannel, MesService mesService)
        {
            _RFIDChannel = RFIDChannel;
            _mesService = mesService;
            InitializeComponent();
        }
        private void FrmRFIDInputCable_Load(object sender, EventArgs e)
        {
            ValidationInvoices();

            _RFIDChannel.SetChannelState(state =>
            {
                Invoke((EventHandler)delegate
                {
                    if (!IsRead) return;
                    if (state)
                    {
                        try
                        {
                            groupBox1.BackColor = System.Drawing.Color.Yellow;
                            var content = _RFIDChannel.Read();
                            LogManager.Info($"读取电子标签:{content};");
                            if (code == content)
                            {
                                groupBox1.BackColor = System.Drawing.Color.Green;
                                return;
                            }
                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                var station = _mesService.GetCurrStation(content);
                                if (string.IsNullOrEmpty(station))
                                {
                                    code = _mesService.GetNewSN(DataContent.SystemConfig.TestStation, tbxInvoices.Text);
                                }
                                else if (DataContent.SystemConfig.ConfirmStation == station)
                                {
                                    code = _mesService.GetNewSN(DataContent.SystemConfig.TestStation, tbxInvoices.Text);
                                }
                                else
                                {
                                    lblTip.Text = $"产品不是确认工站：{{{station}：{DataContent.SystemConfig.ConfirmStation}}}；";
                                    lblTip.BackColor = Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                code = _mesService.GetNewSN(DataContent.SystemConfig.TestStation, tbxInvoices.Text);
                            }

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
                        catch (Exception ex)
                        {
                            LogManager.Error(ex);
                        }
                    }
                    else
                    {
                        lblTip.Text = string.Empty;
                        lblTip.BackColor = SystemColors.Control;
                        groupBox1.BackColor = SystemColors.Control;
                    }
                });
            });
        }

        private void ValidationInvoices()
        {
            if (_mesService.ValidationInvoices(tbxInvoices.Text))
            {
                IsRead = true;
                tbxCable.Enabled = true;
                tbxInvoices.Enabled = false;
            }
            else
            {
                IsRead = false;
                tbxCable.Enabled = false;
                tbxInvoices.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ValidationInvoices();
        }

        private void tbxInvoices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ValidationInvoices();
            }
        }
    }
}
