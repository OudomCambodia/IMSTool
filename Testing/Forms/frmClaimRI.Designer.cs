namespace Testing.Forms
{
    partial class frmClaimRI
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
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lbDateTo = new System.Windows.Forms.Label();
            this.lbDateFrom = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.bnSearch = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(12, 9);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(808, 32);
            this.lbTitle.TabIndex = 22;
            this.lbTitle.Text = "Claim Incurred with FAC Reinsurer";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(253, 61);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(161, 20);
            this.dtpTo.TabIndex = 36;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(12, 62);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(161, 20);
            this.dtpFrom.TabIndex = 35;
            // 
            // lbDateTo
            // 
            this.lbDateTo.AutoSize = true;
            this.lbDateTo.BackColor = System.Drawing.Color.Transparent;
            this.lbDateTo.ForeColor = System.Drawing.Color.White;
            this.lbDateTo.Location = new System.Drawing.Point(250, 46);
            this.lbDateTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDateTo.Name = "lbDateTo";
            this.lbDateTo.Size = new System.Drawing.Size(88, 13);
            this.lbDateTo.TabIndex = 38;
            this.lbDateTo.Text = "Notified Date To:";
            // 
            // lbDateFrom
            // 
            this.lbDateFrom.AutoSize = true;
            this.lbDateFrom.BackColor = System.Drawing.Color.Transparent;
            this.lbDateFrom.ForeColor = System.Drawing.Color.White;
            this.lbDateFrom.Location = new System.Drawing.Point(9, 46);
            this.lbDateFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDateFrom.Name = "lbDateFrom";
            this.lbDateFrom.Size = new System.Drawing.Size(98, 13);
            this.lbDateFrom.TabIndex = 37;
            this.lbDateFrom.Text = "Notified Date From:";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 88);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.Size = new System.Drawing.Size(809, 484);
            this.dataGridView.TabIndex = 40;
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(452, 53);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(83, 29);
            this.bnSearch.TabIndex = 39;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = true;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // frmClaimRI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(833, 584);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lbDateTo);
            this.Controls.Add(this.lbDateFrom);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmClaimRI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Claim with RI";
            this.Load += new System.EventHandler(this.frmClaimRI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private cus_button bnSearch;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lbDateTo;
        private System.Windows.Forms.Label lbDateFrom;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}