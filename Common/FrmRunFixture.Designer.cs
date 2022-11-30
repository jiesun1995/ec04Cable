namespace Common
{
    partial class FrmRunFixture
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnQuering = new System.Windows.Forms.Button();
            this.txtFixturePat = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFixture = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCable = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnQuering);
            this.panel3.Controls.Add(this.txtFixturePat);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.txtFixture);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtCable);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1223, 71);
            this.panel3.TabIndex = 1;
            // 
            // btnQuering
            // 
            this.btnQuering.Location = new System.Drawing.Point(684, 8);
            this.btnQuering.Margin = new System.Windows.Forms.Padding(4);
            this.btnQuering.Name = "btnQuering";
            this.btnQuering.Size = new System.Drawing.Size(140, 52);
            this.btnQuering.TabIndex = 6;
            this.btnQuering.Text = "查 询";
            this.btnQuering.UseVisualStyleBackColor = true;
            this.btnQuering.Click += new System.EventHandler(this.btnQuering_Click);
            // 
            // txtFixturePat
            // 
            this.txtFixturePat.Location = new System.Drawing.Point(523, 15);
            this.txtFixturePat.Margin = new System.Windows.Forms.Padding(4);
            this.txtFixturePat.Name = "txtFixturePat";
            this.txtFixturePat.Size = new System.Drawing.Size(132, 25);
            this.txtFixturePat.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(440, 26);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "母治具SN:";
            // 
            // txtFixture
            // 
            this.txtFixture.Location = new System.Drawing.Point(303, 15);
            this.txtFixture.Margin = new System.Windows.Forms.Padding(4);
            this.txtFixture.Name = "txtFixture";
            this.txtFixture.Size = new System.Drawing.Size(132, 25);
            this.txtFixture.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "子治具SN:";
            // 
            // txtCable
            // 
            this.txtCable.Location = new System.Drawing.Point(83, 15);
            this.txtCable.Margin = new System.Windows.Forms.Padding(4);
            this.txtCable.Name = "txtCable";
            this.txtCable.Size = new System.Drawing.Size(132, 25);
            this.txtCable.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "线材SN：";
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 71);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersWidth = 51;
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(1223, 551);
            this.dgvData.TabIndex = 4;
            // 
            // FrmRunFixture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1223, 622);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.panel3);
            this.Name = "FrmRunFixture";
            this.Text = "FrmRunFixture";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnQuering;
        private System.Windows.Forms.TextBox txtFixturePat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFixture;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvData;
    }
}