namespace Common
{
    partial class FrmRFIDSetting
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.btnTestConnect = new System.Windows.Forms.Button();
            this.tbxIP1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxStartAddress1 = new System.Windows.Forms.TextBox();
            this.tbxChannel1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxDataLength1 = new System.Windows.Forms.TextBox();
            this.tbxPort1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 185);
            this.panel1.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblContent, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.btnTestConnect, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbxIP1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbxStartAddress1, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbxChannel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbxDataLength1, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbxPort1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66666F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 185);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(108, 150);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(0, 15);
            this.lblContent.TabIndex = 10;
            // 
            // btnTestConnect
            // 
            this.btnTestConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestConnect.Location = new System.Drawing.Point(3, 153);
            this.btnTestConnect.Name = "btnTestConnect";
            this.btnTestConnect.Size = new System.Drawing.Size(99, 29);
            this.btnTestConnect.TabIndex = 11;
            this.btnTestConnect.Text = "测试读取";
            this.btnTestConnect.UseVisualStyleBackColor = true;
            this.btnTestConnect.Click += new System.EventHandler(this.btnTestConnect_Click);
            // 
            // tbxIP1
            // 
            this.tbxIP1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxIP1.Location = new System.Drawing.Point(108, 3);
            this.tbxIP1.Name = "tbxIP1";
            this.tbxIP1.Size = new System.Drawing.Size(239, 25);
            this.tbxIP1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "通道：";
            // 
            // tbxStartAddress1
            // 
            this.tbxStartAddress1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxStartAddress1.Location = new System.Drawing.Point(108, 123);
            this.tbxStartAddress1.Name = "tbxStartAddress1";
            this.tbxStartAddress1.Size = new System.Drawing.Size(239, 25);
            this.tbxStartAddress1.TabIndex = 9;
            // 
            // tbxChannel1
            // 
            this.tbxChannel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxChannel1.Location = new System.Drawing.Point(108, 33);
            this.tbxChannel1.Name = "tbxChannel1";
            this.tbxChannel1.Size = new System.Drawing.Size(239, 25);
            this.tbxChannel1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "开始地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "端口：";
            // 
            // tbxDataLength1
            // 
            this.tbxDataLength1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxDataLength1.Location = new System.Drawing.Point(108, 93);
            this.tbxDataLength1.Name = "tbxDataLength1";
            this.tbxDataLength1.Size = new System.Drawing.Size(239, 25);
            this.tbxDataLength1.TabIndex = 7;
            // 
            // tbxPort1
            // 
            this.tbxPort1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxPort1.Location = new System.Drawing.Point(108, 63);
            this.tbxPort1.Name = "tbxPort1";
            this.tbxPort1.Size = new System.Drawing.Size(239, 25);
            this.tbxPort1.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "数据长度：";
            // 
            // FrmRFIDSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 185);
            this.Controls.Add(this.panel1);
            this.Name = "FrmRFIDSetting";
            this.Text = "FrmRFIDSetting";
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Button btnTestConnect;
        private System.Windows.Forms.TextBox tbxIP1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxStartAddress1;
        private System.Windows.Forms.TextBox tbxChannel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxDataLength1;
        private System.Windows.Forms.TextBox tbxPort1;
        private System.Windows.Forms.Label label4;
    }
}