namespace Testing.Forms
{
    partial class frmGenerateSettlementLetterNotice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGenerateSettlementLetterNotice));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtRiskName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAdditionalText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtNonPayableAmt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLastPayment = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPreviuosPayment = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPayableAmt = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClaimAmt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInsured = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPolNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtClaimNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtAdmissionDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.btnGenerate = new Testing.cus_button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 349);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtRiskName);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtAdditionalText);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.txtNonPayableAmt);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtLastPayment);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPreviuosPayment);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtPayableAmt);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtClaimAmt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtInsured);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPolNo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtClaimNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtAdmissionDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1004, 349);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(452, 91);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(534, 62);
            this.txtAddress.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(362, 94);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 15);
            this.label12.TabIndex = 23;
            this.label12.Text = "Address:";
            // 
            // txtRiskName
            // 
            this.txtRiskName.Location = new System.Drawing.Point(129, 91);
            this.txtRiskName.Name = "txtRiskName";
            this.txtRiskName.Size = new System.Drawing.Size(209, 23);
            this.txtRiskName.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 15);
            this.label11.TabIndex = 21;
            this.label11.Text = "Risk Name:";
            // 
            // txtAdditionalText
            // 
            this.txtAdditionalText.Location = new System.Drawing.Point(129, 217);
            this.txtAdditionalText.Multiline = true;
            this.txtAdditionalText.Name = "txtAdditionalText";
            this.txtAdditionalText.Size = new System.Drawing.Size(857, 83);
            this.txtAdditionalText.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(94, 15);
            this.label10.TabIndex = 19;
            this.label10.Text = "Additional Text:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnGenerate);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 315);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(998, 31);
            this.panel3.TabIndex = 18;
            // 
            // txtNonPayableAmt
            // 
            this.txtNonPayableAmt.Location = new System.Drawing.Point(788, 188);
            this.txtNonPayableAmt.Name = "txtNonPayableAmt";
            this.txtNonPayableAmt.Size = new System.Drawing.Size(198, 23);
            this.txtNonPayableAmt.TabIndex = 8;
            this.txtNonPayableAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaimAmt_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(676, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "Non-Payable Amt:";
            // 
            // txtLastPayment
            // 
            this.txtLastPayment.Location = new System.Drawing.Point(452, 188);
            this.txtLastPayment.Name = "txtLastPayment";
            this.txtLastPayment.Size = new System.Drawing.Size(202, 23);
            this.txtLastPayment.TabIndex = 7;
            this.txtLastPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaimAmt_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(362, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Last Payment:";
            // 
            // txtPreviuosPayment
            // 
            this.txtPreviuosPayment.Location = new System.Drawing.Point(129, 188);
            this.txtPreviuosPayment.Name = "txtPreviuosPayment";
            this.txtPreviuosPayment.Size = new System.Drawing.Size(209, 23);
            this.txtPreviuosPayment.TabIndex = 6;
            this.txtPreviuosPayment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaimAmt_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Previous Payment:";
            // 
            // txtPayableAmt
            // 
            this.txtPayableAmt.Location = new System.Drawing.Point(788, 160);
            this.txtPayableAmt.Name = "txtPayableAmt";
            this.txtPayableAmt.Size = new System.Drawing.Size(198, 23);
            this.txtPayableAmt.TabIndex = 5;
            this.txtPayableAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaimAmt_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(676, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Payable Amt:";
            // 
            // txtClaimAmt
            // 
            this.txtClaimAmt.Location = new System.Drawing.Point(452, 159);
            this.txtClaimAmt.Name = "txtClaimAmt";
            this.txtClaimAmt.Size = new System.Drawing.Size(202, 23);
            this.txtClaimAmt.TabIndex = 4;
            this.txtClaimAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaimAmt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(362, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Claim Amt:";
            // 
            // txtInsured
            // 
            this.txtInsured.Location = new System.Drawing.Point(788, 28);
            this.txtInsured.Multiline = true;
            this.txtInsured.Name = "txtInsured";
            this.txtInsured.Size = new System.Drawing.Size(198, 57);
            this.txtInsured.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(676, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Insured Name:";
            // 
            // txtPolNo
            // 
            this.txtPolNo.Enabled = false;
            this.txtPolNo.Location = new System.Drawing.Point(452, 28);
            this.txtPolNo.Name = "txtPolNo";
            this.txtPolNo.Size = new System.Drawing.Size(202, 23);
            this.txtPolNo.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Policy No:";
            // 
            // txtClaimNo
            // 
            this.txtClaimNo.Enabled = false;
            this.txtClaimNo.Location = new System.Drawing.Point(129, 28);
            this.txtClaimNo.Name = "txtClaimNo";
            this.txtClaimNo.Size = new System.Drawing.Size(209, 23);
            this.txtClaimNo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Claim No:";
            // 
            // dtAdmissionDate
            // 
            this.dtAdmissionDate.CustomFormat = "dd-MMM-yyyy";
            this.dtAdmissionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAdmissionDate.Location = new System.Drawing.Point(129, 159);
            this.dtAdmissionDate.Name = "dtAdmissionDate";
            this.dtAdmissionDate.Size = new System.Drawing.Size(209, 23);
            this.dtAdmissionDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Admission Date:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.crViewer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 349);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 402);
            this.panel2.TabIndex = 1;
            // 
            // crViewer
            // 
            this.crViewer.ActiveViewIndex = -1;
            this.crViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.crViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crViewer.Location = new System.Drawing.Point(0, 0);
            this.crViewer.Name = "crViewer";
            this.crViewer.Size = new System.Drawing.Size(1004, 402);
            this.crViewer.TabIndex = 0;
            this.crViewer.ToolPanelWidth = 233;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnGenerate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerate.FlatAppearance.BorderSize = 2;
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(0, 0);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(998, 31);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // frmGenerateSettlementLetterNotice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1004, 751);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmGenerateSettlementLetterNotice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settlement Letter Notice";
            this.Load += new System.EventHandler(this.frmGenerateSettlementLetterNotice_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtInsured;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPolNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtClaimNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtAdmissionDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClaimAmt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPayableAmt;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNonPayableAmt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLastPayment;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPreviuosPayment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private cus_button btnGenerate;
        private System.Windows.Forms.TextBox txtAdditionalText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtRiskName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label12;
    }
}