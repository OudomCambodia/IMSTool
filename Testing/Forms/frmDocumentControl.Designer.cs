namespace Testing.Forms
{
    partial class frmDocumentControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pDoc = new System.Windows.Forms.Panel();
            this.dgvDoc = new System.Windows.Forms.DataGridView();
            this.pNotification = new System.Windows.Forms.Panel();
            this.dgvNoti = new System.Windows.Forms.DataGridView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnNotification = new System.Windows.Forms.PictureBox();
            this.lblNotiCount = new System.Windows.Forms.Label();
            this.btnPendingAtDP = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnPackaged = new System.Windows.Forms.Button();
            this.btnPackaging = new System.Windows.Forms.Button();
            this.btnPendingforSign = new System.Windows.Forms.Button();
            this.btnDPProcessed = new System.Windows.Forms.Button();
            this.btnDPProcessing = new System.Windows.Forms.Button();
            this.btnControllerAccepted = new System.Windows.Forms.Button();
            this.btnSubmittedtoUW = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gbAllRecordOption = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpSpecificDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSpecificDateFr = new System.Windows.Forms.DateTimePicker();
            this.rbSpecificDate = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbCustomer = new System.Windows.Forms.RadioButton();
            this.rbRefID = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFilterdgvDoc = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.lblTot = new System.Windows.Forms.Label();
            this.lblSel = new System.Windows.Forms.Label();
            this.btnExportRecord = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDPPendingRemark = new System.Windows.Forms.Button();
            this.btnManageCrono = new System.Windows.Forms.Button();
            this.btnReverse = new System.Windows.Forms.Button();
            this.btnRefreshdgv = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.timerNoti = new System.Windows.Forms.Timer(this.components);
            this.btnReturn = new Testing.cus_button();
            this.btnReassignDP = new Testing.cus_button();
            this.btnChangeStatus = new Testing.cus_button();
            this.btnCloseReopen = new Testing.cus_button();
            this.btnAddDoc = new Testing.cus_button();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pDoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoc)).BeginInit();
            this.pNotification.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoti)).BeginInit();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNotification)).BeginInit();
            this.panel3.SuspendLayout();
            this.gbAllRecordOption.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1087, 631);
            this.panel1.TabIndex = 12;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pDoc);
            this.panel4.Controls.Add(this.pNotification);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1087, 506);
            this.panel4.TabIndex = 46;
            // 
            // pDoc
            // 
            this.pDoc.Controls.Add(this.dgvDoc);
            this.pDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDoc.Location = new System.Drawing.Point(0, 0);
            this.pDoc.Name = "pDoc";
            this.pDoc.Size = new System.Drawing.Size(883, 506);
            this.pDoc.TabIndex = 10;
            // 
            // dgvDoc
            // 
            this.dgvDoc.AllowUserToAddRows = false;
            this.dgvDoc.AllowUserToDeleteRows = false;
            this.dgvDoc.AllowUserToResizeRows = false;
            this.dgvDoc.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvDoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDoc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoc.Location = new System.Drawing.Point(0, 0);
            this.dgvDoc.Name = "dgvDoc";
            this.dgvDoc.RowHeadersVisible = false;
            this.dgvDoc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvDoc.RowTemplate.Height = 30;
            this.dgvDoc.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDoc.Size = new System.Drawing.Size(883, 506);
            this.dgvDoc.TabIndex = 8;
            this.dgvDoc.DataSourceChanged += new System.EventHandler(this.dgvDoc_DataSourceChanged);
            this.dgvDoc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoc_CellContentClick);
            this.dgvDoc.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoc_CellDoubleClick);
            this.dgvDoc.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDoc_CellFormatting);
            this.dgvDoc.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvOpen_Paint);
            // 
            // pNotification
            // 
            this.pNotification.Controls.Add(this.dgvNoti);
            this.pNotification.Dock = System.Windows.Forms.DockStyle.Right;
            this.pNotification.Location = new System.Drawing.Point(883, 0);
            this.pNotification.Name = "pNotification";
            this.pNotification.Size = new System.Drawing.Size(204, 506);
            this.pNotification.TabIndex = 9;
            // 
            // dgvNoti
            // 
            this.dgvNoti.AllowUserToAddRows = false;
            this.dgvNoti.AllowUserToDeleteRows = false;
            this.dgvNoti.AllowUserToResizeColumns = false;
            this.dgvNoti.AllowUserToResizeRows = false;
            this.dgvNoti.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNoti.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvNoti.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvNoti.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvNoti.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNoti.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNoti.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNoti.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNoti.Location = new System.Drawing.Point(0, 0);
            this.dgvNoti.MultiSelect = false;
            this.dgvNoti.Name = "dgvNoti";
            this.dgvNoti.ReadOnly = true;
            this.dgvNoti.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvNoti.RowHeadersVisible = false;
            this.dgvNoti.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvNoti.RowTemplate.Height = 35;
            this.dgvNoti.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvNoti.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNoti.Size = new System.Drawing.Size(204, 506);
            this.dgvNoti.TabIndex = 1;
            this.dgvNoti.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNoti_CellDoubleClick);
            this.dgvNoti.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvNoti_CellFormatting);
            this.dgvNoti.SelectionChanged += new System.EventHandler(this.dgvNoti_SelectionChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btnNotification);
            this.panel8.Controls.Add(this.lblNotiCount);
            this.panel8.Controls.Add(this.btnPendingAtDP);
            this.panel8.Controls.Add(this.btnAll);
            this.panel8.Controls.Add(this.btnCancel);
            this.panel8.Controls.Add(this.btnDone);
            this.panel8.Controls.Add(this.btnPackaged);
            this.panel8.Controls.Add(this.btnPackaging);
            this.panel8.Controls.Add(this.btnPendingforSign);
            this.panel8.Controls.Add(this.btnDPProcessed);
            this.panel8.Controls.Add(this.btnDPProcessing);
            this.panel8.Controls.Add(this.btnControllerAccepted);
            this.panel8.Controls.Add(this.btnSubmittedtoUW);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 83);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1087, 42);
            this.panel8.TabIndex = 45;
            // 
            // btnNotification
            // 
            this.btnNotification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNotification.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotification.Image = global::Testing.Properties.Resources._4Tlt_unscreen1;
            this.btnNotification.Location = new System.Drawing.Point(1038, 6);
            this.btnNotification.Name = "btnNotification";
            this.btnNotification.Size = new System.Drawing.Size(28, 28);
            this.btnNotification.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnNotification.TabIndex = 12;
            this.btnNotification.TabStop = false;
            this.btnNotification.Click += new System.EventHandler(this.btnNotification_Click);
            // 
            // lblNotiCount
            // 
            this.lblNotiCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNotiCount.AutoSize = true;
            this.lblNotiCount.BackColor = System.Drawing.Color.Transparent;
            this.lblNotiCount.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotiCount.ForeColor = System.Drawing.Color.Red;
            this.lblNotiCount.Location = new System.Drawing.Point(1063, -2);
            this.lblNotiCount.Name = "lblNotiCount";
            this.lblNotiCount.Size = new System.Drawing.Size(21, 14);
            this.lblNotiCount.TabIndex = 13;
            this.lblNotiCount.Text = "99";
            this.lblNotiCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotiCount.TextChanged += new System.EventHandler(this.lblNotiCount_TextChanged);
            // 
            // btnPendingAtDP
            // 
            this.btnPendingAtDP.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPendingAtDP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPendingAtDP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPendingAtDP.Location = new System.Drawing.Point(693, 6);
            this.btnPendingAtDP.Name = "btnPendingAtDP";
            this.btnPendingAtDP.Size = new System.Drawing.Size(88, 36);
            this.btnPendingAtDP.TabIndex = 11;
            this.btnPendingAtDP.Text = "PENDING";
            this.btnPendingAtDP.UseVisualStyleBackColor = false;
            this.btnPendingAtDP.Click += new System.EventHandler(this.btnPendingAtDP_Click);
            // 
            // btnAll
            // 
            this.btnAll.BackColor = System.Drawing.Color.Gainsboro;
            this.btnAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAll.Location = new System.Drawing.Point(625, 6);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(68, 36);
            this.btnAll.TabIndex = 1;
            this.btnAll.Text = "All";
            this.btnAll.UseVisualStyleBackColor = false;
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Gainsboro;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(557, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDone.Location = new System.Drawing.Point(489, 6);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(68, 36);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Done";
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnPackaged
            // 
            this.btnPackaged.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPackaged.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPackaged.Location = new System.Drawing.Point(421, 6);
            this.btnPackaged.Name = "btnPackaged";
            this.btnPackaged.Size = new System.Drawing.Size(68, 36);
            this.btnPackaged.TabIndex = 4;
            this.btnPackaged.Text = "Packaged";
            this.btnPackaged.UseVisualStyleBackColor = false;
            this.btnPackaged.Click += new System.EventHandler(this.btnPackaged_Click);
            // 
            // btnPackaging
            // 
            this.btnPackaging.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPackaging.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPackaging.Location = new System.Drawing.Point(353, 6);
            this.btnPackaging.Name = "btnPackaging";
            this.btnPackaging.Size = new System.Drawing.Size(68, 36);
            this.btnPackaging.TabIndex = 4;
            this.btnPackaging.Text = "Packaging";
            this.btnPackaging.UseVisualStyleBackColor = false;
            this.btnPackaging.Click += new System.EventHandler(this.btnPackaging_Click);
            // 
            // btnPendingforSign
            // 
            this.btnPendingforSign.BackColor = System.Drawing.Color.Gainsboro;
            this.btnPendingforSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPendingforSign.Location = new System.Drawing.Point(274, 6);
            this.btnPendingforSign.Name = "btnPendingforSign";
            this.btnPendingforSign.Size = new System.Drawing.Size(79, 36);
            this.btnPendingforSign.TabIndex = 6;
            this.btnPendingforSign.Text = "Pending for Signature";
            this.btnPendingforSign.UseVisualStyleBackColor = false;
            this.btnPendingforSign.Click += new System.EventHandler(this.btnPendingforSign_Click);
            // 
            // btnDPProcessed
            // 
            this.btnDPProcessed.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDPProcessed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDPProcessed.Location = new System.Drawing.Point(206, 6);
            this.btnDPProcessed.Name = "btnDPProcessed";
            this.btnDPProcessed.Size = new System.Drawing.Size(68, 36);
            this.btnDPProcessed.TabIndex = 7;
            this.btnDPProcessed.Text = "DP Processed";
            this.btnDPProcessed.UseVisualStyleBackColor = false;
            this.btnDPProcessed.Click += new System.EventHandler(this.btnDPProcessed_Click);
            // 
            // btnDPProcessing
            // 
            this.btnDPProcessing.BackColor = System.Drawing.Color.Gainsboro;
            this.btnDPProcessing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDPProcessing.Location = new System.Drawing.Point(136, 6);
            this.btnDPProcessing.Name = "btnDPProcessing";
            this.btnDPProcessing.Size = new System.Drawing.Size(70, 36);
            this.btnDPProcessing.TabIndex = 8;
            this.btnDPProcessing.Text = "DP Processing";
            this.btnDPProcessing.UseVisualStyleBackColor = false;
            this.btnDPProcessing.Click += new System.EventHandler(this.btnDPProcessing_Click);
            // 
            // btnControllerAccepted
            // 
            this.btnControllerAccepted.BackColor = System.Drawing.Color.Gainsboro;
            this.btnControllerAccepted.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnControllerAccepted.Location = new System.Drawing.Point(68, 6);
            this.btnControllerAccepted.Name = "btnControllerAccepted";
            this.btnControllerAccepted.Size = new System.Drawing.Size(68, 36);
            this.btnControllerAccepted.TabIndex = 9;
            this.btnControllerAccepted.Text = "Controller Accepted";
            this.btnControllerAccepted.UseVisualStyleBackColor = false;
            this.btnControllerAccepted.Click += new System.EventHandler(this.btnControllerAccepted_Click);
            // 
            // btnSubmittedtoUW
            // 
            this.btnSubmittedtoUW.BackColor = System.Drawing.Color.Gainsboro;
            this.btnSubmittedtoUW.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmittedtoUW.Location = new System.Drawing.Point(0, 6);
            this.btnSubmittedtoUW.Name = "btnSubmittedtoUW";
            this.btnSubmittedtoUW.Size = new System.Drawing.Size(68, 36);
            this.btnSubmittedtoUW.TabIndex = 10;
            this.btnSubmittedtoUW.Text = "Submitted to UW";
            this.btnSubmittedtoUW.UseVisualStyleBackColor = false;
            this.btnSubmittedtoUW.Click += new System.EventHandler(this.btnSubmittedtoUW_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gbAllRecordOption);
            this.panel3.Controls.Add(this.rbCustomer);
            this.panel3.Controls.Add(this.rbRefID);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.tbFilterdgvDoc);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1087, 42);
            this.panel3.TabIndex = 45;
            // 
            // gbAllRecordOption
            // 
            this.gbAllRecordOption.Controls.Add(this.label2);
            this.gbAllRecordOption.Controls.Add(this.dtpSpecificDateTo);
            this.gbAllRecordOption.Controls.Add(this.dtpSpecificDateFr);
            this.gbAllRecordOption.Controls.Add(this.rbSpecificDate);
            this.gbAllRecordOption.Controls.Add(this.rbAll);
            this.gbAllRecordOption.ForeColor = System.Drawing.Color.White;
            this.gbAllRecordOption.Location = new System.Drawing.Point(530, 5);
            this.gbAllRecordOption.Name = "gbAllRecordOption";
            this.gbAllRecordOption.Size = new System.Drawing.Size(340, 34);
            this.gbAllRecordOption.TabIndex = 45;
            this.gbAllRecordOption.TabStop = false;
            this.gbAllRecordOption.Text = "All records option";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(228, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "To";
            // 
            // dtpSpecificDateTo
            // 
            this.dtpSpecificDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSpecificDateTo.Location = new System.Drawing.Point(251, 10);
            this.dtpSpecificDateTo.Name = "dtpSpecificDateTo";
            this.dtpSpecificDateTo.Size = new System.Drawing.Size(70, 20);
            this.dtpSpecificDateTo.TabIndex = 48;
            // 
            // dtpSpecificDateFr
            // 
            this.dtpSpecificDateFr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSpecificDateFr.Location = new System.Drawing.Point(155, 10);
            this.dtpSpecificDateFr.Name = "dtpSpecificDateFr";
            this.dtpSpecificDateFr.Size = new System.Drawing.Size(70, 20);
            this.dtpSpecificDateFr.TabIndex = 47;
            // 
            // rbSpecificDate
            // 
            this.rbSpecificDate.AutoSize = true;
            this.rbSpecificDate.ForeColor = System.Drawing.Color.White;
            this.rbSpecificDate.Location = new System.Drawing.Point(68, 11);
            this.rbSpecificDate.Name = "rbSpecificDate";
            this.rbSpecificDate.Size = new System.Drawing.Size(87, 17);
            this.rbSpecificDate.TabIndex = 46;
            this.rbSpecificDate.TabStop = true;
            this.rbSpecificDate.Text = "Specific date";
            this.rbSpecificDate.UseVisualStyleBackColor = true;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.ForeColor = System.Drawing.Color.White;
            this.rbAll.Location = new System.Drawing.Point(18, 11);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 45;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbCustomer
            // 
            this.rbCustomer.AutoSize = true;
            this.rbCustomer.ForeColor = System.Drawing.Color.White;
            this.rbCustomer.Location = new System.Drawing.Point(210, 5);
            this.rbCustomer.Name = "rbCustomer";
            this.rbCustomer.Size = new System.Drawing.Size(86, 17);
            this.rbCustomer.TabIndex = 44;
            this.rbCustomer.TabStop = true;
            this.rbCustomer.Text = "CUSTOMER";
            this.rbCustomer.UseVisualStyleBackColor = true;
            this.rbCustomer.CheckedChanged += new System.EventHandler(this.rbCustomer_CheckedChanged);
            // 
            // rbRefID
            // 
            this.rbRefID.AutoSize = true;
            this.rbRefID.ForeColor = System.Drawing.Color.White;
            this.rbRefID.Location = new System.Drawing.Point(210, 22);
            this.rbRefID.Name = "rbRefID";
            this.rbRefID.Size = new System.Drawing.Size(63, 17);
            this.rbRefID.TabIndex = 43;
            this.rbRefID.TabStop = true;
            this.rbRefID.Text = "REF_ID";
            this.rbRefID.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(308, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Over Timeline:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(404, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Return Document";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(404, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Normal";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Khaki;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(385, 23);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(16, 16);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(385, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(16, 16);
            this.panel6.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 40;
            this.label3.Text = "Search: ";
            // 
            // tbFilterdgvDoc
            // 
            this.tbFilterdgvDoc.Location = new System.Drawing.Point(53, 11);
            this.tbFilterdgvDoc.Multiline = true;
            this.tbFilterdgvDoc.Name = "tbFilterdgvDoc";
            this.tbFilterdgvDoc.Size = new System.Drawing.Size(153, 21);
            this.tbFilterdgvDoc.TabIndex = 1;
            this.tooltip.SetToolTip(this.tbFilterdgvDoc, "Search by RefID or CustomerName");
            this.tbFilterdgvDoc.TextChanged += new System.EventHandler(this.tbFilterdgvDoc_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReturn);
            this.panel2.Controls.Add(this.btnReassignDP);
            this.panel2.Controls.Add(this.btnChangeStatus);
            this.panel2.Controls.Add(this.btnCloseReopen);
            this.panel2.Controls.Add(this.btnAddDoc);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Controls.Add(this.btnReport);
            this.panel2.Controls.Add(this.lblTot);
            this.panel2.Controls.Add(this.lblSel);
            this.panel2.Controls.Add(this.btnExportRecord);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.btnDPPendingRemark);
            this.panel2.Controls.Add(this.btnManageCrono);
            this.panel2.Controls.Add(this.btnReverse);
            this.panel2.Controls.Add(this.btnRefreshdgv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1087, 41);
            this.panel2.TabIndex = 44;
            // 
            // btnPrint
            // 
            this.btnPrint.BackgroundImage = global::Testing.Properties.Resources.print;
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPrint.Location = new System.Drawing.Point(706, 8);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(24, 24);
            this.btnPrint.TabIndex = 13;
            this.tooltip.SetToolTip(this.btnPrint, "Print Instruction Note");
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackgroundImage = global::Testing.Properties.Resources.report;
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.Location = new System.Drawing.Point(731, 8);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(24, 24);
            this.btnReport.TabIndex = 12;
            this.tooltip.SetToolTip(this.btnReport, "Report");
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // lblTot
            // 
            this.lblTot.AutoSize = true;
            this.lblTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTot.ForeColor = System.Drawing.Color.White;
            this.lblTot.Location = new System.Drawing.Point(832, 20);
            this.lblTot.Name = "lblTot";
            this.lblTot.Size = new System.Drawing.Size(0, 18);
            this.lblTot.TabIndex = 0;
            // 
            // lblSel
            // 
            this.lblSel.AutoSize = true;
            this.lblSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSel.ForeColor = System.Drawing.Color.White;
            this.lblSel.Location = new System.Drawing.Point(832, 3);
            this.lblSel.Name = "lblSel";
            this.lblSel.Size = new System.Drawing.Size(0, 18);
            this.lblSel.TabIndex = 0;
            // 
            // btnExportRecord
            // 
            this.btnExportRecord.BackgroundImage = global::Testing.Properties.Resources.export;
            this.btnExportRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExportRecord.Location = new System.Drawing.Point(631, 8);
            this.btnExportRecord.Name = "btnExportRecord";
            this.btnExportRecord.Size = new System.Drawing.Size(24, 24);
            this.btnExportRecord.TabIndex = 11;
            this.tooltip.SetToolTip(this.btnExportRecord, "Export All Records");
            this.btnExportRecord.UseVisualStyleBackColor = true;
            this.btnExportRecord.Click += new System.EventHandler(this.btnExportRecord_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(763, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Total:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(763, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "Selected:";
            // 
            // btnDPPendingRemark
            // 
            this.btnDPPendingRemark.BackgroundImage = global::Testing.Properties.Resources.remark;
            this.btnDPPendingRemark.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDPPendingRemark.Location = new System.Drawing.Point(681, 8);
            this.btnDPPendingRemark.Name = "btnDPPendingRemark";
            this.btnDPPendingRemark.Size = new System.Drawing.Size(24, 24);
            this.btnDPPendingRemark.TabIndex = 10;
            this.tooltip.SetToolTip(this.btnDPPendingRemark, "Reverse");
            this.btnDPPendingRemark.UseVisualStyleBackColor = true;
            this.btnDPPendingRemark.Click += new System.EventHandler(this.btnDPPendingRemark_Click);
            // 
            // btnManageCrono
            // 
            this.btnManageCrono.BackgroundImage = global::Testing.Properties.Resources.Crono;
            this.btnManageCrono.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnManageCrono.Location = new System.Drawing.Point(606, 8);
            this.btnManageCrono.Name = "btnManageCrono";
            this.btnManageCrono.Size = new System.Drawing.Size(24, 24);
            this.btnManageCrono.TabIndex = 9;
            this.tooltip.SetToolTip(this.btnManageCrono, "Manage Crono");
            this.btnManageCrono.UseVisualStyleBackColor = true;
            this.btnManageCrono.Click += new System.EventHandler(this.btnManageCrono_Click);
            // 
            // btnReverse
            // 
            this.btnReverse.BackgroundImage = global::Testing.Properties.Resources.reverse;
            this.btnReverse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReverse.Location = new System.Drawing.Point(656, 8);
            this.btnReverse.Name = "btnReverse";
            this.btnReverse.Size = new System.Drawing.Size(24, 24);
            this.btnReverse.TabIndex = 8;
            this.tooltip.SetToolTip(this.btnReverse, "Reverse");
            this.btnReverse.UseVisualStyleBackColor = true;
            this.btnReverse.Click += new System.EventHandler(this.btnReverse_Click);
            // 
            // btnRefreshdgv
            // 
            this.btnRefreshdgv.BackgroundImage = global::Testing.Properties.Resources.refresh;
            this.btnRefreshdgv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshdgv.Location = new System.Drawing.Point(581, 8);
            this.btnRefreshdgv.Name = "btnRefreshdgv";
            this.btnRefreshdgv.Size = new System.Drawing.Size(24, 24);
            this.btnRefreshdgv.TabIndex = 7;
            this.tooltip.SetToolTip(this.btnRefreshdgv, "Refresh");
            this.btnRefreshdgv.UseVisualStyleBackColor = true;
            this.btnRefreshdgv.Click += new System.EventHandler(this.btnRefreshOpendgv_Click);
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
            this.label1.TabIndex = 11;
            this.label1.Text = "Document Control Tracking";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // timerNoti
            // 
            this.timerNoti.Interval = 20000;
            this.timerNoti.Tick += new System.EventHandler(this.timerNoti_Tick);
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnReturn.FlatAppearance.BorderSize = 2;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(344, 6);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(100, 27);
            this.btnReturn.TabIndex = 17;
            this.btnReturn.Text = "Return";
            this.btnReturn.UseVisualStyleBackColor = false;
            // 
            // btnReassignDP
            // 
            this.btnReassignDP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnReassignDP.FlatAppearance.BorderSize = 2;
            this.btnReassignDP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReassignDP.ForeColor = System.Drawing.Color.White;
            this.btnReassignDP.Location = new System.Drawing.Point(238, 6);
            this.btnReassignDP.Name = "btnReassignDP";
            this.btnReassignDP.Size = new System.Drawing.Size(100, 27);
            this.btnReassignDP.TabIndex = 16;
            this.btnReassignDP.Text = "Re-assign DP";
            this.btnReassignDP.UseVisualStyleBackColor = false;
            // 
            // btnChangeStatus
            // 
            this.btnChangeStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnChangeStatus.FlatAppearance.BorderSize = 2;
            this.btnChangeStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeStatus.ForeColor = System.Drawing.Color.White;
            this.btnChangeStatus.Location = new System.Drawing.Point(467, 6);
            this.btnChangeStatus.Name = "btnChangeStatus";
            this.btnChangeStatus.Size = new System.Drawing.Size(100, 27);
            this.btnChangeStatus.TabIndex = 18;
            this.btnChangeStatus.Text = "Change Status";
            this.btnChangeStatus.UseVisualStyleBackColor = false;
            // 
            // btnCloseReopen
            // 
            this.btnCloseReopen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnCloseReopen.FlatAppearance.BorderSize = 2;
            this.btnCloseReopen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseReopen.ForeColor = System.Drawing.Color.White;
            this.btnCloseReopen.Location = new System.Drawing.Point(116, 6);
            this.btnCloseReopen.Name = "btnCloseReopen";
            this.btnCloseReopen.Size = new System.Drawing.Size(100, 27);
            this.btnCloseReopen.TabIndex = 15;
            this.btnCloseReopen.Text = "Cancel/Re-open";
            this.btnCloseReopen.UseVisualStyleBackColor = false;
            // 
            // btnAddDoc
            // 
            this.btnAddDoc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnAddDoc.FlatAppearance.BorderSize = 2;
            this.btnAddDoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDoc.ForeColor = System.Drawing.Color.White;
            this.btnAddDoc.Location = new System.Drawing.Point(10, 6);
            this.btnAddDoc.Name = "btnAddDoc";
            this.btnAddDoc.Size = new System.Drawing.Size(100, 27);
            this.btnAddDoc.TabIndex = 14;
            this.btnAddDoc.Text = "Add Document";
            this.btnAddDoc.UseVisualStyleBackColor = false;
            // 
            // frmDocumentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1087, 677);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDocumentControl";
            this.Text = "frmDocumentControl";
            this.Activated += new System.EventHandler(this.frmDocumentControl_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDocumentControl_FormClosing);
            this.Load += new System.EventHandler(this.frmDocumentControl_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pDoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoc)).EndInit();
            this.pNotification.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNoti)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNotification)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.gbAllRecordOption.ResumeLayout(false);
            this.gbAllRecordOption.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefreshdgv;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnReverse;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnPackaging;
        private System.Windows.Forms.Button btnPendingforSign;
        private System.Windows.Forms.Button btnDPProcessed;
        private System.Windows.Forms.Button btnDPProcessing;
        private System.Windows.Forms.Button btnControllerAccepted;
        private System.Windows.Forms.Button btnSubmittedtoUW;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFilterdgvDoc;
        private System.Windows.Forms.Button btnManageCrono;
        private System.Windows.Forms.Button btnPackaged;
        private System.Windows.Forms.Button btnDPPendingRemark;
        private System.Windows.Forms.Button btnExportRecord;
        private System.Windows.Forms.Button btnPendingAtDP;
        private System.Windows.Forms.RadioButton rbCustomer;
        private System.Windows.Forms.RadioButton rbRefID;
        private System.Windows.Forms.GroupBox gbAllRecordOption;
        private System.Windows.Forms.DateTimePicker dtpSpecificDateFr;
        private System.Windows.Forms.RadioButton rbSpecificDate;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpSpecificDateTo;
        private System.Windows.Forms.Label lblSel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridView dgvDoc;
        private System.Windows.Forms.Panel pDoc;
        private System.Windows.Forms.Panel pNotification;
        private System.Windows.Forms.PictureBox btnNotification;
        private System.Windows.Forms.Label lblNotiCount;
        private System.Windows.Forms.DataGridView dgvNoti;
        private System.Windows.Forms.Timer timerNoti;
        private cus_button btnAddDoc;
        private cus_button btnCloseReopen;
        private cus_button btnReturn;
        private cus_button btnReassignDP;
        private cus_button btnChangeStatus;

    }
}