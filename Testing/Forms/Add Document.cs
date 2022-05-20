using Oracle.ManagedDataAccess.Client;
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
    public partial class frmDocumentDetail : Form
    {
        CRUD crud = new CRUD();
        public string doctype="ALL";
        public bool isAdd = true; //false => Edit
        public string EditDocCode = string.Empty;

        public frmDocumentDetail()
        {
            InitializeComponent();
        }

        private void Add_Document_Load(object sender, EventArgs e)
        {
            DataTable dt = crud.ExecQuery("SELECT DISTINCT PRODUCT FROM USER_CLAIM_EMAIL_DOC");
            foreach (DataRow dr in dt.Rows)
                cbProduct.Items.Add(dr[0].ToString());

            if (isAdd)
            {
                //dt = crud.ExecQuery("SELECT COUNT(DOC_CODE) FROM USER_CLAIM_EMAIL_DOC");
                dt = crud.ExecQuery("SELECT MAX(TO_NUMBER(SUBSTR(DOC_CODE,4)))+1 DOC_NUM FROM USER_CLAIM_EMAIL_DOC"); //get largest DOC CODE + 1
                tbDocCode.Text = "DOC" + dt.Rows[0][0].ToString();
                cbProduct.SelectedIndex = cbProduct.FindStringExact(doctype);
            }
            else
            {
                this.Text = "Edit Document";
                lbTitle.Text = "Edit Document";
                bnAdd.Text = "Save";
                tbDocCode.Text = EditDocCode;

                dt = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_DOC WHERE DOC_CODE = '"+EditDocCode+"'");
                cbProduct.SelectedIndex = cbProduct.FindStringExact(dt.Rows[0]["PRODUCT"].ToString());

                tbDocType.Text = dt.Rows[0]["DOC_TYPE"].ToString();
                rtbDocContent.Text = dt.Rows[0]["DOC_CONTENT"].ToString();
            }
            tbDocType.Focus();
        }

        private void bnAdd_Click(object sender, EventArgs e)
        {
            if (cbProduct.Text == "") {
                Msgbox.Show("Please select Product.");
                return;
            }
            if (tbDocType.Text == "") {
                Msgbox.Show("Please input Document Type.");
                return;
            }

            if (isAdd)
            {
                if (rtbDocContent.Text != "")
                    if (!rtbDocContent.Text.StartsWith(": "))
                        rtbDocContent.Text = ": " + rtbDocContent.Text;
                DialogResult res = Msgbox.Show("Do you want to add this document?", "Confirmation");
                if (res == System.Windows.Forms.DialogResult.No)
                    return;
                //crud.ExecNonQuery("INSERT INTO USER_CLAIM_EMAIL_DOC (DOC_TYPE,DOC_CONTENT,PRODUCT,DOC_CODE) VALUES ('" + tbDocType.Text + "','" + rtbDocContent.Text + "','" + cbProduct.Text + "','" + tbDocCode.Text + "')");
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "INSERT INTO USER_CLAIM_EMAIL_DOC (DOC_TYPE,DOC_CONTENT,PRODUCT,DOC_CODE) VALUES (:doctype,:doccontent,:protype,:doccode)";
                cmd.Parameters.Add(new OracleParameter("doctype", tbDocType.Text.Trim()));
                cmd.Parameters.Add(new OracleParameter("doccontent", rtbDocContent.Text.Trim()));
                cmd.Parameters.Add(new OracleParameter("protype", cbProduct.Text));
                cmd.Parameters.Add(new OracleParameter("doccode", tbDocCode.Text));
                crud.ExecNonQuery(cmd);
                Msgbox.Show("Document " + tbDocCode.Text + " added!");
                this.Close();
            }
            else 
            {
                DialogResult res = Msgbox.Show("Are you sure you want to edit this document?", "Confirmation");
                if (res == System.Windows.Forms.DialogResult.No)
                    return;

                //crud.ExecNonQuery("UPDATE USER_CLAIM_EMAIL_DOC SET DOC_TYPE = '" + tbDocType.Text.Trim() + "', DOC_CONTENT = '" + rtbDocContent.Text.Trim() + "' WHERE DOC_CODE = '"+tbDocCode.Text+"'");
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "UPDATE USER_CLAIM_EMAIL_DOC SET DOC_TYPE = :doctype, DOC_CONTENT = :doccontent WHERE DOC_CODE = '" + tbDocCode.Text + "'";
                cmd.Parameters.Add(new OracleParameter("doctype", tbDocType.Text.Trim()));
                cmd.Parameters.Add(new OracleParameter("doccontent", rtbDocContent.Text.Trim()));
                crud.ExecNonQuery(cmd);
                Msgbox.Show("Document " + tbDocCode.Text + " updated!");
                this.Close();
            }
        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
