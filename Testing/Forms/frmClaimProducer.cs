using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmClaimProducer : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        Regex regex = new Regex(@"^[0-9]*(\.[0-9]{1,2})?$");
        public frmClaimProducer()
        {
            InitializeComponent();
        }

        private void butQuery_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            try
            {

                if (txtCusCode.Text.ToString() == "" && txtPolicyNo.Text == "")
                {
                    Msgbox.Show("Either full Customer Code or full Policy Number has to be inputed");
                    return;
                }

                else
                {
                    try
                    {
                        //string sql = "SELECT claim_no,insuredcode,insuredname,uwy,dateofloss,claim_notified,(select int_claimed_amt from cl_t_intimation where claim_no=INT_CLAIM_NO) incurred_amount,STATUS FROM view_premium_claim where ";
                        string sql = "SELECT claim_no,insuredcode,insuredname,uwy,dateofloss,claim_notified,INCURRED_AMT,STATUS FROM view_premium_claim where ";
                        //   string Total;

                        if (txtUWYear.Text != "" && IsNum(txtUWYear) && txtCusCode.Text != "")
                            sql += " INSUREDCODE = '" + txtCusCode.Text.ToUpper() + "' and claim_notified is not NULL and uwy = '" + txtUWYear.Text + "'";
                        else if (txtUWYear.Text != "" && IsNum(txtUWYear) && txtPolicyNo.Text != "")
                            sql += "claim_notified is not NULL and uwy = '" + txtUWYear.Text + "' and POLICY_NO= '" + txtPolicyNo.Text + "'";
                        else if (txtPolicyNo.Text != "")
                            sql += " claim_notified is not NULL and POLICY_NO= '" + txtPolicyNo.Text + "'";
                        else if (txtCusCode.Text != null)
                            sql += " INSUREDCODE = '" + txtCusCode.Text.ToUpper() + "' and claim_notified is not NULL ";
                        else
                            sql += " INSUREDCODE = '" + txtCusCode.Text.ToUpper() + "' and claim_notified is not NULL and POLICY_NO= '" + txtPolicyNo.Text + "' and uwy = '" + txtUWYear.Text + "'";
                        Cursor.Current = Cursors.AppStarting;
                        dt = crud.ExecQuery(sql);
                        if (dt.Rows.Count != 0)
                            dataGridView1.DataSource = dt;
                        else
                            Msgbox.Show("No Record Found!");
                    }
                    catch (Exception ex)
                    {
                        Msgbox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dt.Columns.Count > 0)
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

        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            CommonFunctions.HighLightGrid(dataGridView1);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCusCode.Text = "";
            txtPolicyNo.Text = "";
            txtUWYear.Text = "";
            dataGridView1.DataSource = null;
        }

        bool IsNum(TextBox txt)
        {
            return !(String.IsNullOrEmpty(txt.Text) || !regex.IsMatch(txt.Text));
        }
        private void txtUWYear_Leave(object sender, EventArgs e)
        {
            if (txtUWYear.Text == "")
                return;
            else
            {
                if (!IsNum(txtUWYear))
                {
                    Msgbox.Show("Input format is not correct. Format: 2020,2021...");
                    txtUWYear.Clear();
                }
            }
        }

        private void txtCusCode_Leave(object sender, EventArgs e)
        {
            if (txtCusCode.Text == "")
                return;
            else
            {
                if (txtCusCode.Text.Length != 10 || txtCusCode.Text.ToUpper().Substring(0, 1) != "C")
                {
                    Msgbox.Show("Please input full Customer Code!");
                }
            }
           
             
        }

        private void txtPolicyNo_Leave(object sender, EventArgs e)
        {
            if (txtPolicyNo.Text == "")
                return;
            else
            {
                if (txtPolicyNo.Text.Length != 20 || txtPolicyNo.Text.ToUpper().Substring(0, 1) != "D")
                Msgbox.Show("Please input full policy number!");
            }
            
          
           
        }

        private void txtPolicyNo_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
