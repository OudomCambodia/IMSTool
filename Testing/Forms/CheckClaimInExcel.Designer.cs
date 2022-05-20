namespace Testing.Forms
{
    partial class CheckClaimInExcel
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
            this.dtClaim = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dtClaim)).BeginInit();
            this.SuspendLayout();
            // 
            // dtClaim
            // 
            this.dtClaim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtClaim.Location = new System.Drawing.Point(13, 39);
            this.dtClaim.Name = "dtClaim";
            this.dtClaim.Size = new System.Drawing.Size(828, 384);
            this.dtClaim.TabIndex = 0;
            // 
            // CheckClaimInExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 460);
            this.Controls.Add(this.dtClaim);
            this.Name = "CheckClaimInExcel";
            this.Text = "CheckClaimInExcel";
            ((System.ComponentModel.ISupportInitialize)(this.dtClaim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dtClaim;

    }
}