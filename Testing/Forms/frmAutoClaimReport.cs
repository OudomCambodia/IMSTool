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
    public partial class frmAutoClaimReport : Form
    {
        public frmAutoClaimReport()
        {
            InitializeComponent();
        }

        CRUD crud = new CRUD();
        DataTable result = new DataTable();
        string ReportType = "Incurred";
        public string Username = "";
        private void rdIncurred_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCtrlText();
        }

        private void rdOS_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCtrlText();
        }

        private void rdPaid_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCtrlText();
        }

        private void ChangeCtrlText()
        {
            if (rdIncurred.Checked || rdOS.Checked)
            {
                lbDateFrom.Text = "Notified Date From:";
                lbDateTo.Text = "Notified Date To:";
            }
            else if (rdPaid.Checked)
            {
                lbDateFrom.Text = "Paid Date From:";
                lbDateTo.Text = "Paid Date To:";
            }

            RadioButton rd = groupType.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);
            lbTitle.Text = "Claim " + rd.Text + " Report";
            ReportType = rd.Text;
        }

        private void frmAutoClaimReport_Load(object sender, EventArgs e)
        {
            rdIncurred.Checked = true;
            dataGridView.RowsDefaultCellStyle.BackColor = Color.WhiteSmoke;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;

        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            try
            {
                if (Username == "C-HRM")
                {
                    string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to", "sp_main_class" };
                    string[] Values = new string[] { ReportType, dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59", cbMainClass.Text.Trim() };
                    result = crud.ExecSP_OutPara("SP_USER_AUTO_CLAIM_MONEA", Keys, Values);
                }
                else
                {
                    string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to", "sp_main_class" };
                    string[] Values = new string[] { ReportType, dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59", cbMainClass.Text.Trim() };
                    result = crud.ExecSP_OutPara("SP_AUTO_CLAIM_REPORT", Keys, Values);
                }
                


                if (result.Rows.Count <= 0)
                {
                    Msgbox.Show("No Record Found!");
                    return;
                }
                else
                {
                    dataGridView.DataSource = result;
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }


            Cursor.Current = Cursors.AppStarting;

        }

        private void bnExcel_Click(object sender, EventArgs e)
        {
            if (result.Rows.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                My_DataTable_Extensions.ExportToExcelXML(result, "");
                Cursor.Current = Cursors.AppStarting;
            }
            else
            {
                Msgbox.Show("No record to export!");
            }
        }

        private void bnClear_Click(object sender, EventArgs e)
        {
            rdIncurred.Checked = true;
            dataGridView.DataSource = null;
            dataGridView.Rows.Clear();
        }

    }
}
