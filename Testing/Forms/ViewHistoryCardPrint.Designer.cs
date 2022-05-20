namespace Testing.Forms
{
    partial class ViewHistoryCardPrint
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewHistoryCardPrint));
            this.dtCardHistory = new System.Windows.Forms.DataGridView();
            this.gOption = new System.Windows.Forms.GroupBox();
            this.rdHNS = new System.Windows.Forms.RadioButton();
            this.rdGPA = new System.Windows.Forms.RadioButton();
            this.rdBHP = new System.Windows.Forms.RadioButton();
            this.rdVPC = new System.Windows.Forms.RadioButton();
            this.rdVCM = new System.Windows.Forms.RadioButton();
            this.rdCYC = new System.Windows.Forms.RadioButton();
            this.dtMain = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPrintId = new System.Windows.Forms.Label();
            this.lblPrintDetail = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnCancel = new Testing.cus_button();
            this.btnAlready = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dtCardHistory)).BeginInit();
            this.gOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtMain)).BeginInit();
            this.SuspendLayout();
            // 
            // dtCardHistory
            // 
            this.dtCardHistory.AllowUserToAddRows = false;
            this.dtCardHistory.AllowUserToDeleteRows = false;
            this.dtCardHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtCardHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtCardHistory.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dtCardHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtCardHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtCardHistory.Location = new System.Drawing.Point(12, 283);
            this.dtCardHistory.MultiSelect = false;
            this.dtCardHistory.Name = "dtCardHistory";
            this.dtCardHistory.ReadOnly = true;
            this.dtCardHistory.RowTemplate.Height = 25;
            this.dtCardHistory.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtCardHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtCardHistory.Size = new System.Drawing.Size(988, 280);
            this.dtCardHistory.TabIndex = 0;
            this.dtCardHistory.DataSourceChanged += new System.EventHandler(this.dtCardHistory_DataSourceChanged);
            this.dtCardHistory.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtCardHistory_DataBindingComplete);
            // 
            // gOption
            // 
            this.gOption.Controls.Add(this.rdHNS);
            this.gOption.Controls.Add(this.rdGPA);
            this.gOption.Controls.Add(this.rdBHP);
            this.gOption.Controls.Add(this.rdVPC);
            this.gOption.Controls.Add(this.rdVCM);
            this.gOption.Controls.Add(this.rdCYC);
            this.gOption.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.gOption.Location = new System.Drawing.Point(13, 11);
            this.gOption.Name = "gOption";
            this.gOption.Size = new System.Drawing.Size(318, 63);
            this.gOption.TabIndex = 1;
            this.gOption.TabStop = false;
            this.gOption.Text = "Product";
            // 
            // rdHNS
            // 
            this.rdHNS.AutoSize = true;
            this.rdHNS.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rdHNS.Location = new System.Drawing.Point(17, 38);
            this.rdHNS.Name = "rdHNS";
            this.rdHNS.Size = new System.Drawing.Size(48, 17);
            this.rdHNS.TabIndex = 5;
            this.rdHNS.TabStop = true;
            this.rdHNS.Text = "HNS";
            this.rdHNS.UseVisualStyleBackColor = true;
            this.rdHNS.CheckedChanged += new System.EventHandler(this.rdCYC_CheckedChanged);
            // 
            // rdGPA
            // 
            this.rdGPA.AutoSize = true;
            this.rdGPA.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rdGPA.Location = new System.Drawing.Point(108, 38);
            this.rdGPA.Name = "rdGPA";
            this.rdGPA.Size = new System.Drawing.Size(47, 17);
            this.rdGPA.TabIndex = 4;
            this.rdGPA.TabStop = true;
            this.rdGPA.Text = "GPA";
            this.rdGPA.UseVisualStyleBackColor = true;
            this.rdGPA.CheckedChanged += new System.EventHandler(this.rdCYC_CheckedChanged);
            // 
            // rdBHP
            // 
            this.rdBHP.AutoSize = true;
            this.rdBHP.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rdBHP.Location = new System.Drawing.Point(211, 38);
            this.rdBHP.Name = "rdBHP";
            this.rdBHP.Size = new System.Drawing.Size(47, 17);
            this.rdBHP.TabIndex = 3;
            this.rdBHP.TabStop = true;
            this.rdBHP.Text = "BHP";
            this.rdBHP.UseVisualStyleBackColor = true;
            this.rdBHP.CheckedChanged += new System.EventHandler(this.rdCYC_CheckedChanged);
            // 
            // rdVPC
            // 
            this.rdVPC.AutoSize = true;
            this.rdVPC.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rdVPC.Location = new System.Drawing.Point(211, 16);
            this.rdVPC.Name = "rdVPC";
            this.rdVPC.Size = new System.Drawing.Size(46, 17);
            this.rdVPC.TabIndex = 2;
            this.rdVPC.TabStop = true;
            this.rdVPC.Text = "VPC";
            this.rdVPC.UseVisualStyleBackColor = true;
            this.rdVPC.CheckedChanged += new System.EventHandler(this.rdCYC_CheckedChanged);
            // 
            // rdVCM
            // 
            this.rdVCM.AutoSize = true;
            this.rdVCM.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rdVCM.Location = new System.Drawing.Point(108, 16);
            this.rdVCM.Name = "rdVCM";
            this.rdVCM.Size = new System.Drawing.Size(48, 17);
            this.rdVCM.TabIndex = 1;
            this.rdVCM.TabStop = true;
            this.rdVCM.Text = "VCM";
            this.rdVCM.UseVisualStyleBackColor = true;
            this.rdVCM.CheckedChanged += new System.EventHandler(this.rdCYC_CheckedChanged);
            // 
            // rdCYC
            // 
            this.rdCYC.AutoSize = true;
            this.rdCYC.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.rdCYC.Location = new System.Drawing.Point(17, 16);
            this.rdCYC.Name = "rdCYC";
            this.rdCYC.Size = new System.Drawing.Size(46, 17);
            this.rdCYC.TabIndex = 0;
            this.rdCYC.TabStop = true;
            this.rdCYC.Text = "CYC";
            this.rdCYC.UseVisualStyleBackColor = true;
            this.rdCYC.CheckedChanged += new System.EventHandler(this.rdCYC_CheckedChanged);
            // 
            // dtMain
            // 
            this.dtMain.AllowUserToAddRows = false;
            this.dtMain.AllowUserToDeleteRows = false;
            this.dtMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtMain.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dtMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtMain.Location = new System.Drawing.Point(12, 98);
            this.dtMain.MultiSelect = false;
            this.dtMain.Name = "dtMain";
            this.dtMain.ReadOnly = true;
            this.dtMain.RowTemplate.Height = 25;
            this.dtMain.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dtMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtMain.Size = new System.Drawing.Size(988, 142);
            this.dtMain.TabIndex = 2;
            this.dtMain.DataSourceChanged += new System.EventHandler(this.dtMain_DataSourceChanged);
            this.dtMain.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dtMain_DataBindingComplete);
            this.dtMain.SelectionChanged += new System.EventHandler(this.dtMain_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(13, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Print ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(13, 267);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Print Detail";
            // 
            // lblPrintId
            // 
            this.lblPrintId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrintId.AutoSize = true;
            this.lblPrintId.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPrintId.Location = new System.Drawing.Point(911, 243);
            this.lblPrintId.Name = "lblPrintId";
            this.lblPrintId.Size = new System.Drawing.Size(0, 13);
            this.lblPrintId.TabIndex = 5;
            // 
            // lblPrintDetail
            // 
            this.lblPrintDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrintDetail.AutoSize = true;
            this.lblPrintDetail.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblPrintDetail.Location = new System.Drawing.Point(911, 566);
            this.lblPrintDetail.Name = "lblPrintDetail";
            this.lblPrintDetail.Size = new System.Drawing.Size(0, 13);
            this.lblPrintDetail.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(414, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(213, 32);
            this.label6.TabIndex = 36;
            this.label6.Text = "Card Print History";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(735, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 63;
            this.label4.Text = "Go To";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(778, 75);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 20);
            this.textBox1.TabIndex = 62;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(735, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 65;
            this.label3.Text = "Go To";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(778, 260);
            this.textBox2.MaxLength = 100;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(222, 20);
            this.textBox2.TabIndex = 64;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(596, 61);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(132, 30);
            this.btnCancel.TabIndex = 66;
            this.btnCancel.Text = "Cancel Pending";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAlready
            // 
            this.btnAlready.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAlready.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnAlready.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlready.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlready.ForeColor = System.Drawing.Color.White;
            this.btnAlready.Location = new System.Drawing.Point(447, 61);
            this.btnAlready.Margin = new System.Windows.Forms.Padding(4);
            this.btnAlready.Name = "btnAlready";
            this.btnAlready.Size = new System.Drawing.Size(132, 30);
            this.btnAlready.TabIndex = 67;
            this.btnAlready.Text = "Already Printed";
            this.btnAlready.UseVisualStyleBackColor = true;
            this.btnAlready.Click += new System.EventHandler(this.btnAlready_Click);
            // 
            // ViewHistoryCardPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1012, 591);
            this.Controls.Add(this.btnAlready);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblPrintDetail);
            this.Controls.Add(this.lblPrintId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtMain);
            this.Controls.Add(this.gOption);
            this.Controls.Add(this.dtCardHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewHistoryCardPrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Card Print History";
            this.Load += new System.EventHandler(this.ViewHistoryCardPrint_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtCardHistory)).EndInit();
            this.gOption.ResumeLayout(false);
            this.gOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtCardHistory;
        private System.Windows.Forms.GroupBox gOption;
        private System.Windows.Forms.RadioButton rdHNS;
        private System.Windows.Forms.RadioButton rdGPA;
        private System.Windows.Forms.RadioButton rdBHP;
        private System.Windows.Forms.RadioButton rdVPC;
        private System.Windows.Forms.RadioButton rdVCM;
        private System.Windows.Forms.RadioButton rdCYC;
        private System.Windows.Forms.DataGridView dtMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPrintId;
        private System.Windows.Forms.Label lblPrintDetail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private cus_button btnCancel;
        private cus_button btnAlready;
    }
}