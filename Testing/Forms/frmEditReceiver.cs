using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing.Forms
{
    public partial class frmEditReceiver : Form
    {

        public string Username = string.Empty;
        CRUD crud = new CRUD();
        DataTable dtReceiver = new DataTable();
        DataTable dtCC = new DataTable();
        OracleDataAdapter daReceiver = new OracleDataAdapter();
        OracleDataAdapter daCC = new OracleDataAdapter();
        OracleCommandBuilder cmdbuilderReceiver = new OracleCommandBuilder();
        OracleCommandBuilder cmdbuilderCC = new OracleCommandBuilder();
        BindingSource bsReceiver = new BindingSource();
        BindingSource bsCC = new BindingSource();

        string connString = ConfigurationManager.ConnectionStrings["Testing.Properties.Settings.ConnectionString"].ConnectionString;


        public frmEditReceiver()
        {
            InitializeComponent();
        }

        private void frmEditReceiver_Load(object sender, EventArgs e)
        {
            string receivercmd = "SELECT * FROM USER_EMAIL_SENDER_HIST", cccmd = "SELECT * FROM USER_EMAIL_CC_HIST";

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = connString;
            conn.Open();

            daReceiver = new OracleDataAdapter(receivercmd, conn);
            cmdbuilderReceiver = new OracleCommandBuilder(daReceiver);
            daReceiver.Fill(dtReceiver);
            bsReceiver = new BindingSource() { DataSource = dtReceiver };
            dgvReceiver.DataSource = dtReceiver;

            daCC = new OracleDataAdapter(cccmd, conn);
            cmdbuilderCC = new OracleCommandBuilder(daCC);
            daCC.Fill(dtCC);
            bsCC = new BindingSource() { DataSource = dtCC };
            dgvCC.DataSource = dtCC;

            dgvReceiver.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReceiver.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCC.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dtReceiver.DefaultView.RowFilter = "USER_CODE LIKE '%" + Username + "%'";
            dtCC.DefaultView.RowFilter = "USER_CODE LIKE '%" + Username + "%'";

            dgvReceiver.Columns["USER_CODE"].Width = 80;
            dgvCC.Columns["USER_CODE"].Width = 80;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvReceiver.EndEdit();
            dgvCC.EndEdit();
            daReceiver.Update(dtReceiver);
            daCC.Update(dtCC);
            Msgbox.Show("Receiver and CC updated!");
            this.Close();
        }
    }
}
