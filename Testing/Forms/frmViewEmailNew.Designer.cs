namespace Testing.Forms
{
    partial class frmViewEmailNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewEmailNew));
            this.lbTitle = new System.Windows.Forms.Label();
            this.webBrowserTrick = new System.Windows.Forms.WebBrowser();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bnSave = new Testing.cus_button();
            this.bnClose = new Testing.cus_button();
            this.bnReset = new Testing.cus_button();
            this.bnEdit = new Testing.cus_button();
            this.htmlEditor = new DG.MiniHTMLTextBox.MiniHTMLTextBox();
            this.wbEmail = new System.Windows.Forms.WebBrowser();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.lbTitle.Size = new System.Drawing.Size(1316, 46);
            this.lbTitle.TabIndex = 11;
            this.lbTitle.Text = "View Email Content";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // webBrowserTrick
            // 
            this.webBrowserTrick.Location = new System.Drawing.Point(778, 12);
            this.webBrowserTrick.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserTrick.Name = "webBrowserTrick";
            this.webBrowserTrick.Size = new System.Drawing.Size(20, 20);
            this.webBrowserTrick.TabIndex = 18;
            this.webBrowserTrick.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 48);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(438, 37);
            this.panel2.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 15F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 17;
            this.label1.Text = "View";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(649, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 37);
            this.panel1.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 15F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "Edit";
            // 
            // bnSave
            // 
            this.bnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSave.FlatAppearance.BorderSize = 2;
            this.bnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSave.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnSave.ForeColor = System.Drawing.Color.White;
            this.bnSave.Location = new System.Drawing.Point(1114, 50);
            this.bnSave.Name = "bnSave";
            this.bnSave.Size = new System.Drawing.Size(95, 30);
            this.bnSave.TabIndex = 19;
            this.bnSave.Text = "Save";
            this.bnSave.UseVisualStyleBackColor = false;
            this.bnSave.Click += new System.EventHandler(this.bnSave_Click);
            // 
            // bnClose
            // 
            this.bnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnClose.FlatAppearance.BorderSize = 2;
            this.bnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnClose.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnClose.ForeColor = System.Drawing.Color.White;
            this.bnClose.Location = new System.Drawing.Point(1215, 50);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(95, 30);
            this.bnClose.TabIndex = 15;
            this.bnClose.Text = "Close";
            this.bnClose.UseVisualStyleBackColor = false;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // bnReset
            // 
            this.bnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnReset.FlatAppearance.BorderSize = 2;
            this.bnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnReset.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnReset.ForeColor = System.Drawing.Color.White;
            this.bnReset.Location = new System.Drawing.Point(447, 49);
            this.bnReset.Name = "bnReset";
            this.bnReset.Size = new System.Drawing.Size(95, 33);
            this.bnReset.TabIndex = 13;
            this.bnReset.Text = "Reset";
            this.bnReset.UseVisualStyleBackColor = false;
            this.bnReset.Click += new System.EventHandler(this.bnReset_Click);
            // 
            // bnEdit
            // 
            this.bnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnEdit.FlatAppearance.BorderSize = 2;
            this.bnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnEdit.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnEdit.ForeColor = System.Drawing.Color.White;
            this.bnEdit.Location = new System.Drawing.Point(548, 49);
            this.bnEdit.Name = "bnEdit";
            this.bnEdit.Size = new System.Drawing.Size(95, 33);
            this.bnEdit.TabIndex = 12;
            this.bnEdit.Text = "Edit";
            this.bnEdit.UseVisualStyleBackColor = false;
            this.bnEdit.Click += new System.EventHandler(this.bnEdit_Click);
            // 
            // htmlEditor
            // 
            this.htmlEditor.Enabled = false;
            this.htmlEditor.IllegalPatterns = new string[] {
        "<script.*?>",
        "<\\w+\\s+.*?(j|java|vb|ecma)script:.*?>",
        "<\\w+(\\s+|\\s+.*?\\s+)on\\w+\\s*=.+?>",
        "</?input.*?>"};
            this.htmlEditor.Location = new System.Drawing.Point(649, 84);
            this.htmlEditor.Name = "htmlEditor";
            this.htmlEditor.Padding = new System.Windows.Forms.Padding(1);
            this.htmlEditor.Size = new System.Drawing.Size(661, 691);
            this.htmlEditor.TabIndex = 22;
            this.htmlEditor.Text = null;
            // 
            // wbEmail
            // 
            this.wbEmail.Location = new System.Drawing.Point(3, 84);
            this.wbEmail.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbEmail.Name = "wbEmail";
            this.wbEmail.Size = new System.Drawing.Size(640, 691);
            this.wbEmail.TabIndex = 0;
            // 
            // frmViewEmailNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1316, 787);
            this.Controls.Add(this.htmlEditor);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bnSave);
            this.Controls.Add(this.webBrowserTrick);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.bnReset);
            this.Controls.Add(this.bnEdit);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.wbEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewEmailNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Email Content";
            this.Load += new System.EventHandler(this.frmViewEmail_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private cus_button bnEdit;
        private cus_button bnReset;
        private cus_button bnClose;
        private System.Windows.Forms.WebBrowser webBrowserTrick;
        private cus_button bnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private DG.MiniHTMLTextBox.MiniHTMLTextBox htmlEditor;
        public System.Windows.Forms.WebBrowser wbEmail;

    }
}