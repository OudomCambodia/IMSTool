namespace Testing.Forms
{
    partial class frmUserRoleManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserRoleManagement));
            this.btnUpdateControl = new System.Windows.Forms.Button();
            this.btnAddNewControl = new System.Windows.Forms.Button();
            this.btnUpdateCode = new System.Windows.Forms.Button();
            this.txtGroupCode = new System.Windows.Forms.TextBox();
            this.txtSearchRole = new System.Windows.Forms.TextBox();
            this.gbEnabled = new System.Windows.Forms.GroupBox();
            this.rbEnabledFalse = new System.Windows.Forms.RadioButton();
            this.rbEnabledTrue = new System.Windows.Forms.RadioButton();
            this.lstCode = new System.Windows.Forms.ListBox();
            this.tvUserRole = new System.Windows.Forms.TreeView();
            this.ctmsDeleteControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctmsCopyRole = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbEnabled.SuspendLayout();
            this.ctmsDeleteControl.SuspendLayout();
            this.ctmsCopyRole.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdateControl
            // 
            this.btnUpdateControl.Font = new System.Drawing.Font("Hanuman", 9F);
            this.btnUpdateControl.Location = new System.Drawing.Point(377, 615);
            this.btnUpdateControl.Name = "btnUpdateControl";
            this.btnUpdateControl.Size = new System.Drawing.Size(110, 28);
            this.btnUpdateControl.TabIndex = 22;
            this.btnUpdateControl.Text = "Update Control";
            this.btnUpdateControl.UseVisualStyleBackColor = true;
            this.btnUpdateControl.Click += new System.EventHandler(this.btnUpdateControl_Click);
            // 
            // btnAddNewControl
            // 
            this.btnAddNewControl.Font = new System.Drawing.Font("Hanuman", 9F);
            this.btnAddNewControl.Location = new System.Drawing.Point(493, 615);
            this.btnAddNewControl.Name = "btnAddNewControl";
            this.btnAddNewControl.Size = new System.Drawing.Size(110, 28);
            this.btnAddNewControl.TabIndex = 21;
            this.btnAddNewControl.Text = "Add New Control";
            this.btnAddNewControl.UseVisualStyleBackColor = true;
            this.btnAddNewControl.Click += new System.EventHandler(this.btnAddNewControl_Click);
            // 
            // btnUpdateCode
            // 
            this.btnUpdateCode.Font = new System.Drawing.Font("Hanuman", 9F);
            this.btnUpdateCode.Location = new System.Drawing.Point(159, 615);
            this.btnUpdateCode.Name = "btnUpdateCode";
            this.btnUpdateCode.Size = new System.Drawing.Size(91, 28);
            this.btnUpdateCode.TabIndex = 20;
            this.btnUpdateCode.Text = "Update Code";
            this.btnUpdateCode.UseVisualStyleBackColor = true;
            this.btnUpdateCode.Click += new System.EventHandler(this.btnUpdateCode_Click);
            // 
            // txtGroupCode
            // 
            this.txtGroupCode.Font = new System.Drawing.Font("Hanuman", 9F);
            this.txtGroupCode.ForeColor = System.Drawing.Color.Black;
            this.txtGroupCode.Location = new System.Drawing.Point(12, 616);
            this.txtGroupCode.Name = "txtGroupCode";
            this.txtGroupCode.Size = new System.Drawing.Size(141, 26);
            this.txtGroupCode.TabIndex = 19;
            this.txtGroupCode.TextChanged += new System.EventHandler(this.txtGroupCode_TextChanged);
            // 
            // txtSearchRole
            // 
            this.txtSearchRole.Font = new System.Drawing.Font("Hanuman", 9F);
            this.txtSearchRole.ForeColor = System.Drawing.Color.DarkGray;
            this.txtSearchRole.Location = new System.Drawing.Point(12, 13);
            this.txtSearchRole.Name = "txtSearchRole";
            this.txtSearchRole.Size = new System.Drawing.Size(238, 26);
            this.txtSearchRole.TabIndex = 18;
            this.txtSearchRole.Text = "--- SEARCH ROLE ---";
            this.txtSearchRole.TextChanged += new System.EventHandler(this.txtSearchRole_TextChanged);
            this.txtSearchRole.Enter += new System.EventHandler(this.txtSearchRole_Enter);
            this.txtSearchRole.Leave += new System.EventHandler(this.txtSearchRole_Leave);
            // 
            // gbEnabled
            // 
            this.gbEnabled.Controls.Add(this.rbEnabledFalse);
            this.gbEnabled.Controls.Add(this.rbEnabledTrue);
            this.gbEnabled.Font = new System.Drawing.Font("Hanuman", 8.5F);
            this.gbEnabled.Location = new System.Drawing.Point(609, 11);
            this.gbEnabled.Name = "gbEnabled";
            this.gbEnabled.Size = new System.Drawing.Size(135, 51);
            this.gbEnabled.TabIndex = 17;
            this.gbEnabled.TabStop = false;
            this.gbEnabled.Text = "Enabled";
            // 
            // rbEnabledFalse
            // 
            this.rbEnabledFalse.AutoSize = true;
            this.rbEnabledFalse.Location = new System.Drawing.Point(74, 20);
            this.rbEnabledFalse.Name = "rbEnabledFalse";
            this.rbEnabledFalse.Size = new System.Drawing.Size(55, 22);
            this.rbEnabledFalse.TabIndex = 7;
            this.rbEnabledFalse.TabStop = true;
            this.rbEnabledFalse.Text = "False";
            this.rbEnabledFalse.UseVisualStyleBackColor = true;
            this.rbEnabledFalse.Click += new System.EventHandler(this.rbEnabledFalse_Click);
            // 
            // rbEnabledTrue
            // 
            this.rbEnabledTrue.AutoSize = true;
            this.rbEnabledTrue.Location = new System.Drawing.Point(17, 20);
            this.rbEnabledTrue.Name = "rbEnabledTrue";
            this.rbEnabledTrue.Size = new System.Drawing.Size(49, 22);
            this.rbEnabledTrue.TabIndex = 6;
            this.rbEnabledTrue.TabStop = true;
            this.rbEnabledTrue.Text = "True";
            this.rbEnabledTrue.UseVisualStyleBackColor = true;
            this.rbEnabledTrue.Click += new System.EventHandler(this.rbEnabledTrue_Click);
            // 
            // lstCode
            // 
            this.lstCode.ContextMenuStrip = this.ctmsCopyRole;
            this.lstCode.Font = new System.Drawing.Font("Hanuman", 9F);
            this.lstCode.FormattingEnabled = true;
            this.lstCode.ItemHeight = 18;
            this.lstCode.Location = new System.Drawing.Point(12, 49);
            this.lstCode.Name = "lstCode";
            this.lstCode.Size = new System.Drawing.Size(238, 562);
            this.lstCode.TabIndex = 16;
            this.lstCode.SelectedIndexChanged += new System.EventHandler(this.lstCode_SelectedIndexChanged);
            this.lstCode.DoubleClick += new System.EventHandler(this.lstCode_DoubleClick);
            // 
            // tvUserRole
            // 
            this.tvUserRole.ContextMenuStrip = this.ctmsDeleteControl;
            this.tvUserRole.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvUserRole.Font = new System.Drawing.Font("Hanuman", 9F);
            this.tvUserRole.HideSelection = false;
            this.tvUserRole.Location = new System.Drawing.Point(256, 13);
            this.tvUserRole.Name = "tvUserRole";
            this.tvUserRole.Size = new System.Drawing.Size(347, 598);
            this.tvUserRole.TabIndex = 15;
            this.tvUserRole.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvUserRole_DrawNode);
            this.tvUserRole.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvUserRole_AfterSelect);
            // 
            // ctmsDeleteControl
            // 
            this.ctmsDeleteControl.Font = new System.Drawing.Font("Hanuman", 9F);
            this.ctmsDeleteControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteControlToolStripMenuItem});
            this.ctmsDeleteControl.Name = "ctmsDeleteControl";
            this.ctmsDeleteControl.Size = new System.Drawing.Size(149, 26);
            // 
            // deleteControlToolStripMenuItem
            // 
            this.deleteControlToolStripMenuItem.Image = global::Testing.Properties.Resources.cross_mark_96;
            this.deleteControlToolStripMenuItem.Name = "deleteControlToolStripMenuItem";
            this.deleteControlToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteControlToolStripMenuItem.Text = "Delete Control";
            this.deleteControlToolStripMenuItem.Click += new System.EventHandler(this.deleteControlToolStripMenuItem_Click);
            // 
            // ctmsCopyRole
            // 
            this.ctmsCopyRole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyRoleToolStripMenuItem,
            this.deleteRoleToolStripMenuItem});
            this.ctmsCopyRole.Name = "ctmsCopyRole";
            this.ctmsCopyRole.Size = new System.Drawing.Size(134, 48);
            this.ctmsCopyRole.Opening += new System.ComponentModel.CancelEventHandler(this.ctmsCopyRole_Opening);
            // 
            // copyRoleToolStripMenuItem
            // 
            this.copyRoleToolStripMenuItem.Font = new System.Drawing.Font("Hanuman", 9F);
            this.copyRoleToolStripMenuItem.Image = global::Testing.Properties.Resources.printer_96;
            this.copyRoleToolStripMenuItem.Name = "copyRoleToolStripMenuItem";
            this.copyRoleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.copyRoleToolStripMenuItem.Text = "Copy Role";
            this.copyRoleToolStripMenuItem.Click += new System.EventHandler(this.copyRoleToolStripMenuItem_Click);
            // 
            // deleteRoleToolStripMenuItem
            // 
            this.deleteRoleToolStripMenuItem.Font = new System.Drawing.Font("Hanuman", 9F);
            this.deleteRoleToolStripMenuItem.Image = global::Testing.Properties.Resources.cross_mark_96;
            this.deleteRoleToolStripMenuItem.Name = "deleteRoleToolStripMenuItem";
            this.deleteRoleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.deleteRoleToolStripMenuItem.Text = "Delete Role";
            this.deleteRoleToolStripMenuItem.Click += new System.EventHandler(this.deleteRoleToolStripMenuItem_Click);
            // 
            // frmUserRoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(756, 654);
            this.Controls.Add(this.btnUpdateControl);
            this.Controls.Add(this.btnAddNewControl);
            this.Controls.Add(this.btnUpdateCode);
            this.Controls.Add(this.txtGroupCode);
            this.Controls.Add(this.txtSearchRole);
            this.Controls.Add(this.gbEnabled);
            this.Controls.Add(this.lstCode);
            this.Controls.Add(this.tvUserRole);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmUserRoleManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Role Management";
            this.Load += new System.EventHandler(this.frmUserRoleManagement_Load);
            this.gbEnabled.ResumeLayout(false);
            this.gbEnabled.PerformLayout();
            this.ctmsDeleteControl.ResumeLayout(false);
            this.ctmsCopyRole.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateControl;
        private System.Windows.Forms.Button btnAddNewControl;
        private System.Windows.Forms.Button btnUpdateCode;
        private System.Windows.Forms.TextBox txtGroupCode;
        private System.Windows.Forms.TextBox txtSearchRole;
        private System.Windows.Forms.GroupBox gbEnabled;
        private System.Windows.Forms.RadioButton rbEnabledFalse;
        private System.Windows.Forms.RadioButton rbEnabledTrue;
        private System.Windows.Forms.ListBox lstCode;
        private System.Windows.Forms.TreeView tvUserRole;
        private System.Windows.Forms.ContextMenuStrip ctmsDeleteControl;
        private System.Windows.Forms.ToolStripMenuItem deleteControlToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctmsCopyRole;
        private System.Windows.Forms.ToolStripMenuItem copyRoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteRoleToolStripMenuItem;
    }
}