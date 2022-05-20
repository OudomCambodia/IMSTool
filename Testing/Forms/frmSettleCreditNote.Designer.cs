namespace Testing.Forms
{
    partial class frmSettleCreditNote
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.bnClear = new Testing.cus_button();
            this.bnExcel = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.lbTotalNum = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(378, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 32);
            this.label1.TabIndex = 9;
            this.label1.Text = "Settle Credit Note";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(264, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(294, 66);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(176, 23);
            this.dtpTo.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "From:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(58, 66);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(176, 23);
            this.dtpFrom.TabIndex = 15;
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClear.FlatAppearance.BorderSize = 2;
            this.bnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClear.ForeColor = System.Drawing.Color.White;
            this.bnClear.Location = new System.Drawing.Point(666, 62);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(73, 29);
            this.bnClear.TabIndex = 21;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = false;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // bnExcel
            // 
            this.bnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnExcel.FlatAppearance.BorderSize = 2;
            this.bnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnExcel.ForeColor = System.Drawing.Color.White;
            this.bnExcel.Location = new System.Drawing.Point(587, 62);
            this.bnExcel.Name = "bnExcel";
            this.bnExcel.Size = new System.Drawing.Size(73, 29);
            this.bnExcel.TabIndex = 20;
            this.bnExcel.Text = "Excel";
            this.bnExcel.UseVisualStyleBackColor = false;
            this.bnExcel.Click += new System.EventHandler(this.bnExcel_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearch.FlatAppearance.BorderSize = 2;
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(508, 62);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(73, 29);
            this.bnSearch.TabIndex = 19;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = false;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(15, 109);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.Size = new System.Drawing.Size(963, 529);
            this.dgvResult.TabIndex = 22;
            this.dgvResult.DataSourceChanged += new System.EventHandler(this.dgvResult_DataSourceChanged);
            this.dgvResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvResult_DataBindingComplete);
            // 
            // lbTotalNum
            // 
            this.lbTotalNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotalNum.AutoSize = true;
            this.lbTotalNum.BackColor = System.Drawing.Color.Transparent;
            this.lbTotalNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalNum.ForeColor = System.Drawing.Color.White;
            this.lbTotalNum.Location = new System.Drawing.Point(917, 69);
            this.lbTotalNum.Name = "lbTotalNum";
            this.lbTotalNum.Size = new System.Drawing.Size(17, 18);
            this.lbTotalNum.TabIndex = 46;
            this.lbTotalNum.Text = "0";
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.BackColor = System.Drawing.Color.Transparent;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.White;
            this.lbTotal.Location = new System.Drawing.Point(862, 69);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(51, 18);
            this.lbTotal.TabIndex = 45;
            this.lbTotal.Text = "Total:";
            // 
            // frmSettleCreditNote
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(990, 650);
            this.Controls.Add(this.lbTotalNum);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.dgvResult);
            this.Controls.Add(this.bnClear);
            this.Controls.Add(this.bnExcel);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSettleCreditNote";
            this.Text = "frmSettleCreditNote";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private cus_button bnClear;
        private cus_button bnExcel;
        private cus_button bnSearch;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label lbTotalNum;
        private System.Windows.Forms.Label lbTotal;
    }
}