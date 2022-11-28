namespace EC04Hotbar
{
    partial class FrmFixture
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbxCable2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxCable1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxFixture = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1055, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "子治具绑定线材";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Controls.Add(this.tbxCable2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbxCable1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbxFixture, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1049, 130);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tbxCable2
            // 
            this.tbxCable2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxCable2.Font = new System.Drawing.Font("宋体", 14F);
            this.tbxCable2.Location = new System.Drawing.Point(107, 89);
            this.tbxCable2.Name = "tbxCable2";
            this.tbxCable2.Size = new System.Drawing.Size(939, 34);
            this.tbxCable2.TabIndex = 6;
            this.tbxCable2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxCable2_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 44);
            this.label3.TabIndex = 4;
            this.label3.Text = "线材：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxCable1
            // 
            this.tbxCable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxCable1.Font = new System.Drawing.Font("宋体", 14F);
            this.tbxCable1.Location = new System.Drawing.Point(107, 46);
            this.tbxCable1.Name = "tbxCable1";
            this.tbxCable1.Size = new System.Drawing.Size(939, 34);
            this.tbxCable1.TabIndex = 3;
            this.tbxCable1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxCable1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 43);
            this.label2.TabIndex = 2;
            this.label2.Text = "线材：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 43);
            this.label1.TabIndex = 0;
            this.label1.Text = "子治具：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxFixture
            // 
            this.tbxFixture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxFixture.Font = new System.Drawing.Font("宋体", 14F);
            this.tbxFixture.Location = new System.Drawing.Point(107, 3);
            this.tbxFixture.Name = "tbxFixture";
            this.tbxFixture.Size = new System.Drawing.Size(939, 34);
            this.tbxFixture.TabIndex = 1;
            this.tbxFixture.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxFixture_KeyDown);
            // 
            // FrmFixture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 154);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmFixture";
            this.Text = "FrmFixture";
            this.Load += new System.EventHandler(this.FrmFixture_Load);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxFixture;
        private System.Windows.Forms.TextBox tbxCable2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxCable1;
        private System.Windows.Forms.Label label2;
    }
}