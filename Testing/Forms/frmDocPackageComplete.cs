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
    public partial class frmDocPackageComplete : Form
    {
        public string UserName = string.Empty, UserCode = string.Empty;
        public DataTable SelectedDoc = new DataTable();
        DBS11SqlCrud crud = new DBS11SqlCrud();
        string CusCode = string.Empty;
        bool cronohistExist = false;

        public frmDocPackageComplete()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void frmDocPackageComplete_Load(object sender, EventArgs e)
        {
            string SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);
            tbDocID.Text = SelectedDocCode;

            CusCode = crud.LoadData("SELECT TOP 1 CUS_CODE FROM dbo.tbDOC WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))").Tables[0].Rows[0][0].ToString();
            //DataTable dtTemp = crud.LoadData("SELECT CRONO_NO, CREATE_DATE FROM dbo.tbDOC WHERE CUS_CODE = '" + CusCode + "' order by CREATE_DATE desc").Tables[0];
            DataTable dtTemp = crud.LoadData("SELECT CRONO FROM dbo.tbCrono WHERE CUS_CODE = '" + CusCode + "'").Tables[0];
            if (dtTemp.Rows.Count > 0)
            {
                tbCrono.Text = dtTemp.Rows[0][0].ToString();
                cronohistExist = true;
            }
            tbCrono.Focus();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            //if (tbCrono.Text != "")
            //{
                DialogResult dr = Msgbox.Show("Are you sure " + SelectedDoc.Rows.Count + " selected document(s) now done packaging and put in Crono \""+tbCrono.Text.ToUpper() +"\"?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", tbDocID.Text);
                    //crud.Executing("UPDATE dbo.tbDOC SET STATUS = 'C', STATUS_REMARK = 'DONE', FILLING_CODE = (SELECT USER_CODE FROM dbo.tbDOC_USER WHERE USER_NAME = '" + UserName + "' and ROLE = 'FILLING'), CRONO_NO = '" + tbCrono.Text.Trim().ToUpper() + "', FILLING_REMARK = '" + tbRemark.Text.Trim() + "', DOC_CUR_STATUS = 7, DOC_CUR_STATUS_SET_BY = (SELECT USER_CODE FROM dbo.tbDOC_USER WHERE USER_NAME = '" + UserName + "' and ROLE = 'FILLING'), DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + tbDocID.Text + "',','))");
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE dbo.tbDOC SET FILLING_CODE = '" + UserCode + "', CRONO_NO = @crono, FILLING_REMARK = @remark, DOC_CUR_STATUS = 6, DOC_CUR_STATUS_SET_BY = '" + UserCode + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + tbDocID.Text + "',','))";
                    cmd.Parameters.Add(new SqlParameter("crono", tbCrono.Text.Trim().ToUpper()));
                    cmd.Parameters.Add(new SqlParameter("remark", tbRemark.Text.Trim()));
                    crud.Executing(cmd);

                    if (!cronohistExist)
                    {
                        crud.Executing("INSERT INTO dbo.tbCrono(CUS_CODE, CRONO) VALUES('" + CusCode + "','" + tbCrono.Text.ToUpper() + "')");
                    }
                    
                    Msgbox.Show(SelectedDoc.Rows.Count+" selected document(s) packaging completed!");
                    this.Close();
                }
            //}
            //else Msgbox.Show("Please fill in Crono Number."); --Temp Close
        }


    }
}
