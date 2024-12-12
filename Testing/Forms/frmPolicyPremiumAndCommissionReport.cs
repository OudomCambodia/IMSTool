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
using Oracle.ManagedDataAccess.Types;

namespace Testing.Forms
{
    public partial class frmPolicyPremiumAndCommissionReport : Form
    {
        DBS11SqlCrud sqlCrud = new DBS11SqlCrud();
        private string connectionString = string.Empty;

        public frmPolicyPremiumAndCommissionReport()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                string[] Keys = new string[] { "p_date_from", "p_date_to" };
                string[] Values = new string[] { dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59" };
                var dtClaimRejection = ExecSP_OutPara("SP_POLICY_PAYMENT_AND_COMMISION_REPORT", Keys, Values);

                if (dtClaimRejection == null || dtClaimRejection.Rows.Count <= 0)
                {
                    Cursor = Cursors.Arrow;
                    dgvResult.DataSource = null;
                    return;
                }

                dgvResult.DataSource = dtClaimRejection;

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }

            Cursor = Cursors.Arrow;
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                if (dgvResult.DataSource == null)
                {
                    Cursor = Cursors.Arrow;
                    Msgbox.Show("No row to export");
                    return;
                }

                My_DataTable_Extensions.ExportToExcelXML(dgvResult.DataSource as DataTable);
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                Msgbox.Show(ex.ToString());
            }

            Cursor = Cursors.Arrow;
        }

        private DataTable ExecSP_OutPara(string spName, string[] spParaKeys, string[] spParaValues)
        {
            connectionString = sqlCrud.LoadData("select * from tbConnectionString where ID = 3").Tables[0].Rows[0]["ConnectionString"].ToString();

            if (spParaKeys.Length != spParaValues.Length)
            {
                Msgbox.Show("Error Parameters in Function to call Stored Procedures!");
                return null;
            }
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(connectionString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                for (int i = 0; i < spParaKeys.Length; i++)
                {
                    cmd.Parameters.Add(spParaKeys[i], OracleDbType.NVarchar2).Value = spParaValues[i];
                }
                cmd.Parameters.Add("sp_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
            }
            return dt;
        }
    }
}
