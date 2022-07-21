namespace Testing.Forms
{
    partial class frmUserManagement
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
            PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserManagement));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.dtpExpiryDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.btnViewPassword = new System.Windows.Forms.Button();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnViewEmailPassword = new System.Windows.Forms.Button();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkCreateDocUser = new System.Windows.Forms.CheckBox();
            this.cboRole = new PresentationControls.CheckBoxComboBox();
            this.gbDocumentControl = new System.Windows.Forms.GroupBox();
            this.cboSpecialCode = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cboTeam = new PresentationControls.CheckBoxComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lblSpecialCodeInfo = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.gbDocumentControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.cboType);
            this.groupBox1.Controls.Add(this.dtpExpiryDate);
            this.groupBox1.Controls.Add(this.dtpCreatedDate);
            this.groupBox1.Controls.Add(this.btnViewPassword);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(667, 219);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Information";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(422, 93);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(211, 26);
            this.cboType.TabIndex = 6;
            // 
            // dtpExpiryDate
            // 
            this.dtpExpiryDate.Location = new System.Drawing.Point(96, 92);
            this.dtpExpiryDate.Name = "dtpExpiryDate";
            this.dtpExpiryDate.Size = new System.Drawing.Size(211, 26);
            this.dtpExpiryDate.TabIndex = 5;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(422, 61);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(211, 26);
            this.dtpCreatedDate.TabIndex = 4;
            // 
            // btnViewPassword
            // 
            this.btnViewPassword.Image = global::Testing.Properties.Resources.eye1;
            this.btnViewPassword.Location = new System.Drawing.Point(272, 59);
            this.btnViewPassword.Name = "btnViewPassword";
            this.btnViewPassword.Size = new System.Drawing.Size(35, 28);
            this.btnViewPassword.TabIndex = 3;
            this.btnViewPassword.UseVisualStyleBackColor = true;
            this.btnViewPassword.Click += new System.EventHandler(this.btnViewPassword_Click);
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(96, 124);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(537, 75);
            this.txtRemark.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "Remark:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(326, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Created Date:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(422, 28);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(211, 26);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(326, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "User Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Expiry Date:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(96, 60);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(170, 26);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // txtUserCode
            // 
            this.txtUserCode.Location = new System.Drawing.Point(96, 28);
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(211, 26);
            this.txtUserCode.TabIndex = 0;
            this.txtUserCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserCode_KeyDown);
            this.txtUserCode.Leave += new System.EventHandler(this.txtUserCode_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Code:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Controls.Add(this.btnViewEmailPassword);
            this.groupBox2.Controls.Add(this.txtEmailPassword);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtEmail);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Location = new System.Drawing.Point(12, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(667, 67);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User Additional Information";
            // 
            // btnViewEmailPassword
            // 
            this.btnViewEmailPassword.Image = global::Testing.Properties.Resources.eye1;
            this.btnViewEmailPassword.Location = new System.Drawing.Point(598, 24);
            this.btnViewEmailPassword.Name = "btnViewEmailPassword";
            this.btnViewEmailPassword.Size = new System.Drawing.Size(35, 28);
            this.btnViewEmailPassword.TabIndex = 2;
            this.btnViewEmailPassword.UseVisualStyleBackColor = true;
            this.btnViewEmailPassword.Click += new System.EventHandler(this.btnViewEmailPassword_Click);
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.Location = new System.Drawing.Point(422, 25);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.Size = new System.Drawing.Size(170, 26);
            this.txtEmailPassword.TabIndex = 1;
            this.txtEmailPassword.UseSystemPasswordChar = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(326, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 18);
            this.label8.TabIndex = 15;
            this.label8.Text = "Email Password:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(96, 25);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(211, 26);
            this.txtEmail.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 18);
            this.label9.TabIndex = 13;
            this.label9.Text = "Email:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(498, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(592, 527);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 30);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // chkCreateDocUser
            // 
            this.chkCreateDocUser.AutoSize = true;
            this.chkCreateDocUser.Checked = true;
            this.chkCreateDocUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateDocUser.Location = new System.Drawing.Point(12, 319);
            this.chkCreateDocUser.Name = "chkCreateDocUser";
            this.chkCreateDocUser.Size = new System.Drawing.Size(275, 22);
            this.chkCreateDocUser.TabIndex = 2;
            this.chkCreateDocUser.Text = "Create Document Control account for this user?";
            this.chkCreateDocUser.UseVisualStyleBackColor = true;
            this.chkCreateDocUser.CheckedChanged += new System.EventHandler(this.chkCreateDocUser_CheckedChanged);
            // 
            // cboRole
            // 
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboRole.CheckBoxProperties = checkBoxProperties2;
            this.cboRole.DisplayMemberSingleItem = "";
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(109, 23);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(524, 26);
            this.cboRole.TabIndex = 3;
            // 
            // gbDocumentControl
            // 
            this.gbDocumentControl.BackColor = System.Drawing.Color.Gainsboro;
            this.gbDocumentControl.Controls.Add(this.label13);
            this.gbDocumentControl.Controls.Add(this.lblSpecialCodeInfo);
            this.gbDocumentControl.Controls.Add(this.cboSpecialCode);
            this.gbDocumentControl.Controls.Add(this.label12);
            this.gbDocumentControl.Controls.Add(this.label11);
            this.gbDocumentControl.Controls.Add(this.cboTeam);
            this.gbDocumentControl.Controls.Add(this.label10);
            this.gbDocumentControl.Controls.Add(this.cboRole);
            this.gbDocumentControl.Location = new System.Drawing.Point(12, 348);
            this.gbDocumentControl.Name = "gbDocumentControl";
            this.gbDocumentControl.Size = new System.Drawing.Size(667, 173);
            this.gbDocumentControl.TabIndex = 16;
            this.gbDocumentControl.TabStop = false;
            this.gbDocumentControl.Text = "Document Control account Information";
            // 
            // cboSpecialCode
            // 
            this.cboSpecialCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSpecialCode.FormattingEnabled = true;
            this.cboSpecialCode.Location = new System.Drawing.Point(109, 87);
            this.cboSpecialCode.Name = "cboSpecialCode";
            this.cboSpecialCode.Size = new System.Drawing.Size(524, 26);
            this.cboSpecialCode.TabIndex = 14;
            this.cboSpecialCode.SelectedIndexChanged += new System.EventHandler(this.cboSpecialCode_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 18;
            this.label12.Text = "Special Code:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 59);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 18);
            this.label11.TabIndex = 16;
            this.label11.Text = "Team:";
            // 
            // cboTeam
            // 
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboTeam.CheckBoxProperties = checkBoxProperties1;
            this.cboTeam.DisplayMemberSingleItem = "";
            this.cboTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTeam.FormattingEnabled = true;
            this.cboTeam.Location = new System.Drawing.Point(109, 55);
            this.cboTeam.Name = "cboTeam";
            this.cboTeam.Size = new System.Drawing.Size(524, 26);
            this.cboTeam.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 18);
            this.label10.TabIndex = 14;
            this.label10.Text = "Role:";
            // 
            // lblSpecialCodeInfo
            // 
            this.lblSpecialCodeInfo.Font = new System.Drawing.Font("Hanuman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblSpecialCodeInfo.ForeColor = System.Drawing.Color.DarkRed;
            this.lblSpecialCodeInfo.Location = new System.Drawing.Point(151, 117);
            this.lblSpecialCodeInfo.Name = "lblSpecialCodeInfo";
            this.lblSpecialCodeInfo.Size = new System.Drawing.Size(486, 47);
            this.lblSpecialCodeInfo.TabIndex = 19;
            this.lblSpecialCodeInfo.Text = "N/A";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Hanuman", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.label13.ForeColor = System.Drawing.Color.DarkRed;
            this.label13.Location = new System.Drawing.Point(106, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 19);
            this.label13.TabIndex = 20;
            this.label13.Text = "*Note:";
            // 
            // frmUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(692, 563);
            this.Controls.Add(this.gbDocumentControl);
            this.Controls.Add(this.chkCreateDocUser);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Hanuman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmUserManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Management";
            this.Load += new System.EventHandler(this.frmUserManagement_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.gbDocumentControl.ResumeLayout(false);
            this.gbDocumentControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtEmailPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnViewPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DateTimePicker dtpExpiryDate;
        private System.Windows.Forms.DateTimePicker dtpCreatedDate;
        private System.Windows.Forms.Button btnViewEmailPassword;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckBox chkCreateDocUser;
        private PresentationControls.CheckBoxComboBox cboRole;
        private System.Windows.Forms.GroupBox gbDocumentControl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private PresentationControls.CheckBoxComboBox cboTeam;
        private System.Windows.Forms.ComboBox cboSpecialCode;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSpecialCodeInfo;
        private System.Windows.Forms.Label label13;
    }
}