namespace Testing.Forms
{
    partial class ClPaymentReq
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkProducts = new System.Windows.Forms.CheckedListBox();
            this.tbRequisitionNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lblSel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bnClear = new Testing.cus_button();
            this.bnView = new Testing.cus_button();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.btnNonDirect = new Testing.cus_button();
            this.btnDirectBill = new Testing.cus_button();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(1082, 46);
            this.label1.TabIndex = 14;
            this.label1.Text = "Claim Payment Request List";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chkProducts);
            this.groupBox1.Controls.Add(this.tbRequisitionNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbTotal);
            this.groupBox1.Controls.Add(this.lblSel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.bnClear);
            this.groupBox1.Controls.Add(this.bnView);
            this.groupBox1.Controls.Add(this.dtpDateTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpDateFrom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Location = new System.Drawing.Point(0, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1082, 191);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Requisition Information";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(397, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 17);
            this.label5.TabIndex = 153;
            this.label5.Text = "Selected Products:";
            // 
            // chkProducts
            // 
            this.chkProducts.FormattingEnabled = true;
            this.chkProducts.Location = new System.Drawing.Point(535, 30);
            this.chkProducts.Name = "chkProducts";
            this.chkProducts.Size = new System.Drawing.Size(413, 137);
            this.chkProducts.TabIndex = 152;
            // 
            // tbRequisitionNo
            // 
            this.tbRequisitionNo.Location = new System.Drawing.Point(174, 27);
            this.tbRequisitionNo.Name = "tbRequisitionNo";
            this.tbRequisitionNo.Size = new System.Drawing.Size(199, 24);
            this.tbRequisitionNo.TabIndex = 151;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(32, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 17);
            this.label2.TabIndex = 150;
            this.label2.Text = "Claim/Requisition No:";
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.White;
            this.lbTotal.Location = new System.Drawing.Point(1005, 149);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(12, 18);
            this.lbTotal.TabIndex = 30;
            this.lbTotal.Text = " ";
            // 
            // lblSel
            // 
            this.lblSel.AutoSize = true;
            this.lblSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSel.ForeColor = System.Drawing.Color.White;
            this.lblSel.Location = new System.Drawing.Point(1029, 130);
            this.lblSel.Name = "lblSel";
            this.lblSel.Size = new System.Drawing.Size(12, 18);
            this.lblSel.TabIndex = 29;
            this.lblSel.Text = " ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(954, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 18);
            this.label8.TabIndex = 18;
            this.label8.Text = "Total:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(954, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 18);
            this.label7.TabIndex = 19;
            this.label7.Text = "Selected:";
            // 
            // bnClear
            // 
            this.bnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClear.FlatAppearance.BorderSize = 2;
            this.bnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClear.ForeColor = System.Drawing.Color.White;
            this.bnClear.Location = new System.Drawing.Point(277, 122);
            this.bnClear.Name = "bnClear";
            this.bnClear.Size = new System.Drawing.Size(98, 26);
            this.bnClear.TabIndex = 27;
            this.bnClear.Text = "Clear";
            this.bnClear.UseVisualStyleBackColor = false;
            this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
            // 
            // bnView
            // 
            this.bnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnView.FlatAppearance.BorderSize = 2;
            this.bnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnView.ForeColor = System.Drawing.Color.White;
            this.bnView.Location = new System.Drawing.Point(164, 122);
            this.bnView.Name = "bnView";
            this.bnView.Size = new System.Drawing.Size(98, 26);
            this.bnView.TabIndex = 26;
            this.bnView.Text = "View";
            this.bnView.UseVisualStyleBackColor = false;
            this.bnView.Click += new System.EventHandler(this.bnView_Click);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(252, 73);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(122, 24);
            this.dtpDateTo.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "To :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "From:";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(84, 73);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(122, 24);
            this.dtpDateFrom.TabIndex = 22;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.dgvResult);
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox6.Location = new System.Drawing.Point(0, 243);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1082, 393);
            this.groupBox6.TabIndex = 37;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = " ";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(3, 20);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvResult.RowTemplate.Height = 30;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(1076, 370);
            this.dgvResult.TabIndex = 17;
            this.dgvResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResult_CellContentClick);
            // 
            // btnNonDirect
            // 
            this.btnNonDirect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNonDirect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnNonDirect.FlatAppearance.BorderSize = 2;
            this.btnNonDirect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNonDirect.ForeColor = System.Drawing.Color.White;
            this.btnNonDirect.Location = new System.Drawing.Point(950, 642);
            this.btnNonDirect.Name = "btnNonDirect";
            this.btnNonDirect.Size = new System.Drawing.Size(120, 26);
            this.btnNonDirect.TabIndex = 38;
            this.btnNonDirect.Text = "Non-Direct Billing";
            this.btnNonDirect.UseVisualStyleBackColor = false;
            this.btnNonDirect.Click += new System.EventHandler(this.btnNonDirect_Click);
            // 
            // btnDirectBill
            // 
            this.btnDirectBill.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnDirectBill.FlatAppearance.BorderSize = 2;
            this.btnDirectBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDirectBill.ForeColor = System.Drawing.Color.White;
            this.btnDirectBill.Location = new System.Drawing.Point(835, 642);
            this.btnDirectBill.Name = "btnDirectBill";
            this.btnDirectBill.Size = new System.Drawing.Size(98, 26);
            this.btnDirectBill.TabIndex = 16;
            this.btnDirectBill.Text = "Direct Building";
            this.btnDirectBill.UseVisualStyleBackColor = false;
            this.btnDirectBill.Click += new System.EventHandler(this.btnDirectBill_Click);
            // 
            // ClPaymentReq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1082, 680);
            this.Controls.Add(this.btnNonDirect);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btnDirectBill);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ClPaymentReq";
            this.Text = "ClPaymentReq";
            this.Load += new System.EventHandler(this.ClPaymentReq_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lblSel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private cus_button bnClear;
        private cus_button bnView;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.TextBox tbRequisitionNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvResult;
        private cus_button btnDirectBill;
        private cus_button btnNonDirect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckedListBox chkProducts;

    }
}