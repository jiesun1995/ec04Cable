namespace RFIDWrite
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFixture = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tbxRFIDIP = new System.Windows.Forms.TextBox();
            this.nudChannel = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.gbxConfig = new System.Windows.Forms.GroupBox();
            this.plFixture = new System.Windows.Forms.Panel();
            this.lblOldCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).BeginInit();
            this.gbxConfig.SuspendLayout();
            this.plFixture.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "治具码：";
            // 
            // tbxFixture
            // 
            this.tbxFixture.Location = new System.Drawing.Point(114, 62);
            this.tbxFixture.Name = "tbxFixture";
            this.tbxFixture.Size = new System.Drawing.Size(388, 25);
            this.tbxFixture.TabIndex = 1;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(114, 113);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(88, 37);
            this.btnWrite.TabIndex = 2;
            this.btnWrite.Text = "写 入";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "rfid ip:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(114, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 32);
            this.button2.TabIndex = 5;
            this.button2.Text = "连 接";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbxRFIDIP
            // 
            this.tbxRFIDIP.Location = new System.Drawing.Point(114, 37);
            this.tbxRFIDIP.Name = "tbxRFIDIP";
            this.tbxRFIDIP.Size = new System.Drawing.Size(388, 25);
            this.tbxRFIDIP.TabIndex = 4;
            // 
            // nudChannel
            // 
            this.nudChannel.Location = new System.Drawing.Point(114, 78);
            this.nudChannel.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudChannel.Name = "nudChannel";
            this.nudChannel.Size = new System.Drawing.Size(120, 25);
            this.nudChannel.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "rfid 通道:";
            // 
            // gbxConfig
            // 
            this.gbxConfig.Controls.Add(this.tbxRFIDIP);
            this.gbxConfig.Controls.Add(this.label3);
            this.gbxConfig.Controls.Add(this.label2);
            this.gbxConfig.Controls.Add(this.nudChannel);
            this.gbxConfig.Controls.Add(this.button2);
            this.gbxConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxConfig.Location = new System.Drawing.Point(0, 0);
            this.gbxConfig.Name = "gbxConfig";
            this.gbxConfig.Size = new System.Drawing.Size(708, 191);
            this.gbxConfig.TabIndex = 8;
            this.gbxConfig.TabStop = false;
            this.gbxConfig.Text = "RFID配置";
            // 
            // plFixture
            // 
            this.plFixture.Controls.Add(this.lblOldCode);
            this.plFixture.Controls.Add(this.tbxFixture);
            this.plFixture.Controls.Add(this.label1);
            this.plFixture.Controls.Add(this.btnWrite);
            this.plFixture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plFixture.Location = new System.Drawing.Point(0, 191);
            this.plFixture.Name = "plFixture";
            this.plFixture.Size = new System.Drawing.Size(708, 173);
            this.plFixture.TabIndex = 9;
            // 
            // lblOldCode
            // 
            this.lblOldCode.AutoSize = true;
            this.lblOldCode.Location = new System.Drawing.Point(111, 28);
            this.lblOldCode.Name = "lblOldCode";
            this.lblOldCode.Size = new System.Drawing.Size(67, 15);
            this.lblOldCode.TabIndex = 3;
            this.lblOldCode.Text = "老治具码";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 364);
            this.Controls.Add(this.plFixture);
            this.Controls.Add(this.gbxConfig);
            this.Name = "Form1";
            this.Text = "治具码写入程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudChannel)).EndInit();
            this.gbxConfig.ResumeLayout(false);
            this.gbxConfig.PerformLayout();
            this.plFixture.ResumeLayout(false);
            this.plFixture.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFixture;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbxRFIDIP;
        private System.Windows.Forms.NumericUpDown nudChannel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbxConfig;
        private System.Windows.Forms.Panel plFixture;
        private System.Windows.Forms.Label lblOldCode;
    }
}

