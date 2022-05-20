namespace Testing.Forms
{
    partial class frmEditEmailContent
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
            this.tbEmailContent = new System.Windows.Forms.RichTextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.bnDone = new Testing.cus_button();
            this.bnCancel = new Testing.cus_button();
            this.SuspendLayout();
            // 
            // tbEmailContent
            // 
            this.tbEmailContent.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbEmailContent.Location = new System.Drawing.Point(17, 44);
            this.tbEmailContent.Name = "tbEmailContent";
            this.tbEmailContent.Size = new System.Drawing.Size(785, 444);
            this.tbEmailContent.TabIndex = 0;
            this.tbEmailContent.Text = "";
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
            this.lbTitle.Size = new System.Drawing.Size(820, 46);
            this.lbTitle.TabIndex = 11;
            this.lbTitle.Text = "Edit Email Content";
            this.lbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bnDone
            // 
            this.bnDone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnDone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnDone.FlatAppearance.BorderSize = 2;
            this.bnDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnDone.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnDone.ForeColor = System.Drawing.Color.White;
            this.bnDone.Location = new System.Drawing.Point(584, 499);
            this.bnDone.Name = "bnDone";
            this.bnDone.Size = new System.Drawing.Size(95, 33);
            this.bnDone.TabIndex = 12;
            this.bnDone.Text = "Done";
            this.bnDone.UseVisualStyleBackColor = false;
            this.bnDone.Click += new System.EventHandler(this.bnDone_Click);
            // 
            // bnCancel
            // 
            this.bnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnCancel.FlatAppearance.BorderSize = 2;
            this.bnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnCancel.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.bnCancel.ForeColor = System.Drawing.Color.White;
            this.bnCancel.Location = new System.Drawing.Point(707, 499);
            this.bnCancel.Name = "bnCancel";
            this.bnCancel.Size = new System.Drawing.Size(95, 33);
            this.bnCancel.TabIndex = 13;
            this.bnCancel.Text = "Cancel";
            this.bnCancel.UseVisualStyleBackColor = false;
            this.bnCancel.Click += new System.EventHandler(this.bnCancel_Click);
            // 
            // frmEditEmailContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(820, 546);
            this.Controls.Add(this.bnCancel);
            this.Controls.Add(this.bnDone);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.tbEmailContent);
            this.Name = "frmEditEmailContent";
            this.Text = "frmEditEmailContent";
            this.Load += new System.EventHandler(this.frmEditEmailContent_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tbEmailContent;
        private System.Windows.Forms.Label lbTitle;
        private cus_button bnDone;
        private cus_button bnCancel;
    }
}