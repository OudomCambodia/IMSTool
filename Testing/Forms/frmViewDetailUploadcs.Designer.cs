namespace Testing.Forms
{
    partial class frmViewDetailUploadcs
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewDetailUploadcs));
            this.dgvEndorsement = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCusCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPolicyNo = new System.Windows.Forms.TextBox();
            this.btnAddNew = new Testing.cus_button();
            this.btnView = new Testing.cus_button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRe = new Testing.cus_button();
            this.ttMessage = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndorsement)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEndorsement
            // 
            this.dgvEndorsement.AllowUserToAddRows = false;
            this.dgvEndorsement.AllowUserToDeleteRows = false;
            this.dgvEndorsement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvEndorsement.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvEndorsement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvEndorsement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEndorsement.Location = new System.Drawing.Point(8, 115);
            this.dgvEndorsement.Margin = new System.Windows.Forms.Padding(4);
            this.dgvEndorsement.MultiSelect = false;
            this.dgvEndorsement.Name = "dgvEndorsement";
            this.dgvEndorsement.ReadOnly = true;
            this.dgvEndorsement.RowTemplate.Height = 25;
            this.dgvEndorsement.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvEndorsement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEndorsement.Size = new System.Drawing.Size(957, 484);
            this.dgvEndorsement.TabIndex = 3;
            this.dgvEndorsement.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEndorsement_CellContentClick);
            this.dgvEndorsement.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEndorsement_CellDoubleClick);
            this.dgvEndorsement.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvEndorsement_DataBindingComplete);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(646, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "Customer Name";
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.txtCustomerName.Enabled = false;
            this.txtCustomerName.Location = new System.Drawing.Point(759, 50);
            this.txtCustomerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(202, 22);
            this.txtCustomerName.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(312, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Customer Code";
            // 
            // txtCusCode
            // 
            this.txtCusCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.txtCusCode.Enabled = false;
            this.txtCusCode.Location = new System.Drawing.Point(421, 50);
            this.txtCusCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtCusCode.Name = "txtCusCode";
            this.txtCusCode.ReadOnly = true;
            this.txtCusCode.Size = new System.Drawing.Size(201, 22);
            this.txtCusCode.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Policy No";
            // 
            // txtPolicyNo
            // 
            this.txtPolicyNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.txtPolicyNo.Enabled = false;
            this.txtPolicyNo.Location = new System.Drawing.Point(87, 50);
            this.txtPolicyNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtPolicyNo.Name = "txtPolicyNo";
            this.txtPolicyNo.ReadOnly = true;
            this.txtPolicyNo.Size = new System.Drawing.Size(217, 22);
            this.txtPolicyNo.TabIndex = 24;
            // 
            // btnAddNew
            // 
            this.btnAddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnAddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNew.ForeColor = System.Drawing.Color.White;
            this.btnAddNew.Location = new System.Drawing.Point(16, 82);
            this.btnAddNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(87, 26);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add New";
            this.ttMessage.SetToolTip(this.btnAddNew, "Add file(s) for the last endorsement of the policy.");
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ForeColor = System.Drawing.Color.White;
            this.btnView.Location = new System.Drawing.Point(117, 82);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(88, 26);
            this.btnView.TabIndex = 2;
            this.btnView.Text = "Files";
            this.ttMessage.SetToolTip(this.btnView, "Allow viewing the detail of and file(s) associated in each endorsement.");
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(385, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(180, 32);
            this.label4.TabIndex = 33;
            this.label4.Text = "Upload History";
            // 
            // btnRe
            // 
            this.btnRe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnRe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRe.ForeColor = System.Drawing.Color.White;
            this.btnRe.Location = new System.Drawing.Point(218, 82);
            this.btnRe.Margin = new System.Windows.Forms.Padding(4);
            this.btnRe.Name = "btnRe";
            this.btnRe.Size = new System.Drawing.Size(93, 26);
            this.btnRe.TabIndex = 34;
            this.btnRe.Text = "Reupload";
            this.ttMessage.SetToolTip(this.btnRe, "Re-upload file(s) for the last endorsement of the policy ONLY, not other previous" +
        " endorsements.");
            this.btnRe.UseVisualStyleBackColor = true;
            this.btnRe.Click += new System.EventHandler(this.btnRe_Click);
            // 
            // ttMessage
            // 
            this.ttMessage.AutoPopDelay = 5000;
            this.ttMessage.InitialDelay = 500;
            this.ttMessage.ReshowDelay = 100;
            // 
            // frmViewDetailUploadcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(974, 612);
            this.Controls.Add(this.btnRe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnAddNew);
            this.Controls.Add(this.dgvEndorsement);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCusCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPolicyNo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmViewDetailUploadcs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload History";
            this.Load += new System.EventHandler(this.frmViewDetailUploadcs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEndorsement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEndorsement;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCusCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPolicyNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolTip ttMessage;
        private cus_button btnAddNew;
        private cus_button btnView;
        private cus_button btnRe;
    }
}