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
            this.gbCOI = new System.Windows.Forms.GroupBox();
            this.chkPrintStamp = new System.Windows.Forms.CheckBox();
            this.rdbNo = new System.Windows.Forms.RadioButton();
            this.rdbYes = new System.Windows.Forms.RadioButton();
            this.lbCOI = new System.Windows.Forms.Label();
            this.btnClear = new Testing.cus_button();
            this.btnPrint = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoIn)).BeginInit();
            this.gbCOI.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPolicyNo
            // 
            this.tbPolicyNo.Location = new System.Drawing.Point(73, 59);
            this.tbPolicyNo.Margin = new System.Windows.Forms.Padding(5);
            this.tbPolicyNo.MaxLength = 20;
            this.tbPolicyNo.Name = "tbPolicyNo";
            this.tbPolicyNo.Size = new System.Drawing.Size(183, 22);
            this.tbPolicyNo.TabIndex = 19;
            this.tbPolicyNo.Leave += new System.EventHandler(this.tbPolicyNo_Leave);
            // 
            // lbPolicyNo
            // 
            this.lbPolicyNo.AutoSize = true;
            this.lbPolicyNo.BackColor = System.Drawing.Color.Transparent;
            this.lbPolicyNo.ForeColor = System.Drawing.Color.White;
            this.lbPolicyNo.Location = new System.Drawing.Point(-1, 63);
            this.lbPolicyNo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbPolicyNo.Name = "lbPolicyNo";
            this.lbPolicyNo.Size = new System.Drawing.Size(71, 17);
            this.lbPolicyNo.TabIndex = 22;
            this.lbPolicyNo.Text = "Policy No:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(521, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 40);
            this.label1.TabIndex = 21;
            this.label1.Text = "Print Invoice";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(268, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 53;
            this.label2.Text = "Debit Note :";
            // 
            // comBoxDebit
            // 
            this.comBoxDebit.FormattingEnabled = true;
            this.comBoxDebit.Location = new System.Drawing.Point(363, 59);
            this.comBoxDebit.Margin = new System.Windows.Forms.Padding(4);
            this.comBoxDebit.Name = "comBoxDebit";
            this.comBoxDebit.Size = new System.Drawing.Size(135, 24);
            this.comBoxDebit.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1067, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 17);
            this.label3.TabIndex = 55;
            this.label3.Text = "Bank";
            this.label3.Visible = false;
            // 
            // comboBank
            // 
            this.comboBank.FormattingEnabled = true;
            this.comboBank.Location = new System.Drawing.Point(1119, 6);
            this.comboBank.Margin = new System.Windows.Forms.Padding(4);
            this.comboBank.Name = "comboBank";
            this.comboBank.Size = new System.Drawing.Size(69, 24);
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
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 189);
            this.crystalReportViewer1.Margin = new System.Windows.Forms.Padding(4);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.ShowGroupTreeButton = false;
            this.crystalReportViewer1.Size = new System.Drawing.Size(1290, 567);
            this.crystalReportViewer1.TabIndex = 57;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // cbListAll
            // 
            this.cbListAll.AutoSize = true;
            this.cbListAll.ForeColor = System.Drawing.Color.White;
            this.cbListAll.Location = new System.Drawing.Point(939, 10);
            this.cbListAll.Margin = new System.Windows.Forms.Padding(4);
            this.cbListAll.Name = "cbListAll";
            this.cbListAll.Size = new System.Drawing.Size(114, 21);
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
            this.BHPLetterPrnt.Location = new System.Drawing.Point(623, 102);
            this.BHPLetterPrnt.Margin = new System.Windows.Forms.Padding(4);
            this.BHPLetterPrnt.Name = "BHPLetterPrnt";
            this.BHPLetterPrnt.Size = new System.Drawing.Size(266, 21);
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
            this.label4.Location = new System.Drawing.Point(267, 103);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 53;
            this.label4.Text = "Transaction :";
            // 
            // cbListAllTran
            // 
            this.cbListAllTran.FormattingEnabled = true;
            this.cbListAllTran.Location = new System.Drawing.Point(360, 100);
            this.cbListAllTran.Margin = new System.Windows.Forms.Padding(4);
            this.cbListAllTran.Name = "cbListAllTran";
            this.cbListAllTran.Size = new System.Drawing.Size(240, 24);
            this.cbListAllTran.TabIndex = 52;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCoIn);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(939, 39);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(344, 101);
            this.groupBox1.TabIndex = 61;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Coinsurance Information";
            // 
            // dgvCoIn
            // 
            this.dgvCoIn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCoIn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCoIn.Location = new System.Drawing.Point(4, 19);
            this.dgvCoIn.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCoIn.Name = "dgvCoIn";
            this.dgvCoIn.RowHeadersVisible = false;
            this.dgvCoIn.Size = new System.Drawing.Size(336, 78);
            this.dgvCoIn.TabIndex = 0;
            this.dgvCoIn.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCoIn_DataBindingComplete);
            // 
            // gbCOI
            // 
            this.gbCOI.Controls.Add(this.chkPrintStamp);
            this.gbCOI.Controls.Add(this.rdbNo);
            this.gbCOI.Controls.Add(this.rdbYes);
            this.gbCOI.ForeColor = System.Drawing.Color.White;
            this.gbCOI.Location = new System.Drawing.Point(3, 91);
            this.gbCOI.Name = "gbCOI";
            this.gbCOI.Size = new System.Drawing.Size(145, 70);
            this.gbCOI.TabIndex = 62;
            this.gbCOI.TabStop = false;
            this.gbCOI.Text = "Print with COI?";
            // 
            // chkPrintStamp
            // 
            this.chkPrintStamp.AutoSize = true;
            this.chkPrintStamp.Location = new System.Drawing.Point(6, 43);
            this.chkPrintStamp.Name = "chkPrintStamp";
            this.chkPrintStamp.Size = new System.Drawing.Size(115, 21);
            this.chkPrintStamp.TabIndex = 2;
            this.chkPrintStamp.Text = "Sign && Stamp";
            this.chkPrintStamp.UseVisualStyleBackColor = true;
            // 
            // rdbNo
            // 
            this.rdbNo.AutoSize = true;
            this.rdbNo.Location = new System.Drawing.Point(76, 19);
            this.rdbNo.Name = "rdbNo";
            this.rdbNo.Size = new System.Drawing.Size(47, 21);
            this.rdbNo.TabIndex = 1;
            this.rdbNo.TabStop = true;
            this.rdbNo.Text = "No";
            this.rdbNo.UseVisualStyleBackColor = true;
            this.rdbNo.CheckedChanged += new System.EventHandler(this.rdbNo_CheckedChanged);
            // 
            // rdbYes
            // 
            this.rdbYes.AutoSize = true;
            this.rdbYes.Location = new System.Drawing.Point(6, 19);
            this.rdbYes.Name = "rdbYes";
            this.rdbYes.Size = new System.Drawing.Size(53, 21);
            this.rdbYes.TabIndex = 0;
            this.rdbYes.TabStop = true;
            this.rdbYes.Text = "Yes";
            this.rdbYes.UseVisualStyleBackColor = true;
            this.rdbYes.CheckedChanged += new System.EventHandler(this.rdbYes_CheckedChanged);
            // 
            // lbCOI
            // 
            this.lbCOI.AutoSize = true;
            this.lbCOI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCOI.ForeColor = System.Drawing.Color.Red;
            this.lbCOI.Location = new System.Drawing.Point(8, 162);
            this.lbCOI.Name = "lbCOI";
            this.lbCOI.Size = new System.Drawing.Size(320, 24);
            this.lbCOI.TabIndex = 63;
            this.lbCOI.Text = "*Note: COI is available for PE&&M only";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(844, 52);
            this.btnClear.Margin = new System.Windows.Forms.Padding(5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 37);
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
            this.btnPrint.Location = new System.Drawing.Point(599, 52);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 37);
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
            this.bnSearch.Location = new System.Drawing.Point(684, 52);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(5);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(149, 37);
            this.bnSearch.TabIndex = 20;
            this.bnSearch.Text = "Create Bank";
            this.bnSearch.UseVisualStyleBackColor = false;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // frmPrintInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1299, 753);
            this.Controls.Add(this.lbCOI);
            this.Controls.Add(this.gbCOI);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "frmPrintInvoice";
            this.Text = "frmPrintInvoice";
            this.Load += new System.EventHandler(this.frmPrintInvoice_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoIn)).EndInit();
            this.gbCOI.ResumeLayout(false);
            this.gbCOI.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbCOI;
        private System.Windows.Forms.RadioButton rdbNo;
        private System.Windows.Forms.RadioButton rdbYes;
        private System.Windows.Forms.Label lbCOI;
        private System.Windows.Forms.CheckBox chkPrintStamp;
    }
}