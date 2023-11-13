namespace Testing.Forms
{
    partial class frmEmailNoticeAttachment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmailNoticeAttachment));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.dgvDefinition = new System.Windows.Forms.DataGridView();
            this.panel12 = new System.Windows.Forms.Panel();
            this.btnGenerateEmailNotice = new Testing.cus_button();
            this.panel2.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.panel13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefinition)).BeginInit();
            this.panel12.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 704);
            this.panel2.TabIndex = 1;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.panel13);
            this.groupBox13.Controls.Add(this.panel12);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox13.Font = new System.Drawing.Font("Cambria", 9.75F);
            this.groupBox13.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox13.Location = new System.Drawing.Point(0, 0);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(998, 704);
            this.groupBox13.TabIndex = 3;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Definition/Exclusion";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.dgvDefinition);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(3, 52);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(992, 649);
            this.panel13.TabIndex = 2;
            // 
            // dgvDefinition
            // 
            this.dgvDefinition.AllowUserToAddRows = false;
            this.dgvDefinition.AllowUserToDeleteRows = false;
            this.dgvDefinition.AllowUserToOrderColumns = true;
            this.dgvDefinition.AllowUserToResizeRows = false;
            this.dgvDefinition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDefinition.Location = new System.Drawing.Point(0, 0);
            this.dgvDefinition.Name = "dgvDefinition";
            this.dgvDefinition.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvDefinition.Size = new System.Drawing.Size(992, 649);
            this.dgvDefinition.TabIndex = 0;
            this.dgvDefinition.DataSourceChanged += new System.EventHandler(this.dgvDefinition_DataSourceChanged);
            this.dgvDefinition.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefinition_CellContentClick);
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.btnGenerateEmailNotice);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(3, 19);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(992, 33);
            this.panel12.TabIndex = 1;
            // 
            // btnGenerateEmailNotice
            // 
            this.btnGenerateEmailNotice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.btnGenerateEmailNotice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGenerateEmailNotice.FlatAppearance.BorderSize = 2;
            this.btnGenerateEmailNotice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateEmailNotice.ForeColor = System.Drawing.Color.White;
            this.btnGenerateEmailNotice.Location = new System.Drawing.Point(0, 0);
            this.btnGenerateEmailNotice.Name = "btnGenerateEmailNotice";
            this.btnGenerateEmailNotice.Size = new System.Drawing.Size(992, 33);
            this.btnGenerateEmailNotice.TabIndex = 0;
            this.btnGenerateEmailNotice.Text = "Generate Email Notice";
            this.btnGenerateEmailNotice.UseVisualStyleBackColor = false;
            this.btnGenerateEmailNotice.Click += new System.EventHandler(this.btnGenerateEmailNotice_Click);
            // 
            // frmEmailNoticeAttachment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(998, 704);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEmailNoticeAttachment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Notice Attachment";
            this.Load += new System.EventHandler(this.frmEmailNoticeAttachment_Load);
            this.panel2.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefinition)).EndInit();
            this.panel12.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.DataGridView dgvDefinition;
        private System.Windows.Forms.Panel panel12;
        private cus_button btnGenerateEmailNotice;
    }
}