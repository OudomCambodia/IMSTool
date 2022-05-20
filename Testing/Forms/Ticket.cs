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
    public partial class Ticket : Form
    {
        DataTable table = new DataTable();
        MyDB Mydb = new MyDB();
        public Ticket()
        {
            InitializeComponent();
        }

        public void getData() {
        // Creates a SQL connection
            using (var connection = new SqlConnection("Data Source=FIPNHMID01;Initial Catalog=HRDB;User ID=sa;Password=Forte@1234;"))
            {
                connection.Open();

                // Creates a SQL command
                using (var command = new SqlCommand("SELECT * FROM View_lucky", connection))
                {
                    // Loads the query results into the table
                    table.Load(command.ExecuteReader());
                }

                //  dt = Mydb.getDataTable("View_lucky");

                if (radioButton1.Checked)
                {
                    Reports.Label myDataReport = new Reports.Label();
                    myDataReport.SetDataSource(table);
                    crystalReportViewer1.ReportSource = myDataReport;
                }
                else if (radioButton2.Checked)
                {
                    Reports.LabelBlank myDataReport = new Reports.LabelBlank();
                    myDataReport.SetDataSource(table);
                    crystalReportViewer1.ReportSource = myDataReport;
                }              
                
                connection.Close();
            }
        
        }
        private void Ticket_Load(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            getData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            getData();
        }
    }
}
