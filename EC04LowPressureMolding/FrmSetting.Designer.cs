namespace EC04LowPressureMolding
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.groupBox5, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1337, 726);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tbxCSVPath);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.tbxTestStation);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.tbxFixtureId);
            this.groupBox5.Controls.Add(this.label40);
            this.groupBox5.Controls.Add(this.tbxModel);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 275);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(328, 448);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "站点配置";
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
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox5;
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
    }
}