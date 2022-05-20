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
    public partial class frmSelectAB : Form
    {
        public frmSelectAB()
        {
            InitializeComponent();
        }

        CRUD crud = new CRUD();
        DBS11SqlCrud sqlcrud = new DBS11SqlCrud();
        string VIEW_AB = "(SELECT CODE,NAME,CODE + ' '+ NAME AS SEARCH_STR FROM dbo.tbPRODUCER WHERE TEAM = 'Agents' or TEAM = 'Brokers' or TEAM = 'Bank Service')T";

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.CurrentCell.RowIndex != -1)
                {
                    int selectedRow = dgvResult.CurrentCell.RowIndex;
                    frmAddDocument1.selectedABCode = dgvResult.Rows[selectedRow].Cells["CODE"].Value.ToString();
                    frmAddDocument1.selectedABName = dgvResult.Rows[selectedRow].Cells["NAME"].Value.ToString();
                    this.Close();
                }
                else
                {
                    Msgbox.Show("Please choose agent/broker in table first.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show("Please choose agent/broker in table first.");
                return;
            }
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tbSearch.Text.Trim() == "")
            {
                Msgbox.Show("Please input Code/Name to search.");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            dgvResult.DataSource = null;
            dgvResult.Columns.Clear();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT CODE,NAME,SEARCH_STR FROM "+VIEW_AB+" WHERE SEARCH_STR like @search";
            cmd.Parameters.Add(new SqlParameter("@search", "%"+tbSearch.Text.Trim().ToUpper()+"%"));
            DataTable dtTemp = sqlcrud.LoadData(cmd).Tables[0];

            dgvResult.DataSource = dtTemp;
            dgvResult.Columns["SEARCH_STR"].Visible = false;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            Cursor.Current = Cursors.AppStarting;
        }

        private void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int selectedRow = e.RowIndex;
            frmAddDocument1.selectedABCode = dgvResult.Rows[selectedRow].Cells["CODE"].Value.ToString();
            frmAddDocument1.selectedABName = dgvResult.Rows[selectedRow].Cells["NAME"].Value.ToString();
            this.Close();
        }

        void updateTblProducer()
        {
            DataTable current = crud.ExecQuery("select 'Agents',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%AGNT%' AND SFC_ACTIVE = 'Y'" +
            " union select 'Brokers',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%BROK%' AND SFC_ACTIVE = 'Y'" +
            " union select 'Bank Service',SFC_SURNAME,SFC_CODE from SM_M_SALES_FORCE where SFC_INT_EXT  like '%FINLE%' AND SFC_ACTIVE = 'Y'");

            DataTable dtTemp = sqlcrud.LoadData("SELECT * from dbo.tbPRODUCER where TEAM in ('Agents','Brokers','Bank Service')").Tables[0];

            DataTable diff = CommonFunctions.getDifferentRecords(current, dtTemp);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            for (int i = 0; i < diff.Rows.Count; i++)
            {
                string team = diff.Rows[i][0].ToString();
                string code = diff.Rows[i][2].ToString();
                string name = diff.Rows[i][1].ToString();

                dtTemp = sqlcrud.LoadData("SELECT * from dbo.tbPRODUCER where CODE = '" + code + "'").Tables[0];
                if (dtTemp.Rows.Count > 0)
                {
                    cmd = new System.Data.SqlClient.SqlCommand("Update dbo.tbPRODUCER set NAME = @name where CODE = '" + code + "'");
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@name"].Value = name;
                    sqlcrud.Executing(cmd);
                }
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("Insert into dbo.tbPRODUCER(TEAM,NAME,CODE) values (@team,@name,@code)");
                    cmd.Parameters.Add("@team", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@team"].Value = team;
                    cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters.Add("@code", System.Data.SqlDbType.VarChar);
                    cmd.Parameters["@code"].Value = code;
                    sqlcrud.Executing(cmd);
                }
            }
            cmd.Dispose();
        }

        private void frmSelectAB_Load(object sender, EventArgs e)
        {
            updateTblProducer();
        }
    }
}
