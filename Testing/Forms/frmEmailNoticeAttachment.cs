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

namespace Testing.Forms
{
    public partial class frmEmailNoticeAttachment : Form
    {
        CRUD crud = new CRUD();
        private bool isChecked;
        private int num;
        private string claimNo = string.Empty;
        public static DataTable dtClaimDt;
        private DataTable dtExcDef;
        CheckBox checkboxHeader = new CheckBox();
        public static DataTable selectedDoc;

        public frmEmailNoticeAttachment(string ClaimNo)
        {
            InitializeComponent();
            claimNo = ClaimNo;
        }

        private void frmEmailNoticeAttachment_Load(object sender, EventArgs e)
        {

            dtClaimDt = crud.ExecQuery("select pk_uw_m_customers.fn_get_cust_name_full(INT_CUS_CODE) POLICY_HOLDER, " +
                "INT_CONT_ADDRESS ADDRESS, INT_POLICY_NO POLICY_NO,INT_CLAIM_NO CLAIM_NO,INT_PRS_NAME \"MEMBER\", " +
                "TRIM(TO_CHAR(INT_CLAIMED_AMT,'999,999,999,990.99')) CLAIMED_AMOUNT, " +
                "nvl(INT_DIAGNOSIS, nvl(trim(substr(INT_COMMENTS, instr(INT_COMMENTS, 'D:') + 2, nvl(nullif(instr(INT_COMMENTS, 'IO:'),0),instr(INT_COMMENTS, 'SC:')) - instr(INT_COMMENTS, 'D:') - 2)), 'N/A')) CAUSE, " +
                "nvl(INT_OTH_HOSPITAL, INT_COMMENTS) HOSPITAL, nvl(INT_OTH_HOSPITAL, 'true') IS_NULL_OTH_HOSPITAL," +
                "TO_CHAR(INT_DATE_LOSS) TREATMENT_DATE, " +
                "INT_BPARTY_CODE CC, (SELECT PLN_DESCRIPTION FROM UW_T_PLANS WHERE CLM_PLAN_CODE=PLN_CODE AND INT_PROD_CODE = PLN_PRD_CODE) PLAN_DESCRIPTION from CL_T_INTIMATION,CL_T_CLM_MEMBERS where  CLM_INT_SEQ = INT_SEQ_NO and INT_CLAIM_NO = '" + claimNo + "'");

            #region DatagridviewData
            dgvDefinition.Columns.Clear();
            var plan = Regex.Replace(dtClaimDt.Rows[0]["PLAN_DESCRIPTION"].ToString(), @"[A-Za-z]+", string.Empty).Trim();

            var pro = claimNo.Substring(6, 4).ToLower();

            if (pro.Contains("hns"))
            {
                dtExcDef = plan == "+" ? crud.ExecQuery("select * from user_email_med_excludef where PRODUCTS = 'HNS' order by PARTS, ENG")
                : crud.ExecQuery("select * from user_email_med_excludef where PRODUCTS = 'HNS++' order by PARTS, ENG");
            }
            else if (pro.Contains("gpa") || pro.Contains("pac"))
            {
                dtExcDef = crud.ExecQuery("select * from user_email_gpa_excludef where PRODUCTS = 'GPA' order by PARTS, ENG");
            }
            else if (pro.Contains("pae"))
            {
                dtExcDef = crud.ExecQuery("select * from user_email_gpa_excludef where PRODUCTS = 'PAE' order by PARTS, ENG");
            }
            else if (pro.Contains("bhp"))
            {
                dtExcDef = crud.ExecQuery("select * from user_email_bhp_excludef where PRODUCTS = 'BHP' order by PARTS, ENG");
            }

            DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
            //CheckBox chk = new CheckBox();
            dgvDefinition.Columns.Add(CheckboxColumn);
            dgvDefinition.DataSource = dtExcDef;

            dgvDefinition.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SetDgvDefinitionColumnWidth();
            DataGridViewColumn column = dgvDefinition.Columns[0];
            column.Width = 35;
            dgvDefinition.Columns[0].Resizable = DataGridViewTriState.False;
            dgvDefinition.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // add checkbox header
            Rectangle rect = dgvDefinition.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = rect.Location.X + 10;
            rect.Y = rect.Location.Y + 15;
            rect.Width = rect.Size.Width;
            rect.Height = rect.Size.Height;

            checkboxHeader.Checked = false;
            checkboxHeader.Visible = true;
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(15, 15);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);
            dgvDefinition.Controls.Add(checkboxHeader);

