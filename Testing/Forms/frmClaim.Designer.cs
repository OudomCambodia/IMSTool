namespace Testing.Forms
{
    partial class frmClaim
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
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbInsured = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbTotNumber = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.tbClaimNo = new System.Windows.Forms.TextBox();
            this.tbPolicyNo = new System.Windows.Forms.TextBox();
            this.lbClaim = new System.Windows.Forms.Label();
            this.lbPolicyNo = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pnFind = new System.Windows.Forms.Panel();
            this.btnX = new System.Windows.Forms.Button();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpIntFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpIntTo = new System.Windows.Forms.DateTimePicker();
            this.rbInt = new System.Windows.Forms.RadioButton();
            this.rbLoss = new System.Windows.Forms.RadioButton();
            this.btnExcel = new Testing.cus_button();
            this.btnPrint = new Testing.cus_button();
            this.btnClear = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.btnLookInExcel = new Testing.cus_button();
            this.tbRiskName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.pnFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(408, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 32);
            this.label1.TabIndex = 8;
            this.label1.Text = "Claim Search";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(459, 73);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(161, 22);
            this.dtpTo.TabIndex = 5;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(165, 73);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(161, 22);
            this.dtpFrom.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(360, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "Loss Date To:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(52, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "Loss Date From:";
            // 
            // tbInsured
            // 
            this.tbInsured.Location = new System.Drawing.Point(378, 41);
            this.tbInsured.Margin = new System.Windows.Forms.Padding(4);
            this.tbInsured.MaxLength = 70;
            this.tbInsured.Name = "tbInsured";
            this.tbInsured.Size = new System.Drawing.Size(161, 22);
            this.tbInsured.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(262, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Customer Name:";
            // 
            // lbTotNumber
            // 
            this.lbTotNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotNumber.AutoSize = true;
            this.lbTotNumber.BackColor = System.Drawing.Color.Transparent;
            this.lbTotNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotNumber.ForeColor = System.Drawing.Color.White;
            this.lbTotNumber.Location = new System.Drawing.Point(901, 620);
            this.lbTotNumber.Name = "lbTotNumber";
            this.lbTotNumber.Size = new System.Drawing.Size(17, 18);
            this.lbTotNumber.TabIndex = 26;
            this.lbTotNumber.Text = "0";
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.BackColor = System.Drawing.Color.Transparent;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.White;
            this.lbTotal.Location = new System.Drawing.Point(846, 620);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(51, 18);
            this.lbTotal.TabIndex = 25;
            this.lbTotal.Text = "Total:";
            // 
            // tbClaimNo
            // 
            this.tbClaimNo.Location = new System.Drawing.Point(716, 73);
            this.tbClaimNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbClaimNo.MaxLength = 20;
            this.tbClaimNo.Name = "tbClaimNo";
            this.tbClaimNo.Size = new System.Drawing.Size(189, 22);
            this.tbClaimNo.TabIndex = 3;
            // 
            // tbPolicyNo
            // 
            this.tbPolicyNo.Location = new System.Drawing.Point(93, 41);
            this.tbPolicyNo.Margin = new System.Windows.Forms.Padding(4);
            this.tbPolicyNo.MaxLength = 20;
            this.tbPolicyNo.Name = "tbPolicyNo";
            this.tbPolicyNo.Size = new System.Drawing.Size(161, 22);
            this.tbPolicyNo.TabIndex = 1;
            this.tbPolicyNo.Leave += new System.EventHandler(this.tbPolicyNo_Leave);
            // 
            // lbClaim
            // 
            this.lbClaim.AutoSize = true;
            this.lbClaim.BackColor = System.Drawing.Color.Transparent;
            this.lbClaim.ForeColor = System.Drawing.Color.White;
            this.lbClaim.Location = new System.Drawing.Point(641, 78);
            this.lbClaim.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbClaim.Name = "lbClaim";
            this.lbClaim.Size = new System.Drawing.Size(66, 16);
            this.lbClaim.TabIndex = 20;
            this.lbClaim.Text = "Claim No:";
            // 
            // lbPolicyNo
            // 
            this.lbPolicyNo.AutoSize = true;
            this.lbPolicyNo.BackColor = System.Drawing.Color.Transparent;
            this.lbPolicyNo.ForeColor = System.Drawing.Color.White;
            this.lbPolicyNo.Location = new System.Drawing.Point(16, 44);
            this.lbPolicyNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPolicyNo.Name = "lbPolicyNo";
            this.lbPolicyNo.Size = new System.Drawing.Size(69, 16);
            this.lbPolicyNo.TabIndex = 18;
            this.lbPolicyNo.Text = "Policy No:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 141);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(964, 472);
            this.dataGridView1.TabIndex = 24;
            this.dataGridView1.DataSourceChanged += new System.EventHandler(this.dataGridView1_DataSourceChanged);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.Leave += new System.EventHandler(this.dataGridView1_Leave);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pnFind
            // 
            this.pnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnFind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.pnFind.Controls.Add(this.btnX);
            this.pnFind.Controls.Add(this.tbFind);
            this.pnFind.Location = new System.Drawing.Point(13, 612);
            this.pnFind.Name = "pnFind";
            this.pnFind.Size = new System.Drawing.Size(213, 33);
            this.pnFind.TabIndex = 33;
            this.pnFind.Visible = false;
            this.pnFind.Leave += new System.EventHandler(this.pnFind_Leave);
            // 
            // btnX
            // 
            this.btnX.FlatAppearance.BorderSize = 2;
            this.btnX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnX.ForeColor = System.Drawing.Color.White;
            this.btnX.Location = new System.Drawing.Point(183, 4);
            this.btnX.Name = "btnX";
            this.btnX.Size = new System.Drawing.Size(25, 25);
            this.btnX.TabIndex = 1;
            this.btnX.Text = "X";
            this.btnX.UseVisualStyleBackColor = true;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(6, 5);
            this.tbFind.Name = "tbFind";
            this.tbFind.Size = new System.Drawing.Size(172, 22);
            this.tbFind.TabIndex = 0;
            this.tbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyDown);
            this.tbFind.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbFind_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(35, 111);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "Intimate Date From:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(342, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 16);
            this.label6.TabIndex = 35;
            this.label6.Text = "Intimate Date To:";
            // 
            // dtpIntFrom
            // 
            this.dtpIntFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIntFrom.Location = new System.Drawing.Point(165, 106);
            this.dtpIntFrom.Name = "dtpIntFrom";
            this.dtpIntFrom.Size = new System.Drawing.Size(161, 22);
            this.dtpIntFrom.TabIndex = 6;
            // 
            // dtpIntTo
            // 
            this.dtpIntTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIntTo.Location = new System.Drawing.Point(459, 106);
            this.dtpIntTo.Name = "dtpIntTo";
            this.dtpIntTo.Size = new System.Drawing.Size(161, 22);
            this.dtpIntTo.TabIndex = 7;
            // 
            // rbInt
            // 
            this.rbInt.AutoSize = true;
            this.rbInt.Location = new System.Drawing.Point(19, 112);
            this.rbInt.Name = "rbInt";
            this.rbInt.Size = new System.Drawing.Size(14, 13);
            this.rbInt.TabIndex = 36;
            this.rbInt.UseVisualStyleBackColor = true;
            this.rbInt.CheckedChanged += new System.EventHandler(this.rbInt_CheckedChanged);
            // 
            // rbLoss
            // 
            this.rbLoss.AutoSize = true;
            this.rbLoss.Checked = true;
            this.rbLoss.Location = new System.Drawing.Point(19, 80);
            this.rbLoss.Name = "rbLoss";
            this.rbLoss.Size = new System.Drawing.Size(14, 13);
            this.rbLoss.TabIndex = 37;
            this.rbLoss.TabStop = true;
            this.rbLoss.UseVisualStyleBackColor = true;
            this.rbLoss.CheckedChanged += new System.EventHandler(this.rbInt_CheckedChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(721, 103);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(61, 30);
            this.btnExcel.TabIndex = 9;
            this.btnExcel.Text = "Excel";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(790, 103);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(50, 30);
            this.btnPrint.TabIndex = 10;
            this.btnPrint.Text = "PDF";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(846, 103);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(59, 30);
            this.btnClear.TabIndex = 11;
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
            this.bnSearch.Location = new System.Drawing.Point(644, 103);
            this.bnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(69, 30);
            this.bnSearch.TabIndex = 8;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = true;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // btnLookInExcel
            // 
            this.btnLookInExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnLookInExcel.Enabled = false;
            this.btnLookInExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLookInExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLookInExcel.ForeColor = System.Drawing.Color.White;
            this.btnLookInExcel.Location = new System.Drawing.Point(860, 13);
            this.btnLookInExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnLookInExcel.Name = "btnLookInExcel";
            this.btnLookInExcel.Size = new System.Drawing.Size(115, 30);
            this.btnLookInExcel.TabIndex = 32;
            this.btnLookInExcel.Text = "Look in Excel";
            this.btnLookInExcel.UseVisualStyleBackColor = true;
            this.btnLookInExcel.Visible = false;
            this.btnLookInExcel.Click += new System.EventHandler(this.btnLookInExcel_Click);
            // 
            // tbRiskName
            // 
            this.tbRiskName.Location = new System.Drawing.Point(633, 41);
            this.tbRiskName.Margin = new System.Windows.Forms.Padding(4);
            this.tbRiskName.MaxLength = 70;
            this.tbRiskName.Name = "tbRiskName";
            this.tbRiskName.Size = new System.Drawing.Size(272, 22);
            this.tbRiskName.TabIndex = 38;
            this.tbRiskName.MouseHover += new System.EventHandler(this.tbRiskName_MouseHover);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(547, 44);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "Risk Name:";
            // 
            // frmClaim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(990, 650);
            this.Controls.Add(this.tbRiskName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.rbLoss);
            this.Controls.Add(this.rbInt);
            this.Controls.Add(this.dtpIntTo);
            this.Controls.Add(this.dtpIntFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnFind);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbInsured);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTotNumber);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.tbClaimNo);
            this.Controls.Add(this.tbPolicyNo);
            this.Controls.Add(this.lbClaim);
            this.Controls.Add(this.lbPolicyNo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmClaim";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmClaim";
            this.Load += new System.EventHandler(this.frmClaim_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmClaim_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.pnFind.ResumeLayout(false);
            this.pnFind.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbInsured;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTotNumber;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.TextBox tbClaimNo;
        private System.Windows.Forms.TextBox tbPolicyNo;
        private System.Windows.Forms.Label lbClaim;
        private System.Windows.Forms.Label lbPolicyNo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel pnFind;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Button btnX;
        private cus_button btnClear;
        private cus_button bnSearch;
        private cus_button btnPrint;
        private cus_button btnExcel;
        private cus_button btnLookInExcel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpIntFrom;
        private System.Windows.Forms.DateTimePicker dtpIntTo;
        private System.Windows.Forms.RadioButton rbInt;
        private System.Windows.Forms.RadioButton rbLoss;
        private System.Windows.Forms.TextBox tbRiskName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}