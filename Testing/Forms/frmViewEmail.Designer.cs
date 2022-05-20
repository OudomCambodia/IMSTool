namespace Testing.Forms
{
    partial class frmViewEmail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmViewEmail));
            this.wbEmail = new System.Windows.Forms.WebBrowser();
            this.lbTitle = new System.Windows.Forms.Label();
            this.rtbEditor = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bnUnderline = new System.Windows.Forms.Button();
            this.bnItalic = new System.Windows.Forms.Button();
            this.bnBold = new System.Windows.Forms.Button();
            this.webBrowserTrick = new System.Windows.Forms.WebBrowser();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.bnSave = new Testing.cus_button();
            this.bnClose = new Testing.cus_button();
            this.bnReset = new Testing.cus_button();
            this.bnEdit = new Testing.cus_button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // wbEmail
            // 
            this.wbEmail.Location = new System.Drawing.Point(3, 84);
            this.wbEmail.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbEmail.Name = "wbEmail";
            this.wbEmail.Size = new System.Drawing.Size(640, 691);
            this.wbEmail.TabIndex = 0;
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
            // rtbEditor
            // 
            this.rtbEditor.AcceptsTab = true;
            this.rtbEditor.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbEditor.Location = new System.Drawing.Point(655, 84);
            this.rtbEditor.Name = "rtbEditor";
            this.rtbEditor.Size = new System.Drawing.Size(655, 691);
            this.rtbEditor.TabIndex = 14;
            this.rtbEditor.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 15F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(8, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "Editor";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.bnUnderline);
            this.panel1.Controls.Add(this.bnItalic);
            this.panel1.Controls.Add(this.bnBold);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(655, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 37);
            this.panel1.TabIndex = 17;
            // 
            // bnUnderline
            // 
            this.bnUnderline.BackgroundImage = global::Testing.Properties.Resources.Underline;
            this.bnUnderline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bnUnderline.Location = new System.Drawing.Point(174, 0);
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
            this.bnItalic.Location = new System.Drawing.Point(134, 0);
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
            this.bnBold.Location = new System.Drawing.Point(94, 0);
            this.bnBold.Name = "bnBold";
            this.bnBold.Size = new System.Drawing.Size(34, 34);
            this.bnBold.TabIndex = 17;
            this.bnBold.UseVisualStyleBackColor = true;
            this.bnBold.Click += new System.EventHandler(this.bnBold_Click);
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
            // frmViewEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1316, 787);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.bnSave);
            this.Controls.Add(this.webBrowserTrick);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bnClose);
            this.Controls.Add(this.rtbEditor);
            this.Controls.Add(this.bnReset);
            this.Controls.Add(this.bnEdit);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.wbEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewEmail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Email Content";
            this.Load += new System.EventHandler(this.frmViewEmail_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser wbEmail;
        private System.Windows.Forms.Label lbTitle;
        private cus_button bnEdit;
        private cus_button bnReset;
        private System.Windows.Forms.RichTextBox rtbEditor;
        private cus_button bnClose;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bnUnderline;
        private System.Windows.Forms.Button bnItalic;
        private System.Windows.Forms.Button bnBold;
        private System.Windows.Forms.WebBrowser webBrowserTrick;
        private cus_button bnSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;

    }
}