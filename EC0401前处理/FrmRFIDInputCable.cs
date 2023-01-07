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

            if (_RFIDChannel == null) return;
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
                            tbxCable.Text = content;
                           
                            if (code == content)
                            {
                                groupBox1.BackColor = System.Drawing.Color.Green;
                                lblTip.Text = $"PASS";
                                lblTip.BackColor = Color.Green;
                                return;
                            }
                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                if (!string.IsNullOrWhiteSpace(DataContent.SystemConfig.CableStr) && content.IndexOf(DataContent.SystemConfig.CableStr) <= 0)
                                {
                                    throw new Exception($"请输入正确的线材码：{content}");
                                }
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
                                    throw new Exception($"产品不是确认工站：【{station}：{DataContent.SystemConfig.ConfirmStation}】");
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
                                    {
                                        groupBox1.BackColor = System.Drawing.Color.Green;
                                        lblTip.Text = $"PASS";
                                        lblTip.BackColor = Color.Green;
                                    }
                                    else
                                    {
                                        throw new Exception($"RFID写入失败");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new Exception($"RFID写入失败. \n{ex.Message}");
                                }
                                tbxCable.Text = code;
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblTip.Text = $"Fail：{ex.Message}";
                            lblTip.BackColor = Color.Red;
                            groupBox1.BackColor = System.Drawing.Color.Red;
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
            var result = false;
            try
            {
                result = _mesService.ValidationInvoices(tbxInvoices.Text);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex);
            }
            if (result)
            {
                IsRead = true;
                tbxCable.Enabled = true;
                tbxInvoices.Enabled = false;
                lblTip.Text = $"工单验证成功";
                lblTip.BackColor = Color.Green;
                groupBox1.BackColor = System.Drawing.Color.Green;
                
            }
            else
            {
                IsRead = false;
                tbxCable.Enabled = false;
                tbxInvoices.Enabled = true;
                lblTip.Text = $"工单验证失败：工单无可用SN";
                lblTip.BackColor = Color.Red;
                groupBox1.BackColor = System.Drawing.Color.Red;
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
