namespace Testing.Forms
{
    partial class ClaimRequisitionReport
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
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbClaimNo = new System.Windows.Forms.TextBox();
            this.lbClaim = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgClaimPaid = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPDF = new Testing.cus_button();
            this.btnExcel = new Testing.cus_button();
            this.btnClear = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgClaimPaid)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(328, 57);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(122, 20);
            this.dtpTo.TabIndex = 39;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(101, 57);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(131, 20);
            this.dtpFrom.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(239, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 44;
            this.label4.Text = "Intimate Date To:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Intimate Date From:";
            // 
            // tbClaimNo
            // 
            this.tbClaimNo.Location = new System.Drawing.Point(328, 89);
            this.tbClaimNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbClaimNo.MaxLength = 20;
            this.tbClaimNo.Name = "tbClaimNo";
            this.tbClaimNo.Size = new System.Drawing.Size(122, 20);
            this.tbClaimNo.TabIndex = 35;
            this.tbClaimNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbClaimNo_KeyDown);
            // 
            // lbClaim
            // 
            this.lbClaim.AutoSize = true;
            this.lbClaim.BackColor = System.Drawing.Color.Transparent;
            this.lbClaim.ForeColor = System.Drawing.Color.White;
            this.lbClaim.Location = new System.Drawing.Point(274, 93);
            this.lbClaim.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbClaim.Name = "lbClaim";
            this.lbClaim.Size = new System.Drawing.Size(52, 13);
            this.lbClaim.TabIndex = 38;
            this.lbClaim.Text = "Claim No:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(265, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 32);
            this.label1.TabIndex = 32;
            this.label1.Text = "Claim Requisition Report";
            // 
            // dgClaimPaid
            // 
            this.dgClaimPaid.AllowUserToAddRows = false;
            this.dgClaimPaid.AllowUserToDeleteRows = false;
            this.dgClaimPaid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgClaimPaid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgClaimPaid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgClaimPaid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgClaimPaid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgClaimPaid.Location = new System.Drawing.Point(5, 119);
            this.dgClaimPaid.Margin = new System.Windows.Forms.Padding(4);
            this.dgClaimPaid.Name = "dgClaimPaid";
            this.dgClaimPaid.ReadOnly = true;
            this.dgClaimPaid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgClaimPaid.RowTemplate.Height = 25;
            this.dgClaimPaid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgClaimPaid.Size = new System.Drawing.Size(972, 506);
            this.dgClaimPaid.TabIndex = 47;
            this.dgClaimPaid.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgClaimPaid_DataBindingComplete);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(833, 603);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(71, 13);
            this.lblTotal.TabIndex = 48;
            this.lblTotal.Text = "Payee Name:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(100, 89);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(132, 21);
            this.comboBox1.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(62, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 51;
            this.label2.Text = "Class:";
            // 
            // btnPDF
            // 
            this.btnPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPDF.ForeColor = System.Drawing.Color.White;
            this.btnPDF.Location = new System.Drawing.Point(664, 81);
            this.btnPDF.Margin = new System.Windows.Forms.Padding(4);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(66, 30);
            this.btnPDF.TabIndex = 49;
            this.btnPDF.Text = "PDF";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(597, 81);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(63, 30);
            this.btnExcel.TabIndex = 46;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(527, 81);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 30);
            this.btnClear.TabIndex = 41;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(454, 81);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(69, 30);
            this.bnSearch.TabIndex = 40;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = true;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // ClaimRequisitionReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(990, 650);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btnPDF);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dgClaimPaid);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbClaimNo);
            this.Controls.Add(this.lbClaim);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ClaimRequisitionReport";
            this.Text = "ClaimPaidReportPayee";
            this.Load += new System.EventHandler(this.ClaimPaidReportPayee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgClaimPaid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private cus_button btnExcel;
        private cus_button btnClear;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbClaimNo;
        private System.Windows.Forms.Label lbClaim;
        private cus_button bnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgClaimPaid;
        private System.Windows.Forms.Label lblTotal;
        private cus_button btnPDF;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}