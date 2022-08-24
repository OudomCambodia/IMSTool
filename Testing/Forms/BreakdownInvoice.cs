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
            CommonFunctions.HighLightGrid(dgvInvoiceDetails);
            this.dgvInvoiceDetails.ForeColor = System.Drawing.Color.Black;
            this.dgvInvoiceDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnOptionI_Click(object sender, EventArgs e)
        {
            dgvInvoiceDetails.Columns.Clear();
            string[] dgvClName = { "INSURED", "SUMINSURED", "GROSSPREMIUM", "ADMINFEE" };
            loadOption(dgvClName,4);
            
        }
        void loadOption(string[] a,int columnno)
        {
            for (int i = 0; i < columnno;i++ )
            {
                dgvInvoiceDetails.Columns.Add("cl"+a[i], a[i]);
            }
            
        }

        private void btnOptionII_Click(object sender, EventArgs e)
        {
            dgvInvoiceDetails.Columns.Clear();
            string[] dgvClName = { "INSURED", "ADDRESS", "NUMBER", "SUMINSURED", "GROSSPREMIUM","ADMINFEE"};
            loadOption(dgvClName,5);
        }
       

        
    }
}
