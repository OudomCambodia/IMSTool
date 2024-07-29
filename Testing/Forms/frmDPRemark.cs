using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmDPRemark : Form
    {

        public string UserName = string.Empty, UserCode = string.Empty;
        public DataTable SelectedDoc = new DataTable();
        DBS11SqlCrud crud = new DBS11SqlCrud();
        CRUD maincrud = new CRUD();
        MyDB Mydb = new MyDB();

        public frmDPRemark()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {

            string remark = tbRemark.Text.Trim(), polendo = tbPolEndo.Text.Trim().ToUpper();
            DataTable dtTemp = new DataTable();

            if (polendo == "")
            {
                Msgbox.Show("Please fill in the Policy/Endorsement No.");
                tbPolEndo.Focus();
                return;
            }
            else
            {
                dtTemp = maincrud.ExecQuery("select POL_SEQ_NO from UW_T_POLICIES where (POL_POLICY_NO = '" + polendo + "' or POL_ENDORSEMENT_NO = '" + polendo + "')");
                if (dtTemp.Rows.Count <= 0)
                {
                    Msgbox.Show("Policy/Endorsement No: " + polendo + " is incorrect, please check again!");
                    tbPolEndo.Focus();
                    return;
                }
            }


            //DialogResult dr = Msgbox.Show("Are you sure you want to remark " + SelectedDoc.Rows.Count + " selected document(s) with " + ((remark == "") ? "nothing" : remark) + " and set Policy/Endorsement No = \"" + polendo + "\"?", "Confirmation", "Yes", "No");
            //if (dr == System.Windows.Forms.DialogResult.Yes)
            //{
            Cursor.Current = Cursors.WaitCursor;

            string SelectedDocCode = frmDocumentControl.getSelectedDocCode(SelectedDoc);
            crud.ExecuteMySql("dbo.sp_insert_to_hist", "@DocCode", SelectedDocCode);
            //crud.Executing("UPDATE dbo.tbDOC SET POLICY_NO = '" + polendo + "', DP_REMARK = '" + remark + "', DOC_CUR_STATUS = 3, DOC_CUR_STATUS_SET_BY = (SELECT USER_CODE FROM dbo.tbDOC_USER WHERE USER_NAME = '" + UserName + "' and ROLE = 'DP'), DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))");
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE dbo.tbDOC SET DP_REMARK = @remark, DOC_CUR_STATUS = 3, DOC_CUR_STATUS_SET_BY = '" + UserCode + "', DOC_CUR_STATUS_SET_ON = '" + DateTime.Now + "' WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
            cmd.Parameters.Add(new SqlParameter("remark", remark));
            crud.Executing(cmd);
            //cmd.CommandText = "UPDATE dbo.tbDOC SET POLICY_NO = @polendo WHERE RTRIM(LTRIM(POLICY_NO)) = '' AND DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',','))";
            cmd.CommandText = "UPDATE dbo.tbDOC SET POLICY_NO = @polendo WHERE DOC_CODE = " + SelectedDoc.Rows[0]["REF_ID"].ToString();
            cmd.Parameters.Add(new SqlParameter("polendo", polendo));
            crud.Executing(cmd);

            if (SelectedDoc.Rows.Count > 1)//multi select => set policy number automatically
            {
                for (int i = 1; i < SelectedDoc.Rows.Count; i++) //skip first record
                {

                    DataRow row = SelectedDoc.Rows[i];

                    if (row["TYPE"].ToString() == "Policy")
                    {
                        dtTemp = crud.LoadData("SELECT POLICY_NO,CUS_CODE FROM dbo.tbDOC WHERE DOC_CODE = " + row["REF_ID"].ToString()).Tables[0];
                        string polTemp = dtTemp.Rows[0]["POLICY_NO"].ToString().Trim(), cuscode = dtTemp.Rows[0]["CUS_CODE"].ToString().Trim();
                        if (polTemp != "")
                        {
                            if (polTemp[0] != 'D' && polTemp.Length != 20)
                                polTemp = "";
                        }
                        if (polTemp == "")
                        {
                            string prod = row["PRODUCT_TYPE"].ToString();
                            prod = (prod == "Chinese PA") ? "GPA" : prod;
                            //string c = "select POL_POLICY_NO, CUS_NAME from " +
                            //            "( " +
                            //           "select POL_POLICY_NO,nvl(CUS_INDV_SURNAME,CUS_CORP_NAME)CUS_NAME from UW_T_POLICIES a, UW_M_CUSTOMERS b " +
                            //           "where a.POL_CUS_CODE = b.CUS_CODE and POL_TRANSACTION_TYPE = 'N' " +
                            //           "and POL_PRD_CODE = '" + prod + "' and TRUNC(POL_PERIOD_TO)>to_date(to_char(sysdate, 'DD/MM/YYYY'),'DD/MM/YYYY') and POL_CANCELLED_BY is null order by POL_AUTHORIZED_DATE desc " +
                            //           ") " +
                            //            "where CUS_NAME = '" + row["CUSTOMER"].ToString() + "'";

                            string c = "select POL_POLICY_NO from UW_T_POLICIES " +
                               " where POL_CUS_CODE = '" + cuscode + "' and POL_TRANSACTION_TYPE = 'N' " +
                               " and POL_PRD_CODE = '" + prod + "' and TRUNC(POL_PERIOD_TO)>to_date(to_char(sysdate, 'DD/MM/YYYY'),'DD/MM/YYYY') and POL_CANCELLED_BY is null order by POL_AUTHORIZED_DATE desc";

                            dtTemp = maincrud.ExecQuery(c);
                            string poltemp = "";
                            if (dtTemp.Rows.Count > 0)
                            {
                                poltemp = dtTemp.Rows[0]["POL_POLICY_NO"].ToString();

                            }
                            else
                            {
                                poltemp = polendo;
                            }
                            cmd = new SqlCommand();
                            cmd.CommandText = "UPDATE dbo.tbDOC SET POLICY_NO = @polendo WHERE DOC_CODE = " + row["REF_ID"].ToString();
                            cmd.Parameters.Add(new SqlParameter("polendo", poltemp));
                            crud.Executing(cmd);
                        }
                    }
                }
            }

            #region print card
            //Print Card
            dtTemp = crud.LoadData("SELECT PRINT_CARD FROM dbo.tbDOC WHERE DOC_CODE in (SELECT * FROM FNC_SPLIT('" + SelectedDocCode + "',',')) AND PRINT_CARD = 'Yes'").Tables[0];
            if (dtTemp.Rows.Count > 0) //Has Print Card
            {
                DialogResult res = Msgbox.Show("Do you want to send card for Card Printing? (If not, select No and send it manually.)", "Confirmation", "Yes", "No");
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (DataRow row in SelectedDoc.Rows)
                    {
                        if (row["TYPE"].ToString() == "Cancellation") continue;

                        string RefID = row["REF_ID"].ToString();
                        dtTemp = crud.LoadData("SELECT PRINT_CARD,PRODUCT_TYPE,POLICY_NO FROM dbo.tbDOC where DOC_CODE = " + RefID).Tables[0];
                        if (dtTemp.Rows[0]["PRINT_CARD"].ToString() == "Yes")
                        {
                            //string PolNo = polendo.Substring(0, 20);
                            string PolNo = dtTemp.Rows[0]["POLICY_NO"].ToString();

                            //string protype = dtTemp.Rows[0]["PRODUCT_TYPE"].ToString(); 
                            string protype = PolNo.Substring(7, 3);

                            if (protype == "Chinese PA") protype = "GPA";
                            string view = "";
                            if (protype == "BHP") view = "VIEW_MEMBERSHIP_BHP";
                            else if (protype == "CYC") view = "VIEW_MEMBERSHIP_CYC";
                            else if (protype == "GPA") view = "VIEW_MEMBERSHIP_GPA";
                            else if (protype == "HNS") view = "VIEW_MEMBERSHIP_HNS";
                            else if (protype == "VCM") view = "VIEW_MEMBERSHIP_VCM";
                            else if (protype == "VPC") view = "VIEW_MEMBERSHIP_VPC";
                            else if (protype == "MCW") view = "VIEW_MEMBERSHIP_MCW";
                            else if (protype == "PAC") view = "VIEW_MEMBERSHIP_PAC";
                            else if (protype == "PAE") view = "VIEW_MEMBERSHIP_PAE";
                            else
                            {
                                //Msgbox.Show("This Policy Type is not available for Card Printing! Please contact administrator.");
                                //Msgbox.Show("Document processed!");
                                //this.Close();
                                continue;
                            }


                            int maxnum = 1;
                            DataTable dtmaxNumber = new DataTable();
                            string username = maincrud.ExecQuery("SELECT USER_NAME FROM USER_PRINT_SYSTEM WHERE USER_CODE = '" + UserName + "'").Rows[0][0].ToString() + "-IMS";

                            string sqlcmd = "SELECT * FROM " + view + " WHERE POLICY_NO = '" + PolNo + "' AND (RECENTLY_ADD = 'Yes' OR RECENTLY_ADD = 'Change')";
                            if (protype == "CYC" || protype == "VPC" || protype == "VCM")
                                sqlcmd += " AND REG_NO <> 'TEMP'";

                            DataTable newrisk = maincrud.ExecQuery(sqlcmd);
                            if (newrisk.Rows.Count > 0)
                            {
                                if (protype == "BHP")
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_figtree_blue_max_print_number", "user", username);
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        //Mydb.ExecuteMySql("sp_figtree_blue_insert", "ref", rw[0], "name", rw[1], "pp_no", rw[2], "valid_from", Common.strToDate(rw[3].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[4].ToString()).ToString("yyyy-MM-dd"), "excess", rw[5], "module", rw[6], "member_since", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "created_by", username, "print_number", maxnum);
                                        Mydb.ExecuteMySql("sp_figtree_blue_insert_docctrl", "ref", rw[1], "policyholder", rw[2], "name", rw[3], "pp_no", rw[4], "valid_from", Common.strToDate(rw[5].ToString()).ToString("yyyy-MM-dd"),
                                            "@valid_to", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), "excess", rw[7], "module", rw[8], "member_since",
                                            Common.strToDate(rw[11].ToString()).ToString("yyyy-MM-dd"), "created_by", username, "print_number", maxnum, "insured_id", rw[0],
                                            "outpatient", rw[9], "maternity", rw[10], "doc_code", RefID);
                                    }

                                }
                                else if (protype == "CYC")
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_auto_max_print_number", "user", username, "auto_type", "MC");
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        Mydb.ExecuteMySql("sp_auto_insert_docctrl", "policy_no", rw[2], "coverage", rw[3], "make_model", rw[4], "chasis_engine_no", rw[5], "policy_holder", rw[1], "valid_from", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "reg_no", rw[0], "auto_type", "MC", "created_by", username, "print_number", maxnum, "doc_code", RefID);
                                    }
                                }
                                else if (protype == "VCM")
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_auto_max_print_number", "user", username, "auto_type", "CV");
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        Mydb.ExecuteMySql("sp_auto_insert_docctrl", "policy_no", rw[2], "coverage", rw[3], "make_model", rw[4], "chasis_engine_no", rw[5], "policy_holder", rw[1], "valid_from", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "reg_no", rw[0], "auto_type", "CV", "created_by", username, "print_number", maxnum, "doc_code", RefID);
                                    }
                                }
                                else if (protype == "VPC")
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_auto_max_print_number", "user", username, "auto_type", "PV");
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        Mydb.ExecuteMySql("sp_auto_insert_docctrl", "policy_no", rw[2], "coverage", rw[3], "make_model", rw[4], "chasis_engine_no", rw[5], "policy_holder", rw[1], "valid_from", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "reg_no", rw[0], "auto_type", "PV", "created_by", username, "print_number", maxnum, "doc_code", RefID);
                                    }
                                }
                                else if (protype == "GPA" || protype == "PAC")  //Update 12-Aug-20:Add PAC
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_gpa_max_print_number", "user", username);
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        DateTime validfrom = DateTime.Parse((Common.strToDate(rw[5].ToString())).ToString());
                                        DateTime validto = DateTime.Parse((Common.strToDate(rw[6].ToString())).ToString());
                                        Mydb.ExecuteMySql("sp_gpa_insert_docctrl", "insured_member", rw[1], "policy_holder", rw[3], "policy_no", rw[4], "valid_from", validfrom, "@valid_to", validto, "optional_benefit", rw[7], "dependent", rw[2], "sum_insured", Convert.ToDecimal(rw[8]), "medical_expense", Convert.ToDecimal(rw[9]), "created_by", username, "print_number", maxnum, "insured_id", rw[0], "doc_code", RefID);
                                    }
                                }
                                else if (protype == "MCW") //Update 04-Dec-19 Add MCW
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_mcw_max_print_number", "user", username);
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        Mydb.ExecuteMySql("sp_mcw_insert_docctrl", "insured_member", rw[1], "policy_holder", rw[2], "policy_no", rw[3], "valid_from", Common.strToDate(rw[4].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[5].ToString()).ToString("yyyy-MM-dd"), "dependent", "", "sum_insured", rw[8], "created_by", username, "print_number", maxnum, "insured_id", rw[0], "doc_code", RefID);
                                    }
                                }
                                else if (protype == "HNS" || protype == "PAE") //Update 31-May-23:Add PAE
                                {
                                    dtmaxNumber = Mydb.getDataTable("sp_hns_max_print_number", "user", username);
                                    if (dtmaxNumber.Rows[0][0] != DBNull.Value)
                                    {
                                        maxnum = (int)(dtmaxNumber.Rows[0][0]) + 1;
                                    }
                                    foreach (DataRow rw in newrisk.Rows)
                                    {
                                        Mydb.ExecuteMySql("sp_hns_insert_excess_docctrl", "insured_member", rw[1], "policy_holder", rw[3], "policy_no", rw[4], "valid_from", Common.strToDate(rw[6].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "optional_benefit", " ", "dependent", rw[2], "plan_type", rw[5], "excess", rw[10], "created_by", username, "print_number", maxnum, "insured_id", rw[0], "doc_code", RefID, "optional_care", rw[11]);
                                        //Mydb.ExecuteMySql("sp_hns_insert_excess", "insured_member", rw[2], "policy_holder", rw[4], "policy_no", rw[5], "valid_from", Common.strToDate(rw[7].ToString()).ToString("yyyy-MM-dd"), "@valid_to", Common.strToDate(rw[8].ToString()).ToString("yyyy-MM-dd"), "optional_benefit", " ", "dependent", rw[3], "plan_type", rw[6], "excess", Excess ? rw[11] : "", "created_by", username, "print_number", maxnum, "insured_id", rw[1], "optional_care", rw[12]);
                                    }
                                }

                                //Inset Card Sent Status
                                crud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) VALUES('" + RefID + "','" + DateTime.Now + "',17,'" + UserCode + "','" + DateTime.Now + "')");
                                //
                            }
                        }
                    }
                }
            }
            #endregion
            //Update on save invoice into database
            foreach (DataRow row1 in SelectedDoc.Rows)
            {
                string RefID = row1["REF_ID"].ToString();
                dtTemp = crud.LoadData("SELECT POLICY_NO,PRODUCT_TYPE FROM dbo.tbDOC where DOC_CODE = " + RefID).Tables[0];
                string PolNo = dtTemp.Rows[0]["POLICY_NO"].ToString().Trim().ToUpper();
                string ProType = dtTemp.Rows[0]["PRODUCT_TYPE"].ToString().ToUpper();

                if (PolNo == "") continue;

                string chkLastEnd = "SELECT POL_TRANSACTION_TYPE FROM UW_T_POLICIES WHERE POL_POLICY_NO =" + "'" + PolNo + "' ORDER BY POL_AUTHORIZED_DATE DESC";
                DataTable dtchkLastEnd = maincrud.ExecQuery(chkLastEnd);
                if (dtchkLastEnd.Rows[0]["POL_TRANSACTION_TYPE"].ToString() != "S")
                {

                    string sqlcmd = "select DEB_DEB_NOTE_NO from " +
                      "( " +
                      "select DEB_DEB_NOTE_NO, CREATED_DATE from RC_T_DEBIT_NOTE where DEB_POLICY_NO='" + PolNo + "' " +
                      "union " +
                      "select CRN_CREDIT_NOTE_NO, CREATED_DATE from RC_T_CREDIT_NOTE where CRN_POL_POLICY_NO='" + PolNo + "' " +
                      ") " +
                      "order by CREATED_DATE DESC";


                    dtTemp = maincrud.ExecQuery(sqlcmd);
                    if (dtTemp.Rows.Count > 0)
                    {
                        string note = dtTemp.Rows[0]["DEB_DEB_NOTE_NO"].ToString(); // debit or credit note
                        crud.Executing("Update tbDOC set DN_CN= '" + note + "' where DOC_CODE=" + RefID);
                    }
                }
            }


            //update theane 02-11-2021 notification on doc processed
            DialogResult dr_msg = Msgbox.Show("Would you like to print invoice?", "", "Yes", "No");
            if (dr_msg == System.Windows.Forms.DialogResult.No)
            {
                Cursor.Current = Cursors.AppStarting;
                Msgbox.Show("Document processed!");
                this.Close();
                return;
            }
            else
            {
                //Invoice

                Cursor.Current = Cursors.WaitCursor;
                foreach (DataRow row in SelectedDoc.Rows)
                {
                    string RefID = row["REF_ID"].ToString();
                    dtTemp = crud.LoadData("SELECT POLICY_NO,PRODUCT_TYPE FROM dbo.tbDOC where DOC_CODE = " + RefID).Tables[0];
                    string PolNo = dtTemp.Rows[0]["POLICY_NO"].ToString().Trim().ToUpper();
                    string ProType = dtTemp.Rows[0]["PRODUCT_TYPE"].ToString().ToUpper();

                    //if (PolNo == "") continue;

                    //string chkLastEnd = "SELECT POL_TRANSACTION_TYPE FROM UW_T_POLICIES WHERE POL_POLICY_NO =" + "'" + PolNo + "' ORDER BY POL_AUTHORIZED_DATE DESC";
                    //DataTable dtchkLastEnd = maincrud.ExecQuery(chkLastEnd);
                    //if (dtchkLastEnd.Rows[0]["POL_TRANSACTION_TYPE"].ToString() != "S")
                    //{

                    //    string sqlcmd = "select DEB_DEB_NOTE_NO from " +
                    //      "( " +
                    //      "select DEB_DEB_NOTE_NO, CREATED_DATE from RC_T_DEBIT_NOTE where DEB_POLICY_NO='" + PolNo + "' " +
                    //      "union " +
                    //      "select CRN_CREDIT_NOTE_NO, CREATED_DATE from RC_T_CREDIT_NOTE where CRN_POL_POLICY_NO='" + PolNo + "' " +
                    //      ") " +
                    //      "order by CREATED_DATE DESC";

                    dtTemp = crud.LoadData("SELECT DN_CN FROM dbo.tbDOC where DOC_CODE = " + RefID).Tables[0];
                    //    dtTemp = maincrud.ExecQuery(sqlcmd);
                    if (dtTemp.Rows.Count > 0)
                    {
                        string note = dtTemp.Rows[0]["DN_CN"].ToString(); // debit or credit note
                        DataTable dt = maincrud.ExecQuery("SELECT * FROM VIEW_PRINT_INVOICE WHERE DEBIT_NOTE = '" + note + "'");

                        if (dt.Rows.Count > 0)
                        {
                            dt.Columns.Add("EXCHANGE_RATE", typeof(System.String));
                            dt.Columns.Add("TOTAL_FUND_KH", typeof(System.String));
                            dt.Columns.Add("KH_NAME", typeof(System.String));
                            dt.Columns.Add("KH_ADDR", typeof(System.String));

                            dt.Columns.Add("COI", typeof(System.String));
                            dt.Columns.Add("IS_PRINTED_COI", typeof(System.String));
                            dt.Columns.Add("IS_PRINTED_STAMP", typeof(System.String));
                            dt.Columns.Add("IS_PRINTED_AUTO_STAMP", typeof(System.String));
                            //crud.Executing("Update tbDOC set DN_CN= '" + note + "' where DOC_CODE=" + RefID); 
                            //dtTemp = maincrud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + DateTime.Now.ToString("dd-MMM-yyyy") + "'");
                            dtTemp = maincrud.ExecQuery("SELECT RATE FROM USER_EXCHANGE_RATE WHERE ON_DATE = '" + dt.Rows[0]["TRAN_DATE"].ToString() + "'");
                            int ExchangeRate = 0;
                            if (dtTemp.Rows.Count > 0)
                            {
                                ExchangeRate = Convert.ToInt32(dtTemp.Rows[0][0]);
                            }


                            string KhName = "", KhAddr = "";
                            dtTemp = maincrud.ExecQuery("SELECT * FROM USER_CUS_KH_DETAIL WHERE CUS_CODE = " +
                                "(SELECT DEB_CUS_CODE FROM RC_T_DEBIT_NOTE WHERE DEB_DEB_NOTE_NO = '" + note + "' " +
                                "union SELECT CRN_CUS_CODE FROM RC_T_CREDIT_NOTE WHERE CRN_CREDIT_NOTE_NO = '" + note + "') ");
                            if (dtTemp.Rows.Count > 0)
                            {
                                KhName = dtTemp.Rows[0]["CUS_NAME"].ToString();
                                KhAddr = dtTemp.Rows[0]["CUS_ADDR"].ToString();
                            }

                            foreach (DataRow r in dt.Rows)
                            {
                                r["EXCHANGE_RATE"] = String.Format("{0:N}", ExchangeRate).Replace(".00", "");
                                r["TOTAL_FUND_KH"] = String.Format("{0:N}", Convert.ToDouble(r["TOTAL_FUND"]) * ExchangeRate);
                                //r["TOTAL_FUND_KH"] = String.Format("{0:N}", Decimal.Round(Convert.ToDecimal(row["TOTAL_FUND"]) * ExchangeRate, 0));
                                r["KH_NAME"] = KhName;
                                r["KH_ADDR"] = KhAddr;

                                r["COI"] = string.Empty;
                                r["IS_PRINTED_COI"] = "FALSE";
                                r["IS_PRINTED_STAMP"] = "FALSE";
                                r["IS_PRINTED_AUTO_STAMP"] = "FALSE";
                            }

                            ReportClass rpt = new ReportClass();

                            if (note[0] == 'D') //Debit Note
                            {
                                string accountcode = dt.Rows[0]["ACCOUNT_CODE"].ToString();
                                string producer = accountcode.Split('/')[1], cuscode = accountcode.Split('/')[2];
                                dtTemp = maincrud.ExecQuery("SELECT DISTINCT BANK_NAME,TRANFER_TO,ACCOUNT_NO,SWIFT_CODE FROM VIEW_PAYMENT_INSTRUCTION WHERE CODE = '" + producer + "' OR CODE = '" + cuscode + "'");
                                if (dtTemp.Rows.Count > 0) //has payment instruction set
                                {
                                    ////check for N/A
                                    DataRow[] ToDelete = dtTemp.Select("BANK_NAME = 'N/A'"); //create this cuz cannot modify data directly in foreach
                                    foreach (DataRow dr in ToDelete)
                                    {
                                        dtTemp.Rows.Remove(dr); //remove N/A
                                    }
                                    //

                                    if (dtTemp.Rows.Count == 0) //that's mean has only N/A record => use NewInvoice for NA
                                    {
                                        if (dt.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                                        {
                                            rpt = new Reports.NewInvoiceNAEndo();
                                            rpt.SetDataSource(dt);
                                        }
                                        else
                                        {
                                            rpt = new Reports.NewInvoiceNA();
                                            rpt.SetDataSource(dt);
                                        }
                                    }
                                    else //after remove N/A still has other bank records => use NewInvoice with Payment instruction bank table
                                    {
                                        dtTemp.Columns.Add("DEBIT_NOTE", typeof(System.String)); //Add in order to link to another table in Report
                                        foreach (DataRow dr in dtTemp.Rows)
                                        {
                                            dr["DEBIT_NOTE"] = dt.Rows[0]["DEBIT_NOTE"].ToString();
                                        }
                                        DataSet ds = new DataSet();
                                        dt.TableName = "VIEW_INVOICE"; //change name in order to make Crystal report recognize (Multi Datatable in Datasource need to have the same name)
                                        dtTemp.TableName = "PAYMENT_INSTRUCTION";
                                        ds.Tables.Add(dt);
                                        ds.Tables.Add(dtTemp);

                                        if (dt.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                                        {
                                            rpt = new Reports.NewInvoiceEndo();
                                            rpt.SetDataSource(ds);
                                        }
                                        else
                                        {
                                            rpt = new Reports.NewInvoice();
                                            rpt.SetDataSource(ds);
                                        }
                                    }
                                }
                                else
                                {
                                    if (dt.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                                    {
                                        rpt = new Reports.PrintInvoiceEndListAll();
                                        rpt.SetDataSource(dt);
                                    }
                                    else
                                    {
                                        rpt = new Reports.PrintInvoiceListAll();
                                        rpt.SetDataSource(dt);
                                    }
                                }
                            }
                            else if (note[0] == 'C') //Credit Note
                            {
                                if (dt.Rows[0]["ENDORSEMENT_NO"].ToString() != "")
                                {
                                    rpt = new Reports.CreditNoteEnd();
                                    rpt.SetDataSource(dt);
                                }
                                else
                                {
                                    rpt = new Reports.CreditNote();
                                    rpt.SetDataSource(dt);
                                }
                            }

                            // rpt.SetDataSource(dt);
                            var frm = new frmViewInstructionNote();
                            frm.Text = "Invoice";
                            frm.rpt = rpt;
                            frm.Show();
                        }

                        
                    }



                    //Print Invoice fro product Figtree Blue Acknowledgement
                    if (ProType == "BHP")
                    {

                        Reports.BHPLetter bhp_rpt = new Reports.BHPLetter();

                        string sqlBHP = "SELECT (CASE WHEN SUBSTR(POL_ENDORSEMENT_NO,1,1)='R' OR POL_ENDORSEMENT_NO is null THEN POL_POLICY_NO ELSE POL_ENDORSEMENT_NO END) POLICY_NO FROM UW_T_POLICIES WHERE POL_POLICY_NO = " + "'" + PolNo + "'" + "ORDER BY POL_AUTHORIZED_DATE DESC";
                        DataTable dtBHP = maincrud.ExecQuery(sqlBHP);
                        string polEnd = dtBHP.Rows[0]["POLICY_NO"].ToString().ToUpper();
                        bhp_rpt.SetParameterValue("POL_NO", polEnd);
                        var BHP_frm = new frmViewInstructionNote();
                        BHP_frm.Text = "BHPLetter";
                        BHP_frm.rpt = bhp_rpt;
                        BHP_frm.Show();
                    }


                    //
                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show("Document processed!");
                    //Switch to Submit Card Printing 
                    //foreach (DataRow row in SelectedDoc.Rows)
                    //{
                    //    string RefID = row["REF_ID"].ToString();
                    //    dtTemp = crud.LoadData("SELECT PRINT_CARD,PRODUCT_TYPE FROM dbo.tbDOC where DOC_CODE = " + RefID).Tables[0];
                    //    if (dtTemp.Rows[0][0].ToString() == "Yes")
                    //    {
                    //        frmDocumentControl.PrintCard = true;
                    //        frmDocumentControl.fwdpolno = polendo.Substring(0, 20);
                    //        break;
                    //    }
                    //}
                    //
                    this.Close();
                    //}
                }
            }
        }

        private void frmDPRemark_Load(object sender, EventArgs e)
        {
            lblDetail.Text = "Document detail: " + SelectedDoc.Rows.Count + " document(s) is/are selected.";
            lblDetail.MaximumSize = new Size(450, 50);

            try
            {
                //string doctype = SelectedDoc.Rows[0]["TYPE"].ToString();
                //if (doctype == "Renewal Policy" || doctype == "Endorsement" || doctype == "Cancellation")
                //{
                //    string refid = SelectedDoc.Rows[0]["REF_ID"].ToString();
                //    DataTable dtTemp = crud.LoadData("SELECT POLICY_NO FROM dbo.tbDOC WHERE DOC_CODE = " + refid).Tables[0];
                //    tbPolEndo.Text = dtTemp.Rows[0][0].ToString();

                //}
                //else if (doctype == "Policy")
                //{
                //    string prod = SelectedDoc.Rows[0]["PRODUCT_TYPE"].ToString();
                //    prod = (prod == "Chinese PA") ? "GPA" : prod;
                //    string c = "select POL_POLICY_NO, CUS_NAME from " +
                //               "( " +
                //               "select POL_POLICY_NO,nvl(CUS_INDV_SURNAME,CUS_CORP_NAME)CUS_NAME from UW_T_POLICIES a, UW_M_CUSTOMERS b where a.POL_CUS_CODE = b.CUS_CODE and POL_TRANSACTION_TYPE = 'N' and POL_PRD_CODE = '" + prod + "' " +
                //               ") " +
                //               "where CUS_NAME = '" + SelectedDoc.Rows[0]["CUSTOMER"].ToString() + "'";
                //    CRUD maincrud = new CRUD();
                //    DataTable dtTemp = maincrud.ExecQuery(c);
                //    if (dtTemp.Rows.Count > 0)
                //        tbPolEndo.Text = dtTemp.Rows[0]["POL_POLICY_NO"].ToString();
                //}
                string refid = SelectedDoc.Rows[0]["REF_ID"].ToString(), cuscode = "";
                DataTable dtTemp = crud.LoadData("SELECT POLICY_NO,CUS_CODE FROM dbo.tbDOC WHERE DOC_CODE = " + refid).Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    tbPolEndo.Text = dtTemp.Rows[0]["POLICY_NO"].ToString().Trim();
                    cuscode = dtTemp.Rows[0]["CUS_CODE"].ToString().Trim();
                }

                string doctype = SelectedDoc.Rows[0]["TYPE"].ToString();

                if (doctype == "Policy")
                {
                    if (tbPolEndo.Text != "")
                    {
                        if (tbPolEndo.Text[0] != 'D' && tbPolEndo.Text.Length != 20)
                            tbPolEndo.Text = "";
                    }

                    if (tbPolEndo.Text == "")
                    {
                        string prod = SelectedDoc.Rows[0]["PRODUCT_TYPE"].ToString();
                        prod = (prod == "Chinese PA") ? "GPA" : prod;
                        //string c = "select POL_POLICY_NO, CUS_NAME from " +
                        //           "( " +
                        //           "select POL_POLICY_NO,nvl(CUS_INDV_SURNAME,CUS_CORP_NAME)CUS_NAME from UW_T_POLICIES a, UW_M_CUSTOMERS b " +
                        //           "where a.POL_CUS_CODE = b.CUS_CODE and POL_TRANSACTION_TYPE = 'N' " +
                        //           "and POL_PRD_CODE = '" + prod + "' and TRUNC(POL_PERIOD_TO)>to_date(to_char(sysdate, 'DD/MM/YYYY'),'DD/MM/YYYY') and POL_CANCELLED_BY is null order by POL_AUTHORIZED_DATE desc " +
                        //           ") " +
                        //           "where CUS_NAME = '" + SelectedDoc.Rows[0]["CUSTOMER"].ToString() + "'";

                        string c = "select POL_POLICY_NO from UW_T_POLICIES " +
                                   " where POL_CUS_CODE = '" + cuscode + "' and POL_TRANSACTION_TYPE = 'N' " +
                                   " and POL_PRD_CODE = '" + prod + "' and TRUNC(POL_PERIOD_TO)>to_date(to_char(sysdate, 'DD/MM/YYYY'),'DD/MM/YYYY') and POL_CANCELLED_BY is null order by POL_AUTHORIZED_DATE desc";
                        dtTemp = maincrud.ExecQuery(c);
                        if (dtTemp.Rows.Count > 0)
                            tbPolEndo.Text = dtTemp.Rows[0]["POL_POLICY_NO"].ToString().Trim();
                    }
                }

                //add endo no
                if (doctype == "Endorsement")
                {
                    dtTemp = maincrud.ExecQuery("SELECT POL_ENDORSEMENT_NO FROM UW_T_POLICIES WHERE POL_STATUS in (4,5,6,10) AND POL_POLICY_NO = '" + tbPolEndo.Text + "' ORDER BY POL_AUTHORIZED_DATE DESC");
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbRemark.Text = dtTemp.Rows[0][0].ToString();
                    }
                }
                else if (doctype == "Cancellation")
                {
                    dtTemp = maincrud.ExecQuery("SELECT POL_CAN_ENDORSEMENT_NO FROM UW_T_POLICIES WHERE POL_STATUS = 9 AND POL_POLICY_NO = '" + tbPolEndo.Text + "' ORDER BY POL_CANCELLED_DATE DESC");
                    if (dtTemp.Rows.Count > 0)
                    {
                        tbRemark.Text = dtTemp.Rows[0][0].ToString();
                    }
                }
                //

                if (tbPolEndo.Text == "")
                    tbPolEndo.Select();
                else
                    tbRemark.Select();
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }
    }
}
