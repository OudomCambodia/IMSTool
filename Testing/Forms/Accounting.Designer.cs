namespace Testing.Forms
{
    partial class Accounting
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
            this.label2 = new System.Windows.Forms.Label();
            this.lbTotRec = new System.Windows.Forms.Label();
            this.lbTotAmo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpOst = new System.Windows.Forms.TabPage();
            this.dgvOst = new System.Windows.Forms.DataGridView();
            this.tpCol = new System.Windows.Forms.TabPage();
            this.dgvCol = new System.Windows.Forms.DataGridView();
            this.lbAmtCol = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbRecCol = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tpRes = new System.Windows.Forms.TabPage();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.lbResAmt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbTotRes = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tpOst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOst)).BeginInit();
            this.tpCol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCol)).BeginInit();
            this.tpRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(432, 546);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Total Records:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(622, 546);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Amount:";
            // 
            // lbTotRec
            // 
            this.lbTotRec.AutoSize = true;
            this.lbTotRec.Location = new System.Drawing.Point(535, 546);
            this.lbTotRec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotRec.Name = "lbTotRec";
            this.lbTotRec.Size = new System.Drawing.Size(18, 19);
            this.lbTotRec.TabIndex = 3;
            this.lbTotRec.Text = "0";
            this.lbTotRec.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTotAmo
            // 
            this.lbTotAmo.AutoSize = true;
            this.lbTotAmo.Location = new System.Drawing.Point(723, 546);
            this.lbTotAmo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotAmo.Name = "lbTotAmo";
            this.lbTotAmo.Size = new System.Drawing.Size(18, 19);
            this.lbTotAmo.TabIndex = 4;
            this.lbTotAmo.Text = "0";
            this.lbTotAmo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpOst);
            this.tabControl1.Controls.Add(this.tpCol);
            this.tabControl1.Controls.Add(this.tpRes);
            this.tabControl1.Location = new System.Drawing.Point(2, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(877, 603);
            this.tabControl1.TabIndex = 5;
            // 
            // tpOst
            // 
            this.tpOst.Controls.Add(this.dgvOst);
            this.tpOst.Controls.Add(this.lbTotAmo);
            this.tpOst.Controls.Add(this.label2);
            this.tpOst.Controls.Add(this.lbTotRec);
            this.tpOst.Controls.Add(this.label1);
            this.tpOst.Location = new System.Drawing.Point(4, 28);
            this.tpOst.Name = "tpOst";
            this.tpOst.Padding = new System.Windows.Forms.Padding(3);
            this.tpOst.Size = new System.Drawing.Size(869, 571);
            this.tpOst.TabIndex = 0;
            this.tpOst.Text = "Oustanding";
            this.tpOst.UseVisualStyleBackColor = true;
            // 
            // dgvOst
            // 
            this.dgvOst.AllowUserToAddRows = false;
            this.dgvOst.AllowUserToDeleteRows = false;
            this.dgvOst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOst.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOst.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvOst.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOst.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOst.Location = new System.Drawing.Point(0, 0);
            this.dgvOst.Margin = new System.Windows.Forms.Padding(4);
            this.dgvOst.Name = "dgvOst";
            this.dgvOst.ReadOnly = true;
            this.dgvOst.RowTemplate.Height = 28;
            this.dgvOst.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOst.Size = new System.Drawing.Size(869, 542);
            this.dgvOst.TabIndex = 25;
            this.dgvOst.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvOst_DataBindingComplete);
            // 
            // tpCol
            // 
            this.tpCol.Controls.Add(this.dgvCol);
            this.tpCol.Controls.Add(this.lbAmtCol);
            this.tpCol.Controls.Add(this.label4);
            this.tpCol.Controls.Add(this.lbRecCol);
            this.tpCol.Controls.Add(this.label6);
            this.tpCol.Location = new System.Drawing.Point(4, 28);
            this.tpCol.Name = "tpCol";
            this.tpCol.Padding = new System.Windows.Forms.Padding(3);
            this.tpCol.Size = new System.Drawing.Size(869, 571);
            this.tpCol.TabIndex = 1;
            this.tpCol.Text = "Collection";
            this.tpCol.UseVisualStyleBackColor = true;
            // 
            // dgvCol
            // 
            this.dgvCol.AllowUserToAddRows = false;
            this.dgvCol.AllowUserToDeleteRows = false;
            this.dgvCol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCol.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCol.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvCol.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCol.Location = new System.Drawing.Point(0, 3);
            this.dgvCol.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCol.Name = "dgvCol";
            this.dgvCol.ReadOnly = true;
            this.dgvCol.RowTemplate.Height = 28;
            this.dgvCol.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCol.Size = new System.Drawing.Size(869, 542);
            this.dgvCol.TabIndex = 30;
            this.dgvCol.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvCol_DataBindingComplete);
            // 
            // lbAmtCol
            // 
            this.lbAmtCol.AutoSize = true;
            this.lbAmtCol.Location = new System.Drawing.Point(723, 549);
            this.lbAmtCol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAmtCol.Name = "lbAmtCol";
            this.lbAmtCol.Size = new System.Drawing.Size(18, 19);
            this.lbAmtCol.TabIndex = 29;
            this.lbAmtCol.Text = "0";
            this.lbAmtCol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(622, 549);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 19);
            this.label4.TabIndex = 27;
            this.label4.Text = "Total Amount:";
            // 
            // lbRecCol
            // 
            this.lbRecCol.AutoSize = true;
            this.lbRecCol.Location = new System.Drawing.Point(535, 549);
            this.lbRecCol.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbRecCol.Name = "lbRecCol";
            this.lbRecCol.Size = new System.Drawing.Size(18, 19);
            this.lbRecCol.TabIndex = 28;
            this.lbRecCol.Text = "0";
            this.lbRecCol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(432, 549);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 19);
            this.label6.TabIndex = 26;
            this.label6.Text = "Total Records:";
            // 
            // tpRes
            // 
            this.tpRes.Controls.Add(this.dgvResult);
            this.tpRes.Controls.Add(this.lbResAmt);
            this.tpRes.Controls.Add(this.label5);
            this.tpRes.Controls.Add(this.lbTotRes);
            this.tpRes.Controls.Add(this.label8);
            this.tpRes.Location = new System.Drawing.Point(4, 28);
            this.tpRes.Name = "tpRes";
            this.tpRes.Size = new System.Drawing.Size(869, 571);
            this.tpRes.TabIndex = 2;
            this.tpRes.Text = "Result";
            this.tpRes.UseVisualStyleBackColor = true;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResult.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Location = new System.Drawing.Point(0, 3);
            this.dgvResult.Margin = new System.Windows.Forms.Padding(4);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 28;
            this.dgvResult.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult.Size = new System.Drawing.Size(869, 542);
            this.dgvResult.TabIndex = 30;
            this.dgvResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvResult_DataBindingComplete);
            // 
            // lbResAmt
            // 
            this.lbResAmt.AutoSize = true;
            this.lbResAmt.Location = new System.Drawing.Point(723, 549);
            this.lbResAmt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbResAmt.Name = "lbResAmt";
            this.lbResAmt.Size = new System.Drawing.Size(18, 19);
            this.lbResAmt.TabIndex = 29;
            this.lbResAmt.Text = "0";
            this.lbResAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(622, 549);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 19);
            this.label5.TabIndex = 27;
            this.label5.Text = "Total Amount:";
            // 
            // lbTotRes
            // 
            this.lbTotRes.AutoSize = true;
            this.lbTotRes.Location = new System.Drawing.Point(535, 549);
            this.lbTotRes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotRes.Name = "lbTotRes";
            this.lbTotRes.Size = new System.Drawing.Size(18, 19);
            this.lbTotRes.TabIndex = 28;
            this.lbTotRes.Text = "0";
            this.lbTotRes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(432, 549);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 19);
            this.label8.TabIndex = 26;
            this.label8.Text = "Total Records:";
            // 
            // Accounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 611);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Accounting";
            this.Text = "Accounting";
            this.Load += new System.EventHandler(this.Accounting_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpOst.ResumeLayout(false);
            this.tpOst.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOst)).EndInit();
            this.tpCol.ResumeLayout(false);
            this.tpCol.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCol)).EndInit();
            this.tpRes.ResumeLayout(false);
            this.tpRes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbTotRec;
        private System.Windows.Forms.Label lbTotAmo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpCol;
        private System.Windows.Forms.TabPage tpOst;
        private System.Windows.Forms.DataGridView dgvOst;
        private System.Windows.Forms.DataGridView dgvCol;
        private System.Windows.Forms.Label lbAmtCol;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbRecCol;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tpRes;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Label lbResAmt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbTotRes;
        private System.Windows.Forms.Label label8;
    }
}