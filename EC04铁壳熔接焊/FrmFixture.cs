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

namespace EC04铁壳熔接焊
{
    public partial class FrmFixture : Form
    {
        private string _fixture = string.Empty;
        private string _cable1 = string.Empty;
        private string _cable2 = string.Empty;
        private Func<string, string, string, bool> _codeCallBack;

        public FrmFixture(Func<string, string, string, bool>  codeCallBack)
        {
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
                    tbxFixture.Focus();
                });
            });
        }
        private void tbxFixture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveData();
            }
        }
        private void tbxCable1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveData();
            }
        }
        
        private void tbxCable2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveData();
            }
        }
        private void SaveData()
        {
            Task.Factory.StartNew(() =>
            {
                Invoke((EventHandler)delegate
                {
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
                });
            });
        }
    }
}
