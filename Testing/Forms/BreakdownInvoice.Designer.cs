namespace Testing.Forms
{
    partial class BreakdownInvoice
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbInvoiceNo = new System.Windows.Forms.TextBox();
            this.btnOptionII = new Testing.cus_button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOptionI = new Testing.cus_button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cus_button1 = new Testing.cus_button();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceDetails)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(641, 62);
            this.lbTitle.TabIndex = 14;
            this.lbTitle.Text = "Issue Breakdown Invoice";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 250);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debit / Credit Note";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.dgvInvoiceDetails);
            this.panel2.Location = new System.Drawing.Point(3, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 157);
            this.panel2.TabIndex = 172;
            // 
            // dgvInvoiceDetails
            // 
            this.dgvInvoiceDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvInvoiceDetails.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvInvoiceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoiceDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvInvoiceDetails.Name = "dgvInvoiceDetails";
            this.dgvInvoiceDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvInvoiceDetails.Size = new System.Drawing.Size(635, 157);
            this.dgvInvoiceDetails.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tbInvoiceNo);
            this.panel1.Controls.Add(this.btnOptionII);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnOptionI);
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 64);
            this.panel1.TabIndex = 171;
            // 
            // tbInvoiceNo
            // 
            this.tbInvoiceNo.Location = new System.Drawing.Point(60, 15);
            this.tbInvoiceNo.MaxLength = 16;
            this.tbInvoiceNo.Name = "tbInvoiceNo";
            this.tbInvoiceNo.Size = new System.Drawing.Size(199, 20);
            this.tbInvoiceNo.TabIndex = 84;
            // 
            // btnOptionII
            // 
            this.btnOptionII.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnOptionII.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptionII.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptionII.ForeColor = System.Drawing.Color.White;
            this.btnOptionII.Location = new System.Drawing.Point(405, 9);
            this.btnOptionII.Name = "btnOptionII";
            this.btnOptionII.Size = new System.Drawing.Size(111, 30);
            this.btnOptionII.TabIndex = 170;
            this.btnOptionII.Text = "Option II";
            this.btnOptionII.UseVisualStyleBackColor = true;
            this.btnOptionII.Click += new System.EventHandler(this.btnOptionII_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "DN/CN";
            // 
            // btnOptionI
            // 
            this.btnOptionI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnOptionI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptionI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOptionI.ForeColor = System.Drawing.Color.White;
            this.btnOptionI.Location = new System.Drawing.Point(276, 9);
            this.btnOptionI.Name = "btnOptionI";
            this.btnOptionI.Size = new System.Drawing.Size(111, 30);
            this.btnOptionI.TabIndex = 169;
            this.btnOptionI.Text = "Option I";
            this.btnOptionI.UseVisualStyleBackColor = true;
            this.btnOptionI.Click += new System.EventHandler(this.btnOptionI_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.cus_button1);
            this.panel3.Location = new System.Drawing.Point(2, 317);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(635, 42);
            this.panel3.TabIndex = 172;
            // 
            // cus_button1
            // 
            this.cus_button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cus_button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.cus_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cus_button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cus_button1.ForeColor = System.Drawing.Color.White;
            this.cus_button1.Location = new System.Drawing.Point(516, 5);
            this.cus_button1.Name = "cus_button1";
            this.cus_button1.Size = new System.Drawing.Size(111, 30);
            this.cus_button1.TabIndex = 170;
            this.cus_button1.Text = "ISSUE";
            this.cus_button1.UseVisualStyleBackColor = true;
            // 
            // BreakdownInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(641, 362);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BreakdownInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BreakdownInvoice";
            this.Load += new System.EventHandler(this.BreakdownInvoice_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbInvoiceNo;
        private cus_button btnOptionI;
        private cus_button btnOptionII;
        private System.Windows.Forms.DataGridView dgvInvoiceDetails;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private cus_button cus_button1;
    }
}