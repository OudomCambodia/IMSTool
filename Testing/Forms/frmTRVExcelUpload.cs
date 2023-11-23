using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;

namespace Testing.Forms
{
    public partial class frmTRVExcelUpload : Form
    {
        public frmTRVExcelUpload()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        CRUD crud = new CRUD();
        //My_DataTable_Extensions exp;
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

                    //DataTable temptb = new DataTable();
                    dt = TableExtension.ConvertExcelToDataTableV2(txtExcelPath.Text.Substring(6), true);
                    dt.AcceptChanges();


                    CommonFunctions.HighLightGrid(dgvView);
                    dgvView.ForeColor = System.Drawing.Color.Black;
                    dgvView.DataSource = dt;

                    lbTotalNum.Text = dt.Rows.Count.ToString();
                }
                
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
            
        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            DataTable dtcopy = dt.Copy();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                DataTable temp = crud.ExecQuery("select CUS_CODE from uw_m_customers where CUS_INDV_NIC_NO = '" + dt.Rows[i][9].ToString() + "'");
                if (temp.Rows.Count > 0)
                {
                    dtcopy.Rows[i][3] = temp.Rows[0][0].ToString();
                    dtcopy.Rows[i][4] = "";
                    dtcopy.Rows[i][5] = "";
                    dtcopy.Rows[i][6] = "";
                    dtcopy.Rows[i][7] = "";
                    dtcopy.Rows[i][8] = "";
                    dtcopy.Rows[i][9] = "";
                }
            }
            My_DataTable_Extensions.ExportToExcel(dtcopy);

        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            dgvView.DataSource = null;
            txtExcelPath.Text = "";
        }
    }
}
