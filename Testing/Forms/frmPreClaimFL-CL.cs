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
    public partial class frmPreClaimFL_CL : Form
    {
        CRUD crud = new CRUD();
        string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
        DataTable dt;


        public frmPreClaimFL_CL()
        {
            InitializeComponent();
        }

        private void cus_button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (dtpFrom.Value > dtpTo.Value)
            {
                Msgbox.Show("Date From is greater than Date To! Please check again.");
                return;
            }

            string[] Values = new string[] { "Pre_Cls_FL_CL", dtpFrom.Value.ToString("yyyy/MM/dd")+" 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd")+" 23:59:59" };
            dt = crud.ExecSP_OutPara("sp_user_print_system", Keys, Values);
            dgvPreClaim.DataSource = dt;
            
            Cursor.Current = Cursors.AppStarting;
        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            if (dt == null || dt.Rows.Count <= 0)
            {
                Msgbox.Show("No Data found in the table!");
                return;
            }

            My_DataTable_Extensions.ExportToExcel(dt, "");
        }
    }
}
