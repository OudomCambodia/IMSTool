namespace Testing.Forms
{
    partial class frmDocumentReturn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentReturn));
            this.label1 = new System.Windows.Forms.Label();
            this.lblDetail = new System.Windows.Forms.Label();
            this.groupReason = new System.Windows.Forms.GroupBox();
            this.rdReason2 = new System.Windows.Forms.RadioButton();
            this.rdReason1 = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new Testing.cus_button();
            this.btnCancel = new Testing.cus_button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.groupReason.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(477, 46);
            this.label1.TabIndex = 59;
            this.label1.Text = "Return Document";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDetail
            // 
            this.lblDetail.AutoSize = true;
            this.lblDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetail.ForeColor = System.Drawing.Color.White;
            this.lblDetail.Location = new System.Drawing.Point(34, 64);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(44, 13);
            this.lblDetail.TabIndex = 65;
            this.lblDetail.Text = "lblDetail";
            // 
            // groupReason
            // 
            this.groupReason.BackColor = System.Drawing.Color.Transparent;
            this.groupReason.Controls.Add(this.rdReason2);
            this.groupReason.Controls.Add(this.rdReason1);
            this.groupReason.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupReason.ForeColor = System.Drawing.Color.White;
            this.groupReason.Location = new System.Drawing.Point(37, 105);
            this.groupReason.Name = "groupReason";
            this.groupReason.Size = new System.Drawing.Size(399, 77);
            this.groupReason.TabIndex = 66;
            this.groupReason.TabStop = false;
            this.groupReason.Text = "Return reason";
            // 
            // rdReason2
            // 
            this.rdReason2.AutoSize = true;
            this.rdReason2.ForeColor = System.Drawing.Color.White;
            this.rdReason2.Location = new System.Drawing.Point(19, 47);
            this.rdReason2.Name = "rdReason2";
            this.rdReason2.Size = new System.Drawing.Size(141, 20);
            this.rdReason2.TabIndex = 1;
            this.rdReason2.TabStop = true;
            this.rdReason2.Text = "Producer\'s mistake";
            this.rdReason2.UseVisualStyleBackColor = true;
            // 
            // rdReason1
            // 
            this.rdReason1.AutoSize = true;
            this.rdReason1.ForeColor = System.Drawing.Color.White;
            this.rdReason1.Location = new System.Drawing.Point(19, 21);
            this.rdReason1.Name = "rdReason1";
            this.rdReason1.Size = new System.Drawing.Size(105, 20);
            this.rdReason1.TabIndex = 0;
            this.rdReason1.TabStop = true;
            this.rdReason1.Text = "DP\'s mistake";
            this.rdReason1.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnConfirm.FlatAppearance.BorderSize = 2;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(240, 241);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 30);
            this.btnConfirm.TabIndex = 67;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 2;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(341, 241);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 68;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(38, 191);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Remark:";
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(91, 188);
            this.tbRemark.Multiline = true;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(345, 34);
            this.tbRemark.TabIndex = 70;
            // 
            // frmDocumentReturn
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(477, 283);
            this.Controls.Add(this.tbRemark);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupReason);
            this.Controls.Add(this.lblDetail);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDocumentReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Return Document";
            this.Load += new System.EventHandler(this.frmDocumentReturn_Load);
            this.groupReason.ResumeLayout(false);
            this.groupReason.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDetail;
        private System.Windows.Forms.GroupBox groupReason;
        private System.Windows.Forms.RadioButton rdReason2;
        private System.Windows.Forms.RadioButton rdReason1;
        private cus_button btnConfirm;
        private cus_button btnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRemark;
    }
}