using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
namespace Testing.Forms
{
    partial class HospitalSurgicalScheme
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtLossDate = new System.Windows.Forms.TextBox();
            this.txtPlan = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtMemberStatus = new System.Windows.Forms.TextBox();
            this.txtMember = new System.Windows.Forms.TextBox();
            this.txtPremiumStatus = new System.Windows.Forms.TextBox();
            this.txtExpiryDate = new System.Windows.Forms.TextBox();
            this.txtEffectiveDate = new System.Windows.Forms.TextBox();
            this.txtPolicyNumber = new System.Windows.Forms.TextBox();
            this.txtPolicyHolder = new System.Windows.Forms.TextBox();
            this.btnOPB123 = new Testing.cus_button();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnSearch = new Testing.cus_button();
            this.btnDetail2Plus = new Testing.cus_button();
            this.btnOPScheme = new Testing.cus_button();
            this.btnDetailsPlus = new Testing.cus_button();
            this.btnDetailOld = new Testing.cus_button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(0, 1);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(1232, 62);
            this.lbTitle.TabIndex = 13;
            this.lbTitle.Text = "HNS Benefit Scheme";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtLossDate);
            this.splitContainer1.Panel1.Controls.Add(this.txtPlan);
            this.splitContainer1.Panel1.Controls.Add(this.txtAge);
            this.splitContainer1.Panel1.Controls.Add(this.txtGender);
            this.splitContainer1.Panel1.Controls.Add(this.txtMemberStatus);
            this.splitContainer1.Panel1.Controls.Add(this.txtMember);
            this.splitContainer1.Panel1.Controls.Add(this.txtPremiumStatus);
            this.splitContainer1.Panel1.Controls.Add(this.txtExpiryDate);
            this.splitContainer1.Panel1.Controls.Add(this.txtEffectiveDate);
            this.splitContainer1.Panel1.Controls.Add(this.txtPolicyNumber);
            this.splitContainer1.Panel1.Controls.Add(this.txtPolicyHolder);
            this.splitContainer1.Panel1.Controls.Add(this.btnOPB123);
            this.splitContainer1.Panel1.Controls.Add(this.txtClaimNo);
            this.splitContainer1.Panel1.Controls.Add(this.textBox22);
            this.splitContainer1.Panel1.Controls.Add(this.textBox18);
            this.splitContainer1.Panel1.Controls.Add(this.textBox20);
            this.splitContainer1.Panel1.Controls.Add(this.textBox14);
            this.splitContainer1.Panel1.Controls.Add(this.textBox16);
            this.splitContainer1.Panel1.Controls.Add(this.textBox10);
            this.splitContainer1.Panel1.Controls.Add(this.textBox12);
            this.splitContainer1.Panel1.Controls.Add(this.textBox6);
            this.splitContainer1.Panel1.Controls.Add(this.textBox8);
            this.splitContainer1.Panel1.Controls.Add(this.textBox4);
            this.splitContainer1.Panel1.Controls.Add(this.textBox24);
            this.splitContainer1.Panel1.Controls.Add(this.textBox2);
            this.splitContainer1.Panel1.Controls.Add(this.btnSearch);
            this.splitContainer1.Panel1.Controls.Add(this.btnDetail2Plus);
            this.splitContainer1.Panel1.Controls.Add(this.btnOPScheme);
            this.splitContainer1.Panel1.Controls.Add(this.btnDetailsPlus);
            this.splitContainer1.Panel1.Controls.Add(this.btnDetailOld);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Size = new System.Drawing.Size(1226, 759);
            this.splitContainer1.SplitterDistance = 475;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtLossDate
            // 
            this.txtLossDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLossDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLossDate.Cursor = System.Windows.Forms.Cursors.No;
            this.txtLossDate.Location = new System.Drawing.Point(206, 456);
            this.txtLossDate.Multiline = true;
            this.txtLossDate.Name = "txtLossDate";
            this.txtLossDate.ReadOnly = true;
            this.txtLossDate.Size = new System.Drawing.Size(218, 20);
            this.txtLossDate.TabIndex = 102;
            // 
            // txtPlan
            // 
            this.txtPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPlan.Cursor = System.Windows.Forms.Cursors.No;
            this.txtPlan.Location = new System.Drawing.Point(206, 437);
            this.txtPlan.Multiline = true;
            this.txtPlan.Name = "txtPlan";
            this.txtPlan.ReadOnly = true;
            this.txtPlan.Size = new System.Drawing.Size(218, 20);
            this.txtPlan.TabIndex = 100;
            // 
            // txtAge
            // 
            this.txtAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAge.Cursor = System.Windows.Forms.Cursors.No;
            this.txtAge.Location = new System.Drawing.Point(206, 419);
            this.txtAge.Multiline = true;
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(218, 20);
            this.txtAge.TabIndex = 98;
            // 
            // txtGender
            // 
            this.txtGender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGender.Cursor = System.Windows.Forms.Cursors.No;
            this.txtGender.Location = new System.Drawing.Point(206, 401);
            this.txtGender.Multiline = true;
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(218, 20);
            this.txtGender.TabIndex = 96;
            // 
            // txtMemberStatus
            // 
            this.txtMemberStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMemberStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMemberStatus.Cursor = System.Windows.Forms.Cursors.No;
            this.txtMemberStatus.Location = new System.Drawing.Point(206, 383);
            this.txtMemberStatus.Multiline = true;
            this.txtMemberStatus.Name = "txtMemberStatus";
            this.txtMemberStatus.ReadOnly = true;
            this.txtMemberStatus.Size = new System.Drawing.Size(218, 20);
            this.txtMemberStatus.TabIndex = 94;
            // 
            // txtMember
            // 
            this.txtMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMember.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMember.Cursor = System.Windows.Forms.Cursors.No;
            this.txtMember.Location = new System.Drawing.Point(206, 365);
            this.txtMember.Multiline = true;
            this.txtMember.Name = "txtMember";
            this.txtMember.ReadOnly = true;
            this.txtMember.Size = new System.Drawing.Size(218, 20);
            this.txtMember.TabIndex = 92;
            // 
            // txtPremiumStatus
            // 
            this.txtPremiumStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPremiumStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPremiumStatus.Cursor = System.Windows.Forms.Cursors.No;
            this.txtPremiumStatus.Location = new System.Drawing.Point(206, 347);
            this.txtPremiumStatus.Multiline = true;
            this.txtPremiumStatus.Name = "txtPremiumStatus";
            this.txtPremiumStatus.ReadOnly = true;
            this.txtPremiumStatus.Size = new System.Drawing.Size(218, 20);
            this.txtPremiumStatus.TabIndex = 90;
            // 
            // txtExpiryDate
            // 
            this.txtExpiryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExpiryDate.Cursor = System.Windows.Forms.Cursors.No;
            this.txtExpiryDate.Location = new System.Drawing.Point(206, 328);
            this.txtExpiryDate.Multiline = true;
            this.txtExpiryDate.Name = "txtExpiryDate";
            this.txtExpiryDate.ReadOnly = true;
            this.txtExpiryDate.Size = new System.Drawing.Size(218, 20);
            this.txtExpiryDate.TabIndex = 88;
            // 
            // txtEffectiveDate
            // 
            this.txtEffectiveDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEffectiveDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEffectiveDate.Cursor = System.Windows.Forms.Cursors.No;
            this.txtEffectiveDate.Location = new System.Drawing.Point(206, 310);
            this.txtEffectiveDate.Multiline = true;
            this.txtEffectiveDate.Name = "txtEffectiveDate";
            this.txtEffectiveDate.ReadOnly = true;
            this.txtEffectiveDate.Size = new System.Drawing.Size(218, 20);
            this.txtEffectiveDate.TabIndex = 86;
            // 
            // txtPolicyNumber
            // 
            this.txtPolicyNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPolicyNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPolicyNumber.Cursor = System.Windows.Forms.Cursors.No;
            this.txtPolicyNumber.Location = new System.Drawing.Point(206, 292);
            this.txtPolicyNumber.Multiline = true;
            this.txtPolicyNumber.Name = "txtPolicyNumber";
            this.txtPolicyNumber.ReadOnly = true;
            this.txtPolicyNumber.Size = new System.Drawing.Size(218, 20);
            this.txtPolicyNumber.TabIndex = 84;
            // 
            // txtPolicyHolder
            // 
            this.txtPolicyHolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPolicyHolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPolicyHolder.Cursor = System.Windows.Forms.Cursors.No;
            this.txtPolicyHolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPolicyHolder.Location = new System.Drawing.Point(206, 258);
            this.txtPolicyHolder.Multiline = true;
            this.txtPolicyHolder.Name = "txtPolicyHolder";
            this.txtPolicyHolder.ReadOnly = true;
            this.txtPolicyHolder.Size = new System.Drawing.Size(218, 35);
            this.txtPolicyHolder.TabIndex = 71;
            // 
            // btnOPB123
            // 
            this.btnOPB123.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOPB123.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnOPB123.Enabled = false;
            this.btnOPB123.FlatAppearance.BorderSize = 2;
            this.btnOPB123.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOPB123.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnOPB123.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.btnOPB123.Location = new System.Drawing.Point(277, 563);
            this.btnOPB123.Name = "btnOPB123";
            this.btnOPB123.Size = new System.Drawing.Size(149, 25);
            this.btnOPB123.TabIndex = 104;
            this.btnOPB123.Text = "OP Scheme + B123";
            this.btnOPB123.UseVisualStyleBackColor = false;
            this.btnOPB123.Click += new System.EventHandler(this.btnOPB123_Click);
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClaimNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClaimNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtClaimNo.Location = new System.Drawing.Point(206, 136);
            this.txtClaimNo.MaxLength = 20;
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.Size = new System.Drawing.Size(218, 20);
            this.txtClaimNo.TabIndex = 1;
            this.txtClaimNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtClaimNo.Leave += new System.EventHandler(this.txtClaimNo_Leave);
            // 
            // textBox22
            // 
            this.textBox22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox22.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox22.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox22.Location = new System.Drawing.Point(58, 456);
            this.textBox22.Multiline = true;
            this.textBox22.Name = "textBox22";
            this.textBox22.ReadOnly = true;
            this.textBox22.Size = new System.Drawing.Size(148, 20);
            this.textBox22.TabIndex = 103;
            this.textBox22.Text = "Loss Date";
            // 
            // textBox18
            // 
            this.textBox18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox18.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox18.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox18.Location = new System.Drawing.Point(58, 437);
            this.textBox18.Multiline = true;
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(148, 20);
            this.textBox18.TabIndex = 101;
            this.textBox18.Text = "Plan";
            // 
            // textBox20
            // 
            this.textBox20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox20.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox20.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox20.Location = new System.Drawing.Point(58, 419);
            this.textBox20.Multiline = true;
            this.textBox20.Name = "textBox20";
            this.textBox20.ReadOnly = true;
            this.textBox20.Size = new System.Drawing.Size(148, 20);
            this.textBox20.TabIndex = 99;
            this.textBox20.Text = "Age";
            // 
            // textBox14
            // 
            this.textBox14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox14.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox14.Location = new System.Drawing.Point(58, 401);
            this.textBox14.Multiline = true;
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(148, 20);
            this.textBox14.TabIndex = 97;
            this.textBox14.Text = "Gender";
            // 
            // textBox16
            // 
            this.textBox16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox16.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox16.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox16.Location = new System.Drawing.Point(58, 383);
            this.textBox16.Multiline = true;
            this.textBox16.Name = "textBox16";
            this.textBox16.ReadOnly = true;
            this.textBox16.Size = new System.Drawing.Size(148, 20);
            this.textBox16.TabIndex = 95;
            this.textBox16.Text = "Member Status";
            // 
            // textBox10
            // 
            this.textBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox10.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox10.Location = new System.Drawing.Point(58, 365);
            this.textBox10.Multiline = true;
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(148, 20);
            this.textBox10.TabIndex = 93;
            this.textBox10.Text = "Member";
            // 
            // textBox12
            // 
            this.textBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox12.Location = new System.Drawing.Point(58, 347);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(148, 20);
            this.textBox12.TabIndex = 91;
            this.textBox12.Text = "Premium Status";
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox6.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox6.Location = new System.Drawing.Point(58, 328);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(148, 20);
            this.textBox6.TabIndex = 89;
            this.textBox6.Text = "Expiry Date";
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox8.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox8.Location = new System.Drawing.Point(58, 310);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(148, 20);
            this.textBox8.TabIndex = 87;
            this.textBox8.Text = "Start Date";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox4.Location = new System.Drawing.Point(58, 292);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(148, 20);
            this.textBox4.TabIndex = 85;
            this.textBox4.Text = "Policy No";
            // 
            // textBox24
            // 
            this.textBox24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox24.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox24.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox24.Location = new System.Drawing.Point(58, 258);
            this.textBox24.Multiline = true;
            this.textBox24.Name = "textBox24";
            this.textBox24.ReadOnly = true;
            this.textBox24.Size = new System.Drawing.Size(148, 35);
            this.textBox24.TabIndex = 72;
            this.textBox24.Text = "Policyholder";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(58, 60);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(368, 33);
            this.textBox2.TabIndex = 83;
            this.textBox2.Text = "Benifit Scheme Calculation for Hospital & Surgical \r\nInsurance Claims";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnSearch.FlatAppearance.BorderSize = 2;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(58, 194);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(366, 25);
            this.btnSearch.TabIndex = 82;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDetail2Plus
            // 
            this.btnDetail2Plus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail2Plus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnDetail2Plus.Enabled = false;
            this.btnDetail2Plus.FlatAppearance.BorderSize = 2;
            this.btnDetail2Plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail2Plus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDetail2Plus.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.btnDetail2Plus.Location = new System.Drawing.Point(58, 620);
            this.btnDetail2Plus.Name = "btnDetail2Plus";
            this.btnDetail2Plus.Size = new System.Drawing.Size(366, 25);
            this.btnDetail2Plus.TabIndex = 81;
            this.btnDetail2Plus.Text = "Detailed Scheme (++)";
            this.btnDetail2Plus.UseVisualStyleBackColor = false;
            this.btnDetail2Plus.Click += new System.EventHandler(this.btnDetail2Plus_Click);
            // 
            // btnOPScheme
            // 
            this.btnOPScheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOPScheme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnOPScheme.Enabled = false;
            this.btnOPScheme.FlatAppearance.BorderSize = 2;
            this.btnOPScheme.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOPScheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnOPScheme.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.btnOPScheme.Location = new System.Drawing.Point(276, 507);
            this.btnOPScheme.Name = "btnOPScheme";
            this.btnOPScheme.Size = new System.Drawing.Size(148, 25);
            this.btnOPScheme.TabIndex = 79;
            this.btnOPScheme.Text = "OP Scheme";
            this.btnOPScheme.UseVisualStyleBackColor = false;
            this.btnOPScheme.Click += new System.EventHandler(this.bnRem3Send_Click);
            // 
            // btnDetailsPlus
            // 
            this.btnDetailsPlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetailsPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnDetailsPlus.Enabled = false;
            this.btnDetailsPlus.FlatAppearance.BorderSize = 2;
            this.btnDetailsPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetailsPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDetailsPlus.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.btnDetailsPlus.Location = new System.Drawing.Point(58, 563);
            this.btnDetailsPlus.Name = "btnDetailsPlus";
            this.btnDetailsPlus.Size = new System.Drawing.Size(148, 25);
            this.btnDetailsPlus.TabIndex = 78;
            this.btnDetailsPlus.Text = "Detailed Scheme (+)";
            this.btnDetailsPlus.UseVisualStyleBackColor = false;
            this.btnDetailsPlus.Click += new System.EventHandler(this.btnDetailsPlus_Click);
            // 
            // btnDetailOld
            // 
            this.btnDetailOld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetailOld.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnDetailOld.Enabled = false;
            this.btnDetailOld.FlatAppearance.BorderSize = 2;
            this.btnDetailOld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetailOld.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnDetailOld.ForeColor = System.Drawing.Color.AntiqueWhite;
            this.btnDetailOld.Location = new System.Drawing.Point(58, 507);
            this.btnDetailOld.Name = "btnDetailOld";
            this.btnDetailOld.Size = new System.Drawing.Size(148, 25);
            this.btnDetailOld.TabIndex = 77;
            this.btnDetailOld.Text = "Detailed Scheme (Old)";
            this.btnDetailOld.UseVisualStyleBackColor = false;
            this.btnDetailOld.Click += new System.EventHandler(this.bnRem2Send_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.textBox1.Location = new System.Drawing.Point(58, 136);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 75;
            this.textBox1.Text = "Claim No.:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1232, 778);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HNS BENIFIT SCHEME";
            // 
            // HospitalSurgicalScheme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1232, 841);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbTitle);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HospitalSurgicalScheme";
            this.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HospitalSurgicalScheme";
            this.Load += new System.EventHandler(this.HospitalSurgicalScheme_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private SplitContainer splitContainer1;
        private cus_button btnOPB123;
        public System.Windows.Forms.TextBox txtClaimNo;
        private System.Windows.Forms.TextBox textBox22;
        public System.Windows.Forms.TextBox txtLossDate;
        private System.Windows.Forms.TextBox textBox18;
        public System.Windows.Forms.TextBox txtPlan;
        private System.Windows.Forms.TextBox textBox20;
        public System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox textBox14;
        public System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox textBox16;
        public System.Windows.Forms.TextBox txtMemberStatus;
        private System.Windows.Forms.TextBox textBox10;
        public System.Windows.Forms.TextBox txtMember;
        private System.Windows.Forms.TextBox textBox12;
        public System.Windows.Forms.TextBox txtPremiumStatus;
        private System.Windows.Forms.TextBox textBox6;
        public System.Windows.Forms.TextBox txtExpiryDate;
        private System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.TextBox txtEffectiveDate;
        private System.Windows.Forms.TextBox textBox4;
        public System.Windows.Forms.TextBox txtPolicyNumber;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox2;
        private cus_button btnSearch;
        private cus_button btnDetail2Plus;
        private cus_button btnOPScheme;
        private cus_button btnDetailsPlus;
        private cus_button btnDetailOld;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox txtPolicyHolder;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}