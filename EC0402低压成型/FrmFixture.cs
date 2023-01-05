using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EC0402低压成型
{
    public partial class FrmFixture : Form
    {
        private string _fixture = string.Empty;
        private string _cable1 = string.Empty;
        private string _cable2 = string.Empty;
        private string code = string.Empty;
        private Func<string, string, string, bool> _codeCallBack;
        private readonly MesService _mesService;

        public FrmFixture(Func<string, string, string, bool> codeCallBack, MesService mesService)
        {
            _mesService = mesService;
            _codeCallBack = codeCallBack;
            InitializeComponent();
        }
        private void FrmFixture_Load(object sender, EventArgs e)
        {
            tbxFixture.Focus();
        }

        public void ClearUI()
        {
            Task.Factory.StartNew(() =>
            {
                Invoke((EventHandler)delegate
                {
                    groupBox1.BackColor = System.Drawing.SystemColors.Control;
                    tbxFixture.Text = string.Empty;
                    tbxCable1.Text = string.Empty;
                    tbxCable2.Text = string.Empty;
                    tbxFixture.BackColor = SystemColors.Control;
                    tbxCable1.BackColor = SystemColors.Control;
                    tbxCable2.BackColor = SystemColors.Control;
                    tbxFixture.Focus();
                });
            });
        }
        private void SaveData(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                var textBox = sender as TextBox;
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Focus();
                    return;
                }
                try
                {
                    if (textBox.Name != "tbxFixture")
                    {
                        var station = _mesService.GetCurrStation(textBox.Text);
                        if (DataContent.SystemConfig.ConfirmStation != station)
                        {
                            textBox.BackColor = Color.Red;
                            LogManager.Warn($"线材：{textBox.Text}，不在确认工站{station}");
                        }
                        else
                        {
                            textBox.BackColor = Color.Green;
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(ex);
                }

                if (string.IsNullOrWhiteSpace(tbxFixture.Text))
                {
                    tbxFixture.Focus();
                }
                else if (string.IsNullOrWhiteSpace(tbxCable1.Text))
                {
                    tbxCable1.Focus();
                }
                else if (string.IsNullOrWhiteSpace(tbxCable2.Text))
                {
                    tbxCable2.Focus();
                }
                else if (!string.IsNullOrWhiteSpace(tbxCable1.Text) && !string.IsNullOrWhiteSpace(tbxCable2.Text) && !string.IsNullOrWhiteSpace(tbxFixture.Text))
                {
                    var result = _codeCallBack(tbxFixture.Text, tbxCable1.Text, tbxCable2.Text);
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
            }
        }
    }
}
