namespace Testing.Forms
{
    partial class frmSendEmailClaimDet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendEmailClaimDet));
            this.lvDoc = new System.Windows.Forms.ListView();
            this.lbTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbClaimNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbReqNo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNonPaid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dpNoti = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbAttachFile = new System.Windows.Forms.TextBox();
            this.bnAddDoc = new Testing.cus_button();
            this.bnAddExclu = new Testing.cus_button();
            this.bnDocRec = new Testing.cus_button();
            this.bnPreview = new Testing.cus_button();
            this.bnCancel = new Testing.cus_button();
            this.bnSend = new Testing.cus_button();
            this.bnAttach = new Testing.cus_button();
            this.btnEditReceiver = new System.Windows.Forms.Button();
            this.tbReceiver = new CustomControls.TextBoxEmailAutocomplete();
            this.tbCC = new CustomControls.TextBoxEmailAutocomplete();
            this.SuspendLayout();
            // 
            // lvDoc
            // 
            this.lvDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDoc.FullRowSelect = true;
            this.lvDoc.Location = new System.Drawing.Point(3, 275);
            this.lvDoc.MultiSelect = false;
            this.lvDoc.Name = "lvDoc";
            this.lvDoc.Size = new System.Drawing.Size(992, 381);
            this.lvDoc.TabIndex = 2;
            this.lvDoc.UseCompatibleStateImageBehavior = false;
            this.lvDoc.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvDoc_ItemCheck);
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
            this.lbTitle.Size = new System.Drawing.Size(998, 46);
            this.lbTitle.TabIndex = 10;
            this.lbTitle.Text = "Documents for Email";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(31, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "Claim No:";
            // 
            // lbClaimNo
            // 
            this.lbClaimNo.AutoSize = true;
            this.lbClaimNo.Font = new System.Drawing.Font("Cambria", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbClaimNo.ForeColor = System.Drawing.Color.White;
            this.lbClaimNo.Location = new System.Drawing.Point(98, 38);
            this.lbClaimNo.Name = "lbClaimNo";
            this.lbClaimNo.Size = new System.Drawing.Size(75, 19);
            this.lbClaimNo.TabIndex = 14;
            this.lbClaimNo.Text = "Claim No";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(284, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Requisition No:";
            // 
            // cbReqNo
            // 
            this.cbReqNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReqNo.Enabled = false;
            this.cbReqNo.FormattingEnabled = true;
            this.cbReqNo.Location = new System.Drawing.Point(384, 206);
            this.cbReqNo.Name = "cbReqNo";
            this.cbReqNo.Size = new System.Drawing.Size(181, 23);
            this.cbReqNo.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(34, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Receiver:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(582, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "Non-Paid Amount:";
            // 
            // tbNonPaid
            // 
            this.tbNonPaid.Enabled = false;
            this.tbNonPaid.Location = new System.Drawing.Point(696, 206);
            this.tbNonPaid.MaxLength = 50;
            this.tbNonPaid.Name = "tbNonPaid";
            this.tbNonPaid.Size = new System.Drawing.Size(168, 23);
            this.tbNonPaid.TabIndex = 20;
            this.tbNonPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbNonPaid_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(7, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 15);
            this.label5.TabIndex = 23;
            this.label5.Text = "Notified Date:";
            // 
            // dpNoti
            // 
            this.dpNoti.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpNoti.Location = new System.Drawing.Point(96, 206);
            this.dpNoti.Name = "dpNoti";
            this.dpNoti.Size = new System.Drawing.Size(166, 23);
            this.dpNoti.TabIndex = 24;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(68, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "CC:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(19, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 15);
            this.label7.TabIndex = 31;
            this.label7.Text = "Attach File:";
            // 
            // tbAttachFile
            // 
            this.tbAttachFile.Location = new System.Drawing.Point(95, 177);
            this.tbAttachFile.Name = "tbAttachFile";
            this.tbAttachFile.Size = new System.Drawing.Size(897, 23);
            this.tbAttachFile.TabIndex = 32;
            // 
            // bnAddDoc
            // 
            this.bnAddDoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnAddDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnAddDoc.FlatAppearance.BorderSize = 2;
            this.bnAddDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnAddDoc.ForeColor = System.Drawing.Color.White;
            this.bnAddDoc.Location = new System.Drawing.Point(6, 243);
            this.bnAddDoc.Name = "bnAddDoc";
            this.bnAddDoc.Size = new System.Drawing.Size(135, 26);
            this.bnAddDoc.TabIndex = 33;
            this.bnAddDoc.Text = "Manage Document";
            this.bnAddDoc.UseVisualStyleBackColor = false;
            this.bnAddDoc.Click += new System.EventHandler(this.bnAddDoc_Click);
            // 
            // bnAddExclu
            // 
            this.bnAddExclu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnAddExclu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnAddExclu.FlatAppearance.BorderSize = 2;
            this.bnAddExclu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnAddExclu.ForeColor = System.Drawing.Color.White;
            this.bnAddExclu.Location = new System.Drawing.Point(6, 243);
            this.bnAddExclu.Name = "bnAddExclu";
            this.bnAddExclu.Size = new System.Drawing.Size(135, 26);
            this.bnAddExclu.TabIndex = 28;
            this.bnAddExclu.Text = "Manage Exclusion";
            this.bnAddExclu.UseVisualStyleBackColor = false;
            this.bnAddExclu.Click += new System.EventHandler(this.bnAddExclu_Click);
            // 
            // bnDocRec
            // 
            this.bnDocRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnDocRec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnDocRec.FlatAppearance.BorderSize = 2;
            this.bnDocRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnDocRec.ForeColor = System.Drawing.Color.White;
            this.bnDocRec.Location = new System.Drawing.Point(857, 244);
            this.bnDocRec.Name = "bnDocRec";
            this.bnDocRec.Size = new System.Drawing.Size(135, 26);
            this.bnDocRec.TabIndex = 22;
            this.bnDocRec.Text = "Set Received for Doc";
            this.bnDocRec.UseVisualStyleBackColor = false;
            this.bnDocRec.Click += new System.EventHandler(this.bnDocRec_Click);
            // 
            // bnPreview
            // 
            this.bnPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnPreview.FlatAppearance.BorderSize = 2;
            this.bnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnPreview.ForeColor = System.Drawing.Color.White;
            this.bnPreview.Location = new System.Drawing.Point(6, 662);
            this.bnPreview.Name = "bnPreview";
            this.bnPreview.Size = new System.Drawing.Size(95, 33);
            this.bnPreview.TabIndex = 21;
            this.bnPreview.Text = "Preview";
            this.bnPreview.UseVisualStyleBackColor = false;
            this.bnPreview.Click += new System.EventHandler(this.bnPreview_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnCancel.FlatAppearance.BorderSize = 2;
            this.bnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnCancel.ForeColor = System.Drawing.Color.White;
            this.bnCancel.Location = new System.Drawing.Point(894, 662);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(95, 33);
            this.bnCancel.TabIndex = 12;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.UseVisualStyleBackColor = false;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // bnSend
            // 
            this.bnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSend.FlatAppearance.BorderSize = 2;
            this.bnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSend.ForeColor = System.Drawing.Color.White;
            this.bnSend.Location = new System.Drawing.Point(793, 662);
            this.bnSend.Name = "bnSend";
            this.bnSend.Size = new System.Drawing.Size(95, 33);
            this.bnSend.TabIndex = 11;
            this.bnSend.Text = "Send";
            this.bnSend.UseVisualStyleBackColor = false;
            this.bnSend.Click += new System.EventHandler(this.bnSend_Click);
            // 
            // bnAttach
            // 
            this.bnAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bnAttach.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnAttach.FlatAppearance.BorderSize = 2;
            this.bnAttach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnAttach.ForeColor = System.Drawing.Color.White;
            this.bnAttach.Location = new System.Drawing.Point(419, 244);
            this.bnAttach.Name = "bnAttach";
            this.bnAttach.Size = new System.Drawing.Size(135, 26);
            this.bnAttach.TabIndex = 30;
            this.bnAttach.Text = "Attach File";
            this.bnAttach.UseVisualStyleBackColor = false;
            this.bnAttach.Click += new System.EventHandler(this.bnAttach_Click);
            // 
            // btnEditReceiver
            // 
            this.btnEditReceiver.BackgroundImage = global::Testing.Properties.Resources.Edit;
            this.btnEditReceiver.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEditReceiver.Location = new System.Drawing.Point(6, 62);
            this.btnEditReceiver.Name = "btnEditReceiver";
            this.btnEditReceiver.Size = new System.Drawing.Size(24, 23);
            this.btnEditReceiver.TabIndex = 34;
            this.btnEditReceiver.UseVisualStyleBackColor = true;
            this.btnEditReceiver.Click += new System.EventHandler(this.btnEditReceiver_Click);
            // 
            // tbReceiver
            // 
            this.tbReceiver.EmailAutocompleteSource = null;
            this.tbReceiver.HighlightColor = System.Drawing.SystemColors.ControlLight;
            this.tbReceiver.Location = new System.Drawing.Point(96, 63);
            this.tbReceiver.MinimumSize = new System.Drawing.Size(120, 284);
            this.tbReceiver.Name = "tbReceiver";
            this.tbReceiver.Size = new System.Drawing.Size(896, 284);
            this.tbReceiver.TabIndex = 35;
            this.tbReceiver.TextColor = System.Drawing.SystemColors.WindowText;
            // 
            // tbCC
            // 
            this.tbCC.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.tbCC.EmailAutocompleteSource = null;
            this.tbCC.HighlightColor = System.Drawing.SystemColors.ControlLight;
            this.tbCC.Location = new System.Drawing.Point(96, 120);
            this.tbCC.MinimumSize = new System.Drawing.Size(140, 328);
            this.tbCC.Name = "tbCC";
            this.tbCC.Size = new System.Drawing.Size(896, 328);
            this.tbCC.TabIndex = 36;
            this.tbCC.TextColor = System.Drawing.SystemColors.WindowText;
            // 
            // frmSendEmailClaimDet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(998, 704);
            this.Controls.Add(this.tbReceiver);
            this.Controls.Add(this.tbCC);
            this.Controls.Add(this.btnEditReceiver);
            this.Controls.Add(this.bnAddDoc);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bnAddExclu);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dpNoti);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bnDocRec);
            this.Controls.Add(this.bnPreview);
            this.Controls.Add(this.tbNonPaid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbReqNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbClaimNo);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnSend);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lvDoc);
            this.Controls.Add(this.bnAttach);
            this.Controls.Add(this.tbAttachFile);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSendEmailClaimDet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sending Claim Email";
            this.Load += new System.EventHandler(this.frmSendEmailClaimDet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvDoc;
        private System.Windows.Forms.Label lbTitle;
        private cus_button bnSend;
        private cus_button bnCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lbClaimNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbReqNo;
        private System.Windows.Forms.TextBox tbNonPaid;
        private cus_button bnPreview;
        private cus_button bnDocRec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dpNoti;
        private System.Windows.Forms.Label label6;
        private cus_button bnAddExclu;
        private cus_button bnAttach;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbAttachFile;
        private cus_button bnAddDoc;
        private System.Windows.Forms.Button btnEditReceiver;
        private CustomControls.TextBoxEmailAutocomplete tbReceiver;
        private CustomControls.TextBoxEmailAutocomplete tbCC;
    }
}