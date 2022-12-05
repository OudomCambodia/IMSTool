namespace Testing.Forms
{
    partial class frmAddExclusion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddExclusion));
            this.cbExcluType = new System.Windows.Forms.ComboBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.tbExcluCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbExcluDetail = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnCurrent = new System.Windows.Forms.Button();
            this.bnUnderline = new System.Windows.Forms.Button();
            this.bnItalic = new System.Windows.Forms.Button();
            this.bnBold = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trickBrowser = new System.Windows.Forms.WebBrowser();
            this.bnClose = new Testing.cus_button();
            this.bnAdd = new Testing.cus_button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbExcluType
            // 
            this.cbExcluType.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.cbExcluType.FormattingEnabled = true;
            this.cbExcluType.Location = new System.Drawing.Point(362, 52);
            this.cbExcluType.Name = "cbExcluType";
            this.cbExcluType.Size = new System.Drawing.Size(90, 23);
            this.cbExcluType.TabIndex = 0;
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
            this.lbTitle.Size = new System.Drawing.Size(720, 46);
            this.lbTitle.TabIndex = 11;
            this.lbTitle.Text = "Add New Exclusion";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbExcluCode
            // 
            this.tbExcluCode.Enabled = false;
            this.tbExcluCode.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.tbExcluCode.Location = new System.Drawing.Point(112, 51);
            this.tbExcluCode.Name = "tbExcluCode";
            this.tbExcluCode.ReadOnly = true;
            this.tbExcluCode.Size = new System.Drawing.Size(96, 23);
            this.tbExcluCode.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 15);
            this.label3.TabIndex = 19;
            this.label3.Text = "Exclusion Code:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(261, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 15);
            this.label1.TabIndex = 21;
            this.label1.Text = "Exclusion Type:";
            // 
            // rtbExcluDetail
            // 
            this.rtbExcluDetail.AcceptsTab = true;
            this.rtbExcluDetail.Font = new System.Drawing.Font("Calibri", 11.25F);
            this.rtbExcluDetail.Location = new System.Drawing.Point(12, 124);
            this.rtbExcluDetail.Name = "rtbExcluDetail";
            this.rtbExcluDetail.Size = new System.Drawing.Size(695, 240);
            this.rtbExcluDetail.TabIndex = 22;
            this.rtbExcluDetail.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.bnCurrent);
            this.panel1.Controls.Add(this.bnUnderline);
            this.panel1.Controls.Add(this.bnItalic);
            this.panel1.Controls.Add(this.bnBold);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(695, 37);
            this.panel1.TabIndex = 23;
            // 
            // bnCurrent
            // 
            this.bnCurrent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnCurrent.Location = new System.Drawing.Point(598, 3);
            this.bnCurrent.Name = "bnCurrent";
            this.bnCurrent.Size = new System.Drawing.Size(90, 26);
            this.bnCurrent.TabIndex = 20;
            this.bnCurrent.Text = "Current detail";
            this.bnCurrent.UseVisualStyleBackColor = true;
            this.bnCurrent.Click += new System.EventHandler(this.bnCurrent_Click);
            // 
            // bnUnderline
            // 
            this.bnUnderline.BackgroundImage = global::Testing.Properties.Resources.Underline;
            this.bnUnderline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnUnderline.Location = new System.Drawing.Point(245, 0);
            this.bnUnderline.Name = "bnUnderline";
            this.bnUnderline.Size = new System.Drawing.Size(34, 34);
            this.bnUnderline.TabIndex = 19;
            this.bnUnderline.UseVisualStyleBackColor = true;
            this.bnUnderline.Click += new System.EventHandler(this.bnUnderline_Click);
            // 
            // bnItalic
            // 
            this.bnItalic.BackgroundImage = global::Testing.Properties.Resources.Italic;
            this.bnItalic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnItalic.Location = new System.Drawing.Point(205, 0);
            this.bnItalic.Name = "bnItalic";
            this.bnItalic.Size = new System.Drawing.Size(34, 34);
            this.bnItalic.TabIndex = 18;
            this.bnItalic.UseVisualStyleBackColor = true;
            this.bnItalic.Click += new System.EventHandler(this.bnItalic_Click);
            // 
            // bnBold
            // 
            this.bnBold.BackgroundImage = global::Testing.Properties.Resources.Bold;
            this.bnBold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnBold.Location = new System.Drawing.Point(165, 0);
            this.bnBold.Name = "bnBold";
            this.bnBold.Size = new System.Drawing.Size(34, 34);
            this.bnBold.TabIndex = 17;
            this.bnBold.UseVisualStyleBackColor = true;
            this.bnBold.Click += new System.EventHandler(this.bnBold_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 15F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Exclusion Detail";
            // 
            // trickBrowser
            // 
            this.trickBrowser.Location = new System.Drawing.Point(26, 374);
            this.trickBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.trickBrowser.Name = "trickBrowser";
            this.trickBrowser.Size = new System.Drawing.Size(36, 30);
            this.trickBrowser.TabIndex = 26;
            this.trickBrowser.Visible = false;
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClose.FlatAppearance.BorderSize = 2;
            this.bnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClose.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnClose.ForeColor = System.Drawing.Color.White;
            this.bnClose.Location = new System.Drawing.Point(612, 374);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(95, 30);
            this.bnClose.TabIndex = 25;
            this.bnClose.Text = "Close";
            this.bnClose.UseVisualStyleBackColor = false;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // bnAdd
            // 
            this.bnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnAdd.FlatAppearance.BorderSize = 2;
            this.bnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnAdd.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnAdd.ForeColor = System.Drawing.Color.White;
            this.bnAdd.Location = new System.Drawing.Point(497, 374);
            this.bnAdd.Name = "bnAdd";
            this.bnAdd.Size = new System.Drawing.Size(95, 30);
            this.bnAdd.TabIndex = 24;
            this.bnAdd.Text = "Add";
            this.bnAdd.UseVisualStyleBackColor = false;
            this.bnAdd.Click += new System.EventHandler(this.bnAdd_Click);
            // 
            // frmAddExclusion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(720, 413);
            this.Controls.Add(this.trickBrowser);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.bnAdd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rtbExcluDetail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbExcluCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.cbExcluType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAddExclusion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Exclusion";
            this.Load += new System.EventHandler(this.frmAddExclusion_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbExcluType;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.TextBox tbExcluCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbExcluDetail;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bnUnderline;
        private System.Windows.Forms.Button bnItalic;
        private System.Windows.Forms.Button bnBold;
        private System.Windows.Forms.Label label2;
        private cus_button bnAdd;
        private cus_button bnClose;
        private System.Windows.Forms.WebBrowser trickBrowser;
        private System.Windows.Forms.Button bnCurrent;
    }
}