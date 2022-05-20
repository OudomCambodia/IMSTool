namespace Testing.Forms
{
    partial class frmViewAttachments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewAttachments));
            this.fbdDownload = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbProType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbCusName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDocType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDocID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbCreateDate = new System.Windows.Forms.TextBox();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Open = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Download = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnClose = new Testing.cus_button();
            this.btnAddFile = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(617, 37);
            this.label1.TabIndex = 60;
            this.label1.Text = "Attachments";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(360, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 76;
            this.label7.Text = "Product Type: ";
            // 
            // tbProType
            // 
            this.tbProType.Location = new System.Drawing.Point(439, 75);
            this.tbProType.MaxLength = 20;
            this.tbProType.Name = "tbProType";
            this.tbProType.ReadOnly = true;
            this.tbProType.Size = new System.Drawing.Size(145, 20);
            this.tbProType.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(31, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 75;
            this.label5.Text = "Customer Name: ";
            // 
            // tbCusName
            // 
            this.tbCusName.Location = new System.Drawing.Point(126, 101);
            this.tbCusName.MaxLength = 20;
            this.tbCusName.Name = "tbCusName";
            this.tbCusName.ReadOnly = true;
            this.tbCusName.Size = new System.Drawing.Size(458, 20);
            this.tbCusName.TabIndex = 71;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(31, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 74;
            this.label3.Text = "Document Type: ";
            // 
            // tbDocType
            // 
            this.tbDocType.Location = new System.Drawing.Point(126, 75);
            this.tbDocType.MaxLength = 20;
            this.tbDocType.Name = "tbDocType";
            this.tbDocType.ReadOnly = true;
            this.tbDocType.Size = new System.Drawing.Size(187, 20);
            this.tbDocType.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(31, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "Ref ID: ";
            // 
            // tbDocID
            // 
            this.tbDocID.Location = new System.Drawing.Point(126, 49);
            this.tbDocID.MaxLength = 20;
            this.tbDocID.Name = "tbDocID";
            this.tbDocID.ReadOnly = true;
            this.tbDocID.Size = new System.Drawing.Size(187, 20);
            this.tbDocID.TabIndex = 69;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(360, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 78;
            this.label11.Text = "Create Date: ";
            // 
            // tbCreateDate
            // 
            this.tbCreateDate.Location = new System.Drawing.Point(439, 49);
            this.tbCreateDate.MaxLength = 20;
            this.tbCreateDate.Name = "tbCreateDate";
            this.tbCreateDate.ReadOnly = true;
            this.tbCreateDate.Size = new System.Drawing.Size(145, 20);
            this.tbCreateDate.TabIndex = 77;
            // 
            // dgvFile
            // 
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.AllowUserToResizeColumns = false;
            this.dgvFile.AllowUserToResizeRows = false;
            this.dgvFile.BackgroundColor = System.Drawing.Color.White;
            this.dgvFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.File_Name,
            this.File_Path,
            this.Open,
            this.Download});
            this.dgvFile.Location = new System.Drawing.Point(34, 127);
            this.dgvFile.MultiSelect = false;
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.RowHeadersVisible = false;
            this.dgvFile.RowTemplate.Height = 30;
            this.dgvFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFile.Size = new System.Drawing.Size(550, 159);
            this.dgvFile.TabIndex = 79;
            this.dgvFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFile_CellClick);
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
            this.File_Name.Width = 315;
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
            this.Open.Width = 105;
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
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(511, 298);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 30);
            this.btnClose.TabIndex = 80;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFile.ForeColor = System.Drawing.Color.White;
            this.btnAddFile.Location = new System.Drawing.Point(34, 298);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(150, 30);
            this.btnAddFile.TabIndex = 81;
            this.btnAddFile.Text = "Add File(s)";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // frmViewAttachments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(617, 340);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbCreateDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbProType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbCusName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbDocType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbDocID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmViewAttachments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Attachments";
            this.Load += new System.EventHandler(this.frmViewAttachments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog fbdDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbProType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbCusName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDocType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDocID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbCreateDate;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Path;
        private System.Windows.Forms.DataGridViewButtonColumn Open;
        private System.Windows.Forms.DataGridViewButtonColumn Download;
        private cus_button btnClose;
        private cus_button btnAddFile;
    }
}