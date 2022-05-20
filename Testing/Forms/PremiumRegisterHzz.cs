using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class PremiumRegisterHzz : Form
    {
       
        CRUD crud = new CRUD();
        public PremiumRegisterHzz()
        {
            InitializeComponent();
        }

       

        private void PremiumRegisterHzz_Load(object sender, System.EventArgs e)
        {
            string year;
            DateTime mydatetime = DateTime.Now;
            year = mydatetime.Year.ToString();
            dtpFrom.Value = new DateTime(Convert.ToInt32(year), 01, 01); 
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            if (cbAccount.Text == "Agent Team")
            {
                DataSet ds= new DataSet();
                ds = DataGenerate("Agent",  new string[] {"Agent"}); 
                My_DataTable_Extensions.ExportToExcelXML(ds, "");
            }
            else if (cbAccount.Text == "Chinese Dept")
            {
                DataSet ds = new DataSet();
                ds = DataGenerate("AH", new string[] { "B-PAL", "B-HZZ", "B-LAU", "B-CHN", "B-PAL|B-HZZ|B-LAU|B-CHN" });
                My_DataTable_Extensions.ExportToExcelXML(ds, "");
            }
            else if (cbAccount.Text == "Direct Sale Dept")
            {
                DataSet ds = new DataSet();
                ds = DataGenerate("AH", new string[] { "B-LEN", "B-STH", "B-HHG", "B-LSE", "B-STL","B-YSN", "B-TRV","B-WCI","2-PRS","2-HSL","2-SCR","2-CVD","2-HSK","4-TNR","4-SML","5-HSR","5-HSL","3-NCR","3-DNN","3-BRN"});
                My_DataTable_Extensions.ExportToExcelXML(ds, "");
            }
            else if (cbAccount.Text == "Japanese Dept")
            {
                DataSet ds = new DataSet();
                ds = DataGenerate("AH", new string[] { "B-JPN|U-JPN" });
                My_DataTable_Extensions.ExportToExcelXML(ds, "");
            }
            else
            {
                DataSet ds = new DataSet();
                ds = DataGenerate("Bank", new string[] { "Bank" });
                My_DataTable_Extensions.ExportToExcelXML(ds, "");
            }
        }


        DataSet DataGenerate(string sptype, string[] accounthandler)
        {

            DataSet dsData = new System.Data.DataSet();
            DataTable dt = new DataTable();
            Cursor.Current = Cursors.WaitCursor;
            for (int i = 0; i < accounthandler.Length; i++)
            {
                dt = new DataTable();
                string[] Keys = new string[] { "p_account", "p_loss_date_fr", "p_loss_date_to", "p_account_handler" };
                string[] Values = new string[] { sptype, dtpFrom.Value.ToString("yyyy/MM/dd") + " 00:00:00", dtpTo.Value.ToString("yyyy/MM/dd") + " 23:59:59", accounthandler[i] };
                dt = crud.ExecSP_OutPara("SP_USER_PREMIUM_REGISTER", Keys, Values);
                if (i == accounthandler.Length - 1)
                {
                    dt.TableName = "All";
                }
                else
                {
                    dt.TableName = accounthandler[i];
                }
                dsData.Tables.Add(dt);
            }
            return dsData;
        }
        
    

    }
}
