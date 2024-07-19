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
                    Cursor.Current = Cursors.WaitCursor;
                    //DataTable temptb = new DataTable();
                    dt = TableExtension.ConvertExcelToDataTableApose(txtExcelPath.Text.Substring(6));
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

        private void bnExcel_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DataTable dtcopy = dt.Copy();
            Random rnd = new Random();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString() != "AUTO")
                {
                    DataTable temp = crud.ExecQuery("select CUS_CODE from uw_m_customers where CUS_INDV_NIC_NO = '" + dt.Rows[i][9].ToString() + "'");
                    DataTable loctemp = crud.ExecQuery("select GPL_CODE from sm_m_geoarea_paramln where TRIM(GPL_DESC) =TRIM(q'[" + dt.Rows[i][10].ToString() + "]')");
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
                    if (loctemp.Rows.Count <= 0)
                    {
                        bool isExist = false;
                        string gplCode = string.Empty;

                        // if GPL_CODE exists, random the number again.
                        do
                        {
                            gplCode = string.Concat("N0", rnd.Next(1, 9999).ToString("D4"));
                            isExist = crud.ExecQuery("select GPL_CODE from SM_M_GEOAREA_PARAMLN where GPL_CODE = '" + gplCode + "'").Rows.Count > 0;
                        }
                        while (isExist);

                        Oracle.ManagedDataAccess.Client.OracleCommand cmd = new Oracle.ManagedDataAccess.Client.OracleCommand();
                        cmd.CommandText = "INSERT INTO SM_M_GEOAREA_PARAMLN(GPL_CODE, GPL_DESC, GPL_SMG_CODE, GPL_SMG_LEVEL, GPL_SEQ_NO, GPL_DTL_INSERT) VALUES(:gpl_code, :gpl_desc, :gpl_smg_code, :gpl_smg_level, :gpl_seq_no, :gpl_dtl_insert)";
                        cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("gpl_code", gplCode));
                        cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("gpl_desc", dt.Rows[i][10].ToString()));
                        cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("gpl_smg_code", "9"));
                        cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("gpl_smg_level", "9"));
                        cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("gpl_seq_no", string.Concat("000012300", crud.ExecQuery("select SEQ_SM_M_GEOAREA_DETAILS.NEXTVAL from dual").Rows[0][0].ToString())));
                        cmd.Parameters.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("gpl_dtl_insert", "N"));
                        crud.ExecNonQuery(cmd);

                        //crud.ExecNonQuery("INSERT INTO SM_M_GEOAREA_PARAMLN(GPL_CODE,GPL_DESC,GPL_SMG_CODE,GPL_SMG_LEVEL,GPL_SEQ_NO,GPL_DTL_INSERT) VALUES ('" + "N0" + rnd.Next(10, 99) + rnd.Next(10, 80) + "','" + dt.Rows[i][10].ToString().Replace("'", "''") + "','9','9','000012300'||SEQ_SM_M_GEOAREA_DETAILS.NEXTVAL,'N')");
                        //crud.ExecNonQuery("commit"); 
                    }
                }
                else
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
        

        private void bnClear_Click(object sender, EventArgs e)
        {
            dgvView.DataSource = null;
            txtExcelPath.Text = "";
            lbTotal.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
