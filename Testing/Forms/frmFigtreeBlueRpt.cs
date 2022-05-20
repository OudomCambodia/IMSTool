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
    public partial class frmFigtreeBlueRpt : Form
    {
        DataTable dt = new DataTable();
        CRUD crud = new CRUD();
        public bool abort = false;
        public frmFigtreeBlueRpt()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //btnClear.PerformClick();


            if (cboprdcode.SelectedIndex == 0 )
            {
                Msgbox.Show("Please choose product type");
            }
            else
            {
                //try
                //{
                //    Cursor.Current = Cursors.WaitCursor;
                //    string sp_type = "view_claim_infoby_grouprisk";
                //    string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                //    //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                //    string[] Values = new string[] { sp_type, tbClExpSearch.Text, "" };
                //    dt = crud.ExecSP_OutPara("sp_user_print_system", Keys, Values);
                //    //dgvResult.DataSource = dt;
                //    dgvClExp.DataSource = dt;
                //    Cursor.Current = Cursors.AppStarting;
                //    if (dt.Rows.Count == 0)
                //    {
                //        Msgbox.Show("No data found!");
                //    }
                //    else
                //    {
                //        frmDocumentControl.enabledButt(bnClExpGenerate);
                //    }

                //}
                //catch (Exception ex)
                //{
                //    Cursor.Current = Cursors.AppStarting;
                //    Msgbox.Show(ex.Message);

                //}
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    string p_ins_name = cboprdcode.Text.ToUpper();
                    //string dt_from = dtpFrom.Value.ToShortDateString();
                    //string dt_to = dtpTo.Value.ToShortDateString();
                    string dt_from = dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00";
                    string dt_to = dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59"; 
                    string[] Keys = new string[] { "p_ins_name", "p_date_fr", "p_date_to" };
                    //string[] Values = new string[] { sp_type, dtpFrom.Value.ToString("yyyy/MM/dd"), dtpTo.Value.ToString("yyyy/MM/dd") };
                    string[] Values = new string[] { p_ins_name, dt_from, dt_to };
                    dt = crud.ExecSP_OutPara("SP_FIGTREEBLUE_RISK_REPORT", Keys, Values);
                    //dgvResult.DataSource = dt;
                   
                    Cursor.Current = Cursors.AppStarting;
                    if (dt.Rows.Count == 0)
                    {
                        Msgbox.Show("No data found!");
                    }
                    else
                    {
                        dataGridView1.DataSource = dt;
                        lbTotalNum.Text = dt.Rows.Count.ToString();
                    }

                }
                catch (Exception ex)
                {
                    Cursor.Current = Cursors.AppStarting;
                    Msgbox.Show(ex.Message);

                }
                
            }
           
            Cursor.Current = Cursors.AppStarting;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboprdcode.Text = "-----Select Produt Type-----";
            dataGridView1.DataSource = null;
            lbTotalNum.Text = "0";
        }

        private void frmFigtreeBlueRpt_Load(object sender, EventArgs e)
        {
            btnClear.PerformClick();
            cboprdcode.Text = "-----Select Produt Type-----";
            lbTotalNum.Text = "0";
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.RowCount > 0)
            {
                DialogResult dr = Msgbox.Show("Are you sure to export the record?", "Confirmation");
                if (dr == System.Windows.Forms.DialogResult.No)
                    return;
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    My_DataTable_Extensions.ExportToExcel(dt, "");
                    Cursor.Current = Cursors.AppStarting;
                }
                
            }
            else
            {
                Msgbox.Show("No data found to be printed.");
            }
        }
    
    }
}
