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
            lblKHM.Text = Convert.ToInt32(dtInvoicefrm2.Rows[0][21].ToString().ToUpper()).ToString("N").Replace(".00", "");
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
        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

        
        
    }
}
