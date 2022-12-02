namespace Testing.Forms
{
    partial class MonthlyReportAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyReportAdmin));
            this.bnQuery = new System.Windows.Forms.Button();
            this.tbCONTENT = new System.Windows.Forms.TextBox();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.tbTO = new System.Windows.Forms.TextBox();
            this.tbCC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbPeriod = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.rbCustom = new System.Windows.Forms.RadioButton();
            this.rbMonth = new System.Windows.Forms.RadioButton();
            this.bnClear = new System.Windows.Forms.Button();
            this.bnSend = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSavedPath = new System.Windows.Forms.TextBox();
            this.gbPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnQuery
            // 
            this.bnQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnQuery.Location = new System.Drawing.Point(541, 623);
            this.bnQuery.Name = "bnQuery";
            this.bnQuery.Size = new System.Drawing.Size(67, 27);
            this.bnQuery.TabIndex = 23;
            this.bnQuery.Text = "Query";
            this.bnQuery.UseVisualStyleBackColor = true;
            this.bnQuery.Click += new System.EventHandler(this.bnQuery_Click);
            // 
            // tbCONTENT
            // 
            this.tbCONTENT.Location = new System.Drawing.Point(63, 257);
            this.tbCONTENT.Multiline = true;
            this.tbCONTENT.Name = "tbCONTENT";
            this.tbCONTENT.Size = new System.Drawing.Size(654, 250);
            this.tbCONTENT.TabIndex = 22;
            // 
            // tbSubject
            // 
            this.tbSubject.Location = new System.Drawing.Point(63, 231);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(654, 20);
            this.tbSubject.TabIndex = 21;
            // 
            // tbTO
            // 
            this.tbTO.Location = new System.Drawing.Point(63, 95);
            this.tbTO.Multiline = true;
            this.tbTO.Name = "tbTO";
            this.tbTO.Size = new System.Drawing.Size(654, 62);
            this.tbTO.TabIndex = 19;
            // 
            // tbCC
            // 
            this.tbCC.Location = new System.Drawing.Point(63, 163);
            this.tbCC.Multiline = true;
            this.tbCC.Name = "tbCC";
            this.tbCC.Size = new System.Drawing.Size(654, 62);
            this.tbCC.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Subject:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Content:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "To:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "CC:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(243, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sending Monthly Report Email";
            // 
            // gbPeriod
            // 
            this.gbPeriod.Controls.Add(this.label8);
            this.gbPeriod.Controls.Add(this.dtpTo);
            this.gbPeriod.Controls.Add(this.label7);
            this.gbPeriod.Controls.Add(this.dtpFrom);
            this.gbPeriod.Controls.Add(this.cbYear);
            this.gbPeriod.Controls.Add(this.label6);
            this.gbPeriod.Controls.Add(this.label5);
            this.gbPeriod.Controls.Add(this.cbMonth);
            this.gbPeriod.Controls.Add(this.rbCustom);
            this.gbPeriod.Controls.Add(this.rbMonth);
            this.gbPeriod.Location = new System.Drawing.Point(12, 513);
            this.gbPeriod.Name = "gbPeriod";
            this.gbPeriod.Size = new System.Drawing.Size(705, 103);
            this.gbPeriod.TabIndex = 13;
            this.gbPeriod.TabStop = false;
            this.gbPeriod.Text = "Period";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(458, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(487, 64);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(153, 20);
            this.dtpTo.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(177, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(216, 64);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(153, 20);
            this.dtpFrom.TabIndex = 10;
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(487, 27);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(153, 21);
            this.cbYear.TabIndex = 9;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(449, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Year:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Month:";
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Items.AddRange(new object[] {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"});
            this.cbMonth.Location = new System.Drawing.Point(216, 27);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(153, 21);
            this.cbMonth.TabIndex = 2;
            this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(12, 64);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(60, 17);
            this.rbCustom.TabIndex = 1;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Checked = true;
            this.rbMonth.Location = new System.Drawing.Point(12, 28);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(70, 17);
            this.rbMonth.TabIndex = 0;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "By Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // bnClear
            // 
            this.bnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnClear.Location = new System.Drawing.Point(632, 623);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(67, 27);
            this.bnClear.TabIndex = 12;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = true;
            // 
            // bnSend
            // 
            this.bnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSend.Location = new System.Drawing.Point(449, 623);
            this.bnSend.Name = "bnSend";
            this.bnSend.Size = new System.Drawing.Size(67, 27);
            this.bnSend.TabIndex = 11;
            this.bnSend.Text = "Send";
            this.bnSend.UseVisualStyleBackColor = true;
            this.bnSend.Click += new System.EventHandler(this.bnSend_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Report:";
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Location = new System.Drawing.Point(63, 62);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(230, 21);
            this.cbReportType.TabIndex = 25;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(311, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(66, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Saved Path:";
            // 
            // txtSavedPath
            // 
            this.txtSavedPath.Location = new System.Drawing.Point(383, 63);
            this.txtSavedPath.Name = "txtSavedPath";
            this.txtSavedPath.Size = new System.Drawing.Size(188, 20);
            this.txtSavedPath.TabIndex = 27;
            // 
            // MonthlyReportAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 667);
            this.Controls.Add(this.txtSavedPath);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbReportType);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bnQuery);
            this.Controls.Add(this.tbCONTENT);
            this.Controls.Add(this.tbSubject);
            this.Controls.Add(this.tbTO);
            this.Controls.Add(this.tbCC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbPeriod);
            this.Controls.Add(this.bnClear);
            this.Controls.Add(this.bnSend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MonthlyReportAdmin";
            this.Text = "MonthlyReportAdmin";
            this.Load += new System.EventHandler(this.MonthlyReportAdmin_Load);
            this.gbPeriod.ResumeLayout(false);
            this.gbPeriod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnQuery;
        private System.Windows.Forms.TextBox tbCONTENT;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.TextBox tbTO;
        private System.Windows.Forms.TextBox tbCC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbPeriod;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.Button bnClear;
        private System.Windows.Forms.Button bnSend;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSavedPath;
    }
}