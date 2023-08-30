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
    public partial class AcledaSalePerson : Form
    {

        
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        

        public AcledaSalePerson()
        {
            InitializeComponent();
        }

        private void bnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                
                string[] Keys = new string[] { "sp_type", "sp_date_from", "sp_date_to" };
                string[] Values = new string[] { "AcledaSale", dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59" };
                dt = crud.ExecSP_OutPara("SP_USERACLEDA_SALES", Keys, Values);

                if (dt.Rows.Count <= 0)
                {
                    Msgbox.Show("No Record Found!");
                    return;
                }
                else
                {
                    My_DataTable_Extensions.ExportToExcelXML(dt, "");
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }
        }
    }
}
