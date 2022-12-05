namespace Testing.Forms
{
    partial class frmManageCrono
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageCrono));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectCus = new System.Windows.Forms.Button();
            this.tbCusName = new System.Windows.Forms.TextBox();
            this.tbCusCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbCrono = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new Testing.cus_button();
            this.btnClose = new Testing.cus_button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnReport = new Testing.cus_button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(622, 46);
            this.label1.TabIndex = 60;
            this.label1.Text = "Manage Crono";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSelectCus
            // 
            this.btnSelectCus.BackgroundImage = global::Testing.Properties.Resources.search;
            this.btnSelectCus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSelectCus.Location = new System.Drawing.Point(318, 55);
            this.btnSelectCus.Name = "btnSelectCus";
            this.btnSelectCus.Size = new System.Drawing.Size(24, 24);
            this.btnSelectCus.TabIndex = 106;
            this.toolTip1.SetToolTip(this.btnSelectCus, "Select Customer");
            this.btnSelectCus.UseVisualStyleBackColor = true;
            this.btnSelectCus.Click += new System.EventHandler(this.btnSelectCus_Click);
            // 
            // tbCusName
            // 
            this.tbCusName.Location = new System.Drawing.Point(113, 84);
            this.tbCusName.Name = "tbCusName";
            this.tbCusName.Size = new System.Drawing.Size(485, 20);
            this.tbCusName.TabIndex = 107;
            this.toolTip1.SetToolTip(this.tbCusName, "Ctrl+L to Select Customer");
            this.tbCusName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCusName_KeyDown);
            // 
            // tbCusCode
            // 
            this.tbCusCode.Location = new System.Drawing.Point(113, 59);
            this.tbCusCode.Name = "tbCusCode";
            this.tbCusCode.Size = new System.Drawing.Size(199, 20);
            this.tbCusCode.TabIndex = 105;
            this.toolTip1.SetToolTip(this.tbCusCode, "Ctrl+L to Select Customer");
            this.tbCusCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCusCode_KeyDown);
            this.tbCusCode.Leave += new System.EventHandler(this.tbCusCode_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(19, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 109;
            this.label5.Text = "Customer Name: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(19, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 108;
            this.label4.Text = "Customer Code: ";
            // 
            // tbCrono
            // 
            this.tbCrono.Location = new System.Drawing.Point(113, 109);
            this.tbCrono.Name = "tbCrono";
            this.tbCrono.Size = new System.Drawing.Size(199, 20);
            this.tbCrono.TabIndex = 110;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 111;
            this.label2.Text = "Crono No:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(372, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(128, 35);
            this.btnSave.TabIndex = 112;
            this.btnSave.Text = "Save change";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(513, 155);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 35);
            this.btnClose.TabIndex = 113;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(22, 155);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(128, 35);
            this.btnReport.TabIndex = 114;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // frmManageCrono
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(622, 211);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbCrono);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSelectCus);
            this.Controls.Add(this.tbCusName);
            this.Controls.Add(this.tbCusCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmManageCrono";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Crono";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectCus;
        private System.Windows.Forms.TextBox tbCusName;
        private System.Windows.Forms.TextBox tbCusCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbCrono;
        private System.Windows.Forms.Label label2;
        private cus_button btnSave;
        private cus_button btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private cus_button btnReport;
    }
}