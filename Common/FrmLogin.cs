using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Common
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            
            lblTip.Text = string.Empty;
            lblUpdateTip.Text = string.Empty;   
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbxPassword.Text == DataContent.SystemConfig.PassWord)
            {
                DataContent.User = "管理员";
                this.Close();
            }
            else
            {
                lblTip.Text = "输入密码有误";
                lblTip.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbxNewPassword.Text))
            {
                lblUpdateTip.Text = "新密码不能为空。";
                lblUpdateTip.ForeColor = Color.Red;
                return;
            }
            if (tbxOldPassword.Text == DataContent.SystemConfig.PassWord)
            {
                if (tbxNewPassword.Text == tbxConfigPassword.Text)
                {
                    DataContent.SystemConfig.PassWord= tbxNewPassword.Text;
                    DataContent.SetConfig(DataContent.SystemConfig);
                    lblUpdateTip.Text= "修改成功";
                    lblUpdateTip.ForeColor = Color.Green;
                }
            }
            else
            {
                lblUpdateTip.Text = "输入密码有误";
                lblUpdateTip.ForeColor = Color.Red;
            }
        }

        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            tbxPassword.Focus();
        }
    }
}
