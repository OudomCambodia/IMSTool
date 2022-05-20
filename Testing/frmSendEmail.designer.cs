namespace Testing
{
    partial class frmSendEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendEmail));
            this.bnSend = new System.Windows.Forms.Button();
            this.bnClear = new System.Windows.Forms.Button();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCC = new System.Windows.Forms.TextBox();
            this.tbSubject = new System.Windows.Forms.TextBox();
            this.tbCONTENT = new System.Windows.Forms.TextBox();
            this.bnQuery = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.tbTO = new System.Windows.Forms.TextBox();
            this.gbPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // bnSend
            // 
            this.bnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSend.Location = new System.Drawing.Point(454, 570);
            this.bnSend.Name = "bnSend";
            this.bnSend.Size = new System.Drawing.Size(67, 27);
            this.bnSend.TabIndex = 0;
            this.bnSend.Text = "Send";
            this.bnSend.UseVisualStyleBackColor = true;
            this.bnSend.Click += new System.EventHandler(this.bnSend_Click);
            // 
            // bnClear
            // 
            this.bnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnClear.Location = new System.Drawing.Point(637, 570);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(67, 27);
            this.bnClear.TabIndex = 1;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = true;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
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
            this.gbPeriod.Location = new System.Drawing.Point(17, 460);
            this.gbPeriod.Name = "gbPeriod";
            this.gbPeriod.Size = new System.Drawing.Size(705, 103);
            this.gbPeriod.TabIndex = 2;
            this.gbPeriod.TabStop = false;
            this.gbPeriod.Text = "Period";
            this.gbPeriod.Enter += new System.EventHandler(this.gbPeriod_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(452, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(481, 64);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(153, 20);
            this.dtpTo.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(171, 66);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(210, 64);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(153, 20);
            this.dtpFrom.TabIndex = 10;
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(481, 27);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(153, 21);
            this.cbYear.TabIndex = 9;
            this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(443, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Year:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 30);
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
            this.cbMonth.Location = new System.Drawing.Point(210, 27);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(153, 21);
            this.cbMonth.TabIndex = 2;
            this.cbMonth.SelectedIndexChanged += new System.EventHandler(this.cbMonth_SelectedIndexChanged);
            // 
            // rbCustom
            // 
            this.rbCustom.AutoSize = true;
            this.rbCustom.Location = new System.Drawing.Point(6, 64);
            this.rbCustom.Name = "rbCustom";
            this.rbCustom.Size = new System.Drawing.Size(60, 17);
            this.rbCustom.TabIndex = 1;
            this.rbCustom.Text = "Custom";
            this.rbCustom.UseVisualStyleBackColor = true;
            this.rbCustom.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // rbMonth
            // 
            this.rbMonth.AutoSize = true;
            this.rbMonth.Checked = true;
            this.rbMonth.Location = new System.Drawing.Point(6, 28);
            this.rbMonth.Name = "rbMonth";
            this.rbMonth.Size = new System.Drawing.Size(70, 17);
            this.rbMonth.TabIndex = 0;
            this.rbMonth.TabStop = true;
            this.rbMonth.Text = "By Month";
            this.rbMonth.UseVisualStyleBackColor = true;
            this.rbMonth.CheckedChanged += new System.EventHandler(this.rbMonth_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(248, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sending RI Pending Email";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CC:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Content:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Subject:";
            // 
            // tbCC
            // 
            this.tbCC.Location = new System.Drawing.Point(68, 110);
            this.tbCC.Multiline = true;
            this.tbCC.Name = "tbCC";
            this.tbCC.Size = new System.Drawing.Size(654, 62);
            this.tbCC.TabIndex = 7;
            // 
            // tbSubject
            // 
            this.tbSubject.Location = new System.Drawing.Point(68, 178);
            this.tbSubject.Name = "tbSubject";
            this.tbSubject.Size = new System.Drawing.Size(654, 20);
            this.tbSubject.TabIndex = 8;
            // 
            // tbCONTENT
            // 
            this.tbCONTENT.Location = new System.Drawing.Point(68, 204);
            this.tbCONTENT.Multiline = true;
            this.tbCONTENT.Name = "tbCONTENT";
            this.tbCONTENT.Size = new System.Drawing.Size(654, 250);
            this.tbCONTENT.TabIndex = 9;
            // 
            // bnQuery
            // 
            this.bnQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnQuery.Location = new System.Drawing.Point(546, 570);
            this.bnQuery.Name = "bnQuery";
            this.bnQuery.Size = new System.Drawing.Size(67, 27);
            this.bnQuery.TabIndex = 10;
            this.bnQuery.Text = "Query";
            this.bnQuery.UseVisualStyleBackColor = true;
            this.bnQuery.Click += new System.EventHandler(this.bnQuery_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "To:";
            // 
            // tbTO
            // 
            this.tbTO.Location = new System.Drawing.Point(68, 42);
            this.tbTO.Multiline = true;
            this.tbTO.Name = "tbTO";
            this.tbTO.Size = new System.Drawing.Size(654, 62);
            this.tbTO.TabIndex = 7;
            // 
            // frmSendEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 612);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSendEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Email";
            this.Load += new System.EventHandler(this.frmSendEmail_Load);
            this.gbPeriod.ResumeLayout(false);
            this.gbPeriod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bnSend;
        private System.Windows.Forms.Button bnClear;
        private System.Windows.Forms.GroupBox gbPeriod;
        private System.Windows.Forms.RadioButton rbCustom;
        private System.Windows.Forms.RadioButton rbMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCC;
        private System.Windows.Forms.TextBox tbSubject;
        private System.Windows.Forms.TextBox tbCONTENT;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button bnQuery;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbTO;
    }
}

