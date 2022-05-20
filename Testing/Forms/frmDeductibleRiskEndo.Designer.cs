namespace Testing.Forms
{
    partial class frmDeductibleRiskEndo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDeductibleRiskEndo));
            this.label1 = new System.Windows.Forms.Label();
            this.dgvEndoDetail = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            this.tbRiskName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPolicyNo = new System.Windows.Forms.TextBox();
            this.bnClose = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndoDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(890, 53);
            this.label1.TabIndex = 13;
            this.label1.Text = "Risk Endorsement Detail";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvEndoDetail
            // 
            this.dgvEndoDetail.AllowUserToAddRows = false;
            this.dgvEndoDetail.AllowUserToDeleteRows = false;
            this.dgvEndoDetail.AllowUserToResizeRows = false;
            this.dgvEndoDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEndoDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEndoDetail.Location = new System.Drawing.Point(12, 89);
            this.dgvEndoDetail.Name = "dgvEndoDetail";
            this.dgvEndoDetail.ReadOnly = true;
            this.dgvEndoDetail.RowHeadersVisible = false;
            this.dgvEndoDetail.Size = new System.Drawing.Size(866, 326);
            this.dgvEndoDetail.TabIndex = 14;
            this.dgvEndoDetail.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvEndoDetail_DataBindingComplete);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 15);
            this.label14.TabIndex = 33;
            this.label14.Text = "Vehicle No.:";
            // 
            // tbRiskName
            // 
            this.tbRiskName.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRiskName.Location = new System.Drawing.Point(92, 56);
            this.tbRiskName.Multiline = true;
            this.tbRiskName.Name = "tbRiskName";
            this.tbRiskName.ReadOnly = true;
            this.tbRiskName.Size = new System.Drawing.Size(202, 18);
            this.tbRiskName.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(562, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Policy No.:";
            // 
            // tbPolicyNo
            // 
            this.tbPolicyNo.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPolicyNo.Location = new System.Drawing.Point(635, 56);
            this.tbPolicyNo.Multiline = true;
            this.tbPolicyNo.Name = "tbPolicyNo";
            this.tbPolicyNo.ReadOnly = true;
            this.tbPolicyNo.Size = new System.Drawing.Size(239, 18);
            this.tbPolicyNo.TabIndex = 34;
            // 
            // bnClose
            // 
            this.bnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClose.FlatAppearance.BorderSize = 2;
            this.bnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClose.ForeColor = System.Drawing.Color.White;
            this.bnClose.Location = new System.Drawing.Point(780, 421);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(98, 26);
            this.bnClose.TabIndex = 36;
            this.bnClose.Text = "Close";
            this.bnClose.UseVisualStyleBackColor = false;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // frmDeductibleRiskEndo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(890, 459);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbPolicyNo);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tbRiskName);
            this.Controls.Add(this.dgvEndoDetail);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmDeductibleRiskEndo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Risk Endorsement Detail";
            this.Load += new System.EventHandler(this.frmDeductibleRiskEndo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndoDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvEndoDetail;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbRiskName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPolicyNo;
        private cus_button bnClose;
    }
}