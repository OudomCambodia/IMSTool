namespace Testing.Forms
{
    partial class frmCreateBank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateBank));
            this.label1 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lbPolicyNo = new System.Windows.Forms.Label();
            this.tbTransfer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbAccount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvBank = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.bnDefault = new Testing.cus_button();
            this.btnNew = new Testing.cus_button();
            this.btnClose = new Testing.cus_button();
            this.btnSave = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(175, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 32);
            this.label1.TabIndex = 22;
            this.label1.Text = "Bank Information";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(116, 69);
            this.tbName.Margin = new System.Windows.Forms.Padding(4);
            this.tbName.MaxLength = 100;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(275, 20);
            this.tbName.TabIndex = 1;
            // 
            // lbPolicyNo
            // 
            this.lbPolicyNo.AutoSize = true;
            this.lbPolicyNo.BackColor = System.Drawing.Color.Transparent;
            this.lbPolicyNo.ForeColor = System.Drawing.Color.White;
            this.lbPolicyNo.Location = new System.Drawing.Point(42, 72);
            this.lbPolicyNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPolicyNo.Name = "lbPolicyNo";
            this.lbPolicyNo.Size = new System.Drawing.Size(66, 13);
            this.lbPolicyNo.TabIndex = 24;
            this.lbPolicyNo.Text = "Bank Name:";
            // 
            // tbTransfer
            // 
            this.tbTransfer.Location = new System.Drawing.Point(116, 97);
            this.tbTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.tbTransfer.MaxLength = 100;
            this.tbTransfer.Name = "tbTransfer";
            this.tbTransfer.Size = new System.Drawing.Size(275, 20);
            this.tbTransfer.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(43, 100);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Transfer To:";
            // 
            // tbAccount
            // 
            this.tbAccount.Location = new System.Drawing.Point(116, 125);
            this.tbAccount.Margin = new System.Windows.Forms.Padding(4);
            this.tbAccount.MaxLength = 100;
            this.tbAccount.Name = "tbAccount";
            this.tbAccount.Size = new System.Drawing.Size(275, 20);
            this.tbAccount.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(41, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Account No:";
            // 
            // dgvBank
            // 
            this.dgvBank.AllowUserToAddRows = false;
            this.dgvBank.AllowUserToDeleteRows = false;
            this.dgvBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBank.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvBank.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBank.Location = new System.Drawing.Point(8, 182);
            this.dgvBank.MultiSelect = false;
            this.dgvBank.Name = "dgvBank";
            this.dgvBank.ReadOnly = true;
            this.dgvBank.RowTemplate.Height = 27;
            this.dgvBank.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBank.Size = new System.Drawing.Size(537, 291);
            this.dgvBank.TabIndex = 7;
            this.dgvBank.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvBank_DataBindingComplete);
            this.dgvBank.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgvBank_MouseClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(59, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 59;
            this.label4.Text = "Bank ID:";
            // 
            // tbID
            // 
            this.tbID.Enabled = false;
            this.tbID.Location = new System.Drawing.Point(116, 41);
            this.tbID.Margin = new System.Windows.Forms.Padding(4);
            this.tbID.MaxLength = 20;
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(275, 20);
            this.tbID.TabIndex = 60;
            this.tbID.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // bnDefault
            // 
            this.bnDefault.BackColor = System.Drawing.Color.Gray;
            this.bnDefault.Enabled = false;
            this.bnDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnDefault.ForeColor = System.Drawing.Color.White;
            this.bnDefault.Location = new System.Drawing.Point(199, 151);
            this.bnDefault.Margin = new System.Windows.Forms.Padding(4);
            this.bnDefault.Name = "bnDefault";
            this.bnDefault.Size = new System.Drawing.Size(148, 25);
            this.bnDefault.TabIndex = 61;
            this.bnDefault.Text = "Make default bank";
            this.bnDefault.UseVisualStyleBackColor = false;
            this.bnDefault.Click += new System.EventHandler(this.bnDefault_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(399, 78);
            this.btnNew.Margin = new System.Windows.Forms.Padding(4);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(83, 30);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(399, 115);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(83, 30);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(399, 41);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmCreateBank
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(552, 484);
            this.Controls.Add(this.bnDefault);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvBank);
            this.Controls.Add(this.tbAccount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTransfer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbPolicyNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCreateBank";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Bank";
            this.Load += new System.EventHandler(this.frmCreateBank_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lbPolicyNo;
        private System.Windows.Forms.TextBox tbTransfer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbAccount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvBank;
        private cus_button btnClose;
        private cus_button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbID;
        private cus_button btnNew;
        private cus_button bnDefault;
    }
}