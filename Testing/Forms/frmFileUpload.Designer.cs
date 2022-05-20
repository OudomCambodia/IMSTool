namespace Testing.Forms
{
    partial class frmFileUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFileUpload));
            this.ofdUpload = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndoNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpEffFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpEffTo = new System.Windows.Forms.DateTimePicker();
            this.dgvFile = new System.Windows.Forms.DataGridView();
            this.File_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOpen = new Testing.cus_button();
            this.btnRemove = new Testing.cus_button();
            this.btnClose = new Testing.cus_button();
            this.bnSave = new Testing.cus_button();
            this.bnBrowse = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).BeginInit();
            this.SuspendLayout();
            // 
            // ofdUpload
            // 
            this.ofdUpload.Multiselect = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(321, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Effective To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Effective From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Endorsement No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(84, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Files";
            // 
            // txtEndoNo
            // 
            this.txtEndoNo.BackColor = System.Drawing.SystemColors.Control;
            this.txtEndoNo.Enabled = false;
            this.txtEndoNo.Location = new System.Drawing.Point(124, 57);
            this.txtEndoNo.Name = "txtEndoNo";
            this.txtEndoNo.ReadOnly = true;
            this.txtEndoNo.Size = new System.Drawing.Size(180, 22);
            this.txtEndoNo.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(239, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 32);
            this.label6.TabIndex = 35;
            this.label6.Text = "File Upload";
            // 
            // dtpEffFrom
            // 
            this.dtpEffFrom.CalendarMonthBackground = System.Drawing.Color.Gainsboro;
            this.dtpEffFrom.Enabled = false;
            this.dtpEffFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffFrom.Location = new System.Drawing.Point(124, 98);
            this.dtpEffFrom.Name = "dtpEffFrom";
            this.dtpEffFrom.Size = new System.Drawing.Size(180, 22);
            this.dtpEffFrom.TabIndex = 36;
            // 
            // dtpEffTo
            // 
            this.dtpEffTo.CalendarMonthBackground = System.Drawing.Color.Gainsboro;
            this.dtpEffTo.Enabled = false;
            this.dtpEffTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEffTo.Location = new System.Drawing.Point(406, 98);
            this.dtpEffTo.Name = "dtpEffTo";
            this.dtpEffTo.Size = new System.Drawing.Size(174, 22);
            this.dtpEffTo.TabIndex = 37;
            // 
            // dgvFile
            // 
            this.dgvFile.AllowDrop = true;
            this.dgvFile.AllowUserToAddRows = false;
            this.dgvFile.AllowUserToDeleteRows = false;
            this.dgvFile.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFile.BackgroundColor = System.Drawing.Color.White;
            this.dgvFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.File_Name,
            this.File_Path});
            this.dgvFile.Location = new System.Drawing.Point(124, 141);
            this.dgvFile.Name = "dgvFile";
            this.dgvFile.ReadOnly = true;
            this.dgvFile.RowHeadersVisible = false;
            this.dgvFile.RowTemplate.Height = 30;
            this.dgvFile.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFile.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFile.Size = new System.Drawing.Size(368, 139);
            this.dgvFile.TabIndex = 4;
            this.dgvFile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFile_CellDoubleClick);
            this.dgvFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.dgvFile_DragDrop);
            this.dgvFile.DragEnter += new System.Windows.Forms.DragEventHandler(this.dgvFile_DragEnter);
            // 
            // File_Name
            // 
            this.File_Name.HeaderText = "File Name";
            this.File_Name.Name = "File_Name";
            this.File_Name.ReadOnly = true;
            // 
            // File_Path
            // 
            this.File_Path.HeaderText = "Path";
            this.File_Path.Name = "File_Path";
            this.File_Path.ReadOnly = true;
            this.File_Path.Visible = false;
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(124, 301);
            this.txtRemark.MaxLength = 1000;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(456, 88);
            this.txtRemark.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(63, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Remark";
            // 
            // btnOpen
            // 
            this.btnOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.SplitButton;
            this.btnOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpen.ForeColor = System.Drawing.Color.White;
            this.btnOpen.Location = new System.Drawing.Point(511, 197);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(69, 25);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.Location = new System.Drawing.Point(511, 255);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(69, 25);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(486, 406);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 35);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // bnSave
            // 
            this.bnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSave.ForeColor = System.Drawing.Color.White;
            this.bnSave.Location = new System.Drawing.Point(376, 406);
            this.bnSave.Name = "bnSave";
            this.bnSave.Size = new System.Drawing.Size(85, 35);
            this.bnSave.TabIndex = 5;
            this.bnSave.Text = "Save";
            this.bnSave.UseVisualStyleBackColor = true;
            this.bnSave.Click += new System.EventHandler(this.bnSave_Click);
            // 
            // bnBrowse
            // 
            this.bnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnBrowse.ForeColor = System.Drawing.Color.White;
            this.bnBrowse.Location = new System.Drawing.Point(511, 141);
            this.bnBrowse.Name = "bnBrowse";
            this.bnBrowse.Size = new System.Drawing.Size(69, 25);
            this.bnBrowse.TabIndex = 1;
            this.bnBrowse.Text = "Browse";
            this.bnBrowse.UseVisualStyleBackColor = true;
            this.bnBrowse.Click += new System.EventHandler(this.bnBrowse_Click);
            // 
            // frmFileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(599, 462);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.dgvFile);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.dtpEffTo);
            this.Controls.Add(this.dtpEffFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtEndoNo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bnSave);
            this.Controls.Add(this.bnBrowse);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmFileUpload";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Upload";
            this.Load += new System.EventHandler(this.frmFileUpload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdUpload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEndoNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpEffFrom;
        private System.Windows.Forms.DateTimePicker dtpEffTo;
        private System.Windows.Forms.DataGridView dgvFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn File_Path;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label5;
        private cus_button bnBrowse;
        private cus_button btnClose;
        private cus_button btnRemove;
        private cus_button btnOpen;
        private cus_button bnSave;
    }
}