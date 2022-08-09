using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmDPPendingRemark : Form
    {
        public string UserName = string.Empty, UserCode = string.Empty;
        public DataTable SelectedDoc = new DataTable();
        DBS11SqlCrud crud = new DBS11SqlCrud();
        public bool isRevised = false;

        public frmDPPendingRemark()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!isRevised)
            {
                if (cbPending.Text == "")
                {
                    Msgbox.Show("Please select Pending!");
                    return;
                }
                string remark = tbRemark.Text.Trim();
                if (remark == "")
                {
                    Msgbox.Show("Please input Pending Remark!");
                    return;
                }
                DialogResult dr = Msgbox.Show("Are you sure you want to remark " + SelectedDoc.Rows.Count + " selected document(s) with " + remark + " ?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    string SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);
                    crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE dbo.tbDOC SET STATUS_REMARK = @remark, DOC_CUR_STATUS = " + (cbPending.SelectedItem as ComboboxItem).Value.ToString() + ", DOC_CUR_STATUS_SET_BY = '" + UserCode + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                    cmd.Parameters.Add(new SqlParameter("remark", DateTime.UtcNow.AddHours(7).ToString("dd'/'MM'/'yyyy")+"-"+remark+Environment.NewLine));
                    crud.Executing(cmd);

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1 = new SqlCommand();
                    cmd1.CommandText = "INSERT INTO dbo.tbNoti(NOTI_DETAIL, NOTI_TO, NOTI_DATE, REMARK, NOTI_TYPE) SELECT 'Instruction Note No \"' + DOC_CODE + '\" has been pending due to \"'+ @reason +'\"', (SELECT USER_NAME FROM dbo.tbDOC_USER WHERE FULL_NAME = CREATE_BY), getdate(), DOC_CODE, '" + CommonFunctions.NotiType.PENDING + "' FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                    cmd1.Parameters.Add(new SqlParameter("reason", tbRemark.Text.Trim()));
                    crud.Executing(cmd1);


                    Msgbox.Show("Pending Remark Set! Status change to "+ cbPending.Text.ToUpper() +".");
                    this.Close();
                }
            }
            else
            {
                string remark = tbRemark.Text.Trim();
                if (remark == "")
                {
                    Msgbox.Show("Please input Pending Remark!");
                    return;
                }
                DialogResult dr = Msgbox.Show("Are you sure you want to remark " + SelectedDoc.Rows.Count + " selected document(s) with " + remark + " ?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    string SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE dbo.tbDOC SET STATUS_REMARK = STATUS_REMARK + @remark WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                    cmd.Parameters.Add(new SqlParameter("remark", DateTime.UtcNow.AddHours(7).ToString("dd'/'MM'/'yyyy") + "-" + remark + Environment.NewLine));
                    crud.Executing(cmd);

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1 = new SqlCommand();
                    cmd1.CommandText = "INSERT INTO dbo.tbNoti(NOTI_DETAIL, NOTI_TO, NOTI_DATE, REMARK, NOTI_TYPE) SELECT 'Instruction Note No \"' + DOC_CODE + '\" has been pending due to \"'+ @reason +'\"', (SELECT USER_NAME FROM dbo.tbDOC_USER WHERE FULL_NAME = CREATE_BY), getdate(), DOC_CODE, '" + CommonFunctions.NotiType.PENDING + "' FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                    cmd1.Parameters.Add(new SqlParameter("reason", tbRemark.Text.Trim()));
                    crud.Executing(cmd1);

                    Msgbox.Show("Pending Remark Set!");
                    this.Close();
                }
            }
        }

        private void frmDPPendingRemark_Load(object sender, EventArgs e)
        {
            lblDetail.Text = "Document detail: " + SelectedDoc.Rows.Count + " document(s) is/are selected.";
            lblDetail.MaximumSize = new Size(450, 50);

            cbPending.Items.Add(new ComboboxItem("DP Pending", "14"));
            cbPending.Items.Add(new ComboboxItem("Producer Pending", "15"));
            cbPending.Items.Add(new ComboboxItem("UW Pending", "16"));
            cbPending.SelectedIndex = -1;

            if (isRevised)
            {
                lbTitle.Text = "Revise Pending Remark";
                cbPending.Visible = false;
                lblPending.Visible = false;
            }
        }
    }
}
