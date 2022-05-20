namespace Testing.Forms
{
    partial class frmAcceptRejectDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAcceptRejectDoc));
            this.lbTitle = new System.Windows.Forms.Label();
            this.groupReason = new System.Windows.Forms.GroupBox();
            this.tbOther = new System.Windows.Forms.TextBox();
            this.rdOther = new System.Windows.Forms.RadioButton();
            this.rdReason3 = new System.Windows.Forms.RadioButton();
            this.rdReason2 = new System.Windows.Forms.RadioButton();
            this.rdReason1 = new System.Windows.Forms.RadioButton();
            this.rdAccept = new System.Windows.Forms.RadioButton();
            this.rdReject = new System.Windows.Forms.RadioButton();
            this.lblDetail = new System.Windows.Forms.Label();
            this.btnConfirm = new Testing.cus_button();
            this.btnCancel = new Testing.cus_button();
            this.groupReason.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(502, 46);
            this.lbTitle.TabIndex = 14;
            this.lbTitle.Text = "Accept/Reject Document";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupReason
            // 
            this.groupReason.BackColor = System.Drawing.Color.Transparent;
            this.groupReason.Controls.Add(this.tbOther);
            this.groupReason.Controls.Add(this.rdOther);
            this.groupReason.Controls.Add(this.rdReason3);
            this.groupReason.Controls.Add(this.rdReason2);
            this.groupReason.Controls.Add(this.rdReason1);
            this.groupReason.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupReason.ForeColor = System.Drawing.Color.White;
            this.groupReason.Location = new System.Drawing.Point(202, 101);
            this.groupReason.Name = "groupReason";
            this.groupReason.Size = new System.Drawing.Size(256, 158);
            this.groupReason.TabIndex = 15;
            this.groupReason.TabStop = false;
            this.groupReason.Text = "Reject reason";
            // 
            // tbOther
            // 
            this.tbOther.Location = new System.Drawing.Point(19, 119);
            this.tbOther.Name = "tbOther";
            this.tbOther.Size = new System.Drawing.Size(219, 22);
            this.tbOther.TabIndex = 4;
            // 
            // rdOther
            // 
            this.rdOther.AutoSize = true;
            this.rdOther.ForeColor = System.Drawing.Color.White;
            this.rdOther.Location = new System.Drawing.Point(19, 96);
            this.rdOther.Name = "rdOther";
            this.rdOther.Size = new System.Drawing.Size(58, 20);
            this.rdOther.TabIndex = 3;
            this.rdOther.TabStop = true;
            this.rdOther.Text = "Other";
            this.rdOther.UseVisualStyleBackColor = true;
            this.rdOther.CheckedChanged += new System.EventHandler(this.rdOther_CheckedChanged);
            // 
            // rdReason3
            // 
            this.rdReason3.AutoSize = true;
            this.rdReason3.ForeColor = System.Drawing.Color.White;
            this.rdReason3.Location = new System.Drawing.Point(19, 73);
            this.rdReason3.Name = "rdReason3";
            this.rdReason3.Size = new System.Drawing.Size(92, 20);
            this.rdReason3.TabIndex = 2;
            this.rdReason3.TabStop = true;
            this.rdReason3.Text = "No fire tariff";
            this.rdReason3.UseVisualStyleBackColor = true;
            this.rdReason3.CheckedChanged += new System.EventHandler(this.rdReason3_CheckedChanged);
            // 
            // rdReason2
            // 
            this.rdReason2.AutoSize = true;
            this.rdReason2.ForeColor = System.Drawing.Color.White;
            this.rdReason2.Location = new System.Drawing.Point(19, 50);
            this.rdReason2.Name = "rdReason2";
            this.rdReason2.Size = new System.Drawing.Size(178, 20);
            this.rdReason2.TabIndex = 1;
            this.rdReason2.TabStop = true;
            this.rdReason2.Text = "No reinsurance iddication";
            this.rdReason2.UseVisualStyleBackColor = true;
            this.rdReason2.CheckedChanged += new System.EventHandler(this.rdReason2_CheckedChanged);
            // 
            // rdReason1
            // 
            this.rdReason1.AutoSize = true;
            this.rdReason1.ForeColor = System.Drawing.Color.White;
            this.rdReason1.Location = new System.Drawing.Point(19, 27);
            this.rdReason1.Name = "rdReason1";
            this.rdReason1.Size = new System.Drawing.Size(167, 20);
            this.rdReason1.TabIndex = 0;
            this.rdReason1.TabStop = true;
            this.rdReason1.Text = "Document not complete";
            this.rdReason1.UseVisualStyleBackColor = true;
            this.rdReason1.CheckedChanged += new System.EventHandler(this.rdReason1_CheckedChanged);
            // 
            // rdAccept
            // 
            this.rdAccept.AutoSize = true;
            this.rdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdAccept.ForeColor = System.Drawing.Color.White;
            this.rdAccept.Location = new System.Drawing.Point(45, 101);
            this.rdAccept.Name = "rdAccept";
            this.rdAccept.Size = new System.Drawing.Size(83, 24);
            this.rdAccept.TabIndex = 16;
            this.rdAccept.TabStop = true;
            this.rdAccept.Text = "Accept";
            this.rdAccept.UseVisualStyleBackColor = true;
            this.rdAccept.CheckedChanged += new System.EventHandler(this.rdAccept_CheckedChanged);
            // 
            // rdReject
            // 
            this.rdReject.AutoSize = true;
            this.rdReject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdReject.ForeColor = System.Drawing.Color.White;
            this.rdReject.Location = new System.Drawing.Point(45, 131);
            this.rdReject.Name = "rdReject";
            this.rdReject.Size = new System.Drawing.Size(79, 24);
            this.rdReject.TabIndex = 17;
            this.rdReject.TabStop = true;
            this.rdReject.Text = "Reject";
            this.rdReject.UseVisualStyleBackColor = true;
            // 
            // lblDetail
            // 
            this.lblDetail.AutoSize = true;
            this.lblDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetail.ForeColor = System.Drawing.Color.White;
            this.lblDetail.Location = new System.Drawing.Point(42, 57);
            this.lblDetail.Name = "lblDetail";
            this.lblDetail.Size = new System.Drawing.Size(44, 13);
            this.lblDetail.TabIndex = 64;
            this.lblDetail.Text = "lblDetail";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnConfirm.FlatAppearance.BorderSize = 2;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(250, 279);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(95, 30);
            this.btnConfirm.TabIndex = 63;
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
            this.btnCancel.Location = new System.Drawing.Point(363, 279);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 30);
            this.btnCancel.TabIndex = 63;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmAcceptRejectDoc
            // 
            this.AcceptButton = this.btnConfirm;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(502, 325);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lblDetail);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rdReject);
            this.Controls.Add(this.rdAccept);
            this.Controls.Add(this.groupReason);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAcceptRejectDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Accept or Reject";
            this.Load += new System.EventHandler(this.frmAcceptRejectDoc_Load);
            this.groupReason.ResumeLayout(false);
            this.groupReason.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox groupReason;
        private System.Windows.Forms.TextBox tbOther;
        private System.Windows.Forms.RadioButton rdOther;
        private System.Windows.Forms.RadioButton rdReason3;
        private System.Windows.Forms.RadioButton rdReason2;
        private System.Windows.Forms.RadioButton rdReason1;
        private System.Windows.Forms.RadioButton rdAccept;
        private System.Windows.Forms.RadioButton rdReject;
        private cus_button btnCancel;
        private cus_button btnConfirm;
        private System.Windows.Forms.Label lblDetail;
    }
}