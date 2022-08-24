using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class InvoiceOption1 : Form
    {
        public BreakdownInvoice mainBi = new BreakdownInvoice();
        public DataTable dtInvoicefrm2 ;
        CRUD crud = new CRUD();
        public InvoiceOption1(BreakdownInvoice obj)
        {
            InitializeComponent();
        }

        private void InvoiceOption1_Load(object sender, EventArgs e)
        {
            // DataTable dtlblKh = crud.ExecQuery("select rate from user_exchange_rate,view_print_invoice p where TRAN_DATE = ON_DATE and DEBIT_NOTE='"+  dtInvoicefrm2.Rows[0][4].ToString().ToUpper() + "'");
            if (dtInvoicefrm2.Rows[0][21].ToString() =="")
                lblKHM.Visible = false;
            else
            lblKHM.Text = Convert.ToInt32(dtInvoicefrm2.Rows[0][21].ToString().ToUpper()).ToString("N").Replace(".00", "");
            
            //lblKHM.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(dtInvoicefrm2.Rows[0][21].ToString()) ? 0.00 : Convert.ToDouble(dtInvoicefrm2.Rows[0][21].ToString())));
            lblPolicyNo.Text = dtInvoicefrm2.Rows[0][8].ToString().ToUpper();
            lbProduct.Text = dtInvoicefrm2.Rows[0][9].ToString().ToUpper();
            rbInsured.Text = dtInvoicefrm2.Rows[0][0].ToString().ToUpper();
            lblAccCode.Text = dtInvoicefrm2.Rows[0][6].ToString().ToUpper();
            lblInvoiceDate.Text = dtInvoicefrm2.Rows[0][5].ToString().ToUpper();
            lblInvoiceNo.Text = dtInvoicefrm2.Rows[0][4].ToString().ToUpper();
            lblAcc.Text = dtInvoicefrm2.Rows[0][7].ToString().ToUpper();
            lblEffective.Text = dtInvoicefrm2.Rows[0][10].ToString().ToUpper();
            //lblTotalSI.Text =  Convert.ToInt32( dtInvoicefrm2.Rows[0][11].ToString().ToUpper()).ToString("N").Replace(".00","");
            lblVat.Text = dtInvoicefrm2.Rows[0][2].ToString().ToUpper();
            lblTel.Text = dtInvoicefrm2.Rows[0][3].ToString().ToUpper();
            lbDNNo.Text = dtInvoicefrm2.Rows[0][4].ToString().ToUpper();
            rtbAddress.Text = dtInvoicefrm2.Rows[0][1].ToString().ToUpper();
        }

        #region LoadInvoiceData
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(String.IsNullOrEmpty(tbAdminFee.Text)) != 0 || tbAdminFee.Text == "")
                tbTotalDue.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text) ? 0.00 : Convert.ToDouble(tbGross.Text)));
            else
                tbTotalDue.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text)  ? 0.00 : Convert.ToDouble(tbGross.Text) + Convert.ToDouble(tbAdminFee.Text)));
        }

        private void tbGross_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
        {
            e.Handled = true;
        }
        }

        private void tbGross_Leave(object sender, EventArgs e)
        {
            tbGross.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text) ? 0.00 : Convert.ToDouble(tbGross.Text)));
        }

        private void tbAdminFee_TextChanged(object sender, EventArgs e)
        {
            //tbTotalDue.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text) || String.IsNullOrEmpty(tbAdminFee.Text) ? 0.00 : Convert.ToDouble(tbGross.Text) + Convert.ToDouble(tbAdminFee.Text)));
            if (Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text)) != 0 || tbGross.Text == "")
                tbTotalDue.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text) ? 0.00 : Convert.ToDouble(tbAdminFee.Text)));
            else
                tbTotalDue.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text) ? 0.00 : Convert.ToDouble(tbGross.Text) + Convert.ToDouble(tbAdminFee.Text)));
        }

        private void tbAdminFee_Leave(object sender, EventArgs e)
        {
            tbAdminFee.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbAdminFee.Text) ? 0.00 : Convert.ToDouble(tbAdminFee.Text)));
            //tbGross.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(tbGross.Text) ? Convert.ToInt32("") : Convert.ToDouble(tbGross.Text) + Convert.ToDouble(tbAdminFee.Text)));
          
        }

        private void tbAdminFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }
        private void txtTotalSI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }

        private void txtTotalSI_TextChanged(object sender, EventArgs e)
        {
            txtTotalSI.Text = String.Format("{0:N}", Convert.ToDecimal(String.IsNullOrEmpty(txtTotalSI.Text) ? 0.00 : Convert.ToDouble(txtTotalSI.Text)));

        }

        private void tbTotalDue_TextChanged(object sender, EventArgs e)
        {
            decimal a = Convert.ToDecimal(tbGross.Text);
            decimal b = Convert.ToDecimal(lblKHM.Text);
            decimal c = Math.Round(a * b, 2);
            lblKhTotal.Text = String.Format("{0:N}", c);
        }


        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Invoice Option One
            DialogResult dr_msg = Msgbox.Show("Would you like to issue breakdown invoice?", "", "Yes", "No");
            if (dr_msg == System.Windows.Forms.DialogResult.No)
            {
                Cursor.Current = Cursors.AppStarting;
                return;
            }
            else
            {
                string note = dtInvoicefrm2.Rows[0][4].ToString().ToUpper(); // debit or credit note
                DataTable chkInvoice = crud.ExecQuery("SELECT * FROM VIEW_PRINT_INVOICE WHERE DEBIT_NOTE = '" + dtInvoicefrm2.Rows[0][4].ToString().ToUpper() + "'");
                if (chkInvoice.Rows.Count > 0)
                {
                    ReportClass rpt = new ReportClass();

                    if (note[0] == 'D') //Debit Note
                    {
                        string accountcode = dtInvoicefrm2.Rows[0][6].ToString().ToUpper();
                        string producer = accountcode.Split('/')[1], cuscode = accountcode.Split('/')[2];
                        DataTable dtTemp = crud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = '" + producer + "' OR CODE = '" + cuscode + "'");
                        if (dtTemp.Rows.Count > 0) //has payment instruction set
                        {
                            ////check for N/A
                            DataRow[] ToDelete = dtTemp.Select("BANK_NAME = 'N/A'"); //create this cuz cannot modify data directly in foreach
                            foreach (DataRow dr in ToDelete)
                            {
                                dtTemp.Rows.Remove(dr); //remove N/A
                            }
                            //

                            if (dtTemp.Rows.Count == 0) //that's mean has only N/A record => use NewInvoice for NA
                            {
                                if (chkInvoice.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                                {
                                    rpt = new Reports.NewInvoiceNAEndo();
                                    rpt.SetDataSource(chkInvoice);
                                }
                                else
                                {
                                    rpt = new Reports.NewInvoiceNABreakdown();
                                    rpt.SetDataSource(chkInvoice);
                                }
                            }
                            else //after remove N/A still has other bank records => use NewInvoice with Payment instruction bank table
                            {
                                dtTemp.Columns.Add("DEBIT_NOTE", typeof(System.String)); //Add in order to link to another table in Report
                                foreach (DataRow dr in dtTemp.Rows)
                                {
                                    dr["DEBIT_NOTE"] = chkInvoice.Rows[0]["DEBIT_NOTE"].ToString();
                                }
                                DataSet ds = new DataSet();
                                chkInvoice.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                                dtTemp.TableName = "PAYMENT_INSTRUCTION";
                                ds.Tables.Add(chkInvoice);
                                ds.Tables.Add(dtTemp);

                                if (chkInvoice.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                                {
                                    rpt = new Reports.NewInvoiceEndo();
                                    rpt.SetDataSource(ds);
                                }
                                else
                                {
                                    rpt = new Reports.NewInvoice();
                                    rpt.SetDataSource(ds);
                                }
                            }
                        }
                        else
                        {
                            if (chkInvoice.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                            {
                                rpt = new Reports.PrintInvoiceEndListAll();
                                rpt.SetDataSource(chkInvoice);
                            }
                            else
                            {
                                rpt = new Reports.NewInvoiceNABreakdown();
                                rpt.SetDataSource(chkInvoice);
                            }
                        }
                        var frm = new frmViewInstructionNote();
                        frm.Text = "Invoice";
                        frm.rpt = rpt;
                        frm.Show();
                    }
                }
            }
        }

       
        
        
    }
}
