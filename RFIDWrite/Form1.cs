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

namespace RFIDWrite
{
    public partial class Form1 : Form
    {
        private RFIDChannel _RFIDChannel;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            plFixture.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbxRFIDIP.Text))
            {
                MessageBox.Show("RFID ip 不能为空");
                return;
            }
            _RFIDChannel = RFIDFactory.Instance(tbxRFIDIP.Text, int.Parse(nudChannel.Value.ToString()));

            if(_RFIDChannel != null )
            {
                plFixture.Enabled= true;
                _RFIDChannel.SetChannelState(result =>
                {
                    this.Invoke(new Action(() =>
                    {
                        if (result)
                        {
                            lblOldCode.Text = _RFIDChannel.Read();
                            tbxFixture.Enabled=true;
                            btnWrite.Enabled=true;
                        }
                        else
                        {
                            lblOldCode.Text = string.Empty;
                            tbxFixture.Enabled = false;
                            btnWrite.Enabled = false;
                        }
                    }));
                });
            }
            else
            {
                plFixture.Enabled = false;
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbxFixture.Text))
            {
                MessageBox.Show("治具码不能为空");
                return;
            }
            var result = _RFIDChannel.Wirte(tbxFixture.Text);
            lblOldCode.Text = result? "写入成功":"写入失败";
            if(result)
            {
                lblOldCode.Text = "写入成功";
                lblOldCode.BackColor= Color.Green;
            }
            else
            {
                lblOldCode.Text = "写入失败";
                lblOldCode.BackColor = Color.Red;
            }
        }
    }
}
