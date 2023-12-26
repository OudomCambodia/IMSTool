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
    public partial class frmAutoUploadReport : Form
    {
        DataTable dt = new DataTable();
        CRUD crud = new CRUD();
        //My_DataTable_Extensions exp;
        public frmAutoUploadReport()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExcelPath.Text.ToUpper() == "")
                {
                    Msgbox.Show("Batch No is required!");
                }else{
                    dt = crud.ExecQuery("SELECT PUL_REF_NO BATCH_NUM,PUL_POL_REF_NO REF_NO,POL_POLICY_NO,PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO(POL_SEQ_NO,'ADDITIONAL INSURED') ADDITIONAL_INSURED   FROM " +
"(select PUL_REF_NO, " +
"TO_NUMBER(PUL_POL_REF_NO) PUL_POL_REF_NO, " +
"PUL_DESC " +
"from UW_T_POL_UPLOAD_LOG " +
"WHERE PUL_REF_NO ='" + txtExcelPath.Text.ToUpper() + "') LEFT OUTER JOIN UW_T_POLICIES ON POL_PROPOSAL_NO = PUL_DESC " +
"ORDER BY PUL_POL_REF_NO ASC");
                    if (dt.Rows.Count > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        CommonFunctions.HighLightGrid(dgvView);
                        dgvView.ForeColor = System.Drawing.Color.Black;
                        dgvView.DataSource = dt;
                        lbTotal.Text = dt.Rows.Count.ToString();

                    }
                    else
                    {
                        Msgbox.Show("Batch is not existed!");
                    }
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
            
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            dgvView.DataSource = null;
            txtExcelPath.Text = "";
            lbTotal.Text = "";
        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dtcopy = dt.Clone();
            if (dgvView.Rows.Count <= 0)
            {
                Msgbox.Show("No Data to export!");
                
            }
            else
            {
                foreach (DataGridViewRow row in dgvView.Rows)
                {
                    dtcopy.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                }

                My_DataTable_Extensions.ExportToExcel(dtcopy);
            }
            
              
            }
        }
    }

