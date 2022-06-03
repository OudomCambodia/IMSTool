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
    public partial class ViewHistoryCardPrint : Form
    {
        MyDB Mydb = new MyDB();
        public string username = "SICL";
        int Index;

        public ViewHistoryCardPrint()
        {
            InitializeComponent();
        }

        private void ViewHistoryCardPrint_Load(object sender, EventArgs e)
        {

        }

        private void rdCYC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dtMain.DataSource = null;
                dtMain.Columns.Clear();

                DataGridViewCheckBoxColumn CheckboxColumn = new DataGridViewCheckBoxColumn();
                dtMain.Columns.Add(CheckboxColumn);

                DataGridViewColumn column = dtMain.Columns[0];
                column.Width = 35;
                dtMain.Columns[0].Resizable = DataGridViewTriState.False;
                dtMain.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                if (rdCYC.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_auto_list_user", "@auto_type", "MC", "@user", username);
                    Index = 8;
                }
                else if (rdVCM.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_auto_list_user", "@auto_type", "CV", "@user", username);
                    Index = 8;
                }
                else if (rdVPC.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_auto_list_user", "@auto_type", "PV", "@user", username);
                    Index = 8;
                }
                else if (rdHNS.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_hns_list_user", "@user", username);
                    Index = 2;
                }
                else if (rdPAE.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_pae_list_user", "@user", username);
                    Index = 2;
                }
                else if (rdBHP.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_figtree_blue_list_user", "@user", username);
                    Index = 2;
                }
                else if (rdGPA.Checked)
                {
                    dtMain.DataSource = Mydb.getDataTable("sp_gpa_list_user", "@user", username);
                    Index = 2;
                }
                dtMain.Columns[0].Width = 50;
                dtMain.Columns[1].Width = 150;


                for (int i = 1; i < dtMain.Columns.Count; i++)
                {
                    dtMain.Columns[i].ReadOnly = true;
                }

                //dtMain.Refresh();
                Mydb.Dispose();
            }
            catch (Exception ex) 
            {
                Msgbox.Show(ex.ToString());            
            }
        }

        private void dtMain_SelectionChanged(object sender, EventArgs e)
        {
            dtCardHistory.DataSource = null;
            dtCardHistory.Rows.Clear();

            foreach (DataGridViewRow row in dtMain.SelectedRows)
            {
                string value1 = row.Cells[1].Value.ToString();

                if (rdCYC.Checked)
                {
                    dtCardHistory.DataSource=Mydb.getDataTable("sp_auto_list_detail_admin", "@print_number", value1, "@auto_type", "MC", "@user", username);
                }
                else if (rdVCM.Checked)
                {
                    dtCardHistory.DataSource = Mydb.getDataTable("sp_auto_list_detail_admin", "@print_number", value1, "@auto_type", "CV", "@user", username);
                }
                else if (rdVPC.Checked)
                {
                    dtCardHistory.DataSource = Mydb.getDataTable("sp_auto_list_detail_admin", "@print_number", value1, "@auto_type", "PV", "@user", username);
                }
                else if (rdHNS.Checked)
                {
                    dtCardHistory.DataSource = Mydb.getDataTable("sp_hns_list_detail_admin", "@print_number", value1, "@user", username);
                }
                else if (rdPAE.Checked)
                {
                    dtCardHistory.DataSource = Mydb.getDataTable("sp_pae_list_detail_admin", "@print_number", value1, "@user", username);
                }
                else if (rdBHP.Checked)
                {
                    dtCardHistory.DataSource = Mydb.getDataTable("sp_figtree_blue_list_detail_admin", "@print_number", value1, "@user", username);
                }
                else if (rdGPA.Checked)
                {
                    dtCardHistory.DataSource = Mydb.getDataTable("sp_gpa_list_detail_admin", "@print_number", value1, "@user", username);
                }
            }

            //dtMain.Refresh();
            Mydb.Dispose();
        }

        private void dtMain_DataSourceChanged(object sender, EventArgs e)
        {
            try {
            
            lblPrintId.Text="Total Record(s): " + dtMain.Rows.Count.ToString();            
            }
            catch(Exception ex)
            {

                Msgbox.Show(ex.ToString());
            
            }
        }

        private void dtCardHistory_DataSourceChanged(object sender, EventArgs e)
        {
            try 
            {
                lblPrintDetail.Text = "Total Record(s): " + dtCardHistory.Rows.Count.ToString();            
            }
            catch(Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void dtCardHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dtCardHistory);
        }

        private void dtMain_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            CommonFunctions.HighLightGrid(dtMain);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length >= 20)
                    CommonFunctions.GoTo(dtMain, textBox1, 1, true);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CommonFunctions.GoTo(dtCardHistory, textBox2, Index, true);
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DataTable dtGetCheckedRows = GetCheckedRowsFromDFV(dtMain);

            if (dtGetCheckedRows.Rows.Count <= 0 || dtGetCheckedRows == null)
            {
                Msgbox.Show("Please check record to cancel!");
                return;
            }

            List<string> printNumbers = new List<string>();
            int selectedRow = dtMain.SelectedCells[0].RowIndex;

            for (int i = 0; i < dtGetCheckedRows.Rows.Count; i++)
            {
                bool isPrinted = dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("P") || dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("D");
                var printNumber = dtGetCheckedRows.Rows[i]["print_number"].ToString().Trim();

                if (isPrinted)
                    printNumbers.Add(printNumber);
            }

            if (printNumbers.Count == dtGetCheckedRows.Rows.Count)
            {
                if (printNumbers.Count > 0)
                {
                    string printedPrintNumber = string.Empty;
                    for (int i = 0; i < printNumbers.Count; i++)
                    {
                        printedPrintNumber += string.Concat(printNumbers[i], ", ");
                    }
                    var printedCardNumbers = printedPrintNumber.Remove(printedPrintNumber.Length - 2);
                    Msgbox.Show(string.Format("You cannot cancel print number \"{0}\" because it is already printed or already canceled.", printedCardNumbers));
                    return;
                }
            }

            DialogResult dr = Msgbox.Show("Do you want to cancel all the checked record(s)?", "Confirmation");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                printNumbers.Clear();

                for (int i = 0; i < dtGetCheckedRows.Rows.Count; i++)
                {
                    bool isPrinted = dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("P") || dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("D");
                    var printNumber = dtGetCheckedRows.Rows[i]["print_number"].ToString().Trim();

                    if (isPrinted)
                    {
                        printNumbers.Add(printNumber);
                        continue;
                    }

                    if (rdCYC.Checked)
                    {
                        Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "D", "@print_number", printNumber, "@auto_type", "MC", "@user", username);
                        //rdCYC.Checked = false;
                        //rdCYC.Checked = true;
                    }
                    else if (rdVCM.Checked)
                    {
                        Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "D", "@print_number", printNumber, "@auto_type", "CV", "@user", username);
                        //rdVCM.Checked = false;
                        //rdVCM.Checked = true;
                    }
                    else if (rdVPC.Checked)
                    {
                        Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "D", "@print_number", printNumber, "@auto_type", "PV", "@user", username);
                        //rdVPC.Checked = false;
                        //rdVPC.Checked = true;
                    }
                    else if (rdHNS.Checked)
                    {
                        Mydb.ExecuteMySql("sp_hns_update_status", "@print_status", "D", "@print_number", printNumber, "@user", username);
                        //rdHNS.Checked = false;
                        //rdHNS.Checked = true;
                    }
                    else if (rdPAE.Checked)
                    {
                        Mydb.ExecuteMySql("sp_pae_update_status", "@print_status", "D", "@print_number", printNumber, "@user", username);
                        //rdPAE.Checked = false;
                        //rdPAE.Checked = true;
                    }
                    else if (rdBHP.Checked)
                    {
                        Mydb.ExecuteMySql("sp_figtree_blue_update_status", "@print_status", "D", "@print_number", printNumber, "@user", username);
                        //rdBHP.Checked = false;
                        //rdBHP.Checked = true;
                    }
                    else if (rdGPA.Checked)
                    {
                        Mydb.ExecuteMySql("sp_gpa_update_status", "@print_status", "D", "@print_number", printNumber, "@user", username);
                        //rdGPA.Checked = false;
                        //rdGPA.Checked = true;
                    }
                }
                if (printNumbers.Count > 0)
                {
                    string printedPrintNumber = string.Empty;
                    for (int i = 0; i < printNumbers.Count; i++)
                    {
                        printedPrintNumber += string.Concat(printNumbers[i], ", ");
                    }
                    var cancelCardNumbers = printedPrintNumber.Remove(printedPrintNumber.Length - 2);
                    Msgbox.Show(string.Format("You cannot cancel print number \"{0}\" because it is already printed or already canceled.", cancelCardNumbers));
                }
                rdCYC_CheckedChanged(null, null);

                if (selectedRow >= 0)
                    dtMain.Rows[selectedRow].Selected = true;

                for (int i = 0; i < dtGetCheckedRows.Rows.Count; i++)
                {
                    for (int j = 0; j < dtMain.Rows.Count; j++)
                    {
                        if (dtGetCheckedRows.Rows[i]["print_number"].ToString().Trim().Equals(dtMain.Rows[j].Cells["print_number"].Value.ToString().Trim()))
                        {
                            dtMain.Rows[j].Cells[0].Value = true;
                            break;
                        }
                    }
                }
            }
        }

        private void btnAlready_Click(object sender, EventArgs e)
        {
            DataTable dtGetCheckedRows = GetCheckedRowsFromDFV(dtMain);

            if (dtGetCheckedRows.Rows.Count <= 0 || dtGetCheckedRows == null)
            {
                Msgbox.Show("Please check record to update!");
                return;
            }

            List<string> printNumbers = new List<string>();
            int selectedRow = dtMain.SelectedCells[0].RowIndex;

            for (int i = 0; i < dtGetCheckedRows.Rows.Count; i++)
            {
                bool isCancel = dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("D");
                var printNumber = dtGetCheckedRows.Rows[i]["print_number"].ToString().Trim();

                if (isCancel)
                    printNumbers.Add(printNumber);
            }

            if (printNumbers.Count == dtGetCheckedRows.Rows.Count)
            {
                if (printNumbers.Count > 0)
                {
                    string cancelPrintNumber = string.Empty;
                    for (int i = 0; i < printNumbers.Count; i++)
                    {
                        cancelPrintNumber += string.Concat(printNumbers[i], ", ");
                    }
                    var cancelCardNumbers = cancelPrintNumber.Remove(cancelPrintNumber.Length - 2);
                    Msgbox.Show(string.Format("You cannot update print number \"{0}\" because it is already canceled.", cancelCardNumbers));
                    return;
                }
            }

            DialogResult dr = Msgbox.Show("Do you want to update all the checked record(s)?", "Confirmation");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                printNumbers.Clear();

                for (int i = 0; i < dtGetCheckedRows.Rows.Count; i++)
                {
                    bool isActive = dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("A");
                    bool isCancel = dtGetCheckedRows.Rows[i]["print_status"].ToString().Trim().Equals("D");
                    var printNumber = dtGetCheckedRows.Rows[i]["print_number"].ToString().Trim();

                    if (isCancel)
                    {
                        printNumbers.Add(printNumber);
                        continue;
                    }

                    if (rdCYC.Checked)
                    {
                        Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@auto_type", "MC", "@user", username);
                        //rdCYC.Checked = false;
                        //rdCYC.Checked = true;
                    }
                    else if (rdVCM.Checked)
                    {
                        Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@auto_type", "CV", "@user", username);
                        //rdVCM.Checked = false;
                        //rdVCM.Checked = true;
                    }
                    else if (rdVPC.Checked)
                    {
                        Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@auto_type", "PV", "@user", username);
                        //rdVPC.Checked = false;
                        //rdVPC.Checked = true;
                    }
                    else if (rdHNS.Checked)
                    {
                        Mydb.ExecuteMySql("sp_hns_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@user", username);
                        //rdHNS.Checked = false;
                        //rdHNS.Checked = true;
                    }
                    else if (rdPAE.Checked)
                    {
                        Mydb.ExecuteMySql("sp_pae_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@user", username);
                        //rdPAE.Checked = false;
                        //rdPAE.Checked = true;
                    }
                    else if (rdBHP.Checked)
                    {
                        Mydb.ExecuteMySql("sp_figtree_blue_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@user", username);
                        //rdBHP.Checked = false;
                        //rdBHP.Checked = true;
                    }
                    else if (rdGPA.Checked)
                    {
                        Mydb.ExecuteMySql("sp_gpa_update_status", "@print_status", isActive ? "P" : "A", "@print_number", printNumber, "@user", username);
                        //rdGPA.Checked = false;
                        //rdGPA.Checked = true;
                    }
                }
                if (printNumbers.Count > 0)
                {
                    string cancelPrintNumber = string.Empty;
                    for (int i = 0; i < printNumbers.Count; i++)
                    {
                        cancelPrintNumber += string.Concat(printNumbers[i], ", ");
                    }
                    var cancelCardNumbers = cancelPrintNumber.Remove(cancelPrintNumber.Length - 2);
                    Msgbox.Show(string.Format("You cannot update print number \"{0}\" because it is already canceled.", cancelCardNumbers));
                }
                rdCYC_CheckedChanged(null, null);

                if (selectedRow >= 0)
                    dtMain.Rows[selectedRow].Selected = true;

                for (int i = 0; i < dtGetCheckedRows.Rows.Count; i++)
                {
                    for (int j = 0; j < dtMain.Rows.Count; j++)
                    {
                        if (dtGetCheckedRows.Rows[i]["print_number"].ToString().Trim().Equals(dtMain.Rows[j].Cells["print_number"].Value.ToString().Trim()))
                        {
                            dtMain.Rows[j].Cells[0].Value = true;
                            break;
                        }
                    }
                }
            }
        }

        private void dtMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                e.SuppressKeyPress = true;
                if (dtMain.Rows.Count <= 0) return;
                int selectedrowindex = dtMain.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dtMain.Rows[selectedrowindex];
                string ptintNumber = selectedRow.Cells["print_number"].Value.ToString();
                Clipboard.SetText(ptintNumber);
            }
        }

        private DataTable GetCheckedRowsFromDFV(DataGridView dgv)
        {
            var dt = new DataTable();

            dt.Columns.Add("print_number");
            dt.Columns.Add("print_status");

            string status = "";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    status = row.Cells[0].Value.ToString();
                    if (status == "True")
                    {
                        dt.Rows.Add(row.Cells["print_number"].Value.ToString(), row.Cells["print_status"].Value.ToString());
                    }
                }
            }

            return dt;
        }
    }
}
