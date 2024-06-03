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
            this.lbProductType = new System.Windows.Forms.Label();
            this.tbProType = new System.Windows.Forms.TextBox();
            this.lbCustomerName = new System.Windows.Forms.Label();
            this.tbCusName = new System.Windows.Forms.TextBox();
            this.lbDocumentType = new System.Windows.Forms.Label();
            this.tbDocType = new System.Windows.Forms.TextBox();
            this.lbRefID = new System.Windows.Forms.Label();
            this.tbDocID = new System.Windows.Forms.TextBox();
            this.lbCreatedDate = new System.Windows.Forms.Label();
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
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(823, 46);
            this.label1.TabIndex = 60;
            this.label1.Text = "Attachments";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbProductType
            // 
            this.lbProductType.AutoSize = true;
            this.lbProductType.ForeColor = System.Drawing.Color.White;
            this.lbProductType.Location = new System.Drawing.Point(480, 96);
            this.lbProductType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProductType.Name = "lbProductType";
            this.lbProductType.Size = new System.Drawing.Size(101, 17);
            this.lbProductType.TabIndex = 76;
            this.lbProductType.Text = "Product Type: ";
            // 
            // tbProType
            // 
            this.tbProType.Location = new System.Drawing.Point(585, 92);
            this.tbProType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbProType.MaxLength = 20;
            this.tbProType.Name = "tbProType";
            this.tbProType.ReadOnly = true;
            this.tbProType.Size = new System.Drawing.Size(192, 22);
            this.tbProType.TabIndex = 72;
            // 
            // lbCustomerName
            // 
            this.lbCustomerName.AutoSize = true;
            this.lbCustomerName.ForeColor = System.Drawing.Color.White;
            this.lbCustomerName.Location = new System.Drawing.Point(41, 128);
            this.lbCustomerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCustomerName.Name = "lbCustomerName";
            this.lbCustomerName.Size = new System.Drawing.Size(117, 17);
            this.lbCustomerName.TabIndex = 75;
            this.lbCustomerName.Text = "Customer Name: ";
            // 
            // tbCusName
            // 
            this.tbCusName.Location = new System.Drawing.Point(168, 124);
            this.tbCusName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCusName.MaxLength = 20;
            this.tbCusName.Name = "tbCusName";
            this.tbCusName.ReadOnly = true;
            this.tbCusName.Size = new System.Drawing.Size(609, 22);
            this.tbCusName.TabIndex = 71;
            // 
            // lbDocumentType
            // 
            this.lbDocumentType.AutoSize = true;
            this.lbDocumentType.ForeColor = System.Drawing.Color.White;
            this.lbDocumentType.Location = new System.Drawing.Point(41, 96);
            this.lbDocumentType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDocumentType.Name = "lbDocumentType";
            this.lbDocumentType.Size = new System.Drawing.Size(116, 17);
            this.lbDocumentType.TabIndex = 74;
            this.lbDocumentType.Text = "Document Type: ";
            // 
            // tbDocType
            // 
            this.tbDocType.Location = new System.Drawing.Point(168, 92);
            this.tbDocType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbDocType.MaxLength = 20;
            this.tbDocType.Name = "tbDocType";
            this.tbDocType.ReadOnly = true;
            this.tbDocType.Size = new System.Drawing.Size(248, 22);
            this.tbDocType.TabIndex = 70;
            // 
            // lbRefID
            // 
            this.lbRefID.AutoSize = true;
            this.lbRefID.ForeColor = System.Drawing.Color.White;
            this.lbRefID.Location = new System.Drawing.Point(41, 64);
            this.lbRefID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRefID.Name = "lbRefID";
            this.lbRefID.Size = new System.Drawing.Size(55, 17);
            this.lbRefID.TabIndex = 73;
            this.lbRefID.Text = "Ref ID: ";
            // 
            // tbDocID
            // 
            this.tbDocID.Location = new System.Drawing.Point(168, 60);
            this.tbDocID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbDocID.MaxLength = 20;
            this.tbDocID.Name = "tbDocID";
            this.tbDocID.ReadOnly = true;
            this.tbDocID.Size = new System.Drawing.Size(248, 22);
            this.tbDocID.TabIndex = 69;
            // 
            // lbCreatedDate
            // 
            this.lbCreatedDate.AutoSize = true;
            this.lbCreatedDate.ForeColor = System.Drawing.Color.White;
            this.lbCreatedDate.Location = new System.Drawing.Point(480, 64);
            this.lbCreatedDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCreatedDate.Name = "lbCreatedDate";
            this.lbCreatedDate.Size = new System.Drawing.Size(92, 17);
            this.lbCreatedDate.TabIndex = 78;
            this.lbCreatedDate.Text = "Create Date: ";
            // 
            // tbCreateDate
            // 
            this.tbCreateDate.Location = new System.Drawing.Point(585, 60);
            this.tbCreateDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbCreateDate.MaxLength = 20;
            this.tbCreateDate.Name = "tbCreateDate";
            this.tbCreateDate.ReadOnly = true;
            this.tbCreateDate.Size = new System.Drawing.Size(192, 22);
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
            this.dgvFile.Location = new System.Drawing.Point(45, 156);
            this.dgvFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvFile.MultiSelect = false;
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.RowHeadersVisible = false;
            this.dgvFile.RowTemplate.Height = 30;
            this.dgvFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFile.Size = new System.Drawing.Size(733, 196);
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
            this.btnClose.Location = new System.Drawing.Point(681, 367);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(97, 37);
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
            this.btnAddFile.Location = new System.Drawing.Point(45, 367);
            this.btnAddFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(200, 37);
            this.btnAddFile.TabIndex = 81;
            this.btnAddFile.Text = "Add File(s)";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // frmViewAttachments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(823, 418);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.lbCreatedDate);
            this.Controls.Add(this.tbCreateDate);
            this.Controls.Add(this.lbProductType);
            this.Controls.Add(this.tbProType);
            this.Controls.Add(this.lbCustomerName);
            this.Controls.Add(this.tbCusName);
            this.Controls.Add(this.lbDocumentType);
            this.Controls.Add(this.tbDocType);
            this.Controls.Add(this.lbRefID);
            this.Controls.Add(this.tbDocID);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Label lbProductType;
        private System.Windows.Forms.TextBox tbProType;
        private System.Windows.Forms.Label lbCustomerName;
        private System.Windows.Forms.TextBox tbCusName;
        private System.Windows.Forms.Label lbDocumentType;
        private System.Windows.Forms.TextBox tbDocType;
        private System.Windows.Forms.Label lbRefID;
        private System.Windows.Forms.TextBox tbDocID;
        private System.Windows.Forms.Label lbCreatedDate;
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