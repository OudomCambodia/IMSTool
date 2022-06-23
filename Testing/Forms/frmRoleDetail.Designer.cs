namespace Testing.Forms
{
    partial class frmRoleDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoleDetail));
            this.lblGroupName = new System.Windows.Forms.Label();
            this.lblTotalUser = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstUserInfo = new System.Windows.Forms.ListView();
            this.cUserCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cUserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cCreateDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblGroupName
            // 
            this.lblGroupName.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblGroupName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGroupName.Font = new System.Drawing.Font("Hanuman", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblGroupName.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblGroupName.Location = new System.Drawing.Point(0, 0);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(489, 58);
            this.lblGroupName.TabIndex = 1;
            this.lblGroupName.Text = "User Under \'SA\' Group";
            this.lblGroupName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalUser
            // 
            this.lblTotalUser.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblTotalUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTotalUser.Font = new System.Drawing.Font("Hanuman", 15F, System.Drawing.FontStyle.Italic);
            this.lblTotalUser.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTotalUser.Location = new System.Drawing.Point(0, 372);
            this.lblTotalUser.Name = "lblTotalUser";
            this.lblTotalUser.Size = new System.Drawing.Size(489, 37);
            this.lblTotalUser.TabIndex = 2;
            this.lblTotalUser.Text = "11 Users";
            this.lblTotalUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lstUserInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(489, 314);
            this.panel3.TabIndex = 5;
            // 
            // lstUserInfo
            // 
            this.lstUserInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lstUserInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cUserCode,
            this.cUserName,
            this.cCreateDate});
            this.lstUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstUserInfo.HideSelection = false;
            this.lstUserInfo.Location = new System.Drawing.Point(0, 0);
            this.lstUserInfo.Name = "lstUserInfo";
            this.lstUserInfo.Size = new System.Drawing.Size(489, 314);
            this.lstUserInfo.TabIndex = 1;
            this.lstUserInfo.UseCompatibleStateImageBehavior = false;
            this.lstUserInfo.View = System.Windows.Forms.View.Details;
            this.lstUserInfo.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lstUserInfo_ItemSelectionChanged);
            // 
            // cUserCode
            // 
            this.cUserCode.Text = "User Code";
            this.cUserCode.Width = 150;
            // 
            // cUserName
            // 
            this.cUserName.Text = "User Name";
            this.cUserName.Width = 150;
            // 
            // cCreateDate
            // 
            this.cCreateDate.Text = "User Create Date";
            this.cCreateDate.Width = 150;
            // 
            // frmRoleDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 409);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblTotalUser);
            this.Controls.Add(this.lblGroupName);
            this.Font = new System.Drawing.Font("Hanuman", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRoleDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Role Detail";
            this.Load += new System.EventHandler(this.frmRoleDetail_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGroupName;
        private System.Windows.Forms.Label lblTotalUser;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lstUserInfo;
        private System.Windows.Forms.ColumnHeader cUserCode;
        private System.Windows.Forms.ColumnHeader cUserName;
        private System.Windows.Forms.ColumnHeader cCreateDate;
    }
}