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
    public partial class frmAcceptRejectDoc : Form
    {

        public string UserName = string.Empty, UserCode = string.Empty;
        public DataTable SelectedDoc = new DataTable();

        DBS11SqlCrud crud = new DBS11SqlCrud();

        public frmAcceptRejectDoc()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void rdAccept_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAccept.Checked)
            {
                groupReason.Enabled = false;
            }
            else if (rdReject.Checked)
            {
                groupReason.Enabled = true;
            }
        }

        private void rdReason1_CheckedChanged(object sender, EventArgs e)
        {
            checkReasonRd();
        }


        void checkReasonRd()
        {
            foreach (RadioButton rdo in groupReason.Controls.OfType<RadioButton>())
            {
                if (rdo.Text == "Other" && rdo.Checked)
                {
                    tbOther.Text = "";
                    tbOther.Enabled = true;
                    break;
                }
                else
                    tbOther.Enabled = false;
            }
        }

        private void frmAcceptRejectDoc_Load(object sender, EventArgs e)
        {
            groupReason.Enabled = false;
            lblDetail.Text = "Document detail: " + SelectedDoc.Rows.Count + " document(s) is/are selected.";
            lblDetail.MaximumSize = new Size(450, 50);
        }

        private void rdReason2_CheckedChanged(object sender, EventArgs e)
        {
            checkReasonRd();

        }

        private void rdReason3_CheckedChanged(object sender, EventArgs e)
        {
            checkReasonRd();

        }

        private void rdOther_CheckedChanged(object sender, EventArgs e)
        {
            checkReasonRd();

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdAccept.Checked || rdReject.Checked)
                {
                    string SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);

                    if (rdAccept.Checked)
                    {
                        DialogResult dr = Msgbox.Show("Are you sure you want to accept "+SelectedDoc.Rows.Count.ToString()+" selected document(s)?", "Confirmation", "Yes", "No");
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
                            crud.Executing("UPDATE dbo.tbDOC SET DOC_CUR_STATUS = 1, DOC_CUR_STATUS_SET_BY = '" + UserCode + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                            Msgbox.Show(SelectedDoc.Rows.Count.ToString()+" document(s) accepted!");
                            this.Close();
                        }
                    }
                    else if (rdReject.Checked)
                    {
                        if (rdReason1.Checked || rdReason2.Checked || rdReason3.Checked || rdOther.Checked)
                        {
                            if (rdOther.Checked && tbOther.Text.Trim() == "")
                            {
                                Msgbox.Show("Please fill in other reason.");
                                return;
                            }
                            string reason = (rdReason1.Checked) ? "Document not complete" :
                                (rdReason2.Checked) ? "No reinsurance indication" :
                                (rdReason3.Checked) ? "No fire tariff" : tbOther.Text;

                            DialogResult dr = Msgbox.Show("Are you sure you want to reject " + SelectedDoc.Rows.Count.ToString() + " selected document(s) because \"" + reason + "\"?", "Confirmation", "Yes", "No");
                            if (dr == System.Windows.Forms.DialogResult.Yes)
                            {
                                crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = "UPDATE dbo.tbDOC SET STATUS = 'C', STATUS_REMARK = @reason, DOC_CUR_STATUS = 9, DOC_CUR_STATUS_SET_BY = '" + UserCode + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                                cmd.Parameters.Add(new SqlParameter("reason", reason));
                                crud.Executing(cmd);
                                //crud.Executing("UPDATE dbo.tbDOC SET STATUS = 'C', STATUS_REMARK = '" + reason + "', DOC_CUR_STATUS = 9, DOC_CUR_STATUS_SET_BY = (SELECT USER_CODE FROM dbo.tbDOC_USER WHERE USER_NAME = '" + UserName + "' and ROLE = 'CONTROLLER'), DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                                //DataTable dtTemp = crud.LoadData("SELECT CUS_CODE,CUS_NAME,PRODUCER_NAME,DOC_TYPE FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE = '" + DocCode + "'").Tables[0];
                                //string cuscode = dtTemp.Rows[0][0].ToString(), cusname = dtTemp.Rows[0][1].ToString(), producer = dtTemp.Rows[0][2].ToString(), doctype = dtTemp.Rows[0][3].ToString();

                                cmd = new SqlCommand();
                                cmd.CommandText = "INSERT INTO dbo.tbNoti(NOTI_DETAIL, NOTI_TO, NOTI_DATE, REMARK) SELECT DOC_TYPE + ' Document of \"' + DOC_CODE + '-' + CUS_CODE + '-' + CUS_NAME + '\" has been rejected due to \"'+ @reason +'\"', (SELECT USER_NAME FROM dbo.tbDOC_USER WHERE FULL_NAME = CREATE_BY), getdate(), DOC_CODE FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
                                cmd.Parameters.Add(new SqlParameter("reason", reason));
                                crud.Executing(cmd);
                                //crud.Executing("INSERT INTO dbo.tbNoti(NOTI_DETAIL, NOTI_TO, NOTI_DATE, REMARK) SELECT DOC_TYPE + ' Document of \"' + CUS_CODE + '-' + CUS_NAME + '\" has been rejected due to \"" + reason + "\"',(SELECT USER_NAME FROM dbo.tbDOC_USER WHERE USER_CODE = PRODUCER_CODE),getdate(),DOC_CODE FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                                Msgbox.Show(SelectedDoc.Rows.Count.ToString() + " document(s) rejected!");
                                this.Close();
                            }
                        }
                        else
                        {
                            Msgbox.Show("Please choose your reject reason.");
                            return;
                        }
                    }
                }
                else
                {
                    Msgbox.Show("Please choose to accept or reject the document.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show("Error occured: " + ex.Message);
                return;
            }
        }


    }
}
