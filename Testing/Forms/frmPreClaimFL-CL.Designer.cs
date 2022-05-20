namespace Testing.Forms
{
    partial class frmPreClaimFL_CL
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
            this.dgvPreClaim = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bnExcel = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreClaim)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPreClaim
            // 
            this.dgvPreClaim.AllowUserToAddRows = false;
            this.dgvPreClaim.AllowUserToDeleteRows = false;
            this.dgvPreClaim.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPreClaim.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvPreClaim.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPreClaim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreClaim.Location = new System.Drawing.Point(15, 97);
            this.dgvPreClaim.Margin = new System.Windows.Forms.Padding(5);
            this.dgvPreClaim.Name = "dgvPreClaim";
            this.dgvPreClaim.ReadOnly = true;
            this.dgvPreClaim.RowHeadersVisible = false;
            this.dgvPreClaim.RowTemplate.Height = 25;
            this.dgvPreClaim.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPreClaim.Size = new System.Drawing.Size(844, 451);
            this.dgvPreClaim.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(252, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 32);
            this.label1.TabIndex = 10;
            this.label1.Text = "Premium Claim for Financial Lines";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(419, 60);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(175, 23);
            this.dtpTo.TabIndex = 16;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(127, 60);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(175, 23);
            this.dtpFrom.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(321, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 18;
            this.label4.Text = "Expiry Date To:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Expiry Date From:";
            // 
            // bnExcel
            // 
            this.bnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnExcel.FlatAppearance.BorderSize = 2;
            this.bnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnExcel.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnExcel.ForeColor = System.Drawing.Color.White;
            this.bnExcel.Location = new System.Drawing.Point(744, 56);
            this.bnExcel.Name = "bnExcel";
            this.bnExcel.Size = new System.Drawing.Size(91, 33);
            this.bnExcel.TabIndex = 19;
            this.bnExcel.Text = "To Excel";
            this.bnExcel.UseVisualStyleBackColor = false;
            this.bnExcel.Click += new System.EventHandler(this.bnExcel_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearch.FlatAppearance.BorderSize = 2;
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(633, 56);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(91, 33);
            this.bnSearch.TabIndex = 1;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = false;
            this.bnSearch.Click += new System.EventHandler(this.cus_button1_Click);
            // 
            // frmPreClaimFL_CL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(874, 562);
            this.Controls.Add(this.bnExcel);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPreClaim);
            this.Controls.Add(this.bnSearch);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPreClaimFL_CL";
            this.Text = "Premium and Claim FL and CL";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreClaim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private cus_button bnSearch;
        private System.Windows.Forms.DataGridView dgvPreClaim;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private cus_button bnExcel;
    }
}