            dgvDefinition.Columns[1].Visible = false;
            dgvDefinition.Columns[6].Visible = false;
            dgvDefinition.Columns[7].Visible = false;

            dgvDefinition.RowsDefaultCellStyle.Font = new Font("Khmer OS Siemreap", 9.75f, FontStyle.Regular);

            for (int i = 1; i < dgvDefinition.Columns.Count; i++)
            {
                dgvDefinition.Columns[i].ReadOnly = true;
            }
            #endregion
        }

        private void dgvDefinition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (e.ColumnIndex != 0)
                return;

             
            if (dgvDefinition.SelectedCells[0].ColumnIndex == 0)
            {
                foreach (DataGridViewCell dgvc in dgvDefinition.SelectedCells)
                {
                    dgvDefinition[0, dgvc.RowIndex].Value = true;
                }
                for (int i = 0; i < dgvDefinition.RowCount; i++)
                {
                    isChecked = (bool)dgvDefinition.Rows[i].Cells[0].EditedFormattedValue;
                    CheckCount(isChecked);
                    dgvDefinition.Rows[i].Cells[0].Value = isChecked;
                }
            }
        }

        private void dgvDefinition_DataSourceChanged(object sender, EventArgs e)
        {
            this.dgvDefinition.ForeColor = System.Drawing.Color.Black;
            dgvDefinition.Columns[2].Width = 530;
            dgvDefinition.Columns[4].Width = 530;
            CommonFunctions.HighLightGrid(dgvDefinition);
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                num = 0;
                for (int i = 0; i < dgvDefinition.RowCount; i++)
                {
                    dgvDefinition[0, i].Value = ((CheckBox)dgvDefinition.Controls.Find("checkboxHeader", true)[0]).Checked;
                    isChecked = (bool)dgvDefinition[0, i].Value;
                    CheckCount(isChecked);
                }
                //lblSel.Text = num.ToString();
                dgvDefinition.EndEdit();


            }
            catch (Exception EX)
            {
                Msgbox.Show(EX.Message);
            }
        }

        private void btnGenerateEmailNotice_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            selectedDoc = GetDataTableFromDGV(dgvDefinition);
            if (selectedDoc.Rows.Count <= 0 || selectedDoc == null)
            {
                Msgbox.Show("Please check some Definition or Exclusion");
                Cursor = Cursors.Arrow;
                return;
            }
            frmEmailNoticeAttachmentEdit frmEmailNoticeEdit = new frmEmailNoticeAttachmentEdit();
            frmEmailNoticeEdit.ShowDialog();

            if (frmEmailNoticeEdit.IsSaveSuccess)
                Close();

            Cursor = Cursors.Arrow;
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {

            DataTable dt1 = new DataTable();

            dt1.Columns.Add("TYPE");
            dt1.Columns.Add("PARTS");
            dt1.Columns.Add("PARTS_KH");
            dt1.Columns.Add("TYPE_KH");
            dt1.Columns.Add("ENG");
            dt1.Columns.Add("KH");



            foreach (DataGridViewRow row in dgvDefinition.Rows)
            {
                string status = "";
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {

                        dt1.Rows.Add(row.Cells["TYPE"].Value.ToString(), row.Cells["PARTS"].Value.ToString(), row.Cells["PARTS_KH"].Value.ToString(), row.Cells["TYPE_KH"].Value.ToString(), row.Cells["ENG"].Value.ToString(), row.Cells["KH"].Value.ToString());


                    }
                }
            }
            return dt1;
        }

        private void CheckCount(bool isChecked)
        {
            if (isChecked)
                num++;
        }

        private void SetDgvDefinitionColumnWidth()
        {
            dgvDefinition.Columns["TYPE"].Width = 45;
            dgvDefinition.Columns["PARTS"].Width = 20;
            dgvDefinition.Columns["ENG"].Width = 180;
            dgvDefinition.Columns["KH"].Width = 180;
        }
    }
}
