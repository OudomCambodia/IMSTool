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
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = "c:\\";
                ofd.Filter = "Excel Files(*.XLSX;*.XLS)|*.XLS;*.XLSX|All files (*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtExcelPath.Text = "File: " + ofd.FileName;
                    Cursor.Current = Cursors.WaitCursor;
                    //DataTable temptb = new DataTable();
                    dt = TableExtension.ConvertExcelToDataTableAposeV1(txtExcelPath.Text.Substring(6));
                    dt.AcceptChanges();


                    CommonFunctions.HighLightGrid(dgvView);
                    dgvView.ForeColor = System.Drawing.Color.Black;
                    dgvView.DataSource = dt;
                    lbTotal.Text = dt.Rows.Count.ToString();

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
            DataTable dtcopy = dt.Copy();
            Random rnd = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() == "AUTO")
                {
                    DataTable temp = crud.ExecQuery("select pol_policy_no from ( " +
             "select pol_policy_no ,POL_CUS_CODE,PK_MONTHLY_REPORTS.FN_GET_POLICY_COMMON_INFO(POL_SEQ_NO,'ADDITIONAL INSURED') ADDITIONAL_INSURED " +
             "from uw_t_policies WHERE  POL_PRD_CODE IN ('VPC','CYC','VCM')" +
             ") where ADDITIONAL_INSURED ='" + dt.Rows[i][25].ToString() + "' AND POL_CUS_CODE ='" + dt.Rows[i][3].ToString() + "'");
                    if (temp.Rows.Count > 0)
                    {
                        dtcopy.Rows[i][4] = temp.Rows[0][0].ToString();

                    }
                }
                


            }
            Cursor.Current = Cursors.WaitCursor;
            TableExtension.ExportToExcel(dtcopy);

        }

        
    }
}
