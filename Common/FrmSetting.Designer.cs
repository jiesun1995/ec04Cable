namespace Common
{
    partial class FrmSetting
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxWCF = new System.Windows.Forms.GroupBox();
            this.gbxWCFClient = new System.Windows.Forms.GroupBox();
            this.tbxWCFClinetPort = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.tbxWCFClinetIP = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.gbxWCFServer = new System.Windows.Forms.GroupBox();
            this.tbxWCFServerPort = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tbxWCFServerIP = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.gbxPLC = new System.Windows.Forms.GroupBox();
            this.tbxPLCPort = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tbxPLCIp = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.gbxStation = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.nudScannerCode = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.tbxCSVPath = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tbxTestStation = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tbxFixtureId = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.tbxModel = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.gbxRFID1 = new System.Windows.Forms.GroupBox();
            this.gbxRFID2 = new System.Windows.Forms.GroupBox();
            this.gbxRFID3 = new System.Windows.Forms.GroupBox();
            this.gbxRFID4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbxWCF.SuspendLayout();
            this.gbxWCFClient.SuspendLayout();
            this.gbxWCFServer.SuspendLayout();
            this.gbxPLC.SuspendLayout();
            this.gbxStation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScannerCode)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.gbxRFID4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxRFID3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxRFID2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxWCF, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbxPLC, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbxStation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.gbxRFID1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1337, 726);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbxWCF
            // 
            this.gbxWCF.Controls.Add(this.gbxWCFClient);
            this.gbxWCF.Controls.Add(this.gbxWCFServer);
            this.gbxWCF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxWCF.Location = new System.Drawing.Point(671, 275);
            this.gbxWCF.Name = "gbxWCF";
            this.gbxWCF.Size = new System.Drawing.Size(328, 448);
            this.gbxWCF.TabIndex = 12;
            this.gbxWCF.TabStop = false;
            this.gbxWCF.Text = "WCF配置";
            // 
            // gbxWCFClient
            // 
            this.gbxWCFClient.Controls.Add(this.tbxWCFClinetPort);
            this.gbxWCFClient.Controls.Add(this.label32);
            this.gbxWCFClient.Controls.Add(this.tbxWCFClinetIP);
            this.gbxWCFClient.Controls.Add(this.label33);
            this.gbxWCFClient.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxWCFClient.Location = new System.Drawing.Point(3, 121);
            this.gbxWCFClient.Name = "gbxWCFClient";
            this.gbxWCFClient.Size = new System.Drawing.Size(322, 100);
            this.gbxWCFClient.TabIndex = 19;
            this.gbxWCFClient.TabStop = false;
            this.gbxWCFClient.Text = "WCF客户端配置";
            // 
            // tbxWCFClinetPort
            // 
            this.tbxWCFClinetPort.Location = new System.Drawing.Point(134, 59);
            this.tbxWCFClinetPort.Name = "tbxWCFClinetPort";
            this.tbxWCFClinetPort.Size = new System.Drawing.Size(184, 25);
            this.tbxWCFClinetPort.TabIndex = 13;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(2, 22);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(111, 15);
            this.label32.TabIndex = 10;
            this.label32.Text = "WCFClinet IP:";
            // 
            // tbxWCFClinetIP
            // 
            this.tbxWCFClinetIP.Location = new System.Drawing.Point(134, 19);
            this.tbxWCFClinetIP.Name = "tbxWCFClinetIP";
            this.tbxWCFClinetIP.Size = new System.Drawing.Size(184, 25);
            this.tbxWCFClinetIP.TabIndex = 11;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(2, 62);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(127, 15);
            this.label33.TabIndex = 12;
            this.label33.Text = "WCFClinet Port:";
            // 
            // gbxWCFServer
            // 
            this.gbxWCFServer.Controls.Add(this.tbxWCFServerPort);
            this.gbxWCFServer.Controls.Add(this.label24);
            this.gbxWCFServer.Controls.Add(this.tbxWCFServerIP);
            this.gbxWCFServer.Controls.Add(this.label21);
            this.gbxWCFServer.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxWCFServer.Location = new System.Drawing.Point(3, 21);
            this.gbxWCFServer.Name = "gbxWCFServer";
            this.gbxWCFServer.Size = new System.Drawing.Size(322, 100);
            this.gbxWCFServer.TabIndex = 18;
            this.gbxWCFServer.TabStop = false;
            this.gbxWCFServer.Text = "WCF服务端配置";
            // 
            // tbxWCFServerPort
            // 
            this.tbxWCFServerPort.Location = new System.Drawing.Point(134, 59);
            this.tbxWCFServerPort.Name = "tbxWCFServerPort";
            this.tbxWCFServerPort.Size = new System.Drawing.Size(184, 25);
            this.tbxWCFServerPort.TabIndex = 13;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(2, 22);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(111, 15);
            this.label24.TabIndex = 10;
            this.label24.Text = "WCFServer IP:";
            // 
            // tbxWCFServerIP
            // 
            this.tbxWCFServerIP.Location = new System.Drawing.Point(134, 19);
            this.tbxWCFServerIP.Name = "tbxWCFServerIP";
            this.tbxWCFServerIP.Size = new System.Drawing.Size(184, 25);
            this.tbxWCFServerIP.TabIndex = 11;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(2, 62);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(127, 15);
            this.label21.TabIndex = 12;
            this.label21.Text = "WCFServer Port:";
            // 
            // gbxPLC
            // 
            this.gbxPLC.Controls.Add(this.tbxPLCPort);
            this.gbxPLC.Controls.Add(this.label26);
            this.gbxPLC.Controls.Add(this.tbxPLCIp);
            this.gbxPLC.Controls.Add(this.label27);
            this.gbxPLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxPLC.Location = new System.Drawing.Point(337, 275);
            this.gbxPLC.Name = "gbxPLC";
            this.gbxPLC.Size = new System.Drawing.Size(328, 448);
            this.gbxPLC.TabIndex = 11;
            this.gbxPLC.TabStop = false;
            this.gbxPLC.Text = "PLC配置";
            // 
            // tbxPLCPort
            // 
            this.tbxPLCPort.Location = new System.Drawing.Point(138, 86);
            this.tbxPLCPort.Name = "tbxPLCPort";
            this.tbxPLCPort.Size = new System.Drawing.Size(184, 25);
            this.tbxPLCPort.TabIndex = 13;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 89);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 15);
            this.label26.TabIndex = 12;
            this.label26.Text = "Port:";
            // 
            // tbxPLCIp
            // 
            this.tbxPLCIp.Location = new System.Drawing.Point(138, 46);
            this.tbxPLCIp.Name = "tbxPLCIp";
            this.tbxPLCIp.Size = new System.Drawing.Size(184, 25);
            this.tbxPLCIp.TabIndex = 11;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 49);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(63, 15);
            this.label27.TabIndex = 10;
            this.label27.Text = "PLC IP:";
            // 
            // gbxStation
            // 
            this.gbxStation.Controls.Add(this.label31);
            this.gbxStation.Controls.Add(this.nudScannerCode);
            this.gbxStation.Controls.Add(this.label30);
            this.gbxStation.Controls.Add(this.tbxCSVPath);
            this.gbxStation.Controls.Add(this.label22);
            this.gbxStation.Controls.Add(this.tbxTestStation);
            this.gbxStation.Controls.Add(this.label23);
            this.gbxStation.Controls.Add(this.tbxFixtureId);
            this.gbxStation.Controls.Add(this.label40);
            this.gbxStation.Controls.Add(this.tbxModel);
            this.gbxStation.Controls.Add(this.label25);
            this.gbxStation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxStation.Location = new System.Drawing.Point(3, 275);
            this.gbxStation.Name = "gbxStation";
            this.gbxStation.Size = new System.Drawing.Size(328, 448);
            this.gbxStation.TabIndex = 4;
            this.gbxStation.TabStop = false;
            this.gbxStation.Text = "站点配置";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("宋体", 6F);
            this.label31.ForeColor = System.Drawing.Color.Brown;
            this.label31.Location = new System.Drawing.Point(135, 251);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(150, 10);
            this.label31.TabIndex = 20;
            this.label31.Text = "为0时代表服务端，监控流道信号";
            // 
            // nudScannerCode
            // 
            this.nudScannerCode.Location = new System.Drawing.Point(138, 207);
            this.nudScannerCode.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudScannerCode.Name = "nudScannerCode";
            this.nudScannerCode.Size = new System.Drawing.Size(184, 25);
            this.nudScannerCode.TabIndex = 19;
            this.nudScannerCode.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudScannerCode.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(9, 209);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(97, 15);
            this.label30.TabIndex = 18;
            this.label30.Text = "人工扫码位：";
            // 
            // tbxCSVPath
            // 
            this.tbxCSVPath.Location = new System.Drawing.Point(138, 166);
            this.tbxCSVPath.Name = "tbxCSVPath";
            this.tbxCSVPath.Size = new System.Drawing.Size(184, 25);
            this.tbxCSVPath.TabIndex = 17;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 169);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(131, 15);
            this.label22.TabIndex = 16;
            this.label22.Text = "MES CSV保存地址:";
            // 
            // tbxTestStation
            // 
            this.tbxTestStation.Location = new System.Drawing.Point(138, 126);
            this.tbxTestStation.Name = "tbxTestStation";
            this.tbxTestStation.Size = new System.Drawing.Size(184, 25);
            this.tbxTestStation.TabIndex = 15;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 129);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(103, 15);
            this.label23.TabIndex = 14;
            this.label23.Text = "TestStation:";
            // 
            // tbxFixtureId
            // 
            this.tbxFixtureId.Location = new System.Drawing.Point(138, 86);
            this.tbxFixtureId.Name = "tbxFixtureId";
            this.tbxFixtureId.Size = new System.Drawing.Size(184, 25);
            this.tbxFixtureId.TabIndex = 13;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(6, 89);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(87, 15);
            this.label40.TabIndex = 12;
            this.label40.Text = "FixtureID:";
            // 
            // tbxModel
            // 
            this.tbxModel.Location = new System.Drawing.Point(138, 46);
            this.tbxModel.Name = "tbxModel";
            this.tbxModel.Size = new System.Drawing.Size(184, 25);
            this.tbxModel.TabIndex = 11;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 49);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(55, 15);
            this.label25.TabIndex = 10;
            this.label25.Text = "Model:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1005, 275);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(329, 448);
            this.panel1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 20F);
            this.button2.Location = new System.Drawing.Point(77, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 68);
            this.button2.TabIndex = 6;
            this.button2.Text = "取 消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 20F);
            this.button1.Location = new System.Drawing.Point(77, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 68);
            this.button1.TabIndex = 5;
            this.button1.Text = "保  存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbxRFID1
            // 
            this.gbxRFID1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRFID1.Location = new System.Drawing.Point(3, 3);
            this.gbxRFID1.Name = "gbxRFID1";
            this.gbxRFID1.Size = new System.Drawing.Size(328, 266);
            this.gbxRFID1.TabIndex = 13;
            this.gbxRFID1.TabStop = false;
            this.gbxRFID1.Text = "groupBox1";
            // 
            // gbxRFID2
            // 
            this.gbxRFID2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRFID2.Location = new System.Drawing.Point(337, 3);
            this.gbxRFID2.Name = "gbxRFID2";
            this.gbxRFID2.Size = new System.Drawing.Size(328, 266);
            this.gbxRFID2.TabIndex = 14;
            this.gbxRFID2.TabStop = false;
            this.gbxRFID2.Text = "groupBox2";
            // 
            // gbxRFID3
            // 
            this.gbxRFID3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRFID3.Location = new System.Drawing.Point(671, 3);
            this.gbxRFID3.Name = "gbxRFID3";
            this.gbxRFID3.Size = new System.Drawing.Size(328, 266);
            this.gbxRFID3.TabIndex = 15;
            this.gbxRFID3.TabStop = false;
            this.gbxRFID3.Text = "groupBox3";
            // 
            // gbxRFID4
            // 
            this.gbxRFID4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRFID4.Location = new System.Drawing.Point(1005, 3);
            this.gbxRFID4.Name = "gbxRFID4";
            this.gbxRFID4.Size = new System.Drawing.Size(329, 266);
            this.gbxRFID4.TabIndex = 16;
            this.gbxRFID4.TabStop = false;
            this.gbxRFID4.Text = "groupBox4";
            // 
            // FrmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 726);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FrmSetting";
            this.Text = "配置设置";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbxWCF.ResumeLayout(false);
            this.gbxWCFClient.ResumeLayout(false);
            this.gbxWCFClient.PerformLayout();
            this.gbxWCFServer.ResumeLayout(false);
            this.gbxWCFServer.PerformLayout();
            this.gbxPLC.ResumeLayout(false);
            this.gbxPLC.PerformLayout();
            this.gbxStation.ResumeLayout(false);
            this.gbxStation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudScannerCode)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbxStation;
        private System.Windows.Forms.TextBox tbxCSVPath;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tbxTestStation;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tbxFixtureId;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox tbxModel;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbxPLC;
        private System.Windows.Forms.TextBox tbxPLCPort;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tbxPLCIp;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox gbxWCF;
        private System.Windows.Forms.TextBox tbxWCFServerPort;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox tbxWCFServerIP;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.NumericUpDown nudScannerCode;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox gbxWCFClient;
        private System.Windows.Forms.TextBox tbxWCFClinetPort;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox tbxWCFClinetIP;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox gbxWCFServer;
        private System.Windows.Forms.GroupBox gbxRFID4;
        private System.Windows.Forms.GroupBox gbxRFID3;
        private System.Windows.Forms.GroupBox gbxRFID2;
        private System.Windows.Forms.GroupBox gbxRFID1;
    }
}
