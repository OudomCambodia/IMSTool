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
    public partial class frmEditReqDocDetail : Form
    {
        private CRUD crud = new CRUD();

        private string claimNo = string.Empty;
        private DataTable dtDocReqDetail = new DataTable();

        private bool isFormClosing = false;
        private bool isSaved = false;

        public frmEditReqDocDetail(string ClaimNo, DataTable DtDocReqDetail)
        {
            InitializeComponent();

            claimNo = ClaimNo;
            dtDocReqDetail = DtDocReqDetail;
        }

        private void frmEditReqDocDetail_Load(object sender, EventArgs e)
        {
            dgvDocDetail.DataSource = dtDocReqDetail;

            dgvDocDetail.Columns["DOC_CODE"].Visible = false;
            dgvDocDetail.Columns["DOC_TYPE"].Width = 200;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var dtToSave = dgvDocDetail.DataSource as DataTable;

            if (dtToSave.Rows.Count <= 0)
                return;

            if (!isSaved)
            {
                crud.ExecNonQuery("delete from user_claim_email_doc_new where claim_number = '" + claimNo + "'");

                if (isFormClosing)
                {
                    for (int i = 0; i < dtToSave.Rows.Count; i++)
                    {
                        string docCode = dtToSave.Rows[i]["DOC_CODE"].ToString();
                        string docType = dtToSave.Rows[i]["DOC_TYPE"].ToString();
                        string docContent = dtToSave.Rows[i]["DOC_CONTENT"].ToString();

                        crud.ExecNonQuery("insert into user_claim_email_doc_new(claim_number, doc_code, doc_type, doc_content) values('" + claimNo + "', '" + docCode + "', '" + docType.Replace("'", "''") + "', '" + docContent.Replace("'", "''") + "')");
                    }
                }
                else
                {
                    DialogResult dr = Msgbox.Show("Do you want to save changes?", "Confirmation");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {
                        for (int i = 0; i < dtToSave.Rows.Count; i++)
                        {
                            string docCode = dtToSave.Rows[i]["DOC_CODE"].ToString();
                            string docType = dtToSave.Rows[i]["DOC_TYPE"].ToString();
                            string docContent = dtToSave.Rows[i]["DOC_CONTENT"].ToString();

                            crud.ExecNonQuery("insert into user_claim_email_doc_new(claim_number, doc_code, doc_type, doc_content) values('" + claimNo + "', '" + docCode + "', '" + docType.Replace("'", "''") + "', '" + docContent.Replace("'", "''") + "')");
                        }
                        Close();
                    }
                }
                isSaved = true;
            }
        }

        private void frmEditReqDocDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            if (!isSaved)
                btnSave_Click(null, null);
        }
    }
}
