using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class BreakdownInvoice : Form 
    {
        public string Username = "Admin";
        public  DataTable dtInvoice;
        CRUD crud = new CRUD();
        public BreakdownInvoice()
        {
            InitializeComponent();
        }

        private void BreakdownInvoice_Load(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbInvoiceNo.Text != "")
                {
                    dtInvoice = crud.ExecQuery("select p.*,rate from user_exchange_rate,view_print_invoice p where TRAN_DATE = ON_DATE and DEBIT_NOTE='" + tbInvoiceNo.Text.ToUpper() + "'");
                    if (dtInvoice.Rows.Count == 0)
                    {
                        Msgbox.Show("Debit / Credit Note not found!");
                    }
                    else
                    {

                        dtInvoice.Columns.Add("EXCHANGE_RATE", typeof(System.String));
                        dtInvoice.Columns.Add("TOTAL_FUND_KH", typeof(System.String));
                        dtInvoice.Columns.Add("KH_NAME", typeof(System.String));
                        dtInvoice.Columns.Add("KH_ADDR", typeof(System.String));

                        InvoiceOption1 Invoice1 = new InvoiceOption1(this);
                        Invoice1.dtInvoicefrm2 = this.dtInvoice;
                        Invoice1.Show();
                    }

                }
                else
                {
                    Msgbox.Show("Debit / Credit note is required");
                }
                

            }catch(Exception ex){
                Msgbox.Show(ex.Message);
            }
            

        }

        
    }
}
