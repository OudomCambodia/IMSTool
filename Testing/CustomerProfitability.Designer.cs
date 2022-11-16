namespace Testing
{
    partial class CustomerProfitability
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.txtCusCode = new System.Windows.Forms.TextBox();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CboGroupCustomer = new System.Windows.Forms.ComboBox();
            this.cbMainClass = new System.Windows.Forms.ComboBox();
            this.MainClass = new System.Windows.Forms.Label();
            this.chCusV2 = new System.Windows.Forms.CheckBox();
            this.btnExcel = new Testing.cus_button();
            this.btnClear = new Testing.cus_button();
            this.butQuery = new Testing.cus_button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(-1, 108);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(977, 506);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // txtCusCode
            // 
            this.txtCusCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCusCode.Location = new System.Drawing.Point(126, 49);
            this.txtCusCode.MaxLength = 10;
            this.txtCusCode.Name = "txtCusCode";
            this.txtCusCode.Size = new System.Drawing.Size(184, 20);
            this.txtCusCode.TabIndex = 1;
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(126, 80);
            this.txtCusName.MaxLength = 100;
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(184, 20);
            this.txtCusName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Customer Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Customer Name";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(349, 7);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "Customer Profitability";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(331, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Group Customer";
            // 
            // CboGroupCustomer
            // 
            this.CboGroupCustomer.FormattingEnabled = true;
            this.CboGroupCustomer.Location = new System.Drawing.Point(434, 46);
            this.CboGroupCustomer.Name = "CboGroupCustomer";
            this.CboGroupCustomer.Size = new System.Drawing.Size(234, 21);
            this.CboGroupCustomer.TabIndex = 12;
            // 
            // cbMainClass
            // 
            this.cbMainClass.FormattingEnabled = true;
            this.cbMainClass.Location = new System.Drawing.Point(760, 75);
            this.cbMainClass.Name = "cbMainClass";
            this.cbMainClass.Size = new System.Drawing.Size(143, 21);
            this.cbMainClass.TabIndex = 15;
            this.cbMainClass.Visible = false;
            // 
            // MainClass
            // 
            this.MainClass.AutoSize = true;
            this.MainClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainClass.ForeColor = System.Drawing.Color.White;
            this.MainClass.Location = new System.Drawing.Point(686, 76);
            this.MainClass.Name = "MainClass";
            this.MainClass.Size = new System.Drawing.Size(68, 15);
            this.MainClass.TabIndex = 14;
            this.MainClass.Text = "Main Class";
            this.MainClass.Visible = false;
            // 
            // chCusV2
            // 
            this.chCusV2.AutoSize = true;
            this.chCusV2.ForeColor = System.Drawing.Color.Transparent;
            this.chCusV2.Location = new System.Drawing.Point(689, 46);
            this.chCusV2.Name = "chCusV2";
            this.chCusV2.Size = new System.Drawing.Size(69, 17);
            this.chCusV2.TabIndex = 16;
            this.chCusV2.Text = "Summary";
            this.chCusV2.UseVisualStyleBackColor = true;
            this.chCusV2.CheckedChanged += new System.EventHandler(this.chCusV2_CheckedChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(515, 72);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(75, 30);
            this.btnExcel.TabIndex = 9;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(596, 72);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // butQuery
            // 
            this.butQuery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.butQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butQuery.ForeColor = System.Drawing.Color.White;
            this.butQuery.Location = new System.Drawing.Point(434, 72);
            this.butQuery.Name = "butQuery";
            this.butQuery.Size = new System.Drawing.Size(75, 30);
            this.butQuery.TabIndex = 5;
            this.butQuery.Text = "Query";
            this.butQuery.UseVisualStyleBackColor = true;
            this.butQuery.Click += new System.EventHandler(this.butQuery_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(392, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 7;
            this.label3.Visible = false;
            // 
            // CustomerProfitability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(974, 612);
            this.Controls.Add(this.chCusV2);
            this.Controls.Add(this.cbMainClass);
            this.Controls.Add(this.MainClass);
            this.Controls.Add(this.CboGroupCustomer);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.butQuery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCusName);
            this.Controls.Add(this.txtCusCode);
            this.Controls.Add(this.crystalReportViewer1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CustomerProfitability";
            this.Text = "CustomerProfitability";
            this.Activated += new System.EventHandler(this.CustomerProfitability_Activated);
            this.Load += new System.EventHandler(this.CustomerProfitability_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TextBox txtCusCode;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private cus_button butQuery;
        private cus_button btnClear;
        private cus_button btnExcel;
        private System.Windows.Forms.ComboBox CboGroupCustomer;
        private System.Windows.Forms.ComboBox cbMainClass;
        private System.Windows.Forms.Label MainClass;
        private System.Windows.Forms.CheckBox chCusV2;
        private System.Windows.Forms.Label label3;
    }
}