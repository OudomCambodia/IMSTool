namespace Testing.Forms
{
    partial class frmViewFiles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewFiles));
            this.label6 = new System.Windows.Forms.Label();
            this.txtEndoNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPolicyNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpEffTo = new System.Windows.Forms.DateTimePicker();
            this.dtpEffFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new cus_button();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Open = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.fbdDownload = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(218, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 32);
            this.label6.TabIndex = 36;
            this.label6.Text = "File Upload";
            // 
            // txtEndoNo
            // 
            this.txtEndoNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtEndoNo.Enabled = false;
            this.txtEndoNo.Location = new System.Drawing.Point(386, 55);
            this.txtEndoNo.Name = "txtEndoNo";
            this.txtEndoNo.ReadOnly = true;
            this.txtEndoNo.Size = new System.Drawing.Size(168, 22);
            this.txtEndoNo.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(269, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 38;
            this.label1.Text = "Endorsement No";
            // 
            // txtPolicyNo
            // 
            this.txtPolicyNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtPolicyNo.Enabled = false;
            this.txtPolicyNo.Location = new System.Drawing.Point(109, 55);
            this.txtPolicyNo.Name = "txtPolicyNo";
            this.txtPolicyNo.ReadOnly = true;
            this.txtPolicyNo.Size = new System.Drawing.Size(154, 22);
            this.txtPolicyNo.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(37, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 40;
            this.label2.Text = "Policy No";
            // 
            // dtpEffTo
            // 
            this.dtpEffTo.CalendarMonthBackground = System.Drawing.Color.Gainsboro;
            this.dtpEffTo.Enabled = false;
            this.dtpEffTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffTo.Location = new System.Drawing.Point(386, 89);
            this.dtpEffTo.Name = "dtpEffTo";
            this.dtpEffTo.Size = new System.Drawing.Size(168, 22);
            this.dtpEffTo.TabIndex = 44;
            // 
            // dtpEffFrom
            // 
            this.dtpEffFrom.CalendarMonthBackground = System.Drawing.Color.Gainsboro;
            this.dtpEffFrom.Enabled = false;
            this.dtpEffFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffFrom.Location = new System.Drawing.Point(109, 89);
            this.dtpEffFrom.Name = "dtpEffFrom";
            this.dtpEffFrom.Size = new System.Drawing.Size(154, 22);
            this.dtpEffFrom.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(299, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 42;
            this.label3.Text = "Effective To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(10, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 41;
            this.label4.Text = "Effective From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(47, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Enabled = false;
            this.txtRemark.Location = new System.Drawing.Point(109, 123);
            this.txtRemark.MaxLength = 1000;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.ReadOnly = true;
            this.txtRemark.Size = new System.Drawing.Size(445, 67);
            this.txtRemark.TabIndex = 45;
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.BackgroundColor = System.Drawing.Color.White;
            this.dgvFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.File_Name,
            this.File_Path,
            this.Open,
            this.Download});
            this.dgvFile.Location = new System.Drawing.Point(109, 201);
            this.dgvFile.MultiSelect = false;
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.RowHeadersVisible = false;
            this.dgvFile.RowTemplate.Height = 30;
            this.dgvFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFile.Size = new System.Drawing.Size(445, 159);
            this.dgvFile.TabIndex = 48;
            this.dgvFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFile_CellClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(66, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 16);
            this.label7.TabIndex = 47;
            this.label7.Text = "Files";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(469, 366);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 35);
            this.btnClose.TabIndex = 49;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // No
            // 
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 30;
            // 
            // File_Name
            // 
            this.File_Name.HeaderText = "File Name";
            this.File_Name.Name = "File_Name";
            this.File_Name.ReadOnly = true;
            this.File_Name.Width = 200;
            // 
            // File_Path
            // 
            this.File_Path.HeaderText = "Path";
            this.File_Path.Name = "File_Path";
            this.File_Path.ReadOnly = true;
            this.File_Path.Visible = false;
            this.File_Path.Width = 60;
            // 
            // Open
            // 
            this.Open.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Open.HeaderText = "Open";
            this.Open.Name = "Open";
            this.Open.ReadOnly = true;
            this.Open.Text = "Open";
            this.Open.UseColumnTextForButtonValue = true;
            // 
            // Download
            // 
            this.Download.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Download.HeaderText = "Download";
            this.Download.Name = "Download";
            this.Download.ReadOnly = true;
            this.Download.Text = "Download";
            this.Download.UseColumnTextForButtonValue = true;
            // 
            // frmViewFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(569, 408);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.dtpEffTo);
            this.Controls.Add(this.dtpEffFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPolicyNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEndoNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmViewFiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Files";
            this.Load += new System.EventHandler(this.frmViewFiles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClose;
        public System.Windows.Forms.TextBox txtEndoNo;
        public System.Windows.Forms.TextBox txtPolicyNo;
        public System.Windows.Forms.DateTimePicker dtpEffTo;
        public System.Windows.Forms.DateTimePicker dtpEffFrom;
        public System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Path;
        private System.Windows.Forms.DataGridViewButtonColumn Open;
        private System.Windows.Forms.DataGridViewButtonColumn Download;
        private System.Windows.Forms.FolderBrowserDialog fbdDownload;
    }
}