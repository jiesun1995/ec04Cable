namespace Common
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblTip = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblUpdateTip = new System.Windows.Forms.Label();
            this.tbxNewPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbxConfigPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tbxOldPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(366, 403);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblTip);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.btnLogin);
            this.tabPage1.Controls.Add(this.tbxPassword);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 374);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "登 录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(106, 39);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(55, 15);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "label5";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(212, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 53);
            this.button2.TabIndex = 3;
            this.button2.Text = "取 消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(7, 232);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(84, 53);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "确 定";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbxPassword
            // 
            this.tbxPassword.Location = new System.Drawing.Point(109, 87);
            this.tbxPassword.Name = "tbxPassword";
            this.tbxPassword.PasswordChar = '*';
            this.tbxPassword.Size = new System.Drawing.Size(190, 25);
            this.tbxPassword.TabIndex = 1;
            this.tbxPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "密 码：";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblUpdateTip);
            this.tabPage2.Controls.Add(this.tbxNewPassword);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.tbxConfigPassword);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.btnUpdate);
            this.tabPage2.Controls.Add(this.tbxOldPassword);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(358, 374);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "修 改 密 码";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblUpdateTip
            // 
            this.lblUpdateTip.AutoSize = true;
            this.lblUpdateTip.Location = new System.Drawing.Point(102, 3);
            this.lblUpdateTip.Name = "lblUpdateTip";
            this.lblUpdateTip.Size = new System.Drawing.Size(55, 15);
            this.lblUpdateTip.TabIndex = 12;
            this.lblUpdateTip.Text = "label5";
            // 
            // tbxNewPassword
            // 
            this.tbxNewPassword.Location = new System.Drawing.Point(121, 100);
            this.tbxNewPassword.Name = "tbxNewPassword";
            this.tbxNewPassword.Size = new System.Drawing.Size(190, 25);
            this.tbxNewPassword.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "新 密 码：";
            // 
            // tbxConfigPassword
            // 
            this.tbxConfigPassword.Location = new System.Drawing.Point(121, 147);
            this.tbxConfigPassword.Name = "tbxConfigPassword";
            this.tbxConfigPassword.Size = new System.Drawing.Size(190, 25);
            this.tbxConfigPassword.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "确 认 密 码：";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(212, 222);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 48);
            this.button3.TabIndex = 7;
            this.button3.Text = "取 消";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(7, 222);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(84, 48);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "确 定";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tbxOldPassword
            // 
            this.tbxOldPassword.Location = new System.Drawing.Point(121, 49);
            this.tbxOldPassword.Name = "tbxOldPassword";
            this.tbxOldPassword.Size = new System.Drawing.Size(190, 25);
            this.tbxOldPassword.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "旧 密 码：";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 403);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmLogin";
            this.Text = "权限登陆";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tbxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbxNewPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbxConfigPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox tbxOldPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Label lblUpdateTip;
    }
}