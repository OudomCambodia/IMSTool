namespace Testing.Forms
{
    partial class frmMedicalRejectionHistory
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedicalRejectionHistory));
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.dgvClaimRejectionHist = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnViewRefDoc = new Testing.cus_button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimRejectionHist)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.dgvClaimRejectionHist);
            this.groupBox14.Controls.Add(this.panel1);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox14.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.groupBox14.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox14.Location = new System.Drawing.Point(0, 0);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(1012, 601);
            this.groupBox14.TabIndex = 2;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Rejection Claim History";
            // 
            // dgvClaimRejectionHist
            // 
            this.dgvClaimRejectionHist.AllowUserToAddRows = false;
            this.dgvClaimRejectionHist.AllowUserToDeleteRows = false;
            this.dgvClaimRejectionHist.AllowUserToOrderColumns = true;
            this.dgvClaimRejectionHist.AllowUserToResizeRows = false;
            this.dgvClaimRejectionHist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClaimRejectionHist.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvClaimRejectionHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Cambria", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClaimRejectionHist.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClaimRejectionHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClaimRejectionHist.Location = new System.Drawing.Point(3, 83);
            this.dgvClaimRejectionHist.Name = "dgvClaimRejectionHist";
            this.dgvClaimRejectionHist.ReadOnly = true;
            this.dgvClaimRejectionHist.RowHeadersVisible = false;
            this.dgvClaimRejectionHist.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvClaimRejectionHist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClaimRejectionHist.Size = new System.Drawing.Size(1006, 515);
            this.dgvClaimRejectionHist.TabIndex = 2;
            this.dgvClaimRejectionHist.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClaimRejectionHist_CellDoubleClick);
            this.dgvClaimRejectionHist.SelectionChanged += new System.EventHandler(this.dgvClaimRejectionHist_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnViewRefDoc);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1006, 64);
            this.panel1.TabIndex = 1;
            // 
            // btnViewRefDoc
            // 
            this.btnViewRefDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnViewRefDoc.FlatAppearance.BorderSize = 2;
            this.btnViewRefDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewRefDoc.ForeColor = System.Drawing.Color.White;
            this.btnViewRefDoc.Location = new System.Drawing.Point(458, 15);
            this.btnViewRefDoc.Name = "btnViewRefDoc";
            this.btnViewRefDoc.Size = new System.Drawing.Size(182, 32);
            this.btnViewRefDoc.TabIndex = 2;
            this.btnViewRefDoc.Text = "View Reference Document";
            this.btnViewRefDoc.UseVisualStyleBackColor = false;
            this.btnViewRefDoc.Click += new System.EventHandler(this.btnViewRefDoc_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClaimNo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(18, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.Location = new System.Drawing.Point(80, 17);
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.Size = new System.Drawing.Size(345, 23);
            this.txtClaimNo.TabIndex = 1;
            this.txtClaimNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClaimNo_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Claim No:";
            // 
            // frmMedicalRejectionHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1012, 601);
            this.Controls.Add(this.groupBox14);
            this.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMedicalRejectionHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medical Rejection History";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMedicalRejectionHistory_Load);
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimRejectionHist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvClaimRejectionHist;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClaimNo;
        private cus_button btnViewRefDoc;
    }
}