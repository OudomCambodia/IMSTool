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

namespace Testing
{
    public partial class frmSearch : Form
    {
        public string UserName = "SICL";
        private DataTable dt;

        public frmSearch()
        {
            InitializeComponent();
        }          

        private void bnSearch_Click(object sender, EventArgs e)
        {
            CRUD crud = new CRUD();
            dt = new DataTable();

            try
            {
                if (tbPolicyNo.Text.Trim().ToString() == "")
                {
                    Msgbox.Show("Policy No must be input before seaching.");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                string sql = "INSERT INTO user_print_history (user_name, print_datetime, filter2, type) VALUES ('" + UserName + "', TO_DATE('" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS'), '" + tbPolicyNo.Text + ";;;" + tbInsured.Text + ";;;" + tbRiskName.Text + "', '1')";
                crud.ExecNonQuery(sql);

                sql = "SELECT * FROM view_risk_name WHERE rownum <= 10000";

                sql += " and PRS_POLICY_NO like '%" + tbPolicyNo.Text.Trim().ToUpper() + "%'";

                if (tbRiskName.Text.Trim() != "")
                    sql += " and PRS_NAME_DESC like '%" + tbRiskName.Text.Trim().ToUpper() + "%'";

                if (tbInsured.Text.Trim() != "")
                    sql += " and CUSTOMER_NAME like '%" + tbInsured.Text.Trim().ToUpper() + "%'";

                sql += " and PRS_EFFECT_FROM_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS')";
                sql += " and PRS_EFFECT_FROM_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd HH:mm:ss") + "','YYYY/MM/DD HH24:MI:SS')";
                sql += " order by POL_ENDORSEMENT_NO desc, PRS_NAME_DESC";

                dt = crud.ExecQuery(sql);
                dataGridView1.DataSource = dt;

                //setting headers and width for DataGridView
                dataGridView1.Columns[0].HeaderText = "Policy No";
                dataGridView1.Columns[1].HeaderText = "Risk Name";
                dataGridView1.Columns[2].HeaderText = "Effective Date";
                dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MMM/yyyy";
                dataGridView1.Columns[3].HeaderText = "Expiry Date";
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd/MMM/yyyy";
                dataGridView1.Columns[4].HeaderText = "Endo Date";
                dataGridView1.Columns[4].DefaultCellStyle.Format = "dd/MMM/yyyy";
                dataGridView1.Columns[5].HeaderText = "Endo Insert";
                dataGridView1.Columns[6].HeaderText = "Endo Update";
                dataGridView1.Columns[7].HeaderText = "Endo Delete";
                dataGridView1.Columns[8].HeaderText = "Deletion Date";
                dataGridView1.Columns[8].DefaultCellStyle.Format = "dd/MMM/yyyy";
                dataGridView1.Columns[9].HeaderText = "Endo No";
                dataGridView1.Columns[10].HeaderText = "Cancelled Endo No";
                dataGridView1.Columns[11].HeaderText = "Insured Name";
                dataGridView1.Columns[12].HeaderText = "Issue Date";
                dataGridView1.Columns[12].DefaultCellStyle.Format = "dd/MMM/yyyy";

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    dataGridView1.Columns[i].Width = 170;

                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[8].Width = 100;
                dataGridView1.Columns[12].Width = 100;

                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
        }

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            lbTotNumber.Text = dataGridView1.RowCount.ToString();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbPolicyNo.Text = "";
            tbRiskName.Text = "";
            tbInsured.Text = "";
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 01, 01);
            dtpTo.Value = DateTime.Now;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            this.ActiveControl = tbPolicyNo;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.GoTo(dataGridView1, textBox1, 1);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
                //  dtPolicy.DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dataGridView1);
        }

        private void tbPolicyNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
