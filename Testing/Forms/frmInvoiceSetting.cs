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
    public partial class frmInvoiceSetting : Form
    {
        CRUD crud = new CRUD();
        public static string selectedCusCode = string.Empty, selectedCusName = string.Empty;

        public frmInvoiceSetting()
        {
            InitializeComponent();
        }

        private void frmInvoiceSetting_Load(object sender, EventArgs e)
        {
            tbExchangeRate.Text = "";

            DateTime On = dtpOn.Value.Date;

            try
            {

                DataTable dtTemp = crud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + On.ToString("dd-MMM-yyyy") + "'");
                if (dtTemp.Rows.Count > 0)
                {
                    tbExchangeRate.Text = dtTemp.Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ExchangeRate = tbExchangeRate.Text.Trim();
                DateTime On = dtpOn.Value.Date;
                if (ExchangeRate == "")
                {
                    Msgbox.Show("Please input Exchange Rate.");
                    return;
                }

                DialogResult dr = Msgbox.Show("Are you sure you want to save exchange rate on " + On.ToString("dd-MMM-yyyy") + " for 1 Dollar = " + tbExchangeRate.Value + " Riels?"
                    , "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dtTemp = crud.ExecQuery("SELECT * FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + On.ToString("dd-MMM-yyyy") + "'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        crud.ExecNonQuery("UPDATE USER_EXCHANGE_RATE SET RATE = " + tbExchangeRate.Value + " WHERE ON_DATE = '" + On.ToString("dd-MMM-yyyy") + "'");
                    }
                    else
                    {
                        crud.ExecNonQuery("INSERT INTO USER_EXCHANGE_RATE(ON_DATE,RATE) VALUES('" + On.ToString("dd-MMM-yyyy") + "'," + tbExchangeRate.Value + ")");
                    }

                    Msgbox.Show("Exchange Rate Saved.");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void dtpOn_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime On = dtpOn.Value.Date;
                DataTable dtTemp = crud.ExecQuery("SELECT * FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + On.ToString("dd-MMM-yyyy") + "'");
                if (dtTemp.Rows.Count > 0)
                {
                    tbExchangeRate.Value = Convert.ToDecimal(dtTemp.Rows[0]["RATE"]);
                }
                else
                {
                    tbExchangeRate.Text = "";
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectCus_Click(object sender, EventArgs e)
        {
            selectedCusCode = "";
            selectedCusName = "";
            frmSelectCustomer frm = new frmSelectCustomer();
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.ShowDialog();
        }

        void frmClose(object sender, FormClosedEventArgs e)
        {
            if (selectedCusCode != "")
                tbCusCode.Text = selectedCusCode;
            if (selectedCusName != "")
                tbCusName.Text = selectedCusName;

            if (selectedCusCode.Trim() != "")
            {
                DataTable dtTemp = crud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = '" + selectedCusCode + "'");
                if (dtTemp.Rows.Count > 0)
                {
                    tbName.Text = dtTemp.Rows[0]["CUS_NAME"].ToString();
                    tbAddr.Text = dtTemp.Rows[0]["CUS_ADDR"].ToString();
                }
            }
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

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string cuscode = tbCusCode.Text.Trim(), cusname = tbCusName.Text.Trim();

                if (cuscode == "" || cusname == "")
                {
                    Msgbox.Show("Please select Customer.");
                    tbCusCode.Focus();
                    return;
                }

                string khname = tbName.Text.Trim(), khaddr = tbAddr.Text.Trim();

                if (khname == "" && khaddr == "")
                {
                    Msgbox.Show("Both Name and Address are empty, no change to save.");
                    return;
                }

                DialogResult dr = Msgbox.Show("Are you sure you want to save customer khmer detail?"
                 , "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    DataTable dtTemp = crud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = '" + cuscode + "'");
                    if (dtTemp.Rows.Count > 0)
                    {
                        crud.ExecNonQuery("UPDATE USER_CUS_KH_DETAIL SET CUS_NAME = N'" + khname + "',CUS_ADDR = N'" + khaddr + "' WHERE CUS_CODE = '" + cuscode + "'");
                    }
                    else
                    {
                        crud.ExecNonQuery("INSERT INTO USER_CUS_KH_DETAIL(CUS_CODE,CUS_NAME,CUS_ADDR) VALUES('" + cuscode + "',N'" + khname + "',N'"+khaddr+"')");
                    }

                    Msgbox.Show("Customer Detail Saved.");
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void tbCusCode_Leave(object sender, EventArgs e)
        {
            CRUD maincrud = new CRUD();
            tbCusCode.Text = tbCusCode.Text.Trim().ToUpper();
            DataTable dtTemp = maincrud.ExecQuery("SELECT CUS_CODE, nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) CUS_NAME FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + tbCusCode.Text + "'");
            if (dtTemp.Rows.Count > 0)
            {
                tbCusName.Text = dtTemp.Rows[0]["CUS_NAME"].ToString();

                dtTemp = crud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = '" + selectedCusCode + "'");
                if (dtTemp.Rows.Count > 0)
                {
                    tbName.Text = dtTemp.Rows[0]["CUS_NAME"].ToString();
                    tbAddr.Text = dtTemp.Rows[0]["CUS_ADDR"].ToString();
                }
            }
        }
    }
}
