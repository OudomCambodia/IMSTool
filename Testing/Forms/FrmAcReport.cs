using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace Testing.Forms
{
    public partial class FrmAcReport : Form
    {
        CRUD crud = new CRUD();
        private DataTable dt;
        public FrmAcReport()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            //string cs = ConfigurationManager.ConnectionStrings["Testing.Properties.Settings.ConnectionString"].ConnectionString;
            using (OracleConnection con = new OracleConnection(frmLogIn.OracleConnectionString))
            {
                Cursor.Current = Cursors.WaitCursor;
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AC_PREMIUM";
                cmd.Parameters.Add("P_DATETO", OracleDbType.NVarchar2).Value = dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss");
                cmd.Parameters.Add("P_DATEFR", OracleDbType.NVarchar2).Value = dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss");
                OracleParameter OUA = cmd.Parameters.Add("DATAOUTPUT", OracleDbType.RefCursor);
                OUA.Value = DateTime.Now;
                OUA.Direction = ParameterDirection.Output;
                try
                {
                    
                    dt.Load(cmd.ExecuteReader());

                    dataGridView1.DataSource = dt;



                }
                 catch (Exception)
                {

                }
                Cursor.Current = Cursors.AppStarting;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcel(dt, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpTo.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
        }
    }
}
