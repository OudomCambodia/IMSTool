namespace Testing.Forms
{
    partial class CardPrinting
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
            this.dtPolicy = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPolicyNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTotNumber = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAlp = new System.Windows.Forms.RadioButton();
            this.rbDef = new System.Windows.Forms.RadioButton();
            this.chkGPAExcludeSI = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnHistory = new Testing.cus_button();
            this.btnSend = new Testing.cus_button();
            this.btnPreview = new Testing.cus_button();
            this.btnClear = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.btnPrint = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dtPolicy)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtPolicy
            // 
            this.dtPolicy.AllowUserToAddRows = false;
            this.dtPolicy.AllowUserToDeleteRows = false;
            this.dtPolicy.AllowUserToOrderColumns = true;
            this.dtPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtPolicy.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtPolicy.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dtPolicy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtPolicy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtPolicy.Location = new System.Drawing.Point(12, 137);
            this.dtPolicy.Name = "dtPolicy";
            this.dtPolicy.RowHeadersVisible = false;
            this.dtPolicy.RowTemplate.Height = 30;
            this.dtPolicy.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtPolicy.Size = new System.Drawing.Size(955, 442);
            this.dtPolicy.TabIndex = 0;
            this.dtPolicy.DataSourceChanged += new System.EventHandler(this.dtPolicy_DataSourceChanged);
            this.dtPolicy.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtPolicy_CellContentClick);
            this.dtPolicy.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtPolicy_DataBindingComplete);
            this.dtPolicy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtPolicy_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Policy No";
            // 
            // txtPolicyNo
            // 
            this.txtPolicyNo.Location = new System.Drawing.Point(81, 45);
            this.txtPolicyNo.MaxLength = 20;
            this.txtPolicyNo.Name = "txtPolicyNo";
            this.txtPolicyNo.Size = new System.Drawing.Size(196, 20);
            this.txtPolicyNo.TabIndex = 51;
            this.txtPolicyNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPolicyNo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(292, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(581, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Products available for card printing: CYC, VCM, VPC, HNS, GPA, PAC, BHP, MCW and " +
    "PAE.";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(365, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 32);
            this.label3.TabIndex = 53;
            this.label3.Text = "Submit Card Printing";
            // 
            // lbTotNumber
            // 
            this.lbTotNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotNumber.AutoSize = true;
            this.lbTotNumber.BackColor = System.Drawing.Color.Transparent;
            this.lbTotNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotNumber.ForeColor = System.Drawing.Color.White;
            this.lbTotNumber.Location = new System.Drawing.Point(922, 586);
            this.lbTotNumber.Name = "lbTotNumber";
            this.lbTotNumber.Size = new System.Drawing.Size(17, 18);
            this.lbTotNumber.TabIndex = 55;
            this.lbTotNumber.Text = "0";
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.BackColor = System.Drawing.Color.Transparent;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.White;
            this.lbTotal.Location = new System.Drawing.Point(867, 586);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(51, 18);
            this.lbTotal.TabIndex = 54;
            this.lbTotal.Text = "Total:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAlp);
            this.groupBox1.Controls.Add(this.rbDef);
            this.groupBox1.Location = new System.Drawing.Point(15, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 31);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            // 
            // rbAlp
            // 
            this.rbAlp.AutoSize = true;
            this.rbAlp.ForeColor = System.Drawing.Color.White;
            this.rbAlp.Location = new System.Drawing.Point(83, 9);
            this.rbAlp.Name = "rbAlp";
            this.rbAlp.Size = new System.Drawing.Size(82, 17);
            this.rbAlp.TabIndex = 1;
            this.rbAlp.TabStop = true;
            this.rbAlp.Text = "By Alphabet";
            this.rbAlp.UseVisualStyleBackColor = true;
            // 
            // rbDef
            // 
            this.rbDef.AutoSize = true;
            this.rbDef.Checked = true;
            this.rbDef.ForeColor = System.Drawing.Color.White;
            this.rbDef.Location = new System.Drawing.Point(6, 9);
            this.rbDef.Name = "rbDef";
            this.rbDef.Size = new System.Drawing.Size(59, 17);
            this.rbDef.TabIndex = 0;
            this.rbDef.TabStop = true;
            this.rbDef.Text = "Default";
            this.rbDef.UseVisualStyleBackColor = true;
            // 
            // chkGPAExcludeSI
            // 
            this.chkGPAExcludeSI.AutoSize = true;
            this.chkGPAExcludeSI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGPAExcludeSI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chkGPAExcludeSI.Location = new System.Drawing.Point(205, 78);
            this.chkGPAExcludeSI.Name = "chkGPAExcludeSI";
            this.chkGPAExcludeSI.Size = new System.Drawing.Size(181, 17);
            this.chkGPAExcludeSI.TabIndex = 58;
            this.chkGPAExcludeSI.Text = "(GPA Only) Exclude Sum Insured";
            this.chkGPAExcludeSI.UseVisualStyleBackColor = true;
            this.chkGPAExcludeSI.CheckedChanged += new System.EventHandler(this.chkGPAExcludeSI_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(55, 585);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 20);
            this.textBox1.TabIndex = 60;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 588);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 61;
            this.label4.Text = "Go To";
            // 
            // lblSel
            // 
            this.lblSel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSel.AutoSize = true;
            this.lblSel.BackColor = System.Drawing.Color.Transparent;
            this.lblSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSel.ForeColor = System.Drawing.Color.White;
            this.lblSel.Location = new System.Drawing.Point(806, 586);
            this.lblSel.Name = "lblSel";
            this.lblSel.Size = new System.Drawing.Size(17, 18);
            this.lblSel.TabIndex = 63;
            this.lblSel.Text = "0";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(718, 586);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 18);
            this.label6.TabIndex = 62;
            this.label6.Text = "Selected:";
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.Location = new System.Drawing.Point(848, 101);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(118, 30);
            this.btnHistory.TabIndex = 57;
            this.btnHistory.Text = "View History";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.bnHistory_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(285, 101);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(83, 30);
            this.btnSend.TabIndex = 50;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(103, 101);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(83, 30);
            this.btnPreview.TabIndex = 49;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = true;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(194, 101);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(83, 30);
            this.btnClear.TabIndex = 48;
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
            this.bnSearch.Location = new System.Drawing.Point(12, 101);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(83, 30);
            this.bnSearch.TabIndex = 47;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = true;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(740, 101);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 64;
            this.btnPrint.Text = "Print ECard";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // CardPrinting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(979, 612);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblSel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chkGPAExcludeSI);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbTotNumber);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPolicyNo);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtPolicy);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CardPrinting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CardPrinting";
            this.Activated += new System.EventHandler(this.CardPrinting_Activated);
            this.Load += new System.EventHandler(this.CardPrinting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtPolicy)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtPolicy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPolicyNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTotNumber;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAlp;
        private System.Windows.Forms.RadioButton rbDef;
        private System.Windows.Forms.CheckBox chkGPAExcludeSI;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblSel;
        private System.Windows.Forms.Label label6;
        private cus_button btnPreview;
        private cus_button btnClear;
        private cus_button bnSearch;
        private cus_button btnSend;
        private cus_button btnHistory;
        private cus_button btnPrint;
    }
}