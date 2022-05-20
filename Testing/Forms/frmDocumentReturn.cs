using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmDocumentReturn : Form
    {
        public frmDocumentReturn()
        {
            InitializeComponent();
        }

        public string UserID = string.Empty;
        public DataTable SelectedDoc = new DataTable();

        DBS11SqlCrud crud = new DBS11SqlCrud();

        private void frmDocumentReturn_Load(object sender, EventArgs e)
        {
            lblDetail.Text = "Document detail: " + SelectedDoc.Rows.Count + " document(s) is/are selected.";
            lblDetail.MaximumSize = new Size(450, 50);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdReason1.Checked || rdReason2.Checked)
                {

                    string remark = tbRemark.Text.Trim();
                    remark = (remark == "")?remark:"Reason: "+remark;
                    string SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);

                    if (rdReason1.Checked)
                    {
                        DialogResult dr = Msgbox.Show("Are you sure you want to return " + SelectedDoc.Rows.Count.ToString() + " selected document(s) due to DP's mistake?", "Confirmation", "Yes", "No");
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
                            crud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) SELECT DOC_CODE,getdate(),11,'" + UserID + "',getdate() FROM dbo.tbDOC WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                            crud.Executing("UPDATE dbo.tbDOC SET RETURN_REASON = 'DP', RETURN_DATE = '"+DateTime.Now+"', STATUS_REMARK = 'RETURN DOCUMENT ("+remark+")', DOC_CUR_STATUS = 2, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                            Msgbox.Show(SelectedDoc.Rows.Count.ToString() + " document(s) returned!");
                            this.Close();
                        }
                    }
                    else if (rdReason2.Checked)
                    {
                        string refid = SelectedDoc.Rows[0]["REF_ID"].ToString();
                        //DataTable dtTemp = crud.LoadData("SELECT DOC_CUR_STATUS FROM dbo.tbDOC WHERE DOC_CODE = " + refid).Tables[0];
                        //string curstatus = dtTemp.Rows[0][0].ToString();
                        //string tostastus = "2";
                        //if (curstatus == "2" || curstatus == "3") tostastus = "0";
                        ////from DP Processing and DP Processed to Submitted to UW

                        DialogResult dr = Msgbox.Show("Are you sure you want to return " + SelectedDoc.Rows.Count.ToString() + " selected document(s) due to Producer's mistake?", "Confirmation", "Yes", "No");
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {
                            crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
                            crud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) SELECT DOC_CODE,getdate(),12,'" + UserID + "',getdate() FROM dbo.tbDOC WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                            //crud.Executing("UPDATE dbo.tbDOC SET RETURN_REASON = 'PRODUCER', RETURN_DATE = '" + DateTime.Now + "', STATUS_REMARK = 'RETURN DOCUMENT (" + remark + ")', DOC_CUR_STATUS = " + tostastus + ", DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                            crud.Executing("UPDATE dbo.tbDOC SET RETURN_REASON = 'PRODUCER', RETURN_DATE = '" + DateTime.Now + "', STATUS_REMARK = 'RETURN DOCUMENT (" + remark + ")', DOC_CUR_STATUS = 0, DOC_CUR_STATUS_SET_BY = '" + UserID + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                            Msgbox.Show(SelectedDoc.Rows.Count.ToString() + " document(s) returned!");
                            this.Close();
                        }
                    }
                }
                else
                {
                    Msgbox.Show("Please choose return reason.");
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
