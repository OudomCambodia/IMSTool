namespace Testing.Forms
{
    partial class PolicyRemark
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
            this.dgvPolRem = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPol = new System.Windows.Forms.TextBox();
            this.bnExcel = new Testing.cus_button();
            this.bnSearch = new Testing.cus_button();
            this.bnView = new Testing.cus_button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPolRem)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPolRem
            // 
            this.dgvPolRem.AllowUserToAddRows = false;
            this.dgvPolRem.AllowUserToDeleteRows = false;
            this.dgvPolRem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPolRem.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvPolRem.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPolRem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPolRem.Location = new System.Drawing.Point(7, 98);
            this.dgvPolRem.Margin = new System.Windows.Forms.Padding(5);
            this.dgvPolRem.MultiSelect = false;
            this.dgvPolRem.Name = "dgvPolRem";
            this.dgvPolRem.ReadOnly = true;
            this.dgvPolRem.RowHeadersVisible = false;
            this.dgvPolRem.RowTemplate.Height = 25;
            this.dgvPolRem.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPolRem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvPolRem.Size = new System.Drawing.Size(701, 474);
            this.dgvPolRem.TabIndex = 10;
            this.dgvPolRem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPolRem_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-2, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(718, 32);
            this.label1.TabIndex = 11;
            this.label1.Text = "Policy Remarks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 63);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 18;
            this.label3.Text = "Policy No:";
            // 
            // tbPol
            // 
            this.tbPol.Location = new System.Drawing.Point(84, 60);
            this.tbPol.MaxLength = 20;
            this.tbPol.Name = "tbPol";
            this.tbPol.Size = new System.Drawing.Size(266, 23);
            this.tbPol.TabIndex = 19;
            // 
            // bnExcel
            // 
            this.bnExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnExcel.FlatAppearance.BorderSize = 2;
            this.bnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnExcel.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnExcel.ForeColor = System.Drawing.Color.White;
            this.bnExcel.Location = new System.Drawing.Point(566, 56);
            this.bnExcel.Name = "bnExcel";
            this.bnExcel.Size = new System.Drawing.Size(100, 34);
            this.bnExcel.TabIndex = 21;
            this.bnExcel.Text = "To Excel";
            this.bnExcel.UseVisualStyleBackColor = false;
            this.bnExcel.Click += new System.EventHandler(this.bnExcel_Click);
            // 
            // bnSearch
            // 
            this.bnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnSearch.FlatAppearance.BorderSize = 2;
            this.bnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnSearch.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnSearch.ForeColor = System.Drawing.Color.White;
            this.bnSearch.Location = new System.Drawing.Point(368, 56);
            this.bnSearch.Name = "bnSearch";
            this.bnSearch.Size = new System.Drawing.Size(89, 34);
            this.bnSearch.TabIndex = 20;
            this.bnSearch.Text = "Search";
            this.bnSearch.UseVisualStyleBackColor = false;
            this.bnSearch.Click += new System.EventHandler(this.bnSearch_Click);
            // 
            // bnView
            // 
            this.bnView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(47)))));
            this.bnView.FlatAppearance.BorderSize = 2;
            this.bnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bnView.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnView.ForeColor = System.Drawing.Color.White;
            this.bnView.Location = new System.Drawing.Point(463, 56);
            this.bnView.Name = "bnView";
            this.bnView.Size = new System.Drawing.Size(97, 34);
            this.bnView.TabIndex = 22;
            this.bnView.Text = "View Text";
            this.bnView.UseVisualStyleBackColor = false;
            this.bnView.Click += new System.EventHandler(this.bnView_Click);
            // 
            // PolicyRemark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(714, 576);
            this.Controls.Add(this.bnView);
            this.Controls.Add(this.bnExcel);
            this.Controls.Add(this.bnSearch);
            this.Controls.Add(this.tbPol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPolRem);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PolicyRemark";
            this.Text = "PolicyRemark";
            this.Activated += new System.EventHandler(this.PolicyRemark_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPolRem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPolRem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPol;
        private cus_button bnSearch;
        private cus_button bnExcel;
        private cus_button bnView;
    }
}