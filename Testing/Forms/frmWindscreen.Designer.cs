namespace Testing.Forms
{
    partial class frmWindscreen
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabQueryPolicy = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvClaimDetailPolicy = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblTotalOS = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPaidorOS = new System.Windows.Forms.TextBox();
            this.tbPolicyPremium = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tbAH = new System.Windows.Forms.TextBox();
            this.tbIntermediary = new System.Windows.Forms.TextBox();
            this.tbPolicyPeriod = new System.Windows.Forms.TextBox();
            this.tbInsured = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.bnClearPolicy = new Testing.cus_button();
            this.tbPolNo = new System.Windows.Forms.TextBox();
            this.bnSearchPolicy = new Testing.cus_button();
            this.tabQueryClaim = new System.Windows.Forms.TabPage();
            this.bnGetLetter = new Testing.cus_button();
            this.lblTotalOSCl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblReSentDate = new System.Windows.Forms.Label();
            this.lblSentDate = new System.Windows.Forms.Label();
            this.tbCC = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tbTo = new System.Windows.Forms.TextBox();
            this.label453 = new System.Windows.Forms.Label();
            this.label343 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvClaimDetail = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bnClear = new Testing.cus_button();
            this.tbClaimNo = new System.Windows.Forms.TextBox();
            this.bnClaimSearch = new Testing.cus_button();
            this.bnResendEmail = new Testing.cus_button();
            this.bnSendEmail = new Testing.cus_button();
            this.tabReport = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cbWindscreen = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbExportAs = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.bnExport = new Testing.cus_button();
            this.dtpIntDateFr = new System.Windows.Forms.DateTimePicker();
            this.dtpIntDateTo = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.bnClearReport = new Testing.cus_button();
            this.label19 = new System.Windows.Forms.Label();
            this.bnSearchReport = new Testing.cus_button();
            this.label18 = new System.Windows.Forms.Label();
            this.fdbSelectPath = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControlMain.SuspendLayout();
            this.tabQueryPolicy.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimDetailPolicy)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabQueryClaim.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimDetail)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabReport.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(1087, 46);
            this.label1.TabIndex = 13;
            this.label1.Text = "Windscreen";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabQueryPolicy);
            this.tabControlMain.Controls.Add(this.tabQueryClaim);
            this.tabControlMain.Controls.Add(this.tabReport);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlMain.Location = new System.Drawing.Point(0, 46);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1087, 631);
            this.tabControlMain.TabIndex = 14;
            // 
            // tabQueryPolicy
            // 
            this.tabQueryPolicy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.tabQueryPolicy.Controls.Add(this.groupBox2);
            this.tabQueryPolicy.Controls.Add(this.groupBox6);
            this.tabQueryPolicy.Controls.Add(this.panel1);
            this.tabQueryPolicy.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.tabQueryPolicy.ForeColor = System.Drawing.Color.White;
            this.tabQueryPolicy.Location = new System.Drawing.Point(4, 27);
            this.tabQueryPolicy.Name = "tabQueryPolicy";
            this.tabQueryPolicy.Padding = new System.Windows.Forms.Padding(3);
            this.tabQueryPolicy.Size = new System.Drawing.Size(1079, 600);
            this.tabQueryPolicy.TabIndex = 0;
            this.tabQueryPolicy.Text = "Query - Policy";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvClaimDetailPolicy);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(3, 228);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1073, 369);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Claims Details";
            // 
            // dgvClaimDetailPolicy
            // 
            this.dgvClaimDetailPolicy.AllowUserToAddRows = false;
            this.dgvClaimDetailPolicy.AllowUserToDeleteRows = false;
            this.dgvClaimDetailPolicy.AllowUserToResizeRows = false;
            this.dgvClaimDetailPolicy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClaimDetailPolicy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClaimDetailPolicy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClaimDetailPolicy.Location = new System.Drawing.Point(3, 19);
            this.dgvClaimDetailPolicy.Name = "dgvClaimDetailPolicy";
            this.dgvClaimDetailPolicy.ReadOnly = true;
            this.dgvClaimDetailPolicy.RowHeadersVisible = false;
            this.dgvClaimDetailPolicy.Size = new System.Drawing.Size(1067, 347);
            this.dgvClaimDetailPolicy.TabIndex = 0;
            this.dgvClaimDetailPolicy.DataSourceChanged += new System.EventHandler(this.dgvClaimDetailPolicy_DataSourceChanged);
            this.dgvClaimDetailPolicy.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClaimDetailPolicy_CellDoubleClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblTotalOS);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.tbPaidorOS);
            this.groupBox6.Controls.Add(this.tbPolicyPremium);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.label26);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.label30);
            this.groupBox6.Controls.Add(this.tbAH);
            this.groupBox6.Controls.Add(this.tbIntermediary);
            this.groupBox6.Controls.Add(this.tbPolicyPeriod);
            this.groupBox6.Controls.Add(this.tbInsured);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox6.Location = new System.Drawing.Point(3, 49);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1073, 179);
            this.groupBox6.TabIndex = 43;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Policy Information";
            // 
            // lblTotalOS
            // 
            this.lblTotalOS.AutoSize = true;
            this.lblTotalOS.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOS.Location = new System.Drawing.Point(522, 142);
            this.lblTotalOS.Name = "lblTotalOS";
            this.lblTotalOS.Size = new System.Drawing.Size(0, 19);
            this.lblTotalOS.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(447, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 19);
            this.label3.TabIndex = 47;
            this.label3.Text = "Total OS:";
            // 
            // tbPaidorOS
            // 
            this.tbPaidorOS.Font = new System.Drawing.Font("Cambria", 9F);
            this.tbPaidorOS.Location = new System.Drawing.Point(298, 143);
            this.tbPaidorOS.Multiline = true;
            this.tbPaidorOS.Name = "tbPaidorOS";
            this.tbPaidorOS.ReadOnly = true;
            this.tbPaidorOS.Size = new System.Drawing.Size(78, 18);
            this.tbPaidorOS.TabIndex = 35;
            // 
            // tbPolicyPremium
            // 
            this.tbPolicyPremium.Font = new System.Drawing.Font("Cambria", 9F);
            this.tbPolicyPremium.Location = new System.Drawing.Point(126, 143);
            this.tbPolicyPremium.Multiline = true;
            this.tbPolicyPremium.Name = "tbPolicyPremium";
            this.tbPolicyPremium.ReadOnly = true;
            this.tbPolicyPremium.Size = new System.Drawing.Size(144, 18);
            this.tbPolicyPremium.TabIndex = 35;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(9, 146);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(99, 15);
            this.label23.TabIndex = 34;
            this.label23.Text = "Policy Premium:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(9, 126);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(27, 15);
            this.label25.TabIndex = 34;
            this.label25.Text = "AH:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(9, 106);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 15);
            this.label26.TabIndex = 34;
            this.label26.Text = "Intermediary:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(9, 86);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(84, 15);
            this.label27.TabIndex = 33;
            this.label27.Text = "Policy Period:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(279, 144);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(11, 15);
            this.label29.TabIndex = 31;
            this.label29.Text = "-";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(9, 25);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 15);
            this.label30.TabIndex = 31;
            this.label30.Text = "Insured:";
            // 
            // tbAH
            // 
            this.tbAH.Font = new System.Drawing.Font("Cambria", 9F);
            this.tbAH.Location = new System.Drawing.Point(126, 123);
            this.tbAH.Multiline = true;
            this.tbAH.Name = "tbAH";
            this.tbAH.ReadOnly = true;
            this.tbAH.Size = new System.Drawing.Size(250, 18);
            this.tbAH.TabIndex = 0;
            // 
            // tbIntermediary
            // 
            this.tbIntermediary.Font = new System.Drawing.Font("Cambria", 9F);
            this.tbIntermediary.Location = new System.Drawing.Point(126, 103);
            this.tbIntermediary.Multiline = true;
            this.tbIntermediary.Name = "tbIntermediary";
            this.tbIntermediary.ReadOnly = true;
            this.tbIntermediary.Size = new System.Drawing.Size(250, 18);
            this.tbIntermediary.TabIndex = 0;
            // 
            // tbPolicyPeriod
            // 
            this.tbPolicyPeriod.Font = new System.Drawing.Font("Cambria", 9F);
            this.tbPolicyPeriod.Location = new System.Drawing.Point(126, 83);
            this.tbPolicyPeriod.Multiline = true;
            this.tbPolicyPeriod.Name = "tbPolicyPeriod";
            this.tbPolicyPeriod.ReadOnly = true;
            this.tbPolicyPeriod.Size = new System.Drawing.Size(250, 18);
            this.tbPolicyPeriod.TabIndex = 0;
            // 
            // tbInsured
            // 
            this.tbInsured.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbInsured.Location = new System.Drawing.Point(126, 22);
            this.tbInsured.Multiline = true;
            this.tbInsured.Name = "tbInsured";
            this.tbInsured.ReadOnly = true;
            this.tbInsured.Size = new System.Drawing.Size(250, 59);
            this.tbInsured.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.bnClearPolicy);
            this.panel1.Controls.Add(this.tbPolNo);
            this.panel1.Controls.Add(this.bnSearchPolicy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 46);
            this.panel1.TabIndex = 21;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(9, 14);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(64, 15);
            this.label22.TabIndex = 17;
            this.label22.Text = "Policy No:";
            // 
            // bnClearPolicy
            // 
            this.bnClearPolicy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClearPolicy.FlatAppearance.BorderSize = 2;
            this.bnClearPolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClearPolicy.ForeColor = System.Drawing.Color.White;
            this.bnClearPolicy.Location = new System.Drawing.Point(406, 8);
            this.bnClearPolicy.Name = "bnClearPolicy";
            this.bnClearPolicy.Size = new System.Drawing.Size(98, 26);
            this.bnClearPolicy.TabIndex = 18;
            this.bnClearPolicy.Text = "Clear";
            this.bnClearPolicy.UseVisualStyleBackColor = false;
            this.bnClearPolicy.Click += new System.EventHandler(this.bnClearPolicy_Click);
            // 
            // tbPolNo
            // 
            this.tbPolNo.Location = new System.Drawing.Point(79, 11);
            this.tbPolNo.MaxLength = 20;
            this.tbPolNo.Name = "tbPolNo";
            this.tbPolNo.Size = new System.Drawing.Size(209, 23);
            this.tbPolNo.TabIndex = 16;
            // 
            // bnSearchPolicy
            // 
            this.bnSearchPolicy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearchPolicy.FlatAppearance.BorderSize = 2;
            this.bnSearchPolicy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearchPolicy.ForeColor = System.Drawing.Color.White;
            this.bnSearchPolicy.Location = new System.Drawing.Point(302, 8);
            this.bnSearchPolicy.Name = "bnSearchPolicy";
            this.bnSearchPolicy.Size = new System.Drawing.Size(98, 26);
            this.bnSearchPolicy.TabIndex = 15;
            this.bnSearchPolicy.Text = "Search";
            this.bnSearchPolicy.UseVisualStyleBackColor = false;
            this.bnSearchPolicy.Click += new System.EventHandler(this.bnSearchPolicy_Click);
            // 
            // tabQueryClaim
            // 
            this.tabQueryClaim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tabQueryClaim.Controls.Add(this.bnGetLetter);
            this.tabQueryClaim.Controls.Add(this.lblTotalOSCl);
            this.tabQueryClaim.Controls.Add(this.label5);
            this.tabQueryClaim.Controls.Add(this.lblReSentDate);
            this.tabQueryClaim.Controls.Add(this.lblSentDate);
            this.tabQueryClaim.Controls.Add(this.tbCC);
            this.tabQueryClaim.Controls.Add(this.label20);
            this.tabQueryClaim.Controls.Add(this.label12);
            this.tabQueryClaim.Controls.Add(this.tbTo);
            this.tabQueryClaim.Controls.Add(this.label453);
            this.tabQueryClaim.Controls.Add(this.label343);
            this.tabQueryClaim.Controls.Add(this.groupBox3);
            this.tabQueryClaim.Controls.Add(this.panel2);
            this.tabQueryClaim.Controls.Add(this.bnResendEmail);
            this.tabQueryClaim.Controls.Add(this.bnSendEmail);
            this.tabQueryClaim.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.tabQueryClaim.ForeColor = System.Drawing.Color.White;
            this.tabQueryClaim.Location = new System.Drawing.Point(4, 27);
            this.tabQueryClaim.Name = "tabQueryClaim";
            this.tabQueryClaim.Padding = new System.Windows.Forms.Padding(3);
            this.tabQueryClaim.Size = new System.Drawing.Size(1079, 600);
            this.tabQueryClaim.TabIndex = 1;
            this.tabQueryClaim.Text = "Query - Claim";
            // 
            // bnGetLetter
            // 
            this.bnGetLetter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnGetLetter.FlatAppearance.BorderSize = 2;
            this.bnGetLetter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnGetLetter.ForeColor = System.Drawing.Color.White;
            this.bnGetLetter.Location = new System.Drawing.Point(26, 412);
            this.bnGetLetter.Name = "bnGetLetter";
            this.bnGetLetter.Size = new System.Drawing.Size(111, 25);
            this.bnGetLetter.TabIndex = 64;
            this.bnGetLetter.Text = "Get Letter";
            this.bnGetLetter.UseVisualStyleBackColor = false;
            this.bnGetLetter.Click += new System.EventHandler(this.bnGetLetter_Click);
            // 
            // lblTotalOSCl
            // 
            this.lblTotalOSCl.AutoSize = true;
            this.lblTotalOSCl.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOSCl.Location = new System.Drawing.Point(97, 359);
            this.lblTotalOSCl.Name = "lblTotalOSCl";
            this.lblTotalOSCl.Size = new System.Drawing.Size(0, 19);
            this.lblTotalOSCl.TabIndex = 63;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(22, 359);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 62;
            this.label5.Text = "Total OS:";
            // 
            // lblReSentDate
            // 
            this.lblReSentDate.AutoSize = true;
            this.lblReSentDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReSentDate.Location = new System.Drawing.Point(471, 520);
            this.lblReSentDate.Name = "lblReSentDate";
            this.lblReSentDate.Size = new System.Drawing.Size(0, 15);
            this.lblReSentDate.TabIndex = 60;
            // 
            // lblSentDate
            // 
            this.lblSentDate.AutoSize = true;
            this.lblSentDate.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSentDate.Location = new System.Drawing.Point(471, 489);
            this.lblSentDate.Name = "lblSentDate";
            this.lblSentDate.Size = new System.Drawing.Size(0, 15);
            this.lblSentDate.TabIndex = 61;
            // 
            // tbCC
            // 
            this.tbCC.Location = new System.Drawing.Point(245, 414);
            this.tbCC.Multiline = true;
            this.tbCC.Name = "tbCC";
            this.tbCC.Size = new System.Drawing.Size(392, 63);
            this.tbCC.TabIndex = 59;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(213, 417);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 15);
            this.label20.TabIndex = 58;
            this.label20.Text = "Cc:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(213, 362);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(24, 15);
            this.label12.TabIndex = 57;
            this.label12.Text = "To:";
            // 
            // tbTo
            // 
            this.tbTo.Location = new System.Drawing.Point(246, 359);
            this.tbTo.Multiline = true;
            this.tbTo.Name = "tbTo";
            this.tbTo.Size = new System.Drawing.Size(391, 49);
            this.tbTo.TabIndex = 56;
            // 
            // label453
            // 
            this.label453.AutoSize = true;
            this.label453.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label453.Location = new System.Drawing.Point(404, 519);
            this.label453.Name = "label453";
            this.label453.Size = new System.Drawing.Size(61, 15);
            this.label453.TabIndex = 53;
            this.label453.Text = "Sent Date:";
            // 
            // label343
            // 
            this.label343.AutoSize = true;
            this.label343.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label343.Location = new System.Drawing.Point(404, 488);
            this.label343.Name = "label343";
            this.label343.Size = new System.Drawing.Size(61, 15);
            this.label343.TabIndex = 54;
            this.label343.Text = "Sent Date:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvClaimDetail);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox3.Location = new System.Drawing.Point(3, 49);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1073, 299);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Claims Details";
            // 
            // dgvClaimDetail
            // 
            this.dgvClaimDetail.AllowUserToAddRows = false;
            this.dgvClaimDetail.AllowUserToDeleteRows = false;
            this.dgvClaimDetail.AllowUserToResizeRows = false;
            this.dgvClaimDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClaimDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClaimDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvClaimDetail.Location = new System.Drawing.Point(3, 19);
            this.dgvClaimDetail.Name = "dgvClaimDetail";
            this.dgvClaimDetail.ReadOnly = true;
            this.dgvClaimDetail.RowHeadersVisible = false;
            this.dgvClaimDetail.Size = new System.Drawing.Size(1067, 277);
            this.dgvClaimDetail.TabIndex = 0;
            this.dgvClaimDetail.DataSourceChanged += new System.EventHandler(this.dgvClaimDetail_DataSourceChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.bnClear);
            this.panel2.Controls.Add(this.tbClaimNo);
            this.panel2.Controls.Add(this.bnClaimSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1073, 46);
            this.panel2.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Claim No:";
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClear.FlatAppearance.BorderSize = 2;
            this.bnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClear.ForeColor = System.Drawing.Color.White;
            this.bnClear.Location = new System.Drawing.Point(406, 8);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(98, 26);
            this.bnClear.TabIndex = 18;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = false;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // tbClaimNo
            // 
            this.tbClaimNo.Location = new System.Drawing.Point(76, 11);
            this.tbClaimNo.MaxLength = 20;
            this.tbClaimNo.Name = "tbClaimNo";
            this.tbClaimNo.Size = new System.Drawing.Size(209, 23);
            this.tbClaimNo.TabIndex = 16;
            // 
            // bnClaimSearch
            // 
            this.bnClaimSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClaimSearch.FlatAppearance.BorderSize = 2;
            this.bnClaimSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClaimSearch.ForeColor = System.Drawing.Color.White;
            this.bnClaimSearch.Location = new System.Drawing.Point(302, 8);
            this.bnClaimSearch.Name = "bnClaimSearch";
            this.bnClaimSearch.Size = new System.Drawing.Size(98, 26);
            this.bnClaimSearch.TabIndex = 15;
            this.bnClaimSearch.Text = "Search";
            this.bnClaimSearch.UseVisualStyleBackColor = false;
            this.bnClaimSearch.Click += new System.EventHandler(this.bnClaimSearch_Click);
            // 
            // bnResendEmail
            // 
            this.bnResendEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnResendEmail.FlatAppearance.BorderSize = 2;
            this.bnResendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnResendEmail.ForeColor = System.Drawing.Color.White;
            this.bnResendEmail.Location = new System.Drawing.Point(246, 514);
            this.bnResendEmail.Name = "bnResendEmail";
            this.bnResendEmail.Size = new System.Drawing.Size(151, 25);
            this.bnResendEmail.TabIndex = 48;
            this.bnResendEmail.Text = "Resend Email";
            this.bnResendEmail.UseVisualStyleBackColor = false;
            this.bnResendEmail.Click += new System.EventHandler(this.bnResendEmail_Click);
            // 
            // bnSendEmail
            // 
            this.bnSendEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSendEmail.FlatAppearance.BorderSize = 2;
            this.bnSendEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSendEmail.ForeColor = System.Drawing.Color.White;
            this.bnSendEmail.Location = new System.Drawing.Point(246, 483);
            this.bnSendEmail.Name = "bnSendEmail";
            this.bnSendEmail.Size = new System.Drawing.Size(151, 25);
            this.bnSendEmail.TabIndex = 49;
            this.bnSendEmail.Text = "Send Email";
            this.bnSendEmail.UseVisualStyleBackColor = false;
            this.bnSendEmail.Click += new System.EventHandler(this.bnSendEmail_Click);
            // 
            // tabReport
            // 
            this.tabReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tabReport.Controls.Add(this.groupBox5);
            this.tabReport.Controls.Add(this.panel3);
            this.tabReport.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.tabReport.ForeColor = System.Drawing.Color.White;
            this.tabReport.Location = new System.Drawing.Point(4, 27);
            this.tabReport.Name = "tabReport";
            this.tabReport.Padding = new System.Windows.Forms.Padding(3);
            this.tabReport.Size = new System.Drawing.Size(1079, 600);
            this.tabReport.TabIndex = 2;
            this.tabReport.Text = "Report";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvReport);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox5.Location = new System.Drawing.Point(3, 114);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1073, 483);
            this.groupBox5.TabIndex = 35;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Details of Windscreen Claims";
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AllowUserToDeleteRows = false;
            this.dgvReport.AllowUserToResizeRows = false;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(3, 19);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.Size = new System.Drawing.Size(1067, 461);
            this.dgvReport.TabIndex = 0;
            this.dgvReport.DataSourceChanged += new System.EventHandler(this.dgvReport_DataSourceChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cbWindscreen);
            this.panel3.Controls.Add(this.groupBox4);
            this.panel3.Controls.Add(this.dtpIntDateFr);
            this.panel3.Controls.Add(this.dtpIntDateTo);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.bnClearReport);
            this.panel3.Controls.Add(this.label19);
            this.panel3.Controls.Add(this.bnSearchReport);
            this.panel3.Controls.Add(this.label18);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1073, 111);
            this.panel3.TabIndex = 34;
            // 
            // cbWindscreen
            // 
            this.cbWindscreen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindscreen.FormattingEnabled = true;
            this.cbWindscreen.Items.AddRange(new object[] {
            "Paid",
            "OS",
            "All"});
            this.cbWindscreen.Location = new System.Drawing.Point(157, 41);
            this.cbWindscreen.Name = "cbWindscreen";
            this.cbWindscreen.Size = new System.Drawing.Size(282, 23);
            this.cbWindscreen.TabIndex = 21;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbExportAs);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.bnExport);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox4.Location = new System.Drawing.Point(474, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(249, 89);
            this.groupBox4.TabIndex = 32;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Export Report";
            // 
            // cbExportAs
            // 
            this.cbExportAs.FormattingEnabled = true;
            this.cbExportAs.Items.AddRange(new object[] {
            "Excel",
            "PDF"});
            this.cbExportAs.Location = new System.Drawing.Point(91, 21);
            this.cbExportAs.Name = "cbExportAs";
            this.cbExportAs.Size = new System.Drawing.Size(127, 23);
            this.cbExportAs.TabIndex = 23;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(24, 24);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(61, 15);
            this.label21.TabIndex = 22;
            this.label21.Text = "Export as:";
            // 
            // bnExport
            // 
            this.bnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnExport.FlatAppearance.BorderSize = 2;
            this.bnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnExport.ForeColor = System.Drawing.Color.White;
            this.bnExport.Location = new System.Drawing.Point(76, 52);
            this.bnExport.Name = "bnExport";
            this.bnExport.Size = new System.Drawing.Size(98, 26);
            this.bnExport.TabIndex = 20;
            this.bnExport.Text = "Export";
            this.bnExport.UseVisualStyleBackColor = false;
            this.bnExport.Click += new System.EventHandler(this.bnExport_Click);
            // 
            // dtpIntDateFr
            // 
            this.dtpIntDateFr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIntDateFr.Location = new System.Drawing.Point(157, 11);
            this.dtpIntDateFr.Name = "dtpIntDateFr";
            this.dtpIntDateFr.Size = new System.Drawing.Size(122, 23);
            this.dtpIntDateFr.TabIndex = 0;
            // 
            // dtpIntDateTo
            // 
            this.dtpIntDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIntDateTo.Location = new System.Drawing.Point(317, 11);
            this.dtpIntDateTo.Name = "dtpIntDateTo";
            this.dtpIntDateTo.Size = new System.Drawing.Size(122, 23);
            this.dtpIntDateTo.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 15);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(129, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "Intimation Date From:";
            // 
            // bnClearReport
            // 
            this.bnClearReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClearReport.FlatAppearance.BorderSize = 2;
            this.bnClearReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClearReport.ForeColor = System.Drawing.Color.White;
            this.bnClearReport.Location = new System.Drawing.Point(261, 74);
            this.bnClearReport.Name = "bnClearReport";
            this.bnClearReport.Size = new System.Drawing.Size(98, 26);
            this.bnClearReport.TabIndex = 20;
            this.bnClearReport.Text = "Clear";
            this.bnClearReport.UseVisualStyleBackColor = false;
            this.bnClearReport.Click += new System.EventHandler(this.bnClearReport_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(20, 44);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 15);
            this.label19.TabIndex = 18;
            this.label19.Text = "Windscreen Claims";
            // 
            // bnSearchReport
            // 
            this.bnSearchReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearchReport.FlatAppearance.BorderSize = 2;
            this.bnSearchReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearchReport.ForeColor = System.Drawing.Color.White;
            this.bnSearchReport.Location = new System.Drawing.Point(157, 74);
            this.bnSearchReport.Name = "bnSearchReport";
            this.bnSearchReport.Size = new System.Drawing.Size(98, 26);
            this.bnSearchReport.TabIndex = 19;
            this.bnSearchReport.Text = "Search";
            this.bnSearchReport.UseVisualStyleBackColor = false;
            this.bnSearchReport.Click += new System.EventHandler(this.bnSearchReport_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(287, 15);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 15);
            this.label18.TabIndex = 18;
            this.label18.Text = "To:";
            // 
            // frmWindscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1087, 677);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWindscreen";
            this.Text = "frmWindscreen";
            this.Activated += new System.EventHandler(this.frmWindscreen_Activated);
            this.Load += new System.EventHandler(this.frmWindscreen_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabQueryPolicy.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimDetailPolicy)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabQueryClaim.ResumeLayout(false);
            this.tabQueryClaim.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClaimDetail)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabReport.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabQueryPolicy;
        private System.Windows.Forms.TabPage tabQueryClaim;
        private System.Windows.Forms.TabPage tabReport;
        private System.Windows.Forms.Label label453;
        private System.Windows.Forms.Label label343;
        private cus_button bnResendEmail;
        private cus_button bnSendEmail;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvClaimDetail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private cus_button bnClear;
        private System.Windows.Forms.TextBox tbClaimNo;
        private cus_button bnClaimSearch;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cbWindscreen;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbExportAs;
        private System.Windows.Forms.Label label21;
        private cus_button bnExport;
        private System.Windows.Forms.DateTimePicker dtpIntDateFr;
        private System.Windows.Forms.DateTimePicker dtpIntDateTo;
        private System.Windows.Forms.Label label13;
        private cus_button bnClearReport;
        private System.Windows.Forms.Label label19;
        private cus_button bnSearchReport;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox tbPaidorOS;
        private System.Windows.Forms.TextBox tbPolicyPremium;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox tbAH;
        private System.Windows.Forms.TextBox tbIntermediary;
        private System.Windows.Forms.TextBox tbPolicyPeriod;
        private System.Windows.Forms.TextBox tbInsured;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label22;
        private cus_button bnClearPolicy;
        private System.Windows.Forms.TextBox tbPolNo;
        private cus_button bnSearchPolicy;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvClaimDetailPolicy;
        private System.Windows.Forms.FolderBrowserDialog fdbSelectPath;
        private System.Windows.Forms.Label lblTotalOS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbCC;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbTo;
        private System.Windows.Forms.Label lblReSentDate;
        private System.Windows.Forms.Label lblSentDate;
        private System.Windows.Forms.Label lblTotalOSCl;
        private System.Windows.Forms.Label label5;
        private cus_button bnGetLetter;
    }
}