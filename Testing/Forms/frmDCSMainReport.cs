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
    public partial class frmDCSMainReport : Form
    {
        public frmDCSMainReport()
        {
            InitializeComponent();
        }

        DBS11SqlCrud crud = new DBS11SqlCrud();
        CRUD crud_oracle = new CRUD();
        DataTable dt = new DataTable();
        DataTable dtOracle = new DataTable();
        DataTable dtInvoice = new DataTable();
        public string Team = "";
        DataTable dtTemp = new DataTable();
        string autoTeam = "u-rnk,u-bnw,u-ksb,u-srn";

        DataTable dtBank;

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                Cursor = Cursors.WaitCursor;

                var isBrokerTeam = false;
                var dtBrokerTeams = crud.LoadData("select [GROUP] from tbDOC_USER where USER_NAME = '" + frmLogIn.Usert.ToUpper() + "'").Tables[0];

                if (dtBrokerTeams.Rows.Count > 0)
                {
                    isBrokerTeam = dtBrokerTeams.Rows[0]["GROUP"].ToString().Equals("BROKERTEAM");
                }

                if (isBrokerTeam)
                {
                    var queryBuilder = new StringBuilder();
                    queryBuilder.Append("select * from view_main_report ")
                        .Append("where USER_CODE in ( ")
                        .AppendFormat("select [USER_CODE] from [DocumentControlDB].[dbo].tbDOC_USER where [USER_NAME] = '{0}' ", frmLogIn.Usert.ToUpper())
                        .Append("union all ")
                        .Append("SELECT [USER_CODE] ")
                        .Append("FROM [DocumentControlDB].[dbo].[tbDOC_USER] ")
                        .Append("where [GROUP] = 'BROKERTEAM' ")
                        .AppendFormat("and Parent like (select isnull (PARENT, '') + USER_CODE + '.' as PARENT from [DocumentControlDB].[dbo].tbDOC_USER where [USER_NAME] = '{0}') + '%') ", frmLogIn.Usert.ToUpper())
                        .Append("and convert(datetime,CREATE_DATE,103) >= convert(datetime,'" + dtpFrom.Value.ToString("yyyy/MM/dd ") + " 00:00:00') ")
                        .Append("and convert(datetime,CREATE_DATE,103) <= convert(datetime,'" + dtpTo.Value.ToString("yyyy/MM/dd ") + " 23:59:59') ")
                        .Append("order by user_code");

                    dt = crud.LoadData(queryBuilder.ToString()).Tables[0];
                    dgv.DataSource = dt;
                }
                else
                {
                    string main_rpt = "SELECT * from dbo.VIEW_MAIN_REPORT " +
                        "where convert(datetime," + (autoTeam.Contains(frmLogIn.Usert.ToLower().Trim()) ? "DP_PROCESSED" : "CREATE_DATE") + ",103) >= convert(datetime,'" + dtpFrom.Value.ToString("yyyy/MM/dd ") + " 00:00:00') " +
                        "and convert(datetime," + (autoTeam.Contains(frmLogIn.Usert.ToLower().Trim()) ? "DP_PROCESSED" : "CREATE_DATE") + ",103) <= convert(datetime,'" + dtpTo.Value.ToString("yyyy/MM/dd ") + " 23:59:59') ";
                    string[] TeamSplit = Team.Split(',');
                    if (!String.IsNullOrEmpty(Team) && frmAddDocument1.product.ContainsValue(TeamSplit[0]))
                    {
                        string ProType = "";
                        bool check = false;
                        foreach (string t in TeamSplit)
                        {
                            foreach (KeyValuePair<string, string> entry in frmAddDocument1.product)
                            {
                                if (entry.Value == t)
                                {
                                    check = true;
                                    ProType += "'" + entry.Key + "',";
                                }
                            }
                        }
                        if (check)
                        {
                            ProType = ProType.Remove(ProType.Length - 1);

                            if (dtBrokerTeams.Rows[0]["GROUP"].ToString().ToUpper() == "V-AGENCY")
                            {
                                string bankname = cmbBank.SelectedValue.ToString().ToUpper();


                                main_rpt = "SELECT * from dbo.VIEW_AGENCY_REPORT " +
                  "where convert(datetime,SUBMISSION_DATE,103) >= convert(datetime,'" + dtpFrom.Value.ToString("yyyy/MM/dd ") + " 00:00:00') " +
                  "and convert(datetime,SUBMISSION_DATE,103) <= convert(datetime,'" + dtpTo.Value.ToString("yyyy/MM/dd ") + " 23:59:59')  ";
                                string second_rpt = "select CUS_CODE,CUS_TYPE,nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) customer_name,nvl(CUS_PHONE_1,CUS_PHONE_2) CUSTOMER_PHONE from uw_m_customers";
                                string third_rpt = "select DEB_DEB_NOTE_NO,DEB_BPARTY_CODE,ACC_HANDLER_NAME,DEB_ME_CODE,AGENT_NAME, " +
                                                    "TO_CHAR(FN_GETDNCN_PAID_DATE(DEB_DEB_NOTE_NO)) PAID_DATE ,TO_CHAR(FN_GETDNCN_PAID_DATE(DEB_DEB_NOTE_NO), 'MON-YY') PAID_MONTH,DEB_TOTAL_AMOUNT " +
                                                    "FROM(  " +
                                                        "select DEB_DEB_NOTE_NO,DEB_BPARTY_CODE ,(SELECT SFC_SURNAME FROM SM_M_SALES_FORCE WHERE DEB_BPARTY_CODE = SFC_CODE) ACC_HANDLER_NAME, " +
                                                        "DEB_ME_CODE,(SELECT SFC_SURNAME FROM SM_M_SALES_FORCE WHERE DEB_ME_CODE = SFC_CODE) AGENT_NAME, DEB_TOTAL_AMOUNT  " +
                                                    "from rc_t_debit_note  " +
                                                        "WHERE DEB_ME_CODE LIKE 'F%'  " +
                                                        "union   " +
                                                        "select CRN_CREDIT_NOTE_NO,CRN_BPARTY_CODE,(SELECT SFC_SURNAME FROM SM_M_SALES_FORCE WHERE CRN_BPARTY_CODE = SFC_CODE) ACC_HANDLER_NAME,CRN_ME_CODE, " +
                                                        "(SELECT SFC_SURNAME FROM SM_M_SALES_FORCE WHERE CRN_ME_CODE = SFC_CODE) AGENT_NAME,CRN_TOTAL_AMOUNT*(-1)  " +
                                                        "from rc_t_credit_note WHERE CRN_ME_CODE LIKE 'F%') "; 


                                if (bankname != "ALL BANK")
                                {
                                    main_rpt += " and SALE_AGENT_NAME like '" + (bankname == "FTB" ? "FOREIGN TRADE BANK" : bankname) + "%'";
                                    third_rpt += " WHERE AGENT_NAME LIKE '" + (bankname == "FTB" ? "FOREIGN TRADE BANK" : bankname) + "%'";
                                }
                                else
                                {
                                    bankname = "";
                                    for (int i = 0; i < dtBank.Rows.Count; i++)
                                    {
                                        bankname += dtBank.Rows[i][1].ToString() + "|";
                                    }
                                    bankname = bankname.Remove(bankname.Length - 1, 1);
                                    main_rpt += " and (SALE_AGENT_NAME like 'PRINCE BANK%' " +
                                                " or SALE_AGENT_NAME like 'ACLEDA%' " +
                                                " or SALE_AGENT_NAME like 'CIMB%' " +
                                                " or SALE_AGENT_NAME like 'SATHAPANA%' " +
                                                " or SALE_AGENT_NAME like 'BRED%' " +
                                                " or SALE_AGENT_NAME like 'RHB%' " +
                                                " or SALE_AGENT_NAME like 'ADVANCE%' " +
                                                " or SALE_AGENT_NAME like 'VATTANAC%' " +
                                                " or SALE_AGENT_NAME like 'J TRUST%' " +
                                                " or SALE_AGENT_NAME like 'BANK OF CHINA%' " +
                                                " or SALE_AGENT_NAME like 'ARDB%' " +
                                                " or SALE_AGENT_NAME like 'CANADIA%' " +
                                                " or SALE_AGENT_NAME like 'CHIP MONG%'" +
                                                " or SALE_AGENT_NAME like 'ORIENTAL%' " +
                                                " or SALE_AGENT_NAME like 'MEGA LEASING%' " +
                                                " or SALE_AGENT_NAME like 'CATHAY UNITED%' " +
                                                " or SALE_AGENT_NAME like 'MB BANK%' " +
                                                " or SALE_AGENT_NAME like 'CHIEF%' " +
                                                " or SALE_AGENT_NAME like 'CAM CAPITAL%' " +
                                                " or SALE_AGENT_NAME like 'CAMBODIA POST%' " +
                                                " or SALE_AGENT_NAME like 'KASIKORN%' " +
                                                " or SALE_AGENT_NAME like 'FOREIGN TRADE BANK%' " + 
                                                " or SALE_AGENT_NAME like 'SHINHAN%' " +
                                                " or SALE_AGENT_NAME like 'AMK%' or SALE_AGENT_NAME like 'HONG LEONG%' ) ";
                                    third_rpt += " WHERE regexp_like(nvl(AGENT_NAME, '*'), '" + bankname + "' ,'i')";

                                }

                                dt = crud.LoadData(main_rpt).Tables[0];
                                dtOracle = crud_oracle.ExecQuery(second_rpt);
                                dtInvoice = crud_oracle.ExecQuery(third_rpt);

                                var query = from row1 in dt.AsEnumerable()
                                            join row2 in dtOracle.AsEnumerable()
                                            on row1["CUSTOMER_CODE"] equals row2["CUS_CODE"] into gj
                                            from subitem2 in gj.DefaultIfEmpty()
                                            join row3 in dtInvoice.AsEnumerable()
                                            on row1["DN_CN"] equals row3["DEB_DEB_NOTE_NO"] into gj1
                                            from subitem3 in gj1.DefaultIfEmpty()
                                            select new
                                            {
                                                SUBMISSION_DATE = row1["SUBMISSION_DATE"],
                                                DP_ISSUED_DATE = row1["DP_ISSUED_DATE"],
                                                POLICY_NO = row1["POLICY_NO"],
                                                CUSTOMER_NAME = row1["CUSTOMER_NAME"],
                                                //CUSTOMER_PHONE = row2["CUSTOMER_PHONE"],
                                                CLIENT_CATAG = row1["CLIENT_CATAG"],
                                                CLIENT_DETAILS = row1["CLIENT_DETAILS"],
                                                CUSTOMER_PHONE = subitem2 == null ? null : subitem2.Field<string>("CUSTOMER_PHONE"),
                                                //PREMIUM = subitem3 == null ? 0 : subitem3.Field<decimal>("DEB_TOTAL_AMOUNT"),
                                                PREMIUM = row1["PREMIUM"],
                                                PREMIUM_TYPE = row1["PREMIUM_TYPE"],
                                                PAID_DATE = subitem3 == null ? null : subitem3.Field<string>("PAID_DATE"),
                                                //PAID_MONTH = subitem3 == null ? null : subitem3.Field<string>("PAID_MONTH"),
                                                //SALE_AGENT_CODE = row1["SALE_AGENT_CODE"],
                                                //SALE_AGENT_NAME = row1["SALE_AGENT_NAME"],
                                                SALE_AGENT_CODE = subitem3 == null ? row1["SALE_AGENT_CODE"] : subitem3.Field<string>("DEB_ME_CODE"),
                                                SALE_AGENT_NAME = subitem3 == null ? row1["SALE_AGENT_NAME"] : subitem3.Field<string>("AGENT_NAME"),


                                                //CUS_TYPE = row2["CUS_TYPE"],
                                                CUS_TYPE = subitem2 == null ? null : subitem2.Field<string>("CUS_TYPE"),
                                                DOC_TYPE = row1["DOC_TYPE"],
                                                PRODUCT_LINE = row1["PRODUCT_LINE"],
                                                PRODUCT_TYPE = row1["PRODUCT_TYPE"],
                                                DN_CN = row1["DN_CN"],
                                                REF_ID = row1["REF_ID"],
                                                POLICY_EFFECT_DATE = row1["POLICY_EFFECT_DATE"],
                                                CREATE_BY = row1["CREATE_BY"],
                                                POLICY_STATUS = row1["POLICY_STATUS"],
                                                //PRODUCER_CODE = row1["PRODUCER_CODE"],
                                                //PRODUCER_NAME = row1["PRODUCER_NAME"],
                                                DP_NAME = row1["DP_NAME"],
                                                FILLING_NAME = row1["FILLING_NAME"],
                                                LATEST_UPDATE_AT = row1["LATEST_UPDATE_AT"],
                                                STATUS = row1["STATUS"],
                                                STATUS_BY = row1["STATUS_BY"],
                                                PRIORITY_TYPE = row1["PRIORITY_TYPE"],
                                                NOTE = row1["NOTE"],
                                                DP_REMARK = row1["DP_REMARK"],
                                                DOCUMENT_REMARK = row1["DOCUMENT_REMARK"],
                                                CUR_STATUS = row1["CUR_STATUS"],
                                                STAFF_ID = row1["STAFF_ID"],
                                                SALE_PERSON_NAME = row1["SALE_PERSON_NAME"],
                                                DEPARTMENT = row1["DEPARTMENT"]
                                            };
                                if (dtTemp.Columns.Count <= 0)
                                {
                                    dtTemp.Columns.Add("SUBMISSION_DATE", typeof(string));
                                    dtTemp.Columns.Add("DP_ISSUED_DATE", typeof(string));
                                    dtTemp.Columns.Add("POLICY_NO", typeof(string));
                                    dtTemp.Columns.Add("CUSTOMER_NAME", typeof(string));
                                    dtTemp.Columns.Add("CLIENT_CATAG", typeof(string));
                                    dtTemp.Columns.Add("CLIENT_DETAILS", typeof(string));
                                    dtTemp.Columns.Add("CUSTOMER_PHONE", typeof(string));
                                    dtTemp.Columns.Add("PREMIUM", typeof(decimal));
                                    dtTemp.Columns.Add("PAID_DATE", typeof(string));
                                    dtTemp.Columns.Add("PREMIUM_TYPE", typeof(string));


                                    dtTemp.Columns.Add("BANK_BRANCH_NAME", typeof(string));
                                    dtTemp.Columns.Add("BANK_BRANCH_CODE", typeof(string));
                                    dtTemp.Columns.Add("CUS_TYPE", typeof(string));
                                    dtTemp.Columns.Add("DOC_TYPE", typeof(string));
                                    dtTemp.Columns.Add("PRODUCT_LINE", typeof(string));
                                    dtTemp.Columns.Add("PRODUCT_TYPE", typeof(string));
                                    dtTemp.Columns.Add("DN_CN", typeof(string));
                                    dtTemp.Columns.Add("SALE_AGENT_CODE", typeof(string));
                                    dtTemp.Columns.Add("REF_ID", typeof(string));
                                    dtTemp.Columns.Add("POLICY_EFFECT_DATE", typeof(string));
                                    dtTemp.Columns.Add("CREATE_BY", typeof(string));
                                    dtTemp.Columns.Add("POLICY_STATUS", typeof(string));
                                    //dtTemp.Columns.Add("PRODUCER_CODE", typeof(string));
                                    //dtTemp.Columns.Add("PRODUCER_NAME", typeof(string));
                                    dtTemp.Columns.Add("DP_NAME", typeof(string));
                                    dtTemp.Columns.Add("FILLING_NAME", typeof(string));
                                    dtTemp.Columns.Add("LATEST_UPDATE_AT", typeof(string));
                                    dtTemp.Columns.Add("STATUS", typeof(string));
                                    dtTemp.Columns.Add("STATUS_BY", typeof(string));
                                    dtTemp.Columns.Add("PRIORITY_TYPE", typeof(string));
                                    dtTemp.Columns.Add("NOTE", typeof(string));
                                    dtTemp.Columns.Add("DP_REMARK", typeof(string));
                                    dtTemp.Columns.Add("DOCUMENT_REMARK", typeof(string));

                                    dtTemp.Columns.Add("STAFF_ID", typeof(string));
                                    dtTemp.Columns.Add("SALE_PERSON_NAME", typeof(string));
                                    dtTemp.Columns.Add("DEPARTMENT", typeof(string));
                                }
                                else
                                {
                                    dtTemp.Clear();
                                }
                                foreach (var item in query)
                                {
                                    DataRow dr = dtTemp.NewRow();


                                    dr["SUBMISSION_DATE"] = item.SUBMISSION_DATE.ToString();
                                    dr["DP_ISSUED_DATE"] = String.Format("{0:d}", item.DP_ISSUED_DATE.ToString());
                                    dr["POLICY_NO"] = item.POLICY_NO;
                                    dr["CUSTOMER_NAME"] = item.CUSTOMER_NAME;
                                    dr["CLIENT_CATAG"] = item.CLIENT_CATAG;
                                    dr["CLIENT_DETAILS"] = item.CLIENT_DETAILS;
                                    dr["CUSTOMER_PHONE"] = item.CUSTOMER_PHONE;
                                    dr["PREMIUM"] = item.PREMIUM;
                                    dr["PREMIUM_TYPE"] = item.PREMIUM_TYPE;
                                    dr["PAID_DATE"] = (item.PAID_DATE == null) ? "" : item.PAID_DATE.ToString();
                                    //dr["PAID_MONTH"] = item.PAID_MONTH;
                                    if (item.SALE_AGENT_NAME.ToString().Contains("ACLEDA"))
                                    {
                                        dr["BANK_BRANCH_NAME"] = item.SALE_AGENT_NAME.ToString().Contains("(") ? item.SALE_AGENT_NAME.ToString().Substring((item.SALE_AGENT_NAME.ToString().IndexOf("(") + "(".Length), (item.SALE_AGENT_NAME.ToString().IndexOf(")") - item.SALE_AGENT_NAME.ToString().IndexOf("(") - "(".Length)) : item.SALE_AGENT_NAME.ToString();
                                        dr["BANK_BRANCH_CODE"] = item.SALE_AGENT_NAME.ToString().Contains("(") ? dr["BANK_BRANCH_NAME"].ToString().Substring(dr["BANK_BRANCH_NAME"].ToString().IndexOf('-') + 1, dr["BANK_BRANCH_NAME"].ToString().Length - (dr["BANK_BRANCH_NAME"].ToString().IndexOf('-') + 1)).Trim() : "";
                                    }
                                    else
                                    {
                                        dr["BANK_BRANCH_NAME"] = item.SALE_AGENT_NAME.ToString();
                                        //dr["BANK_BRANCH_CODE"] = item.SALE_AGENT_NAME.ToString().Contains("(") ? dr["BANK_BRANCH_NAME"].ToString().Substring(dr["BANK_BRANCH_NAME"].ToString().IndexOf('-') + 1, dr["BANK_BRANCH_NAME"].ToString().Length - (dr["BANK_BRANCH_NAME"].ToString().IndexOf('-') + 1)).Trim() : "";
                                        dr["BANK_BRANCH_CODE"] = "";
                                    }

                                    dr["CUS_TYPE"] = item.CUS_TYPE;
                                    dr["DOC_TYPE"] = item.DOC_TYPE;
                                    dr["PRODUCT_LINE"] = item.PRODUCT_LINE;
                                    dr["PRODUCT_TYPE"] = item.PRODUCT_TYPE;
                                    dr["DN_CN"] = item.DN_CN;
                                    dr["SALE_AGENT_CODE"] = item.SALE_AGENT_CODE;
                                    dr["REF_ID"] = item.REF_ID;
                                    dr["POLICY_EFFECT_DATE"] = item.POLICY_EFFECT_DATE;
                                    dr["CREATE_BY"] = item.CREATE_BY;
                                    dr["POLICY_STATUS"] = item.POLICY_STATUS;
                                    //dr["PRODUCER_CODE"] = item.PRODUCER_CODE;
                                    //dr["PRODUCER_NAME"] = item.PRODUCER_NAME;
                                    dr["DP_NAME"] = item.DP_NAME;
                                    dr["FILLING_NAME"] = item.FILLING_NAME;
                                    dr["LATEST_UPDATE_AT"] = String.Format("{0:d}", item.LATEST_UPDATE_AT.ToString());
                                    dr["STATUS"] = item.STATUS;
                                    dr["STATUS_BY"] = item.STATUS_BY;
                                    dr["PRIORITY_TYPE"] = item.PRIORITY_TYPE;
                                    dr["NOTE"] = item.NOTE;
                                    dr["DP_REMARK"] = item.DP_REMARK;
                                    dr["DOCUMENT_REMARK"] = item.DOCUMENT_REMARK;

                                    dr["STAFF_ID"] = item.STAFF_ID;
                                    dr["SALE_PERSON_NAME"] = item.SALE_PERSON_NAME;
                                    dr["DEPARTMENT"] = item.DEPARTMENT;

                                    dtTemp.Rows.Add(dr);
                                }


                                dgv.DataSource = dtTemp;

                            }

                            else
                            {
                                main_rpt += " AND PRODUCT_TYPE IN (" + ProType + ")";
                                dt = crud.LoadData(main_rpt).Tables[0];
                                dgv.DataSource = dt;
                            }

                        }
                    }
                }

                Cursor = Cursors.Arrow;
                //dtTemp.Clear();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dtTemp.Clear();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgv.RowCount > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                //My_DataTable_Extensions.ExportToExcel(dt, "");
                if (frmLogIn.Usert.ToUpper() == "U-BVC")
                    My_DataTable_Extensions.ExportToExcelXML(dtTemp, "");
                else
                    My_DataTable_Extensions.ExportToExcelXML(dt, "");

                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No data found.");
            }
        }

        private void frmDCSMainReport_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;

            if (frmLogIn.Usert.ToUpper() == "U-BVC" || frmLogIn.Usert.ToUpper() == "ADMIN")
            {
                dtBank = crud_oracle.ExecQuery("select * from IMSTOOL_BANK_INFO ORDER BY ORDER_SEQ ASC");
                cmbBank.DataSource = dtBank;
                cmbBank.DisplayMember = "BANK_NAME";
                cmbBank.ValueMember = "BANK_NAME";
            }
            else
            {
                lblBank.Visible = false;
                cmbBank.Visible = false;
            }




        }


    }
}
