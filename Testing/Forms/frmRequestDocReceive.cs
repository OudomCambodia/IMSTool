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
    public partial class frmRequestDocReceive : Form
    {
        private CRUD crud = new CRUD();
        private string claimNo = string.Empty;
        private DataTable dtReqDoc = new DataTable();
        private DataTable dtReqDocHist = new DataTable();

        public frmRequestDocReceive(string ClaimNo, DataTable DtReqDoc, DataTable DtReqDocHist)
        {
            InitializeComponent();

            claimNo = ClaimNo;
            dtReqDoc = DtReqDoc;
            dtReqDocHist = DtReqDocHist;
        }

        private void frmRequestDocReceive_Load(object sender, EventArgs e)
        {
            //setting check box for ListView
            lvDoc.CheckBoxes = true;
            lvDoc.View = View.Details;

            lvDoc.Columns.Add("Doc Code", 70);
            lvDoc.Columns.Add("Doc Type", 170);
            lvDoc.Columns.Add("Doc Detail", 330);

            string[] reqDocCodes = { };
            var reqDoc = string.Empty;
            var reqDocCode = dtReqDocHist.Rows[0][0].ToString();
            if (reqDocCode.Contains(","))
            {
                reqDocCodes = reqDocCode.Split(',');

                for (int i = 0; i < reqDocCodes.Count(); i++)
                {
                    reqDoc += "'" + reqDocCodes[i] + "',";
                }
                reqDoc = reqDoc.Remove(reqDoc.Length - 1);

                var qBuilder = new StringBuilder();
                qBuilder.Append("select doc_type, doc_content, doc_code ")
                    .Append("from user_claim_email_doc_new ")
                    .AppendFormat("where doc_code in ({0}) and claim_number = '{1}'", reqDoc, claimNo);

                DataTable dtDoc = crud.ExecQuery(qBuilder.ToString());
                SetReqDoc(dtDoc);
            }
            else
            {
                var qBuilder = new StringBuilder();
                qBuilder.Append("select doc_type, doc_content, doc_code ")
                    .Append("from user_claim_email_doc_new ")
                    .AppendFormat("where doc_code = '{0}' and claim_number = '{1}'", reqDocCode, claimNo);

                DataTable dtDoc = crud.ExecQuery(qBuilder.ToString());
                SetReqDoc(dtDoc);
            }
        }

        private void SetReqDoc(DataTable dtDoc)
        {
            foreach (DataRow dr in dtDoc.Rows)
            {
                ListViewItem lvi = new ListViewItem(dr["DOC_CODE"].ToString());
                lvi.SubItems.Add(dr["DOC_TYPE"].ToString());
                lvi.SubItems.Add(dr["DOC_CONTENT"].ToString());

                lvDoc.Items.Add(lvi);

                var reqDoc = dtReqDoc.Rows[0][0].ToString();
                if (reqDoc.Contains(","))
                {
                    var reqDocs = reqDoc.Split(',');
                    for (int i = 0; i < reqDocs.Count(); i++)
                    {
                        if (dr["DOC_CODE"].ToString().Equals(reqDocs[i].ToString()))
                        {
                            lvi.Checked = true;
                        }
                    }
                }
                else
                {
                    if (dr["DOC_CODE"].ToString().Equals(reqDoc))
                    {
                        lvi.Checked = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var docCode = string.Empty;
            for (int i = 0; i < lvDoc.CheckedItems.Count; i++)
            {
                docCode += lvDoc.CheckedItems[i].Text + ",";
            }
            if (docCode.Length > 0)
                docCode = docCode.Remove(docCode.Length - 1);

            DialogResult confirmSave = Msgbox.Show("Are you sure you want to update?", "Confirmation");
            if (confirmSave == DialogResult.Yes)
            {
                crud.ExecNonQuery("update user_claim_anh_auto_rem_email set doc_code = '" + docCode + "' where claim_number = '" + claimNo + "'");

                crud.ExecNonQuery("delete from user_claim_anh_doc_rec_detail where claim_no = '" + claimNo + "'");
                foreach (ListViewItem item in lvDoc.Items)
                {
                    if (!item.Checked)
                    {
                        var qBuilder = new StringBuilder();
                        qBuilder.Append("insert into user_claim_anh_doc_rec_detail(claim_no, doc_code, rec_date, rec_user) values( ")
                            .AppendFormat("'{0}','{1}','{2}','{3}')", claimNo, item.Text, DateTime.Now.ToString("dd-MMM-yy"), frmLogIn.Usert);

                        crud.ExecNonQuery(qBuilder.ToString());
                    }
                }
                Msgbox.Show("Doc Request successfully updated.");
                Close();
            }
        }
    }
}
