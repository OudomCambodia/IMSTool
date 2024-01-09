using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmAddDocument1 : Form
    {
        public string username = string.Empty, usercode = string.Empty, team = string.Empty;
        CRUD crud = new CRUD();
        public static string drivePath = @"\\192.168.110.228\Infoins_IMS_Upload_doc$\";
        DBS11SqlCrud sqlcrud = new DBS11SqlCrud();
        string VIEW_CUSTOMER = "(select CUS_CODE, nvl(CUS_INDV_SURNAME,CUS_CORP_NAME) CUS_NAME,CUS_BPARTY_CODE,(SELECT SFC_SURNAME FROM SM_M_SALES_FORCE WHERE SFC_CODE = CUS_BPARTY_CODE) BPARTY_NAME from UW_M_CUSTOMERS where CUS_STATUS = 'A')";
        //public static Dictionary<string, string> product = new Dictionary<string, string>() {  
        //    {"GPA", "A&H"}, {"PAC", "A&H"}, {"Chinese PA", "A&H"}, 
        //    {"HNS", "A&H"}, {"EMC", "A&H"}, {"MED", "A&H"}, {"STN", "A&H"}, {"MCW", "A&H"}, {"CAN", "A&H"}, {"BHP", "A&H"},
        //    {"TRV", "A&H"}, {"TRA", "A&H"}, {"TRP", "A&H"},

        //    {"VPC", "AUTO"}, {"VCM", "AUTO"}, {"CYC", "AUTO"},

        //    {"BBB", "FLM"}, {"MON", "FLM"}, {"FIG", "FLM"}, {"DNO", "FLM"}, {"PID", "FLM"}, 
        //    {"TCI", "FLM"}, {"BON", "FLM"},
        //    {"PUL", "FLM"}, {"PRO", "FLM"}, {"CPP", "FLM"}, {"GEL", "FLM"}, {"EML", "FLM"}, {"BAL", "FLM"},
        //    {"MCG", "FLM"}, {"MCO", "FLM"}, {"HUL", "FLM"}, {"FFL", "FLM"}, 

        //    {"FIC", "P&E"}, {"FID", "P&E"}, {"FIR", "P&E"}, {"PLG", "P&E"}, {"PAR", "P&E"}, {"IAR", "P&E"}, {"LOP", "P&E"},
        //    {"HOM", "P&E"}, {"BBD", "P&E"}, {"EQU", "P&E"}, {"MUL", "P&E"},
        //    {"BUR", "P&E"}, {"HOL", "P&E"},
        //    {"CON", "P&E"}, {"CPE", "P&E"}, {"MBD", "P&E"}, {"EAR", "P&E"}, {"BEX", "P&E"}, {"ONG", "P&E"}, {"WID", "P&E"}
        //};
        //public static Dictionary<string, string> product = new Dictionary<string, string>() {  
        //    {"GPA", "A&H"}, {"PAC", "A&H"}, {"Chinese PA", "A&H"}, 
        //    {"HNS", "A&H"}, {"EMC", "A&H"}, {"MED", "A&H"}, {"STN", "A&H"}, {"MCW", "A&H"}, {"CAN", "A&H"}, {"BHP", "A&H"},
        //    {"TRV", "A&H"}, {"TRA", "A&H"}, {"TRP", "A&H"},{"DPA", "A&H"},{"PAP","A&H"},{"TRC","A&H"},{"TRI","A&H"},{"CVD","A&H"},{"CVO","A&H"},
        //    {"CVP","A&H"},

        //    {"VPC", "AUTO"}, {"VCM", "AUTO"}, {"CYC", "AUTO"},

        //    {"BBB", "F&L"}, {"MON", "F&L"}, {"FIG", "F&L"}, {"DNO", "F&L"}, {"PID", "F&L"}, 
        //    {"TCI", "F&L"}, {"BON", "F&L"},
        //    {"PUL", "F&L"}, {"PRO", "F&L"}, {"CPP", "F&L"}, {"GEL", "F&L"}, {"EML", "F&L"}, {"BAL", "F&L"},{"CYB","F&L"},
        //    {"GOL","F&L"},{"STU","F&L"},{"HOL","F&L"},


        //    {"MCG", "PE&M"}, {"MCO", "PE&M"}, {"HUL", "PE&M"}, {"FFL", "PE&M"}, 

        //    {"FIP","PE&M"},{"FIC", "PE&M"}, {"FID", "PE&M"}, {"FIR", "PE&M"}, {"PLG", "PE&M"}, {"PAR", "PE&M"}, {"IAR", "PE&M"}, {"LOP", "PE&M"},
        //    {"HOM", "PE&M"}, {"BBD", "PE&M"}, {"EQU", "PE&M"}, {"MUL", "PE&M"},
        //    {"BUR", "PE&M"},{"AHL","PE&M"},{"PNI","PE&M"},
        //    {"CON", "PE&M"}, {"CPE", "PE&M"}, {"MBD", "PE&M"}, {"EAR", "PE&M"}, {"BEX", "PE&M"}, {"ONG", "PE&M"}, 
            
            
        //    {"WID", "MICR"},{"HNA","MICR"}


        //};

        public static Dictionary<string, string> product = new Dictionary<string, string>();
       
        //print card
        //VPC,VCM,CYC,BHP,GPA,HNS,MCW
        //

        public static string selectedCusCode = string.Empty, selectedCusName = string.Empty;
        public static string selectedABCode = string.Empty, selectedABName = string.Empty;
        //AB: Agents/Brokers


        public frmAddDocument1()
        {
            InitializeComponent();
        }

        private void frmAddDocument1_Load(object sender, EventArgs e)
        {

            //cbDocType.Items.Add(new ComboboxItem("Quotation", "Q"));
            //cbDocType.Items.Add(new ComboboxItem("Renewal Quotation", "RQ"));
            cbDocType.Items.Add(new ComboboxItem("Policy", "P"));
            cbDocType.Items.Add(new ComboboxItem("Renewal Policy", "RP"));
            cbDocType.Items.Add(new ComboboxItem("Endorsement", "E"));
            cbDocType.Items.Add(new ComboboxItem("Cancellation", "C"));
            cbDocType.SelectedIndex = -1;

            cbPriority.Items.Add(new ComboboxItem("Normal", "N"));
            cbPriority.Items.Add(new ComboboxItem("Urgent", "U"));
            cbPriority.Items.Add(new ComboboxItem("Very Urgent", "VU"));
            cbPriority.SelectedIndex = -1;

            cbSubmitVia.Items.Add(new ComboboxItem("Hard Copy", "HC"));
            cbSubmitVia.Items.Add(new ComboboxItem("Email", "E"));
            cbSubmitVia.SelectedIndex = -1;

            cbPremiumType.Items.Add(new ComboboxItem("Paid", "PP"));
            cbPremiumType.Items.Add(new ComboboxItem("Unpaid", "UP"));
            cbPremiumType.Items.Add(new ComboboxItem("Others", "OP"));
            cbPremiumType.SelectedIndex = -1;


            cbClientType.Items.Add(new ComboboxItem("Dep. Pro", "DP"));
            cbClientType.Items.Add(new ComboboxItem("Loan Client", "LC"));
            cbClientType.Items.Add(new ComboboxItem("REF", "RE"));
            cbClientType.Items.Add(new ComboboxItem("Other", "OT"));
            cbClientType.SelectedIndex = -1;

            Cursor.Current = Cursors.WaitCursor;


            //string ProducerTeam = sqlcrud.LoadData("SELECT TEAM FROM dbo.tbDOC_USER WHERE USER_NAME = '" + username + "'").Tables[0].Rows[0][0].ToString();
            //tbProducerTeam.Text = ProducerTeam;

            //if (ProducerTeam == "NO")//UWHEAD
            //{
            //    tbProducerTeam.Text = "Agents";
            //    ProducerTeam = "Agents";
            //}

            //DataTable dtTemp = sqlcrud.LoadData("SELECT NAME FROM dbo.tbPRODUCER WHERE TEAM = '" + ProducerTeam + "'").Tables[0];
            //cbProducerName.DataSource = dtTemp.DefaultView;
            //cbProducerName.DisplayMember = "NAME";
            //cbProducerName.ValueMember = "NAME";


            dtpToBeFinish.MinDate = DateTime.Now;

            //cbProLine.SelectedIndex = 0;

            //select latest Info for default setting
            DataTable dtTemp = sqlcrud.LoadData("SELECT PRODUCT_LINE,PRODUCT_TYPE,SUBMIT_VIA,PRIORITY_TYPE,PRINT_CARD FROM dbo.tbDOC WHERE CREATE_BY = '"+usercode+"' ORDER BY CREATE_DATE DESC").Tables[0];
            if (dtTemp.Rows.Count > 0)
            {
                string line = dtTemp.Rows[0]["PRODUCT_LINE"].ToString(),
                    code = dtTemp.Rows[0]["PRODUCT_TYPE"].ToString(),
                    submit = dtTemp.Rows[0]["SUBMIT_VIA"].ToString(),
                    priority = dtTemp.Rows[0]["PRIORITY_TYPE"].ToString(),
                    printcard = dtTemp.Rows[0]["PRINT_CARD"].ToString();
                cbProLine.SelectedIndex = cbProLine.FindStringExact(line);
                cbProType.SelectedIndex = cbProType.FindStringExact(code);
                cbSubmitVia.SelectedIndex = (submit == "HC") ? 0 : 1;
                if (priority == "N")
                    cbPriority.SelectedIndex = 0;
                else if (priority == "U")
                    cbPriority.SelectedIndex = 1;
                else if (priority == "VU")
                    cbPriority.SelectedIndex = 2;

                if (printcard == "Yes")
                    rbYes.Checked = true;
                else
                    rbNo.Checked = true;
            }
            //

            tbPremium.Text = "";


            //Load data into combo proline
            foreach (var a in frmAddDocument1.product.Values.ToList().Distinct())
            {
                cbProLine.Items.Add(a);
            }
            cbProLine.SelectedIndex = 0;
            Cursor.Current = Cursors.AppStarting;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmDocumentControl.SubFrmChange = false;
            this.Close();
        }

        private void cbProType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbDocType.Text != "" && tbCusCode.Text != "" && tbCusName.Text != "" && cbPriority.Text != "" &&
                    cbProType.Text != "" && cbProLine.Text != "" && tbProducerCode.Text != "" && tbProducerName.Text != "" &&
                    cbSubmitVia.Text != "" && (rbYes.Checked || rbNo.Checked) && tbPremium.Text != "")
                {

                    string PolNo = tbPolicyNo.Text.ToUpper().Trim();


                    if (cbDocType.Text == "Renewal Policy" || cbDocType.Text == "Endorsement" || cbDocType.Text == "Cancellation")
                    {
                        if (PolNo == "")
                        {
                            Msgbox.Show("Please input Policy No.");
                            tbPolicyNo.Focus();
                            return;
                        }
                        else
                        {
                            DataTable dtTemp = crud.ExecQuery("select POL_SEQ_NO from UW_T_POLICIES where (POL_POLICY_NO = '" + PolNo + "' or POL_ENDORSEMENT_NO = '" + PolNo + "')");
                            if (dtTemp.Rows.Count <= 0)
                            {
                                Msgbox.Show("Policy/Endorsement No: " + PolNo + " is incorrect, please check again!");
                                tbPolicyNo.Focus();
                                return;
                            }
                        }
                    }
                    else
                        PolNo = "";

                    //Check Producer Team
                    if (team == "BizDev")
                    {
                        if (tbProducerCode.Text.Trim() == "U-BRK" || tbProducerCode.Text.Trim() == "U-AGT")
                        {
                            Msgbox.Show("The Customer you selected is under Producer Code: " + tbProducerCode.Text + ", cannot Add Document!");
                            return;
                        }
                    }
                    else if (team == "Agents")
                    {
                        if (tbProducerCode.Text.Trim() != "U-AGT")
                        {
                            DataTable dtTemp = sqlcrud.LoadData("SELECT EXCEPTION_CODE FROM dbo.tbExceptionalRole WHERE USER_CODE = '" + usercode + "'").Tables[0];
                            if (dtTemp.Rows.Count > 0)
                            {
                                string[] ExceptionCode = dtTemp.Rows[0][0].ToString().Split(',');
                                if (ExceptionCode[0] == "U-BNK")
                                {
                                    if (tbProducerCode.Text.Trim() != "U-BNK")
                                    {
                                        Msgbox.Show("You can't add document for the selected Customer!");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (tbProducerCode.Text.Trim() == "U-BRK")
                                    {
                                        foreach (string s in ExceptionCode)
                                        {
                                            if (s != tbABCode.Text.Trim())
                                            {
                                                Msgbox.Show("You can't add document for the selected Customer!");
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Msgbox.Show("You can't add document for the selected Customer!");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Msgbox.Show("The Customer you selected is not under Producer Code: U-AGT, cannot Add Document!");
                                return;
                            }
                        }
                    }
                    else if (team == "Brokers")
                    {
                        if (tbProducerCode.Text.Trim() != "U-BRK")
                        {
                            DataTable dtTemp = sqlcrud.LoadData("SELECT EXCEPTION_CODE FROM dbo.tbExceptionalRole WHERE USER_CODE = '" + usercode + "'").Tables[0];
                            if (dtTemp.Rows.Count > 0)
                            {
                                string[] ExceptionCode = dtTemp.Rows[0][0].ToString().Split(',');
                                if (ExceptionCode[0] == "U-BNK")
                                {
                                    if (tbProducerCode.Text.Trim() != "U-BNK")
                                    {
                                        Msgbox.Show("You can't add document for the selected Customer!");
                                        return;
                                    }
                                }
                                else
                                {
                                    if (tbProducerCode.Text.Trim() == "U-AGT")
                                    {
                                        foreach (string s in ExceptionCode)
                                        {
                                            if (s != tbABCode.Text.Trim())
                                            {
                                                Msgbox.Show("You can't add document for the selected Customer!");
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Msgbox.Show("You can't add document for the selected Customer!");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                Msgbox.Show("The Customer you selected is not under Producer Code: U-BRK, cannot Add Document!");
                                return;
                            }
                        }
                    }
                    //


                    string ConfirmMsg = "Are you sure you want to add " + cbDocType.Text + " document of \"" + tbCusCode.Text + "-" + tbCusName.Text + "\"" + ((dgvFile.Rows.Count <= 0) ? "" : " with " + dgvFile.Rows.Count + " attachment(s)") + "?";

                    DialogResult dr = Msgbox.Show(ConfirmMsg, "Confirmation");
                    if (dr == System.Windows.Forms.DialogResult.Yes)
                    {

                        string PrintCard = (rbYes.Checked) ? "Yes" : "No";

                        //DataTable dtTemp = sqlcrud.ExecuteMySqlOutPara("dbo.sp_insert_doc", "@p_DOC_TYPE", (cbDocType.SelectedItem as ComboboxItem).Value.ToString(),
                        //    "@p_CUS_CODE", tbCusCode.Text, "@p_CUS_NAME", tbCusName.Text, "@p_PRODUCT_TYPE", cbProType.Text.ToUpper(),
                        //    "@p_PRODUCT_LINE", cbProLine.Text.ToUpper(), "@p_PRIORITY_TYPE", (cbPriority.SelectedItem as ComboboxItem).Value.ToString(),
                        //    "@p_PRODUCER_CODE", tbProducerCode.Text, "@p_PRODUCER_NAME", tbProducerName.Text,
                        //    "@p_TO_BE_FINISHED_ON", (cbPriority.Text == "Normal") ? "" : dtpToBeFinish.Text, "@p_CREATE_DATE", DateTime.Now,
                        //    "@p_SUBMIT_VIA", (cbSubmitVia.SelectedItem as ComboboxItem).Value.ToString(), "@p_POLICY_NO", tbPolicyNo.Text,
                        //    "@p_QUOT_NO", tbQuotNo.Text, "@p_DOC_CUR_STATUS", 0, "@p_DOC_CUR_STATUS_SET_BY", usercode,
                        //    "@p_DOC_CUR_STATUS_SET_ON", DateTime.Now, "@p_STATUS", "O", "@p_STATUS_REMARK", "", "@p_PRINT_CARD", PrintCard,
                        //    "@p_CREATE_BY", usercode, "@p_AGENT_BROKER_CODE", tbABCode.Text.Trim());


                        //string clienttype, premiumtype;
                        //if (cbClientType.SelectedItem == null)
                        //{
                        //    clienttype = "OT";
                        //}
                        //else
                        //{
                        //    clienttype = (cbClientType.SelectedItem as ComboboxItem).Value.ToString();
                        //}
                        //if (cbPremiumType.SelectedItem == null)
                        //{
                        //    premiumtype = "OP";
                        //}
                        //else
                        //{
                        //    premiumtype = (cbPremiumType.SelectedItem as ComboboxItem).Value.ToString();
                        //}




                        DataTable dtTemp = sqlcrud.ExecuteMySqlOutPara("dbo.sp_insert_doc", "@p_DOC_TYPE", (cbDocType.SelectedItem as ComboboxItem).Value.ToString(),
                        "@p_CUS_CODE", tbCusCode.Text, "@p_CUS_NAME", tbCusName.Text, "@p_PRODUCT_TYPE", cbProType.Text,
                        "@p_PRODUCT_LINE", cbProLine.Text.ToUpper(), "@p_PRIORITY_TYPE", (cbPriority.SelectedItem as ComboboxItem).Value.ToString(),
                        "@p_PRODUCER_CODE", tbProducerCode.Text, "@p_PRODUCER_NAME", tbProducerName.Text,
                        "@p_TO_BE_FINISHED_ON", (cbPriority.Text == "Normal") ? "" : dtpToBeFinish.Text, "@p_CREATE_DATE", DateTime.Now,
                        "@p_SUBMIT_VIA", (cbSubmitVia.SelectedItem as ComboboxItem).Value.ToString(), "@p_POLICY_NO", PolNo,
                        "@p_QUOT_NO", tbQuotNo.Text, "@p_DOC_CUR_STATUS", 0, "@p_DOC_CUR_STATUS_SET_BY", usercode,
                        "@p_DOC_CUR_STATUS_SET_ON", DateTime.Now, "@p_STATUS", "O", "@p_STATUS_REMARK", "", "@p_PRINT_CARD", PrintCard,
                        "@p_CREATE_BY", usercode, "@p_AGENT_BROKER_CODE", tbABCode.Text.Trim(), "@p_COMMISSION", tbCommission.Text, "@p_EFFECTIVE_DATE", tbEffectiveDate.Text,
                        "@p_OTHER_INSTRUCTION", tbOtherInstruction.Text, "@p_REMARK_NOTE", tbRemarkNote.Text, "@p_REMARK_RATE", tbRemarkRate.Text,
                        "@p_ORIGINAL_RATE", tbOriginalRate.Text, "@p_GROUP_DISCOUNT", tbGroupDiscount.Text, "@p_LOYALTY_DISCOUNT", tbLoyaltyDiscount.Text,
                        "@p_NCD", tbNCD.Text, "@p_SPECIAL_DISCOUNT", tbSpecialDiscount.Text, "@p_FLEET_SIZE_DISCOUNT", tbFleetSizeDiscount.Text,
                        "@p_DISCOUNT", tbDiscount.Text, "@p_LOADING", tbLoading.Text, "@p_FINAL_PREMIUM_PER_PERSON", tbFinalPremium.Text, "@p_PREMIUM", tbPremium.Value);
                        //, "@p_PRREMIUM_TYPE", premiumtype,"@p_CLIENT_CATAG", clienttype, "p_ClientDetails",tbClientDetails.Text);

                        string DocCode = "";

                        if (dtTemp.Rows.Count > 0) //Prevent error when add
                        {
                            Msgbox.Show("Document Added!");
                            DocCode = dtTemp.Rows[0][0].ToString();

                            if (!Directory.Exists(drivePath))
                            {
                                if (drivePath.Contains("192.168.110.228"))
                                    drivePath = drivePath.Replace("192.168.110.228", "AD02");
                                else if (drivePath.Contains("AD02"))
                                    drivePath = drivePath.Replace("AD02", "192.168.110.228");
                            }

                            if (dgvFile.Rows.Count > 0)
                            {
                                string DocCodeFolder = DocCode + "\\";
                                Directory.CreateDirectory(drivePath + DocCodeFolder);
                                string fileName = "", fullPath = "", sql = "";
                                foreach (DataGridViewRow dgvr in dgvFile.Rows)
                                {
                                    fileName = dgvr.Cells[0].Value.ToString();
                                    fullPath = drivePath + DocCodeFolder + fileName;
                                    File.Copy(dgvr.Cells[1].Value.ToString(), fullPath, true);
                                    sql = @"INSERT INTO dbo.tbAttachment (DOC_CODE,PATH,FILENAME,ADD_DATE) VALUES (" + DocCode + ",N'" + fullPath + "',N'" + fileName + "','" + DateTime.Now + "')";
                                    sqlcrud.Executing(sql);
                                }
                            }
                        }
                        else
                        {
                            Msgbox.Show("Error when add document, no record added.");
                            return;
                        }

                        dtTemp = sqlcrud.LoadData("SELECT * FROM VIEW_DOC_DETAIL WHERE DOC_CODE = '" + DocCode + "'").Tables[0];
                        Cursor.Current = Cursors.WaitCursor;
                        ReportClass rpt = new ReportClass();
                        if (cbProLine.Text == "A&H")
                        {
                            rpt = new Reports.InstructionNoteANH();
                        }
                        else if (cbProLine.Text == "AUTO")
                        {
                            rpt = new Reports.InstructionNoteAuto();
                        }
                        else if (cbProLine.Text == "FL")
                        {
                            rpt = new Reports.InstructionNoteFNL();
                        }
                        else if (cbProLine.Text == "PE&M")
                        {
                            rpt = new Reports.InstructionNotePME();
                        }
                        else if (cbProLine.Text == "MICR")
                        {
                            rpt = new Reports.InstructionNoteMicr();
                        }
                        //if (cbProLine.Text == "A&H")
                        //{
                        //    rpt = new Reports.InstructionNoteANH();

                        //}
                        //else if (cbProLine.Text == "AUTO")
                        //{
                        //    rpt = new Reports.InstructionNoteAuto();
                        //}
                        //else if (cbProLine.Text == "F&L")
                        //{
                        //    rpt = new Reports.InstructionNoteFNL();
                        //}
                        //else if (cbProLine.Text == "PE&M")
                        //{
                        //    rpt = new Reports.InstructionNotePME();
                        //}
                        //else if (cbProLine.Text == "MICR")
                        //{
                        //    rpt = new Reports.InstructionNoteMicr();
                        //}
                        //else if (cbProLine.Text == "FLM")
                        //{
                        //    rpt = new Reports.InstructionNoteFLM();
                        //}
                        //else if (cbProLine.Text == "P&E")
                        //{
                        //    rpt = new Reports.InstructionNotePNE();
                        //}

                        dtTemp.Columns.Add("CRIN", typeof(System.String));
                        foreach (DataRow row in dtTemp.Rows)
                        {
                            row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                        }


                        rpt.SetDataSource(dtTemp);
                        var frm = new frmViewInstructionNote();
                        frm.rpt = rpt;
                        frm.Show();
                        Cursor.Current = Cursors.AppStarting;

                        this.Close();
                    }
                }
                else
                {
                    if (cbDocType.Text == "")
                        Msgbox.Show("Please select Document Type!");
                    else if (tbCusCode.Text == "" || tbCusName.Text == "")
                        Msgbox.Show("Please input Customer Code!");
                    else if (cbProLine.Text == "")
                        Msgbox.Show("Please select Product Line!");
                    else if (cbProType.Text == "")
                        Msgbox.Show("Please select Product Type!");
                    else if (cbPriority.Text == "")
                        Msgbox.Show("Please select Priority!");
                    else if (cbSubmitVia.Text == "")
                        Msgbox.Show("Please select Submit Via!");
                    else if (!(rbYes.Checked || rbNo.Checked))
                        Msgbox.Show("Please select Print Card!");
                    else if (tbPremium.Text == "")
                        Msgbox.Show("Please input Premium!");
                    return;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }


        //private void cbCusCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        tbCusName.Text = crud.ExecQuery("select CUS_NAME from " + VIEW_CUSTOMER + " where CUS_CODE = '" + tbCusCode.Text.ToUpper() + "'").Rows[0][0].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex.Message.ToUpper().Contains("NO ROW AT POSITION ")) return;
        //        Msgbox.Show(ex.Message);
        //    }
        //}


        private void cbPriority_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpToBeFinish.Enabled = (cbPriority.Text != "Normal") ? true : false;
        }

        private void cbProLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbProType.Items.Clear();
            foreach (KeyValuePair<string, string> entry in product)
            {
                if (entry.Value == cbProLine.Text)
                    cbProType.Items.Add(entry.Key);
            }

            tbOriginalRate.Enabled = true;
            tbGroupDiscount.Enabled = true;
            tbLoyaltyDiscount.Enabled = true;
            tbNCD.Enabled = true;
            tbSpecialDiscount.Enabled = true;
            tbFleetSizeDiscount.Enabled = true;
            tbDiscount.Enabled = true;
            tbLoading.Enabled = true;
            tbFinalPremium.Enabled = true;

            if (cbProLine.Text == "A&H")
            {
                // Remove Leasing
                if (cbPriority.Items.Count == 4)
                    cbPriority.Items.RemoveAt(3);

                tbNCD.Enabled = false; tbNCD.Text = "";
                tbFleetSizeDiscount.Enabled = false; tbFleetSizeDiscount.Text = "";
                tbDiscount.Enabled = false; tbDiscount.Text = "";
            }
            else if (cbProLine.Text == "AUTO")
            {
                // Remove Leasing
                if (cbPriority.Items.Count == 4)
                    cbPriority.Items.RemoveAt(3);

                cbPriority.Items.Add(new ComboboxItem("Leasing", "L"));
                tbOriginalRate.Enabled = false; tbOriginalRate.Text = "";
                tbGroupDiscount.Enabled = false; tbGroupDiscount.Text = "";
                tbDiscount.Enabled = false; tbDiscount.Text = "";
                tbFinalPremium.Enabled = false; tbFinalPremium.Text = "";
            }
            else if (cbProLine.Text == "FL" || cbProLine.Text == "PE&M" || cbProLine.Text == "MICR")
            {
                // Remove Leasing
                if (cbPriority.Items.Count == 4)
                    cbPriority.Items.RemoveAt(3);

                tbFleetSizeDiscount.Enabled = false; tbFleetSizeDiscount.Text = "";
                tbGroupDiscount.Enabled = false; tbGroupDiscount.Text = "";
                tbLoyaltyDiscount.Enabled = false; tbLoyaltyDiscount.Text = "";
                tbNCD.Enabled = false; tbNCD.Text = "";
                tbSpecialDiscount.Enabled = false; tbSpecialDiscount.Text = "";
                tbFinalPremium.Enabled = false; tbFinalPremium.Text = "";
            }
        }

        private void tbCusCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                selectedCusCode = "";
                selectedCusName = "";
                frmSelectCustomer frm = new frmSelectCustomer();
                frm.FormClosed += new FormClosedEventHandler(frmClose);
                frm.Show();
            }
        }


        void frmClose(object sender, FormClosedEventArgs e)
        {
            if (selectedCusCode != "")
                tbCusCode.Text = selectedCusCode;
            if (selectedCusName != "")
                tbCusName.Text = selectedCusName;

            if (tbCusCode.Text.Trim() != "")
            {
                DataTable dtTemp = crud.ExecQuery("SELECT CUS_BPARTY_CODE,BPARTY_NAME FROM " + VIEW_CUSTOMER + " WHERE TRIM(CUS_CODE) = '" + tbCusCode.Text.Trim() + "'");
                if (dtTemp.Rows.Count > 0)
                {
                    tbProducerCode.Text = dtTemp.Rows[0]["CUS_BPARTY_CODE"].ToString();
                    tbProducerName.Text = dtTemp.Rows[0]["BPARTY_NAME"].ToString();
                }
            }
        }

        private void btnSelectCus_Click(object sender, EventArgs e)
        {
            selectedCusCode = "";
            selectedCusName = "";
            frmSelectCustomer frm = new frmSelectCustomer();
            frm.FormClosed += new FormClosedEventHandler(frmClose);
            frm.ShowDialog();
        }

        private void tbCusName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                selectedCusCode = "";
                selectedCusName = "";
                frmSelectCustomer frm = new frmSelectCustomer();
                frm.FormClosed += new FormClosedEventHandler(frmClose);
                frm.ShowDialog();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "c:\\";
            ofd.Filter = "Excel Files(*.XLSX;*.XLS)|*.XLS;*.XLSX|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            string Filename = "";

            if (ofd.ShowDialog() == DialogResult.OK)
                Filename = ofd.FileName;
            else
                return;




            DialogResult dr = Msgbox.Show("Are you sure you want to upload file: " + ofd.SafeFileName + " to the system?", "Confirmation", "Yes", "No");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor.Current = Cursors.WaitCursor;

                try
                {
                    string[] columnToText = { "K" }; // Commission column
                    DataTable tmpUploaddt = My_DataTable_Extensions.ConvertExcelToDataSetXML(Filename, columnToText).Tables[0];
                    DataTable uploaddt = tmpUploaddt.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(f => string.IsNullOrEmpty(f.ToString()))).CopyToDataTable();

                    uploaddt.AcceptChanges();
                    if (uploaddt.Rows.Count > 0)
                    {
                        if (uploaddt.Columns.Count > 26) //Upload file has 26 columns
                        {
                            int col = uploaddt.Columns.Count;
                            for (int i = col - 1; i > 25; i--)
                                uploaddt.Columns.RemoveAt(i);
                        }

                        if (uploaddt.Columns.Count != 26)
                        {
                            Cursor.Current = Cursors.AppStarting;
                            Msgbox.Show("Wrong excel format! (Get the latest format from \"Help\")");
                            return;
                        }

                        string DocType = "", CusCode = "", ProType = "", ABCode = "", Priority = "", PrintCard = "", SubmitVia = "", PolicyNo = "", QuotNo = "";
                        string Commission = "", EffectiveDate = "", OtherInstruction = "", RemarkNote = "", RemarkRate = "", OriginalRate = "", GroupDiscount = "",
                            LoyaltyDiscount = "", NCD = "", SpecialDiscount = "", FleetSizeDiscount = "", Discount = "", Loading = "", FinalPremium = "", Attachment = "",
                            Premium = "";
                        DateTime ToBeFinish = new DateTime();

                        //DataTable 

                        //Check
                        for (int i = 0; i < uploaddt.Rows.Count; i++)
                        {
                            DataRow row = uploaddt.Rows[i];

                            DocType = row[0].ToString().Trim();
                            CusCode = row[1].ToString().Trim().ToUpper();
                            ProType = row[2].ToString().Trim().ToUpper();
                            ABCode = row[3].ToString().Trim().ToUpper();
                            Priority = row[4].ToString().Trim();
                            ToBeFinish = (row[5].ToString() == "") ? new DateTime() : Convert.ToDateTime(row[5]);
                            PrintCard = row[6].ToString().Trim();
                            SubmitVia = row[7].ToString().Trim();
                            PolicyNo = row[8].ToString().Trim().ToUpper();
                            QuotNo = row[9].ToString().Trim();
                            Attachment = row[24].ToString().Trim().Replace("\"","");
                            Premium = row[25].ToString().Trim();

                            string temp = "";
                            if (!(frmDocumentControl.docType.TryGetValue(DocType, out temp)))
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Document Type: " + DocType + " is unavailable, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            else
                            {
                                if (DocType == "RP" || DocType == "E" || DocType == "C")
                                {
                                    DataTable dtTemp = crud.ExecQuery("select POL_SEQ_NO from UW_T_POLICIES where (POL_POLICY_NO = '" + PolicyNo + "' or POL_ENDORSEMENT_NO = '" + PolicyNo + "')");
                                    if (dtTemp.Rows.Count <= 0)
                                    {
                                        Cursor.Current = Cursors.AppStarting;
                                        Msgbox.Show("Policy/Endorsement No: " + PolicyNo + " is incorrect, please check again! (Row " + (i + 2) + ")");
                                        return;
                                    }
                                }
                            }
                            if (crud.ExecQuery("SELECT CUS_CODE FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + CusCode + "' AND CUS_STATUS = 'A'").Rows.Count <= 0)
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Customer Code: " + CusCode + " not found, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            if (!(product.TryGetValue(ProType, out temp)))
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Product Type: " + ProType + " is unavailable, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            if (ABCode.Trim() != "" && sqlcrud.LoadData("SELECT CODE FROM dbo.tbPRODUCER WHERE CODE = '" + ABCode + "'").Tables[0].Rows.Count <= 0)
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Agent/Broker Code: " + ABCode + " not found, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            var proLine = product[ProType];
                            if (Priority == "L" && proLine != "AUTO")
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Priority: " + Priority + " is available for product type AUTO only, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            if (Priority != "N" && Priority != "U" && Priority != "VU" && Priority != "L")
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Priority: " + Priority + " is unavailable, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            if (PrintCard != "Yes" && PrintCard != "No")
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Print Card: " + PrintCard + " is unavailable, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            if (SubmitVia != "HC" && SubmitVia != "E")
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Submit Via: " + SubmitVia + " is unavailable, upload unsucessful. (Row " + (i + 2) + ")");
                                return;
                            }
                            if (Attachment != "")
                            {
                                //Check whether attachmet is correct
                                string[] files = Attachment.Split('|');
                                foreach (string f in files)
                                {
                                    if (f.Trim() == "") continue;
                                    if (!File.Exists(f))
                                    {
                                        Cursor.Current = Cursors.AppStarting;
                                        Msgbox.Show("File: " + f + " is unavailable, upload unsucessful. (Row " + (i + 2) + ")");
                                        return;
                                    }
                                }
                                //
                            }
                            if (Premium == "")
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Please input Premium. (Row " + (i + 2) + ")");
                                return;
                            }
                            double d = 0;
                            if (!Double.TryParse(Premium, out d))
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Premium column only accept numberic. (Row " + (i + 2) + ")");
                                return;
                            }
                        }
                        //

                        string CusName = "", ProducerCode = "", ProducerName = "";
                        string printedDocCodeANH = "", printedDocCodeAuto = "", printedDocCodeFLM = "", printedDocCodePNE = "", printedDocCodeMICR="";

                        //Insert
                        for (int i = 0; i < uploaddt.Rows.Count; i++)
                        {
                            DataRow row = uploaddt.Rows[i];

                            DocType = row[0].ToString().Trim();
                            CusCode = row[1].ToString().Trim().ToUpper();
                            ProType = row[2].ToString().Trim().ToUpper();
                            ABCode = row[3].ToString().Trim().ToUpper();
                            Priority = row[4].ToString().Trim();
                            ToBeFinish = (row[5].ToString() == "") ? new DateTime() : Convert.ToDateTime(row[5]);
                            PrintCard = row[6].ToString().Trim();
                            SubmitVia = row[7].ToString().Trim();
                            PolicyNo = (DocType == "RP" || DocType == "E" || DocType == "C") ? row[8].ToString().Trim().ToUpper() : "";
                            QuotNo = row[9].ToString().Trim();
                            Commission = row[10].ToString().Trim();
                            EffectiveDate = row[11].ToString().Trim();
                            OtherInstruction = row[12].ToString().Trim();
                            RemarkNote = row[13].ToString().Trim();
                            RemarkRate = row[14].ToString().Trim();
                            OriginalRate = row[15].ToString().Trim();
                            GroupDiscount = row[16].ToString().Trim();
                            LoyaltyDiscount = row[17].ToString().Trim();
                            NCD = row[18].ToString().Trim();
                            SpecialDiscount = row[19].ToString().Trim();
                            FleetSizeDiscount = row[20].ToString().Trim();
                            Discount = row[21].ToString().Trim();
                            Loading = row[22].ToString().Trim();
                            FinalPremium = row[23].ToString().Trim();
                            Attachment = row[24].ToString().Trim().Replace("\"","");
                            Premium = row[25].ToString().Trim();
                            //Fix Premium String
                            System.Text.RegularExpressions.Regex charsToDestroy = new System.Text.RegularExpressions.Regex(@"[^\d|\.\-]");
                            Premium = charsToDestroy.Replace(Premium, "");
                            //

                            if (DocType == "P") PolicyNo = "";

                            CusName = crud.ExecQuery("SELECT CUS_NAME FROM " + VIEW_CUSTOMER + "T WHERE T.CUS_CODE = '" + CusCode + "'").Rows[0][0].ToString();
                            DataTable dtTemp = crud.ExecQuery("SELECT CUS_BPARTY_CODE,BPARTY_NAME FROM " + VIEW_CUSTOMER + " WHERE CUS_CODE = '" + CusCode + "'");
                            ProducerCode = dtTemp.Rows[0]["CUS_BPARTY_CODE"].ToString();
                            ProducerName = dtTemp.Rows[0]["BPARTY_NAME"].ToString();

                            //dtTemp = sqlcrud.ExecuteMySqlOutPara("dbo.sp_insert_doc", "@p_DOC_TYPE", DocType,
                            //    "@p_CUS_CODE", CusCode, "@p_CUS_NAME", CusName, "@p_PRODUCT_TYPE", ProType,
                            //    "@p_PRODUCT_LINE", product[ProType], "@p_PRIORITY_TYPE", Priority,
                            //    "@p_PRODUCER_CODE", ProducerCode, "@p_PRODUCER_NAME", ProducerName,
                            //    "@p_TO_BE_FINISHED_ON", (Priority == "N") ? "" : ToBeFinish.ToString(), "@p_CREATE_DATE", DateTime.Now,
                            //    "@p_SUBMIT_VIA", SubmitVia, "@p_POLICY_NO", PolicyNo, "@p_QUOT_NO", QuotNo, "@p_DOC_CUR_STATUS", 0, "@p_DOC_CUR_STATUS_SET_BY", usercode,
                            //    "@p_DOC_CUR_STATUS_SET_ON", DateTime.Now, "@p_STATUS", "O", "@p_STATUS_REMARK", "", "@p_PRINT_CARD", PrintCard,
                            //    "@p_CREATE_BY", usercode, "@p_AGENT_BROKER_CODE", ABCode);

                            dtTemp = sqlcrud.ExecuteMySqlOutPara("dbo.sp_insert_doc", "@p_DOC_TYPE", DocType,
                                "@p_CUS_CODE", CusCode, "@p_CUS_NAME", CusName, "@p_PRODUCT_TYPE", ProType,
                                "@p_PRODUCT_LINE", product[ProType], "@p_PRIORITY_TYPE", Priority,
                                "@p_PRODUCER_CODE", ProducerCode, "@p_PRODUCER_NAME", ProducerName,
                                "@p_TO_BE_FINISHED_ON", (Priority == "N") ? "" : ToBeFinish.ToString(), "@p_CREATE_DATE", DateTime.Now,
                                "@p_SUBMIT_VIA", SubmitVia, "@p_POLICY_NO", PolicyNo, "@p_QUOT_NO", QuotNo, "@p_DOC_CUR_STATUS", 0, "@p_DOC_CUR_STATUS_SET_BY", usercode,
                                "@p_DOC_CUR_STATUS_SET_ON", DateTime.Now, "@p_STATUS", "O", "@p_STATUS_REMARK", "", "@p_PRINT_CARD", PrintCard,
                                "@p_CREATE_BY", usercode, "@p_AGENT_BROKER_CODE", ABCode, "@p_COMMISSION", Commission, "@p_EFFECTIVE_DATE", EffectiveDate,
                                "@p_OTHER_INSTRUCTION", OtherInstruction, "@p_REMARK_NOTE", RemarkNote, "@p_REMARK_RATE", RemarkRate,
                                "@p_ORIGINAL_RATE", OriginalRate, "@p_GROUP_DISCOUNT", GroupDiscount, "@p_LOYALTY_DISCOUNT", LoyaltyDiscount,
                                "@p_NCD", NCD, "@p_SPECIAL_DISCOUNT", SpecialDiscount, "@p_FLEET_SIZE_DISCOUNT", FleetSizeDiscount,
                                "@p_DISCOUNT", Discount, "@p_LOADING", Loading, "@p_FINAL_PREMIUM_PER_PERSON", FinalPremium, "@p_PREMIUM", Premium);

                            if (dtTemp.Rows.Count <= 0)
                            {
                                Cursor.Current = Cursors.AppStarting;
                                Msgbox.Show("Error occured when upload, " + (i + 1) + " record(s) uploaded!");
                                return;
                            }
                            else
                            {
                                string DocCode = dtTemp.Rows[0][0].ToString();
                                //Add Attachment
                                if (Attachment != "")
                                {
                                    if (!Directory.Exists(drivePath))
                                    {
                                        if (drivePath.Contains("192.168.110.228"))
                                            drivePath = drivePath.Replace("192.168.110.228", "AD02");
                                        else if (drivePath.Contains("AD02"))
                                            drivePath = drivePath.Replace("AD02", "192.168.110.228");
                                    }

                                    string DocCodeFolder = DocCode + "\\";
                                    Directory.CreateDirectory(drivePath + DocCodeFolder);
                                    string fileName = "", fullPath = "", sql = "";
                                    string[] files = Attachment.Split('|');
                                    foreach (string f in files)
                                    {
                                        if (f.Trim() == "") continue;
                                        fileName = Path.GetFileName(f);
                                        fullPath = drivePath + DocCodeFolder + fileName;
                                        File.Copy(f, fullPath, true);
                                        sql = @"INSERT INTO dbo.tbAttachment (DOC_CODE,PATH,FILENAME,ADD_DATE) VALUES (" + DocCode + ",N'" + fullPath + "',N'" + fileName + "','" + DateTime.Now + "')";
                                        sqlcrud.Executing(sql);
                                    }
                                }
                                //
                                
                                switch (product[ProType])
                                {
                                    case "A&H":
                                        printedDocCodeANH += "'" + DocCode + "',";
                                        break;
                                    case "AUTO":
                                        printedDocCodeAuto += "'" + DocCode + "',";
                                        break;
                                    case "FL":
                                        printedDocCodeFLM += "'" + DocCode + "',";
                                        break;
                                    case "PE&M":
                                        printedDocCodePNE += "'" + DocCode + "',";
                                        break;
                                    case "MICR":
                                        printedDocCodeMICR += "'" + DocCode + "',";
                                        break;
                                    
                                }
                            }
                        }

                        Cursor.Current = Cursors.AppStarting;
                        Msgbox.Show(uploaddt.Rows.Count + " record(s) successfully uploaded!");

                        Cursor.Current = Cursors.WaitCursor;
                        if (printedDocCodeANH != "")
                        {
                            printedDocCodeANH = printedDocCodeANH.Remove(printedDocCodeANH.Length - 1);
                            DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeANH + ")").Tables[0];
                            Reports.InstructionNoteANH rpt = new Reports.InstructionNoteANH();
                            dtTemp.Columns.Add("CRIN", typeof(System.String));
                            foreach (DataRow row in dtTemp.Rows)
                            {
                                row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                            }
                            rpt.SetDataSource(dtTemp);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = rpt;
                            frm.Show();
                        }
                        if (printedDocCodeAuto != "")
                        {
                            printedDocCodeAuto = printedDocCodeAuto.Remove(printedDocCodeAuto.Length - 1);
                            DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeAuto + ")").Tables[0];
                            Reports.InstructionNoteAuto rpt = new Reports.InstructionNoteAuto();
                            dtTemp.Columns.Add("CRIN", typeof(System.String));
                            foreach (DataRow row in dtTemp.Rows)
                            {
                                row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                            }
                            rpt.SetDataSource(dtTemp);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = rpt;
                            frm.Show();
                        }
                        if (printedDocCodeFLM != "")
                        {
                            printedDocCodeFLM = printedDocCodeFLM.Remove(printedDocCodeFLM.Length - 1);
                            DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeFLM + ")").Tables[0];
                            Reports.InstructionNoteFLM rpt = new Reports.InstructionNoteFLM();
                            dtTemp.Columns.Add("CRIN", typeof(System.String));
                            foreach (DataRow row in dtTemp.Rows)
                            {
                                row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                            }
                            rpt.SetDataSource(dtTemp);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = rpt;
                            frm.Show();
                        }
                        if (printedDocCodePNE != "")
                        {
                            printedDocCodePNE = printedDocCodePNE.Remove(printedDocCodePNE.Length - 1);
                            DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodePNE + ")").Tables[0];
                            Reports.InstructionNotePNE rpt = new Reports.InstructionNotePNE();
                            dtTemp.Columns.Add("CRIN", typeof(System.String));
                            foreach (DataRow row in dtTemp.Rows)
                            {
                                row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                            }
                            rpt.SetDataSource(dtTemp);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = rpt;
                            frm.Show();
                        }
                        if (printedDocCodeMICR != "")
                        {
                            printedDocCodeMICR = printedDocCodeMICR.Remove(printedDocCodeMICR.Length - 1);
                            DataTable dtTemp = sqlcrud.LoadData("SELECT * FROM dbo.VIEW_DOC_DETAIL WHERE DOC_CODE IN (" + printedDocCodeMICR + ")").Tables[0];
                            Reports.InstructionNoteMicr rpt = new Reports.InstructionNoteMicr();
                            dtTemp.Columns.Add("CRIN", typeof(System.String));
                            foreach (DataRow row in dtTemp.Rows)
                            {
                                row["CRIN"] = crud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                            }
                            rpt.SetDataSource(dtTemp);
                            var frm = new frmViewInstructionNote();
                            frm.rpt = rpt;
                            frm.Show();
                        }
                        Cursor.Current = Cursors.AppStarting;
                        this.Close();
                    }
                    else
                    {
                        Msgbox.Show("No data found in Excel file");
                        return;
                    }

                }
                catch (Exception ex)
                {
                    Msgbox.Show(ex.Message);
                }
            }
        }

        private void btnUploadHelp_Click(object sender, EventArgs e)
        {
            frmUploadHelp frm = new frmUploadHelp();
            frm.Show();
        }

        private void btnSelectAB_Click(object sender, EventArgs e)
        {
            selectedABCode = "";
            selectedABName = "";
            frmSelectAB frm = new frmSelectAB();
            frm.FormClosed += new FormClosedEventHandler(frmClose1);
            frm.ShowDialog();
        }


        void frmClose1(object sender, FormClosedEventArgs e)
        {
            if (selectedABCode != "")
                tbABCode.Text = selectedABCode;
            if (selectedABName != "")
                tbABName.Text = selectedABName;
        }

        private void tbABCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                selectedABCode = "";
                selectedABName = "";
                frmSelectAB frm = new frmSelectAB();
                frm.FormClosed += new FormClosedEventHandler(frmClose1);
                frm.ShowDialog();
            }
        }

        private void tbABName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.L)
            {
                e.SuppressKeyPress = true;
                selectedABCode = "";
                selectedABName = "";
                frmSelectAB frm = new frmSelectAB();
                frm.FormClosed += new FormClosedEventHandler(frmClose1);
                frm.ShowDialog();
            }
        }

        private void tbCusCode_Leave(object sender, EventArgs e)
        {
            tbCusCode.Text = tbCusCode.Text.Trim().ToUpper();
            DataTable dtTemp = crud.ExecQuery("SELECT CUS_NAME,CUS_BPARTY_CODE,BPARTY_NAME FROM " + VIEW_CUSTOMER + " WHERE CUS_CODE = '" + tbCusCode.Text + "'");
            if (dtTemp.Rows.Count > 0)
            {
                tbCusName.Text = dtTemp.Rows[0]["CUS_NAME"].ToString();
                tbProducerCode.Text = dtTemp.Rows[0]["CUS_BPARTY_CODE"].ToString();
                tbProducerName.Text = dtTemp.Rows[0]["BPARTY_NAME"].ToString();
            }
        }

        private void tbABCode_Leave(object sender, EventArgs e)
        {
            try
            {
                updateTblProducer();
                tbABCode.Text = tbABCode.Text.Trim().ToUpper();
                DataTable dtTemp = sqlcrud.LoadData("SELECT NAME FROM dbo.tbPRODUCER WHERE CODE = '" + tbABCode.Text + "' AND TEAM in ('Agents','Brokers','Bank Service')").Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    tbABName.Text = dtTemp.Rows[0]["NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        void updateTblProducer()
        {
            DataTable current = crud.ExecQuery("select 'Agents',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%AGNT%' AND SFC_ACTIVE = 'Y'" +
            " union all select 'Brokers',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%BROK%' AND SFC_ACTIVE = 'Y'" +
            " union all select 'Bank Service',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%FINLE%' AND SFC_ACTIVE = 'Y'" +
            " union all select 'BizDev',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%TVAGT%' AND SFC_ACTIVE = 'Y'");

            DataTable dtTemp = sqlcrud.LoadData("SELECT * from dbo.tbPRODUCER where TEAM in ('Agents','Brokers','Bank Service','BizDev')").Tables[0];

            DataTable diff = CommonFunctions.getDifferentRecords(current, dtTemp);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            for (int i = 0; i < diff.Rows.Count; i++)
            {
                string team = diff.Rows[i][0].ToString();
                string code = diff.Rows[i][2].ToString();
                string name = diff.Rows[i][1].ToString();

                dtTemp = sqlcrud.LoadData("SELECT * from dbo.tbPRODUCER where CODE = '" + code + "'").Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    cmd = new System.Data.SqlClient.SqlCommand("Update dbo.tbPRODUCER set NAME = @name where CODE = '" + code + "'");
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@name"].Value = name;
                    sqlcrud.Executing(cmd);
                }
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("Insert into dbo.tbPRODUCER(TEAM,NAME,CODE) values (@team,@name,@code)");
                    cmd.Parameters.Add("@team", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@team"].Value = team;
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters.Add("@code", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@code"].Value = code;
                    sqlcrud.Executing(cmd);
                }
            }
            cmd.Dispose();
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdUpload = new OpenFileDialog();
            ofdUpload.Filter = "Common Files(*.JPEG;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF)|*.BMP;*.JPG;*.GIF;*.PNG;*.DOCX;*.DOC;*.XLSX;*.XLS;*.PDF|All files (*.*)|*.*";
            ofdUpload.Multiselect = true;
            if (ofdUpload.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in ofdUpload.FileNames)
                {
                    string filename = Path.GetFileName(path);

                    for (int i = 0; i < dgvFile.Rows.Count; i++)
                    {
                        if (dgvFile.Rows[i].Cells[0].Value.ToString() == filename)
                        {
                            Msgbox.Show(filename + " already exists in the list. Please check the file again.");
                            return;
                        }
                    }

                    dgvFile.Rows.Add(filename, path);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }


        private void OpenFile()
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No file to open!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                System.Diagnostics.Process.Start(dgvr.Cells[1].Value.ToString());

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvFile.Rows.Count <= 0)
            {
                Msgbox.Show("No record to remove!");
                return;
            }

            foreach (DataGridViewRow dgvr in dgvFile.SelectedRows)
                dgvFile.Rows.Remove(dgvr);
        }

        private void dgvFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            OpenFile();
        }

        private void dgvFile_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void dgvFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in FileList)
                dgvFile.Rows.Add(Path.GetFileName(file), file);
        }

        private void cbDocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDocType.Text == "Renewal Policy" || cbDocType.Text == "Endorsement" || cbDocType.Text == "Cancellation")
                lblPolNo.Text = "Policy No:";
            else
                lblPolNo.Text = "Policy No: +";


            if (cbDocType.Text == "Policy")
            {
                tbPolicyNo.Text = "";
                tbPolicyNo.Enabled = false;
            }
            else
                tbPolicyNo.Enabled = true;

        }
    }
}
