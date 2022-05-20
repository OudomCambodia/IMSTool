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
    public partial class frmClaimRI : Form
    {
        public string type = "ClaimIncurred";
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        DataTable dtDet = new DataTable();

        public frmClaimRI()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string sp_type = (type == "ClaimIncurred") ? "Incurred_Claim_RI" : (type == "ClaimPaid") ? "Paid_Claim_RI" : (type == "ClaimOS") ? "OS_Claim_RI" : "";
                string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59" };
                dt = crud.ExecSP_OutPara("sp_user_print_system", Keys, Values);

                if (dt.Rows.Count <= 0)
                {
                    Msgbox.Show("No Record Found!");
                    return;
                }
                else
                {
                    //string sqlCount = "";

                    //if (type == "ClaimPaid")
                    //    sqlCount = "select CPC_CLAIM_NO,CPC_SEQ_NO,count(*) RI_Num from view_claim_ri_detail where"
                    //           + " TRAN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')"
                    //           + " and TRAN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD') group by CPC_CLAIM_NO,CPC_SEQ_NO";
                    //else
                    //    sqlCount = "select CPC_CLAIM_NO,CPC_SEQ_NO,count(*) RI_Num from view_claim_ri_detail where"
                    //           + " INTIMATION_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')"
                    //           + " and INTIMATION_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD') group by CPC_CLAIM_NO,CPC_SEQ_NO";
                    
                    //string maxStr = crud.ExecQuery("select max(RI_Num) from (" + sqlCount + ")").Rows[0].ItemArray[0].ToString();
                    //int max = maxStr == "" ? 0 : int.Parse(maxStr);

                    //if (max > 0)
                    //{
                    //    for (int i = 1; i <= max; i++)
                    //    {
                    //        DataColumn dcolColumnRI = new DataColumn("RI_code " + i, typeof(string));
                    //        DataColumn dcolColumnRIName = new DataColumn("RI_name " + i, typeof(string));
                    //        DataColumn dcolColumnRIPer = new DataColumn("RI_percent " + i, typeof(string));
                    //        dt.Columns.Add(dcolColumnRI);
                    //        dt.Columns.Add(dcolColumnRIName);
                    //        dt.Columns.Add(dcolColumnRIPer);
                    //    }
                    //    foreach (DataRow row in dt.Rows)
                    //    {
                    //        DataTable noStr = crud.ExecQuery("select RI_Num from (" + sqlCount + ") where CPC_CLAIM_NO = '" + row["CLAIM_NO"].ToString() + "'");
                    //        if (noStr.Rows.Count > 0)
                    //        {
                    //            string sqlComBre = "";
                    //            if (type == "ClaimPaid")
                    //                sqlComBre = "SELECT DISTINCT CRI_REINSURER_CODE,REINSURER_NAME,CRI_RATE FROM VIEW_CLAIM_RI_DETAIL where"
                    //                    + " TRAN_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')"
                    //                    + " and TRAN_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";
                    //            else
                    //                sqlComBre = "SELECT DISTINCT CRI_REINSURER_CODE,REINSURER_NAME,CRI_RATE FROM VIEW_CLAIM_RI_DETAIL where"
                    //                        + " INTIMATION_DATE >= TO_DATE('" + dtpFrom.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')"
                    //                        + " and INTIMATION_DATE <= TO_DATE('" + dtpTo.Value.ToString("yyyy/MM/dd ") + "','YYYY/MM/DD')";

                    //            sqlComBre += " AND CPC_CLAIM_NO = '" + row["CLAIM_NO"].ToString() + "' AND ABS(RI_FAC) = ABS(" + row["FAC PR (LC)"].ToString() + ")";
                    //            dtDet = crud.ExecQuery(sqlComBre);
                    //            int i = 0;
                    //            foreach (DataRow rowDetail in dtDet.Rows)
                    //            {
                    //                i += 1;
                    //                row["RI_code " + i] = rowDetail["CRI_REINSURER_CODE"];
                    //                row["RI_name " + i] = rowDetail["REINSURER_NAME"];
                    //                row["RI_percent " + i] = rowDetail["CRI_RATE"];
                    //            }
                    //        }
                    //    }
                    //}

                    dataGridView.DataSource = dt;

                    My_DataTable_Extensions.ExportToExcelXML(dt, "");
                }
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex) { Msgbox.Show("System Error \n" + ex.ToString()); }
        }

        private void frmClaimRI_Load(object sender, EventArgs e)
        {
            if (type == "ClaimPaid")
            {
                lbTitle.Text = "Claim Paid with FAC Reinsurer";
                lbDateFrom.Text = "Paid Date From:";
                lbDateTo.Text = "Paid Date To:";
            }

            if (type == "ClaimOS")
            {
                lbTitle.Text = "Claim Outstanding with FAC Reinsurer";
            }
        }

    }
}
