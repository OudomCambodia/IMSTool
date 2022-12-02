namespace Testing
{
    partial class Msgbox
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnTop = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pBackground = new System.Windows.Forms.FlowLayoutPanel();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.pnTop.SuspendLayout();
            this.pBackground.SuspendLayout();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.btnOk.Location = new System.Drawing.Point(299, 2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 28);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.btnCancel.Location = new System.Drawing.Point(389, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(0)))), ((int)(((byte)(69)))));
            this.lblTitle.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(4, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 15);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "lbltitle";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(0)))), ((int)(((byte)(69)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.button1.Location = new System.Drawing.Point(456, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 21);
            this.button1.TabIndex = 7;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pnTop
            // 
            this.pnTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(0)))), ((int)(((byte)(69)))));
            this.pnTop.Controls.Add(this.button1);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(480, 31);
            this.pnTop.TabIndex = 8;
            this.pnTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnTop_MouseDown);
            this.pnTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnTop_MouseMove);
            this.pnTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnTop_MouseUp);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(8, 5);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(41, 15);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label1";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pBackground
            // 
            this.pBackground.AutoSize = true;
            this.pBackground.BackColor = System.Drawing.Color.White;
            this.pBackground.Controls.Add(this.lblMessage);
            this.pBackground.Dock = System.Windows.Forms.DockStyle.Top;
            this.pBackground.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.pBackground.Location = new System.Drawing.Point(0, 31);
            this.pBackground.MaximumSize = new System.Drawing.Size(0, 720);
            this.pBackground.MinimumSize = new System.Drawing.Size(0, 100);
            this.pBackground.Name = "pBackground";
            this.pBackground.Padding = new System.Windows.Forms.Padding(5);
            this.pBackground.Size = new System.Drawing.Size(480, 100);
            this.pBackground.TabIndex = 9;
            this.pBackground.SizeChanged += new System.EventHandler(this.pBackground_SizeChanged);
            // 
            // pnBottom
            // 
            this.pnBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.pnBottom.Controls.Add(this.btnOk);
            this.pnBottom.Controls.Add(this.btnCancel);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 132);
            this.pnBottom.Margin = new System.Windows.Forms.Padding(2);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(480, 34);
            this.pnBottom.TabIndex = 10;
            // 
            // Msgbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(480, 166);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.pBackground);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnTop);
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Msgbox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Msgbox";
            this.Activated += new System.EventHandler(this.Msgbox_Activated);
            this.Load += new System.EventHandler(this.Msgbox_Load);
            this.pnTop.ResumeLayout(false);
            this.pBackground.ResumeLayout(false);
            this.pBackground.PerformLayout();
            this.pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.FlowLayoutPanel pBackground;
        private System.Windows.Forms.Panel pnBottom;
    }
}