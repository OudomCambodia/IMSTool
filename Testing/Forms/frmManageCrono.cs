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
    public partial class frmManageCrono : Form
    {
        public frmManageCrono()
        {
            InitializeComponent();
        }

        public static string selectedCusCode = string.Empty, selectedCusName = string.Empty;
        DBS11SqlCrud crud = new DBS11SqlCrud();

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        void frmClose(object sender, FormClosedEventArgs e)
        {
            if (selectedCusCode != "")
                tbCusCode.Text = selectedCusCode;
            if (selectedCusName != "")
                tbCusName.Text = selectedCusName;

            if (selectedCusCode.Trim() != "")
            {
                DataTable dtTemp = crud.LoadData("SELECT CRONO FROM dbo.tbCrono WHERE CUS_CODE = '" + selectedCusCode + "'").Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    tbCrono.Text = dtTemp.Rows[0][0].ToString();
                }
            }
        }

        private void btnSelectCus_Click(object sender, EventArgs e)
        {
            selectedCusCode = "";
            selectedCusName = "";
            frmSelectCustomer frm = new frmSelectCustomer();
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.ShowDialog();
        }

        private void tbCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                selectedCusCode = "";
                selectedCusName = "";
                frmSelectCustomer frm = new frmSelectCustomer();
                frm.FormClosed += new FormClosedEventHandler(frmClose);
                frm.ShowDialog();
            }
        }

        private void tbCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                selectedCusCode = "";
                selectedCusName = "";
                frmSelectCustomer frm = new frmSelectCustomer();
                frm.FormClosed += new FormClosedEventHandler(frmClose);
                frm.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string cuscode = tbCusCode.Text.Trim(), cusname = tbCusName.Text.Trim(), crono = tbCrono.Text.Trim().ToUpper();

            if (cuscode == "" || cusname == "")
            {
                Msgbox.Show("Please select Customer.");
                tbCusCode.Focus();
                return;
            }

            if (crono == "")
            {
                Msgbox.Show("Please input Crono No");
                tbCrono.Focus();
                return;
            }

            DialogResult dr = Msgbox.Show("Are you sure you want to save change the Crono No for \"" + cuscode + "-" + cusname + "\" to " + crono + "?", "Confirmation", "Yes", "No");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                DataTable dtTemp = crud.LoadData("SELECT CUS_CODE FROM dbo.tbCrono WHERE CUS_CODE = '" + cuscode + "'").Tables[0];
                if (dtTemp.Rows.Count > 0)//exist
                {
                    crud.Executing("UPDATE dbo.tbCrono SET CRONO = '" + crono + "' WHERE CUS_CODE = '" + cuscode + "'");
                }
                else
                {
                    crud.Executing("INSERT INTO dbo.tbCrono(CUS_CODE, CRONO) VALUES('" + cuscode + "','" + crono + "')");
                }
                crud.Executing("UPDATE dbo.tbDOC SET CRONO_NO = '" + crono + "' WHERE CUS_CODE = '" + cuscode + "' AND DOC_CUR_STATUS = 7");
                Msgbox.Show("Crono No saved!");
                this.Close();
            }

        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            CRUD oracleDB = new CRUD();
            Cursor.Current = Cursors.WaitCursor;
            DataTable result = crud.LoadData("SELECT * FROM dbo.VIEW_CRONO").Tables[0];
            result.Columns.Add("CRIN", typeof(System.String));
            foreach (DataRow row in result.Rows)
            {
                row["CRIN"] = oracleDB.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
            }
            My_DataTable_Extensions.ExportToExcel(result, "");
            Cursor.Current = Cursors.AppStarting;
        }

        private void tbCusCode_Leave(object sender, EventArgs e)
        {
            CRUD maincrud = new CRUD();
            tbCusCode.Text = tbCusCode.Text.Trim().ToUpper();
            DataTable dtTemp = maincrud.ExecQuery("SELECT CUS_CODE, nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) CUS_NAME FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + tbCusCode.Text + "'");
            if (dtTemp.Rows.Count > 0)
            {
                tbCusName.Text = dtTemp.Rows[0]["CUS_NAME"].ToString();

                dtTemp = crud.LoadData("SELECT CRONO FROM dbo.tbCrono WHERE CUS_CODE = '" + selectedCusCode + "'").Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    tbCrono.Text = dtTemp.Rows[0][0].ToString();
                }
            }
        }
    }
}
