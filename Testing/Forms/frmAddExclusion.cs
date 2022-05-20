using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmAddExclusion : Form
    {
        public frmAddExclusion()
        {
            InitializeComponent();
        }

        CRUD crud = new CRUD();
        public string[][] rtbTextDetail = new string[1][];
        string htmlscript = string.Empty;
        public string exclutype = string.Empty;
        public bool AddExclu = true;
        public bool EditExclu = false;
        public string EditExcluCode = string.Empty;

        private void frmAddExclusion_Load(object sender, EventArgs e)
        {
            DataTable dt = crud.ExecQuery("SELECT DISTINCT PRODUCT FROM USER_CLAIM_EMAIL_EXCLUS");
                foreach(DataRow dr in dt.Rows)
                cbExcluType.Items.Add(dr[0].ToString());

            if (AddExclu)
            {
                //dt = crud.ExecQuery("SELECT COUNT(EXCL_CODE) FROM USER_CLAIM_EMAIL_EXCLUS");
                dt = crud.ExecQuery("SELECT MAX(TO_NUMBER(SUBSTR(EXCL_CODE,4)))+1 EXCLU_NUM FROM USER_CLAIM_EMAIL_EXCLUS"); //get the largest EXCLU CODE + 1
                tbExcluCode.Text = "EXC" + dt.Rows[0][0].ToString();
                cbExcluType.SelectedIndex = cbExcluType.FindStringExact(exclutype);
                bnCurrent.Visible = false;
            }
            else if (EditExclu)
            {
                this.Text = "Edit Exclusion";
                lbTitle.Text = "Edit Exclusion";
                bnAdd.Text = "Save";

                dt = crud.ExecQuery("SELECT * FROM USER_CLAIM_EMAIL_EXCLUS WHERE EXCL_CODE = '"+EditExcluCode+"'");
                tbExcluCode.Text = dt.Rows[0]["EXCL_CODE"].ToString();
                cbExcluType.SelectedIndex = cbExcluType.FindStringExact(dt.Rows[0]["PRODUCT"].ToString());

                string excluHTML = string.Empty;
                using (StreamReader reader = new StreamReader("Html/EmailContent.html"))
                {
                    excluHTML = reader.ReadToEnd();
                }
                excluHTML = excluHTML.Replace("{text}", dt.Rows[0]["EXCLUSION_HTML"].ToString());
                trickBrowser.DocumentText = excluHTML;
                rtbExcluDetail.Text = "";   
            }

            rtbExcluDetail.Focus();
        }

        private void bnBold_Click(object sender, EventArgs e)
        {
            if (rtbExcluDetail.Text == "") return;
            RichTextBoxExtension.ToggleBold(rtbExcluDetail);
        }

        private void bnItalic_Click(object sender, EventArgs e)
        {
            if (rtbExcluDetail.Text == "") return;
            RichTextBoxExtension.ToggleItalic(rtbExcluDetail);
        }

        private void bnUnderline_Click(object sender, EventArgs e)
        {
            if (rtbExcluDetail.Text == "") return;
            RichTextBoxExtension.ToggleUnderline(rtbExcluDetail);
        }
        
        private void bnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbExcluType.Text == "")
                {
                    Msgbox.Show("Please select Exclusion Type.");
                    return;
                }
                if (rtbExcluDetail.Text == "")
                {
                    Msgbox.Show("Please input Exclusion Detail.");
                    return;
                }

                if (AddExclu)
                {

                    DialogResult res = Msgbox.Show("Do you want to add this exclusion?", "Confirmation");
                    if (res == System.Windows.Forms.DialogResult.No)
                        return;

                    Array.Resize(ref rtbTextDetail, 1);       //reset size of array rtbTextDetail
                    htmlscript = string.Empty;  //reset htmlscript
                    RichTextBoxExtension.generateFormatCode(rtbExcluDetail, ref rtbTextDetail);
                    htmlscript = RichTextBoxExtension.groupFormatCode(ref rtbTextDetail);
                    //crud.ExecNonQuery("INSERT INTO USER_CLAIM_EMAIL_EXCLUS (PRODUCT,EXCLUSION,EXCL_CODE,EXCLUSION_HTML) VALUES ('" + cbExcluType.Text + "','" + rtbExcluDetail.Text + "','" + tbExcluCode.Text + "','" + htmlscript + "')");
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = "INSERT INTO USER_CLAIM_EMAIL_EXCLUS (PRODUCT,EXCLUSION,EXCL_CODE,EXCLUSION_HTML) VALUES (:exclutype,:excludetail,:exclucode,:html)";
                    cmd.Parameters.Add(new OracleParameter("exclutype", cbExcluType.Text));
                    cmd.Parameters.Add(new OracleParameter("excludetail", rtbExcluDetail.Text));
                    cmd.Parameters.Add(new OracleParameter("exclucode", tbExcluCode.Text));
                    cmd.Parameters.Add(new OracleParameter("html", htmlscript));
                    crud.ExecNonQuery(cmd);
                    Msgbox.Show("Exclusion "+tbExcluCode.Text+" added!");
                    this.Close();
                }
                else if (EditExclu)
                {
                    DialogResult res = Msgbox.Show("Are you sure you want to edit this exclusion?", "Confirmation");
                    if (res == System.Windows.Forms.DialogResult.No)
                        return;

                    Array.Resize(ref rtbTextDetail, 1);       //reset size of array rtbTextDetail
                    htmlscript = string.Empty;  //reset htmlscript
                    RichTextBoxExtension.generateFormatCode(rtbExcluDetail, ref rtbTextDetail);
                    htmlscript = RichTextBoxExtension.groupFormatCode(ref rtbTextDetail);
                    //crud.ExecNonQuery("UPDATE USER_CLAIM_EMAIL_EXCLUS SET EXCLUSION = '"+rtbExcluDetail.Text + "', EXCLUSION_HTML = '"  + htmlscript + "' WHERE EXCL_CODE = '"+tbExcluCode.Text+"'");
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = "UPDATE USER_CLAIM_EMAIL_EXCLUS SET EXCLUSION = :excludetail, EXCLUSION_HTML = :html WHERE EXCL_CODE = '" + tbExcluCode.Text + "'";
                    cmd.Parameters.Add(new OracleParameter("excludetail", rtbExcluDetail.Text));
                    cmd.Parameters.Add(new OracleParameter("html", htmlscript));
                    crud.ExecNonQuery(cmd);
                    Msgbox.Show("Exclusion " + tbExcluCode.Text + " updated!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }


        }

        private void bnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bnCurrent_Click(object sender, EventArgs e)
        {
            rtbExcluDetail.Text = "";
            trickBrowser.Document.ExecCommand("SelectAll", false, null);
            trickBrowser.Document.ExecCommand("Copy", false, null);
            rtbExcluDetail.Paste();  
        }

    }
}
