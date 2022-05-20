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
                string value1 = row.Cells[0].Value.ToString();
           
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
            if (dtMain.CurrentRow == null)
            {
                Msgbox.Show("Please select a record to cancel pending!");
                return;
            }

            if (dtMain.SelectedRows[0].Cells[4].Value.ToString().Trim() != "A")
            {
                Msgbox.Show("You can only cancel the PENDING CARDS! if the cards are printed ALREADY, you can no longer cancel the process.");
                return;
            }

            DialogResult dr = Msgbox.Show("Do you want to cancel this pending process?","Confirmation");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {

                if (rdCYC.Checked)
                {
                    Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "D", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@auto_type", "MC", "@user", username);
                    rdCYC.Checked = false;
                    rdCYC.Checked = true;
                }
                else if (rdVCM.Checked)
                {
                    Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "D", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@auto_type", "CV", "@user", username);
                    rdVCM.Checked = false;
                    rdVCM.Checked = true;
                }
                else if (rdVPC.Checked)
                {
                    Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "D", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@auto_type", "PV", "@user", username);
                    rdVPC.Checked = false;
                    rdVPC.Checked = true;
                }
                else if (rdHNS.Checked)
                {
                    Mydb.ExecuteMySql("sp_hns_update_status", "@print_status", "D", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@user", username);
                    rdHNS.Checked = false;
                    rdHNS.Checked = true;
                }
                else if (rdBHP.Checked)
                {
                    Mydb.ExecuteMySql("sp_figtree_blue_update_status", "@print_status", "D", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@user", username);
                    rdBHP.Checked = false;
                    rdBHP.Checked = true;
                }
                else if (rdGPA.Checked)
                {
                    Mydb.ExecuteMySql("sp_gpa_update_status", "@print_status", "D", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@user", username);
                    rdGPA.Checked = false;
                    rdGPA.Checked = true;
                }
            }
        }

        private void btnAlready_Click(object sender, EventArgs e)
        {
            if (dtMain.CurrentRow == null)
            {
                Msgbox.Show("Please select a record!");
                return;
            }

            if (dtMain.SelectedRows[0].Cells[4].Value.ToString().Trim() != "A")
            {
                Msgbox.Show("You can only cancel the PENDING CARDS! if the cards are printed ALREADY, you can no longer cancel the process.");
                return;
            }

            DialogResult dr = Msgbox.Show("Are you sure all the submitted card(s) are printed?", "Confirmation");
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {

                if (rdCYC.Checked)
                {
                    Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "P", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@auto_type", "MC", "@user", username);
                    rdCYC.Checked = false;
                    rdCYC.Checked = true;
                }
                else if (rdVCM.Checked)
                {
                    Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "P", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@auto_type", "CV", "@user", username);
                    rdVCM.Checked = false;
                    rdVCM.Checked = true;
                }
                else if (rdVPC.Checked)
                {
                    Mydb.ExecuteMySql("sp_auto_update_status", "@print_status", "P", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@auto_type", "PV", "@user", username);
                    rdVPC.Checked = false;
                    rdVPC.Checked = true;
                }
                else if (rdHNS.Checked)
                {
                    Mydb.ExecuteMySql("sp_hns_update_status", "@print_status", "P", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@user", username);
                    rdHNS.Checked = false;
                    rdHNS.Checked = true;
                }
                else if (rdBHP.Checked)
                {
                    Mydb.ExecuteMySql("sp_figtree_blue_update_status", "@print_status", "P", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@user", username);
                    rdBHP.Checked = false;
                    rdBHP.Checked = true;
                }
                else if (rdGPA.Checked)
                {
                    Mydb.ExecuteMySql("sp_gpa_update_status", "@print_status", "P", "@print_number", dtMain.SelectedRows[0].Cells[0].Value.ToString().Trim(), "@user", username);
                    rdGPA.Checked = false;
                    rdGPA.Checked = true;
                }
            }
        }       
    }
}
