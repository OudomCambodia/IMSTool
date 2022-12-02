namespace Testing
{
    partial class frmLogIn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogIn));
            this.lbUsercode = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tmCheckMaint = new System.Windows.Forms.Timer(this.components);
            this.tmClose = new System.Windows.Forms.Timer(this.components);
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btClose = new Testing.cus_button();
            this.btLogIn = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUsercode
            // 
            this.lbUsercode.AutoSize = true;
            this.lbUsercode.BackColor = System.Drawing.Color.Transparent;
            this.lbUsercode.ForeColor = System.Drawing.Color.Black;
            this.lbUsercode.Location = new System.Drawing.Point(60, 40);
            this.lbUsercode.Name = "lbUsercode";
            this.lbUsercode.Size = new System.Drawing.Size(76, 16);
            this.lbUsercode.TabIndex = 0;
            this.lbUsercode.Text = "User Code:";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.ForeColor = System.Drawing.Color.Black;
            this.lblPassword.Location = new System.Drawing.Point(60, 72);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(71, 16);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password:";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(152, 37);
            this.tbUser.MaxLength = 5;
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(251, 22);
            this.tbUser.TabIndex = 2;
            this.tbUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbUser_KeyDown);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(152, 69);
            this.tbPassword.MaxLength = 20;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(251, 22);
            this.tbPassword.TabIndex = 3;
            this.tbPassword.UseSystemPasswordChar = true;
            this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
            // 
            // tmCheckMaint
            // 
            this.tmCheckMaint.Enabled = true;
            this.tmCheckMaint.Interval = 600000;
            this.tmCheckMaint.Tick += new System.EventHandler(this.tmCheckMaint_Tick);
            // 
            // tmClose
            // 
            this.tmClose.Interval = 120000;
            this.tmClose.Tick += new System.EventHandler(this.tmClose_Tick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Testing.Properties.Resources.new_logo_image_1;
            this.pictureBox2.Location = new System.Drawing.Point(386, 100);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(80, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Testing.Properties.Resources.new_logo_image_2;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // btClose
            // 
            this.btClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.ForeColor = System.Drawing.Color.White;
            this.btClose.Location = new System.Drawing.Point(322, 101);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(81, 28);
            this.btClose.TabIndex = 5;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btLogIn
            // 
            this.btLogIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.btLogIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLogIn.ForeColor = System.Drawing.Color.White;
            this.btLogIn.Location = new System.Drawing.Point(235, 101);
            this.btLogIn.Name = "btLogIn";
            this.btLogIn.Size = new System.Drawing.Size(81, 28);
            this.btLogIn.TabIndex = 4;
            this.btLogIn.Text = "Log In";
            this.btLogIn.UseVisualStyleBackColor = false;
            this.btLogIn.Click += new System.EventHandler(this.btLogIn_Click);
            // 
            // frmLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(460, 146);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lbUsercode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btLogIn);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUser);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogIn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log In";
            this.Load += new System.EventHandler(this.frmLogIn_Load);
            this.Shown += new System.EventHandler(this.frmLogIn_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmLogIn_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUsercode;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.Timer tmCheckMaint;
        private System.Windows.Forms.Timer tmClose;
        public System.Windows.Forms.TextBox tbPassword;
        private cus_button btLogIn;
        private cus_button btClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}