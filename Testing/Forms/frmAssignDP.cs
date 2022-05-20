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
    public partial class frmAssignDP : Form
    {

        public string UserName = string.Empty, ProLine = string.Empty, UserCode = string.Empty;
        public bool ReAssign = false;
        public DataTable SelectedDoc = new DataTable();
        
        DBS11SqlCrud crud = new DBS11SqlCrud();

        public frmAssignDP()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void frmAssignDP_Load(object sender, EventArgs e)
        {
            //if (SelectedDoc.Rows.Count > 0)
            //{
            //    ProLine = frmAddDocument1.product[SelectedDoc.Rows[0]["PRODUCT_TYPE"].ToString()];

            //    for (int i = 0; i < SelectedDoc.Rows.Count; i++)
            //    {
            //        string ProLinetmp = frmAddDocument1.product[SelectedDoc.Rows[i]["PRODUCT_TYPE"].ToString()];
            //        if (ProLine != ProLinetmp)
            //        {
            //            Msgbox.Show("The selected document(s) not belong in the same product line.");
            //            this.Close();
            //        }
            //    }
            //}


            //DataTable dtTemp = crud.LoadData("SELECT USER_CODE,FULL_NAME FROM dbo.tbDOC_USER WHERE TEAM = '" + ProLine + "'").Tables[0];
            DataTable dtTemp = crud.LoadData("SELECT USER_CODE,FULL_NAME FROM dbo.tbDOC_USER WHERE ROLE like '%DP%'").Tables[0];
            for (int i = 0; i < dtTemp.Rows.Count; i++)
                cbDP.Items.Add(new ComboboxItem(dtTemp.Rows[i][1].ToString(),dtTemp.Rows[i][0].ToString()));
            cbDP.SelectedIndex = -1;

            this.Text = (ReAssign) ? "Re-assign DP" : "Assign DP";
            lbTitle.Text = this.Text;

            lblDetail.Text = "Document detail: " + SelectedDoc.Rows.Count + " document(s) is/are selected.";

            lblDetail.MaximumSize = new Size(450, 50);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbDP.Text == "")
            {
                Msgbox.Show("Plese select DP!");
                return;
            }

            string AssignOrReassign = (ReAssign) ? "re-assign" : "assign" ,
                SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);

            DialogResult dr = Msgbox.Show("Are you sure you want to " + AssignOrReassign + " " + cbDP.Text + " to process " + SelectedDoc.Rows.Count.ToString() + " selected document(s)?", "Confirmation", "Yes", "No");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                if (ReAssign)
                    crud.Executing("UPDATE dbo.tbDOC SET DP_CODE = '" + (cbDP.SelectedItem as ComboboxItem).Value.ToString() + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                else
                {
                    crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
                    crud.Executing("UPDATE dbo.tbDOC SET DP_CODE = '" + (cbDP.SelectedItem as ComboboxItem).Value.ToString() + "', DOC_CUR_STATUS = 2, DOC_CUR_STATUS_SET_BY = '" + UserCode + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
                }
                Msgbox.Show("DP "+AssignOrReassign+"ed!");
                this.Close();
            }
            
        }
    }
}
