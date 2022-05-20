namespace Testing.Forms
{
    partial class Hospital_Form
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
            this.cus_button1 = new Testing.cus_button();
            this.sfdExcel = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // cus_button1
            // 
            this.cus_button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.cus_button1.FlatAppearance.BorderSize = 2;
            this.cus_button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cus_button1.ForeColor = System.Drawing.Color.White;
            this.cus_button1.Location = new System.Drawing.Point(137, 65);
            this.cus_button1.Name = "cus_button1";
            this.cus_button1.Size = new System.Drawing.Size(108, 99);
            this.cus_button1.TabIndex = 0;
            this.cus_button1.Text = "cus_button1";
            this.cus_button1.UseVisualStyleBackColor = false;
            this.cus_button1.Click += new System.EventHandler(this.cus_button1_Click);
            // 
            // sfdExcel
            // 
            this.sfdExcel.Filter = "Excel File|*.xlsx";
            // 
            // Hospital_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 552);
            this.Controls.Add(this.cus_button1);
            this.Name = "Hospital_Form";
            this.Text = "Hospital_Form";
            this.Load += new System.EventHandler(this.Hospital_Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private cus_button cus_button1;
        private System.Windows.Forms.SaveFileDialog sfdExcel;

    }
}