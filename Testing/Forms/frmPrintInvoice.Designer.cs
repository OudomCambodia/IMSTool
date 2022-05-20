namespace Testing.Forms
{
    partial class frmPrintInvoice
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
            this.components = new System.ComponentModel.Container();
            this.tbPolicyNo = new System.Windows.Forms.TextBox();
            this.lbPolicyNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comBoxDebit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBank = new System.Windows.Forms.ComboBox();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.cbListAll = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BHPLetterPrnt = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbListAllTran = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCoIn = new System.Windows.Forms.DataGridView();
            this.btnClear = new Testing.cus_button();
            this.btnPrint = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoIn)).BeginInit();
            this.SuspendLayout();
            // 
            // tbPolicyNo
            // 
            this.tbPolicyNo.Location = new System.Drawing.Point(55, 48);
            this.tbPolicyNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbPolicyNo.MaxLength = 20;
            this.tbPolicyNo.Name = "tbPolicyNo";
            this.tbPolicyNo.Size = new System.Drawing.Size(138, 20);
            this.tbPolicyNo.TabIndex = 19;
            this.tbPolicyNo.Leave += new System.EventHandler(this.tbPolicyNo_Leave);
            // 
            // lbPolicyNo
            // 
            this.lbPolicyNo.AutoSize = true;
            this.lbPolicyNo.BackColor = System.Drawing.Color.Transparent;
            this.lbPolicyNo.ForeColor = System.Drawing.Color.White;
            this.lbPolicyNo.Location = new System.Drawing.Point(-1, 51);
            this.lbPolicyNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPolicyNo.Name = "lbPolicyNo";
            this.lbPolicyNo.Size = new System.Drawing.Size(55, 13);
            this.lbPolicyNo.TabIndex = 22;
            this.lbPolicyNo.Text = "Policy No:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(391, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 32);
            this.label1.TabIndex = 21;
            this.label1.Text = "Print Invoice";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(201, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "Debit Note :";
            // 
            // comBoxDebit
            // 
            this.comBoxDebit.FormattingEnabled = true;
            this.comBoxDebit.Location = new System.Drawing.Point(272, 48);
            this.comBoxDebit.Name = "comBoxDebit";
            this.comBoxDebit.Size = new System.Drawing.Size(102, 21);
            this.comBoxDebit.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(800, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Bank";
            this.label3.Visible = false;
            // 
            // comboBank
            // 
            this.comboBank.FormattingEnabled = true;
            this.comboBank.Location = new System.Drawing.Point(839, 5);
            this.comboBank.Name = "comboBank";
            this.comboBank.Size = new System.Drawing.Size(53, 21);
            this.comboBank.TabIndex = 54;
            this.comboBank.Visible = false;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Location = new System.Drawing.Point(2, 120);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(968, 494);
            this.crystalReportViewer1.TabIndex = 57;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // cbListAll
            // 
            this.cbListAll.AutoSize = true;
            this.cbListAll.ForeColor = System.Drawing.Color.White;
            this.cbListAll.Location = new System.Drawing.Point(704, 8);
            this.cbListAll.Name = "cbListAll";
            this.cbListAll.Size = new System.Drawing.Size(89, 17);
            this.cbListAll.TabIndex = 58;
            this.cbListAll.Text = "List All Banks";
            this.cbListAll.UseVisualStyleBackColor = true;
            this.cbListAll.Visible = false;
            this.cbListAll.CheckedChanged += new System.EventHandler(this.cbListAll_CheckedChanged);
            // 
            // BHPLetterPrnt
            // 
            this.BHPLetterPrnt.AutoSize = true;
            this.BHPLetterPrnt.ForeColor = System.Drawing.Color.White;
            this.BHPLetterPrnt.Location = new System.Drawing.Point(458, 84);
            this.BHPLetterPrnt.Name = "BHPLetterPrnt";
            this.BHPLetterPrnt.Size = new System.Drawing.Size(207, 17);
            this.BHPLetterPrnt.TabIndex = 59;
            this.BHPLetterPrnt.Text = "Include BHP Acknowledgement Letter";
            this.BHPLetterPrnt.UseVisualStyleBackColor = true;
            this.BHPLetterPrnt.CheckedChanged += new System.EventHandler(this.BHPLetterPrnt_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(200, 84);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 53;
            this.label4.Text = "Transaction :";
            // 
            // cbListAllTran
            // 
            this.cbListAllTran.FormattingEnabled = true;
            this.cbListAllTran.Location = new System.Drawing.Point(270, 81);
            this.cbListAllTran.Name = "cbListAllTran";
            this.cbListAllTran.Size = new System.Drawing.Size(181, 21);
            this.cbListAllTran.TabIndex = 52;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCoIn);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(704, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 82);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coinsurance Information";
            // 
            // dgvCoIn
            // 
            this.dgvCoIn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCoIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCoIn.Location = new System.Drawing.Point(3, 16);
            this.dgvCoIn.Name = "dgvCoIn";
            this.dgvCoIn.RowHeadersVisible = false;
            this.dgvCoIn.Size = new System.Drawing.Size(252, 63);
            this.dgvCoIn.TabIndex = 0;
            this.dgvCoIn.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCoIn_DataBindingComplete);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(633, 42);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 30);
            this.btnClear.TabIndex = 60;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(449, 42);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(56, 30);
            this.btnPrint.TabIndex = 56;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.Gray;
            this.bnSearch.Enabled = false;
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(513, 42);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(112, 30);
            this.bnSearch.TabIndex = 20;
            this.bnSearch.Text = "Create Bank";
            this.bnSearch.UseVisualStyleBackColor = false;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // frmPrintInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(974, 612);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.BHPLetterPrnt);
            this.Controls.Add(this.cbListAll);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBank);
            this.Controls.Add(this.cbListAllTran);
            this.Controls.Add(this.comBoxDebit);
            this.Controls.Add(this.tbPolicyNo);
            this.Controls.Add(this.lbPolicyNo);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimizeBox = false;
            this.Name = "frmPrintInvoice";
            this.Text = "frmPrintInvoice";
            this.Load += new System.EventHandler(this.frmPrintInvoice_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPolicyNo;
        private System.Windows.Forms.Label lbPolicyNo;
        private cus_button bnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comBoxDebit;
        private System.Windows.Forms.Label label3;
        private cus_button btnPrint;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        public System.Windows.Forms.ComboBox comboBank;
        private System.Windows.Forms.CheckBox cbListAll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox BHPLetterPrnt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbListAllTran;
        private cus_button btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvCoIn;
    }
}