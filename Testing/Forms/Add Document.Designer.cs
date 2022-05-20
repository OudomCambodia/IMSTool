namespace Testing.Forms
{
    partial class frmDocumentDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentDetail));
            this.lbTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDocCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbProduct = new System.Windows.Forms.ComboBox();
            this.tbDocType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbDocContent = new System.Windows.Forms.RichTextBox();
            this.bnAdd = new Testing.cus_button();
            this.bnClose = new Testing.cus_button();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.Transparent;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(711, 46);
            this.lbTitle.TabIndex = 12;
            this.lbTitle.Text = "Add New Document";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(270, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 25;
            this.label1.Text = "Product:";
            // 
            // tbDocCode
            // 
            this.tbDocCode.Enabled = false;
            this.tbDocCode.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.tbDocCode.Location = new System.Drawing.Point(122, 49);
            this.tbDocCode.Name = "tbDocCode";
            this.tbDocCode.ReadOnly = true;
            this.tbDocCode.Size = new System.Drawing.Size(96, 23);
            this.tbDocCode.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(21, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 23;
            this.label3.Text = "Document Code:";
            // 
            // cbProduct
            // 
            this.cbProduct.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.cbProduct.FormattingEnabled = true;
            this.cbProduct.Location = new System.Drawing.Point(328, 50);
            this.cbProduct.Name = "cbProduct";
            this.cbProduct.Size = new System.Drawing.Size(90, 23);
            this.cbProduct.TabIndex = 22;
            // 
            // tbDocType
            // 
            this.tbDocType.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.tbDocType.Location = new System.Drawing.Point(122, 78);
            this.tbDocType.Name = "tbDocType";
            this.tbDocType.Size = new System.Drawing.Size(568, 23);
            this.tbDocType.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 26;
            this.label2.Text = "Document Type:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(21, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "Document Content:";
            // 
            // rtbDocContent
            // 
            this.rtbDocContent.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.rtbDocContent.Location = new System.Drawing.Point(24, 128);
            this.rtbDocContent.Name = "rtbDocContent";
            this.rtbDocContent.Size = new System.Drawing.Size(666, 159);
            this.rtbDocContent.TabIndex = 29;
            this.rtbDocContent.Text = "";
            // 
            // bnAdd
            // 
            this.bnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnAdd.FlatAppearance.BorderSize = 2;
            this.bnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnAdd.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnAdd.ForeColor = System.Drawing.Color.White;
            this.bnAdd.Location = new System.Drawing.Point(482, 301);
            this.bnAdd.Name = "bnAdd";
            this.bnAdd.Size = new System.Drawing.Size(95, 30);
            this.bnAdd.TabIndex = 30;
            this.bnAdd.Text = "Add";
            this.bnAdd.UseVisualStyleBackColor = false;
            this.bnAdd.Click += new System.EventHandler(this.bnAdd_Click);
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClose.FlatAppearance.BorderSize = 2;
            this.bnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClose.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnClose.ForeColor = System.Drawing.Color.White;
            this.bnClose.Location = new System.Drawing.Point(595, 301);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(95, 30);
            this.bnClose.TabIndex = 31;
            this.bnClose.Text = "Close";
            this.bnClose.UseVisualStyleBackColor = false;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // frmDocumentDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(711, 342);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.bnAdd);
            this.Controls.Add(this.rtbDocContent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbDocType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDocCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbProduct);
            this.Controls.Add(this.lbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDocumentDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Document";
            this.Load += new System.EventHandler(this.Add_Document_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDocCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbProduct;
        private System.Windows.Forms.TextBox tbDocType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtbDocContent;
        private cus_button bnAdd;
        private cus_button bnClose;
    }
}