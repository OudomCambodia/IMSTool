namespace Testing.Forms
{
    partial class frmAutoClaimReport
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
            this.groupType = new System.Windows.Forms.GroupBox();
            this.rdOS = new System.Windows.Forms.RadioButton();
            this.rdPaid = new System.Windows.Forms.RadioButton();
            this.rdIncurred = new System.Windows.Forms.RadioButton();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lbDateFrom = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lbDateTo = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.cbMainClass = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bnExcel = new Testing.cus_button();
            this.bnClear = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.groupType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(13, 9);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(807, 32);
            this.lbTitle.TabIndex = 23;
            this.lbTitle.Text = "Claim Incurred Report";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupType
            // 
            this.groupType.BackColor = System.Drawing.Color.Transparent;
            this.groupType.Controls.Add(this.rdOS);
            this.groupType.Controls.Add(this.rdPaid);
            this.groupType.Controls.Add(this.rdIncurred);
            this.groupType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupType.ForeColor = System.Drawing.Color.White;
            this.groupType.Location = new System.Drawing.Point(19, 44);
            this.groupType.Name = "groupType";
            this.groupType.Size = new System.Drawing.Size(175, 83);
            this.groupType.TabIndex = 67;
            this.groupType.TabStop = false;
            this.groupType.Text = "Report Type";
            // 
            // rdOS
            // 
            this.rdOS.AutoSize = true;
            this.rdOS.ForeColor = System.Drawing.Color.White;
            this.rdOS.Location = new System.Drawing.Point(99, 24);
            this.rdOS.Name = "rdOS";
            this.rdOS.Size = new System.Drawing.Size(45, 20);
            this.rdOS.TabIndex = 2;
            this.rdOS.TabStop = true;
            this.rdOS.Text = "OS";
            this.rdOS.UseVisualStyleBackColor = true;
            this.rdOS.CheckedChanged += new System.EventHandler(this.rdOS_CheckedChanged);
            // 
            // rdPaid
            // 
            this.rdPaid.AutoSize = true;
            this.rdPaid.ForeColor = System.Drawing.Color.White;
            this.rdPaid.Location = new System.Drawing.Point(19, 51);
            this.rdPaid.Name = "rdPaid";
            this.rdPaid.Size = new System.Drawing.Size(54, 20);
            this.rdPaid.TabIndex = 1;
            this.rdPaid.TabStop = true;
            this.rdPaid.Text = "Paid";
            this.rdPaid.UseVisualStyleBackColor = true;
            this.rdPaid.CheckedChanged += new System.EventHandler(this.rdPaid_CheckedChanged);
            // 
            // rdIncurred
            // 
            this.rdIncurred.AutoSize = true;
            this.rdIncurred.ForeColor = System.Drawing.Color.White;
            this.rdIncurred.Location = new System.Drawing.Point(19, 24);
            this.rdIncurred.Name = "rdIncurred";
            this.rdIncurred.Size = new System.Drawing.Size(74, 20);
            this.rdIncurred.TabIndex = 0;
            this.rdIncurred.TabStop = true;
            this.rdIncurred.Text = "Incurred";
            this.rdIncurred.UseVisualStyleBackColor = true;
            this.rdIncurred.CheckedChanged += new System.EventHandler(this.rdIncurred_CheckedChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(204, 68);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(161, 20);
            this.dtpFrom.TabIndex = 68;
            // 
            // lbDateFrom
            // 
            this.lbDateFrom.AutoSize = true;
            this.lbDateFrom.BackColor = System.Drawing.Color.Transparent;
            this.lbDateFrom.ForeColor = System.Drawing.Color.White;
            this.lbDateFrom.Location = new System.Drawing.Point(201, 52);
            this.lbDateFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDateFrom.Name = "lbDateFrom";
            this.lbDateFrom.Size = new System.Drawing.Size(98, 13);
            this.lbDateFrom.TabIndex = 69;
            this.lbDateFrom.Text = "Notified Date From:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(204, 107);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(161, 20);
            this.dtpTo.TabIndex = 70;
            // 
            // lbDateTo
            // 
            this.lbDateTo.AutoSize = true;
            this.lbDateTo.BackColor = System.Drawing.Color.Transparent;
            this.lbDateTo.ForeColor = System.Drawing.Color.White;
            this.lbDateTo.Location = new System.Drawing.Point(201, 91);
            this.lbDateTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDateTo.Name = "lbDateTo";
            this.lbDateTo.Size = new System.Drawing.Size(88, 13);
            this.lbDateTo.TabIndex = 71;
            this.lbDateTo.Text = "Notified Date To:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 133);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(833, 451);
            this.dataGridView.TabIndex = 73;
            // 
            // cbMainClass
            // 
            this.cbMainClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbMainClass.FormattingEnabled = true;
            this.cbMainClass.Items.AddRange(new object[] {
            "AUTOMOBILE",
            "CASUALTY",
            "ENGINEERING",
            "MARINE",
            "MEDICAL",
            "MICROINSURANCE",
            "OIL AND GAS",
            "PROPERTY"});
            this.cbMainClass.Location = new System.Drawing.Point(382, 67);
            this.cbMainClass.Name = "cbMainClass";
            this.cbMainClass.Size = new System.Drawing.Size(130, 21);
            this.cbMainClass.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(379, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Main Class:";
            // 
            // bnExcel
            // 
            this.bnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnExcel.ForeColor = System.Drawing.Color.White;
            this.bnExcel.Location = new System.Drawing.Point(528, 98);
            this.bnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.bnExcel.Name = "bnExcel";
            this.bnExcel.Size = new System.Drawing.Size(83, 29);
            this.bnExcel.TabIndex = 74;
            this.bnExcel.Text = "Excel";
            this.bnExcel.UseVisualStyleBackColor = true;
            this.bnExcel.Click += new System.EventHandler(this.bnExcel_Click);
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnClear.ForeColor = System.Drawing.Color.White;
            this.bnClear.Location = new System.Drawing.Point(619, 59);
            this.bnClear.Margin = new System.Windows.Forms.Padding(4);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(83, 29);
            this.bnClear.TabIndex = 72;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = true;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(528, 59);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(83, 29);
            this.bnSearch.TabIndex = 72;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = true;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // frmAutoClaimReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(833, 584);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbMainClass);
            this.Controls.Add(this.bnExcel);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.bnClear);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.lbDateTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lbDateFrom);
            this.Controls.Add(this.groupType);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAutoClaimReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAutoClaimReport";
            this.Load += new System.EventHandler(this.frmAutoClaimReport_Load);
            this.groupType.ResumeLayout(false);
            this.groupType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox groupType;
        private System.Windows.Forms.RadioButton rdOS;
        private System.Windows.Forms.RadioButton rdPaid;
        private System.Windows.Forms.RadioButton rdIncurred;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lbDateFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lbDateTo;
        private cus_button bnSearch;
        private System.Windows.Forms.DataGridView dataGridView;
        private cus_button bnExcel;
        private cus_button bnClear;
        private System.Windows.Forms.ComboBox cbMainClass;
        private System.Windows.Forms.Label label1;
    }
}