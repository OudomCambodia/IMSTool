using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmDocumentDetail1 : Form
    {


        public bool printable = false;
        public string DocId = string.Empty;
        public string Username = string.Empty;
        DBS11SqlCrud crud = new DBS11SqlCrud();
        CRUD maincrud = new CRUD();
        string InputBy = string.Empty, PrintProdCode = string.Empty; //PrintProdCode is Producer code like Broker code or agent code....
        string VIEW_CUS_ATTENTION = "SELECT CUS_CODE,(CASE WHEN CUS_TYPE = 'I' THEN CUS_INDV_SURNAME ELSE (SELECT CCT_NAME FROM UW_M_CUST_CONTACTS WHERE CCT_CUS_CODE = CUS_CODE AND ROWNUM = 1) END) AS CONTACT_PERSON,(CASE WHEN CUS_TYPE = 'I' THEN CUS_PHONE_1 ELSE (SELECT CCT_PHONE_NO FROM UW_M_CUST_CONTACTS WHERE CCT_CUS_CODE = CUS_CODE AND ROWNUM = 1) END) AS CONTACT_PHONE,(SELECT ADR_LOC_DESCRIPTION || ', ' || (SELECT GPL_DESC FROM SM_M_GEOAREA_PARAMLN WHERE GPL_CODE = ADR_DISTRICT) || ', ' || (SELECT GPL_DESC FROM SM_M_GEOAREA_PARAMLN WHERE GPL_CODE = ADR_PROVINCE) FROM UW_M_CUST_ADDRESSES WHERE ADR_CUS_CODE = CUS_CODE) AS ADDRESS FROM UW_M_CUSTOMERS WHERE CUS_STATUS = 'A'";
        string curremark = string.Empty;

        public frmDocumentDetail1()
        {
            InitializeComponent();
        }

        private void frmDocumentDetail1_Load(object sender, EventArgs e)
        {
            try
            {
                #region --- SELECTION COLOR ---
                var selectionColor = frmDocumentControl.SelectionColor.Split(',');

                var r = Convert.ToInt32(selectionColor[0]);
                var g = Convert.ToInt32(selectionColor[1]);
                var b = Convert.ToInt32(selectionColor[2]);

                dgvHist.DefaultCellStyle.SelectionBackColor = Color.FromArgb(r, g, b);

                if (frmDocumentControl.SelectionColor.Equals("51,204,255"))
                    dgvHist.DefaultCellStyle.SelectionForeColor = Color.Black;
                else
                    dgvHist.DefaultCellStyle.SelectionForeColor = Color.White;
                #endregion

                var dtUserRole = crud.LoadData("select role from tbDOC_USER where user_name = '" + frmLogIn.Usert.ToUpper().Trim() + "'").Tables[0];
                bool isController = false;
                if (dtUserRole.Rows.Count > 0)
                {
                    isController = dtUserRole.Rows[0][0].ToString().Contains("CONTROLLER");
                }

                btnEditRemark.Visible = isController;

                tbDocID.Text = DocId;
                DataTable dt = crud.LoadData("SELECT * FROM VIEW_DOC_DETAIL WHERE DOC_CODE = '" + DocId + "'").Tables[0];
                tbCreateDate.Text = dt.Rows[0]["CREATE_DATE"].ToString() + " " + dt.Rows[0]["CREATE_TIME"].ToString();
                tbCrono.Text = dt.Rows[0]["CRONO_NO"].ToString();
                tbCusCode.Text = dt.Rows[0]["CUS_CODE"].ToString();
                tbCusName.Text = dt.Rows[0]["CUS_NAME"].ToString();
                tbDocType.Text = dt.Rows[0]["DOC_TYPE"].ToString();
                tbDPName.Text = dt.Rows[0]["DP_NAME"].ToString();
                tbFillingName.Text = dt.Rows[0]["FILLING_NAME"].ToString();
                tbFillingRemark.Text = dt.Rows[0]["FILLING_REMARK"].ToString();
                tbPolNo.Text = dt.Rows[0]["POLICY_NO"].ToString();
                tbPriority.Text = dt.Rows[0]["PRIORITY_TYPE"].ToString();
                tbProducerName.Text = dt.Rows[0]["PRODUCER_NAME"].ToString();
                tbProducerTeam.Text = dt.Rows[0]["PRODUCER_TEAM"].ToString();
                tbProLine.Text = dt.Rows[0]["PRODUCT_LINE"].ToString();
                tbProType.Text = dt.Rows[0]["PRODUCT_TYPE"].ToString();
                tbQuotNo.Text = dt.Rows[0]["QUOT_NO"].ToString();
                tbStatus.Text = dt.Rows[0]["DOC_STATUS"].ToString();
                tbSubmitVia.Text = dt.Rows[0]["SUBMIT_VIA"].ToString();
                tbToBeFinish.Text = dt.Rows[0]["TO_BE_FINISHED_ON"].ToString();
                tbRemark.Text = dt.Rows[0]["REMARK"].ToString();
                tbDPRemark.Text = dt.Rows[0]["DP_REMARK"].ToString();
                tbStatusSetBy.Text = dt.Rows[0]["DOC_STATUS_SET_BY"].ToString();
                tbStatusSetOn.Text = dt.Rows[0]["DOC_STATUS_SET_ON"].ToString();
                InputBy = dt.Rows[0]["CREATE_BY"].ToString();
                PrintProdCode = dt.Rows[0]["PRODUCER_CODE"].ToString();
                tbPrintCard.Text = dt.Rows[0]["PRINT_CARD"].ToString();
                tbPremium.Text = dt.Rows[0]["PREMIUM"].ToString();

                if (tbToBeFinish.Text == "01/01/1900") tbToBeFinish.Text = "";

                if (printable)
                    enabledButt(btnPrint);
                else
                    disabledButt(btnPrint);


                if (tbStatus.Text.Trim() == "DONE" && printable)
                    enabledButt(btnAttLbl);
                else
                    disabledButt(btnAttLbl);

                string polno = dt.Rows[0]["POLICY_NO"].ToString();

                /////Print Card History
                if (tbPrintCard.Text == "Yes" && tbDPName.Text.Trim() != "")
                {
                    DateTime createdate = DateTime.ParseExact(dt.Rows[0]["CREATE_DATE"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string protype = tbProType.Text, cusname = tbCusName.Text;
                    protype = (protype == "Chinese PA") ? "GPA" : protype;
                    MyDB mid01crud = new MyDB();
                    DataTable temp = crud.LoadData("SELECT DP_CODE FROM dbo.tbDOC WHERE DOC_CODE = '" + DocId + "' and DP_CODE is not null").Tables[0];
                    string DPcode = "";
                    if (temp.Rows.Count > 0)
                    {
                        DPcode = temp.Rows[0][0].ToString();
                    }

                    if (DPcode != "")
                    {
                        temp = crud.LoadData("SELECT * FROM dbo.tbDOC_HIST WHERE DOC_CODE = '" + DocId + "' AND DOC_STATUS = 10").Tables[0]; //Check Card Printed History
                        if (temp.Rows.Count == 0) //Don't have Card Printed History
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "SELECT * FROM dbo.VIEW_PRINTED_DATE WHERE printed_on >= '" + createdate + "' AND policy_holder like '%' + @cusname + '%' AND policy_no = @policyno order by printed_on desc";
                            cmd.Parameters.Add(new SqlParameter("cusname", cusname));
                            cmd.Parameters.Add(new SqlParameter("policyno", polno));
                            dt = mid01crud.ExecQuery(cmd);
                            if (dt.Rows.Count > 0)
                            {
                                crud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) VALUES('" + DocId + "','" + dt.Rows[0]["printed_on"].ToString() + "',10,'" + DPcode + "','" + dt.Rows[0]["printed_on"].ToString() + "')");
                            }
                        }

                        temp = crud.LoadData("SELECT * FROM dbo.tbDOC_HIST WHERE DOC_CODE = '" + DocId + "' AND DOC_STATUS = 17").Tables[0]; //Check Card Sent History
                        if (temp.Rows.Count == 0) //Don't have Card Sent History
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "SELECT * FROM dbo.VIEW_CREATED_DATE WHERE created_date >= '" + createdate + "' AND policy_holder like '%' + @cusname + '%' AND policy_no = @policyno order by created_date desc";
                            cmd.Parameters.Add(new SqlParameter("cusname", cusname));
                            cmd.Parameters.Add(new SqlParameter("policyno", polno));
                            dt = mid01crud.ExecQuery(cmd);
                            if (dt.Rows.Count > 0)
                            {
                                crud.Executing("INSERT INTO dbo.tbDOC_HIST(DOC_CODE,ADD_TO_HIST_ON,DOC_STATUS,DOC_STATUS_SET_BY,DOC_STATUS_SET_ON) VALUES('" + DocId + "','" + dt.Rows[0]["created_date"].ToString() + "',17,'" + DPcode + "','" + dt.Rows[0]["created_date"].ToString() + "')");
                            }
                        }
                    }
                }
                /////

                dt = crud.LoadData("SELECT * FROM VIEW_DOC_HIST WHERE DOC_CODE = '" + DocId + "' order by SET_ON desc").Tables[0];
                dgvHist.DataSource = dt;
                dgvHist.Columns["DOC_CODE"].Visible = false;
                dgvHist.Columns["CREATE_DATE"].Visible = false;
                dgvHist.Columns["SET_ON"].DefaultCellStyle.Format = "dd'/'MM'/'yyyy HH:mm:ss";

                dgvHist.Columns["STATUS"].MinimumWidth = 150;
                dgvHist.Columns["SET_BY"].MinimumWidth = 150;
                dgvHist.Columns["SET_ON"].MinimumWidth = 150;
                dgvHist.Columns["REMARK"].MinimumWidth = 257;
                //

                //Style
                dgvHist.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
                dgvHist.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
                dgvHist.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                //string InputBy = crud.LoadData("SELECT TOP 1 SET_BY FROM dbo.VIEW_DOC_HIST WHERE DOC_CODE = '"+DocId+"' AND STATUS = 'SUBMITTED TO UW' ORDER BY SET_ON DESC").Tables[0].Rows[0][0].ToString();
                DataSet ds = crud.LoadData("SELECT * FROM VIEW_DOC_DETAIL WHERE DOC_CODE = '" + DocId + "'");
                ReportClass rpt = new ReportClass();
                //if (tbProLine.Text == "A&H")
                //{
                //    rpt = new Reports.InstructionNoteANH();
                    
                //}                   
                //else if (tbProLine.Text == "AUTO")
                //{
                //    rpt = new Reports.InstructionNoteAuto();
                //}
                //else if (tbProLine.Text == "FLM")
                //{
                //    rpt = new Reports.InstructionNoteFLM();
                //}
                //else if (tbProLine.Text == "P&E")
                //{
                //    rpt = new Reports.InstructionNotePNE();
                //}
                if (tbProLine.Text == "A&H")
                {
                    rpt = new Reports.InstructionNoteANH();

                }
                else if (tbProLine.Text == "AUTO")
                {
                    rpt = new Reports.InstructionNoteAuto();
                }
                else if (tbProLine.Text == "FL" || tbProLine.Text == "FLM")
                {
                    rpt = new Reports.InstructionNoteFNL();
                }
                else if (tbProLine.Text == "PE&M" || tbProLine.Text == "P&E")
                {
                    rpt = new Reports.InstructionNotePME();
                }
                else if (tbProLine.Text == "MICR")
                {
                    rpt = new Reports.InstructionNoteMicr();
                }
                // else if (tbProLine.Text == "FLM")
                //{
                //    rpt = new Reports.InstructionNoteFLM();
                //}
                //else if (tbProLine.Text == "P&E")
                //{
                //    rpt = new Reports.InstructionNotePNE();
                //}
                DataTable dtTemp = ds.Tables[0];
                dtTemp.Columns.Add("CRIN", typeof(System.String));
                foreach (DataRow row in dtTemp.Rows)
                {
                    row["CRIN"] = maincrud.ExecQuery("SELECT CASE CUS_TYPE WHEN 'I' THEN 'IN' ELSE 'CR' END AS CRIN FROM UW_M_CUSTOMERS WHERE CUS_CODE = '" + row["CUS_CODE"].ToString() + "'").Rows[0][0].ToString();
                }
                rpt.SetDataSource(dtTemp);
                var frm = new frmViewInstructionNote();
                frm.rpt = rpt;
                frm.Show();
            }
            catch (Exception ex) 
            {
                Msgbox.Show(ex.Message);
            }
            Cursor.Current = Cursors.AppStarting;
        }



        private void disabledButt(Button bn)
        {
            bn.Enabled = false;
            bn.BackColor = Color.Gray;
        }

        private void enabledButt(Button bn)
        {
            bn.Enabled = true;
            bn.BackColor = Color.FromArgb(0, 9, 47);
        }

        private void btnAttLbl_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                CRUD crud = new CRUD();
                DataTable dtTemp = crud.ExecQuery("SELECT CONTACT_PERSON,CONTACT_PHONE,ADDRESS FROM (" + VIEW_CUS_ATTENTION + ")T WHERE T.CUS_CODE = '" + tbCusCode.Text.Trim() + "'");
                Reports.AttentionLabel1 rpt = new Reports.AttentionLabel1();
                rpt.SetParameterValue("ContactPerson", dtTemp.Rows[0]["CONTACT_PERSON"].ToString());
                rpt.SetParameterValue("ContactPersonPhone", dtTemp.Rows[0]["CONTACT_PHONE"].ToString());
                rpt.SetParameterValue("Customer", tbCusName.Text);
                rpt.SetParameterValue("Address", dtTemp.Rows[0]["ADDRESS"].ToString());
                rpt.SetParameterValue("Priority", tbPriority.Text);
                var frm = new frmViewInstructionNote();
                frm.rpt = rpt;
                frm.Text = "Attention Label";
                frm.Show();
                Cursor.Current = Cursors.AppStarting;
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnAttachment_Click(object sender, EventArgs e)
        {
            frmViewAttachments frm = new frmViewAttachments();
            frm.DocCode = DocId;
            frm.Show();
        }

        private void btnRemark_Click(object sender, EventArgs e)
        {
            frmViewRemark frm = new frmViewRemark();
            frm.DocCode = DocId;
            frm.Show();

        }

        private void btnEditRemark_Click(object sender, EventArgs e)
        {
            curremark = tbRemark.Text;
            tbRemark.ReadOnly = false;
            btnEditRemark.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            tbRemark.Text = curremark;
            tbRemark.ReadOnly = true;
            btnEditRemark.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = Msgbox.Show("Are you sure you save change document remark to " + tbRemark.Text + "?", "Confirmation", "Yes", "No");
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    string newremark = DateTime.UtcNow.AddHours(7).ToString("dd'/'MM'/'yyyy") + "-" + Username + ": " + tbRemark.Text;
                    newremark = newremark + Environment.NewLine + Environment.NewLine + curremark;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "UPDATE dbo.tbDOC SET STATUS_REMARK = @remark WHERE DOC_CODE = '" + DocId + "'";
                    cmd.Parameters.Add(new SqlParameter("remark", newremark));
                    crud.Executing(cmd);
                    tbRemark.Text = newremark;
                    Msgbox.Show("Remark saved!");
                    tbRemark.ReadOnly = true;
                    btnEditRemark.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
                return;
            }
        }





    }
}
