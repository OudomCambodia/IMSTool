using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Testing
{
    public partial class CountUserlogin : Form
    {
        CRUD crud = new CRUD();
        DataTable dt = new DataTable();
        bool timer = true;
        public CountUserlogin()
        {
            InitializeComponent();
        }


        private void CountUserlogin_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timerAutoSave.Start();
            BindComboBoxDept();
            fillChart();

        }
        public void BindComboBoxDept()
        {
            DataRow dr;
            string SQLcombox = "select DPT_DESCRIPTION,DPT_DESCRIPTION Name from SM_R_DEPARTMENT where DPT_CODE in ('RGN','BIZ','CLM','UW','SPL','AnH','FIN')";
            DataTable dtCombox = new DataTable();
            dtCombox = crud.ExecQuery(SQLcombox);
            dr = dtCombox.NewRow();
            dr.ItemArray = new object[] { 0, "Select ALL" };
            dtCombox.Rows.InsertAt(dr, 0);
            comboDep.ValueMember = "DPT_DESCRIPTION";
            comboDep.DisplayMember = "Name";
            comboDep.DataSource = dtCombox;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT count(DISTINCT USERNAME)   FROM V$SESSION    WHERE USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL') AND PROGRAM LIKE 'frmweb%'";

                DataTable dt = new DataTable();
                dt = crud.ExecQuery(sql);

                //  DataRow dr = dt.Rows[0];
                //  label1.Text = "User Count";
                //  string c = label1.Text;

                //  label1.Text = " \r\n User Count ' " + DateTime.Now.ToString() + "  " + dr[0].ToString() + "' \r\n" + c;
                //label1.Text += "-" + dr[0].ToString();    

                string sqlDetail = "select USERNAME,to_char(LOGON_TIME,'YYYY/MM/DD HH24:MI:SS') Log_time  FROM V$SESSION WHERE USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL')AND PROGRAM LIKE 'frmweb%' order by LOGON_TIME desc";
                dataGridView1.DataSource = crud.ExecQuery(sqlDetail);
                dataGridView1.Columns[0].Width = 80;

                DataTable dtCount = new DataTable();
                string sqlcount = "select (Case When DEP_Name='CLAIM' then DEP_Name  || ' = 5' When DEP_Name='REGIONAL' then DEP_Name  || ' = 3' When DEP_Name='SPECIALIST LINE' then DEP_Name  || ' = 1' When DEP_Name='BUSINESS DEPARTMENT' then DEP_Name  || ' = 15' When DEP_Name='UNDERWRITING' then DEP_Name  || ' = 17' When DEP_Name='FINANCE' then DEP_Name  || ' = 8'   else DEP_Name  || ' = 19' end)Name, count(USERNAME) No_of_login from(select distinct username,SFC_FIRST_NAME||' '||SFC_SURNAME FullName, (select DPT_DESCRIPTION from SM_R_DEPARTMENT where DPT_CODE=SFC_DPT_CODE) DEP_Name from v$session,SM_M_SALES_FORCE where USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL') and (SFC_USERNAME=username)) group by DEP_Name";
                //select DEP_Name, count(USERNAME) No_of_login from(select distinct username,SFC_FIRST_NAME||' '||SFC_SURNAME FullName, (select DPT_DESCRIPTION from SM_R_DEPARTMENT where DPT_CODE=SFC_DPT_CODE) DEP_Name from v$session,SM_M_SALES_FORCE where USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL') and (SFC_USERNAME=username)) group by DEP_Name";
                dtCount = crud.ExecQuery(sqlcount);
                dataGridViewCount.DataSource = dtCount;
                dataGridViewCount.Columns[1].Width = 100;

                int i = 0;
                foreach (DataRow drCount in dtCount.Rows)
                {
                    i += Convert.ToInt32(drCount["No_of_login"]);
                }
                lblCount.Text = i.ToString();

                if (i >= 75)
                {
                    DataTable dtInvalid = new DataTable();
                    string sqlInvalid = "Select * from VIEW_SESSION_KILL order by SID";
                    dtInvalid = crud.ExecQuery(sqlInvalid);
                    dataGridViewInValidUserLogin.DataSource = dtInvalid;
                    dataGridViewInValidUserLogin.Columns[0].Width = 60;
                    dataGridViewInValidUserLogin.Columns[1].Width = 60;
                    dataGridViewInValidUserLogin.Columns[2].Width = 80;
                    if (dtInvalid.Rows.Count > 0)
                    {
                        DateTime KillDate = DateTime.Now;
                        string KillDateStr = KillDate.ToString("yyyy/MM/dd HH:mm:ss");
                        int NoUserKill = 0;
                        DataTable dtTemp = crud.ExecQuery("SELECT COUNT(DISTINCT USERNAME) FROM VIEW_SESSION_KILL");
                        if (dtTemp.Rows.Count > 0)
                            NoUserKill = Convert.ToInt32(dtTemp.Rows[0][0]);

                        crud.ExecNonQuery("INSERT INTO USER_KILL_HISTORY(KILL_DATE,NO_USER,NO_USER_KILL) VALUES (TO_DATE('" + KillDateStr + "','YYYY/MM/DD HH24:MI:SS')," + i + "," + NoUserKill + ")");

                        foreach (DataRow dri in dtInvalid.Rows)
                        {
                            string sid = dri["SID"].ToString();
                            string ser = dri["SER"].ToString();
                            string code = dri["USERNAME"].ToString();
                            string name = dri["NAME"].ToString();
                            dt = crud.ExecQuery("SELECT SID,SERIAL# FROM V$SESSION WHERE SID = '"+sid+"' AND SERIAL# = '"+ser+"'");
                            if (dt.Rows.Count > 0)
                            {
                                try
                                {
                                    crud.ExecNonQuery("ALTER SYSTEM KILL SESSION '" + sid + "," + ser + "' IMMEDIATE");
                                    crud.ExecNonQuery("INSERT INTO USER_KILL_HISTORY_DETAIL(KILL_DATE,USERCODE,USERNAME,SID,SERIAL) VALUES (TO_DATE('" + KillDateStr + "','YYYY/MM/DD HH24:MI:SS'),'" + code + "','" + name + "'," + sid + "," + ser + ")");
                                }
                                catch (Exception ex)
                                {
                                    break;
                                }
                            }
                        }
                    }

                }

                fillChart();
                if (timer == true)
                {
                    //DataTable dtInvalid = new DataTable();
                    //string sqlInvalid = "Select * from VIEW_SESSION_KILL";
                    //dtInvalid = crud.ExecQuery(sqlInvalid);
                    //dataGridViewInValidUserLogin.DataSource = dtInvalid;
                    bind_dataGridViewInValidUserLogin();
                }

            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer = true;
        }

        //private void label1_Click(object sender, EventArgs e)
        //{
        //    label1.Focus();
        //}

        private void CountUserlogin_KeyDown(object sender, KeyEventArgs e)
        {
            //if (label1.ContainsFocus && e.Control && e.KeyCode == Keys.C)
            //    Clipboard.SetText(label1.Text);
        }

        private void comboDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDep.SelectedIndex > 0)
            {
                string sql = "select Rownum No ,V.username,V.FullName from(select distinct username,SFC_FIRST_NAME||' '||SFC_SURNAME FullName, (select DPT_DESCRIPTION from SM_R_DEPARTMENT where DPT_CODE=SFC_DPT_CODE) DEP_Name from v$session,SM_M_SALES_FORCE where USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL') and (SFC_USERNAME=username)) V where DEP_Name='" + comboDep.SelectedValue + "'";
                dataGridViewByDept.DataSource = crud.ExecQuery(sql);
                dataGridViewByDept.Columns[0].Width = 30;
                dataGridViewByDept.Columns[1].Width = 70;
            }
            else
            {

                string sql = "select Rownum No ,V.username,V.FullName from(select distinct username,SFC_FIRST_NAME||' '||SFC_SURNAME FullName, (select DPT_DESCRIPTION from SM_R_DEPARTMENT where DPT_CODE=SFC_DPT_CODE) DEP_Name from v$session,SM_M_SALES_FORCE where USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL') and (SFC_USERNAME=username)) V";
                dataGridViewByDept.DataSource = crud.ExecQuery(sql);
                dataGridViewByDept.Columns[0].Width = 30;
                dataGridViewByDept.Columns[1].Width = 70;
                //dataGridViewByDept.DataSource = null;
                //dataGridViewByDept.Refresh();
            }
        }
        void killSession()
        {
            try
            {
                DataTable dt = new DataTable();
                string sqlInvalid = "Select * from VIEW_SESSION_KILL";
                dt = crud.ExecQuery(sqlInvalid);
                foreach (DataRow dr in dt.Rows)
                {
                    string sid = dr["SID"].ToString();
                    string ser = dr["SER"].ToString();
                    crud.ExecNonQuery("ALTER SYSTEM KILL SESSION '" + sid + "," + ser + "' IMMEDIATE");
                    Msgbox.Show("Kill Session successfully.");
                }
            }
            catch (Exception ex) { Msgbox.Show(ex.ToString()); }
        }
        private void btnKill_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            dr = Msgbox.Show("Do you want to kill all invalid session?", "Information");
            if (dr == DialogResult.Yes)
            {
                killSession();
            }

        }

        private void dataGridViewInValidUserLogin_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //if (timer == false)
                //{
                string sid = dataGridViewInValidUserLogin.SelectedRows[0].Cells[0].Value.ToString();
                string ser = dataGridViewInValidUserLogin.SelectedRows[0].Cells[1].Value.ToString();
                DialogResult dr = new DialogResult();
                dr = Msgbox.Show("Do you want to kill invalid session " + sid + " and " + ser + " ?", "Information");
                if (dr == DialogResult.Yes)
                {

                    crud.ExecNonQuery("ALTER SYSTEM KILL SESSION '" + sid + "," + ser + "' IMMEDIATE");
                    Msgbox.Show(sid + " and " + ser + " Kill Session successfully.");
                    //    }
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }

        private void timerKill_Tick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtInvalid = new DataTable();
                string sqlInvalid = "Select * from VIEW_SESSION_KILL";
                dtInvalid = crud.ExecQuery(sqlInvalid);
                dataGridViewInValidUserLogin.DataSource = dtInvalid;
                dataGridViewInValidUserLogin.Columns[0].Width = 60;
                dataGridViewInValidUserLogin.Columns[1].Width = 60;
                dataGridViewInValidUserLogin.Columns[2].Width = 80;
                if (dtInvalid.Rows.Count > 0)
                {
                    foreach (DataRow dri in dtInvalid.Rows)
                    {
                        string sid = dri["SID"].ToString();
                        string ser = dri["SER"].ToString();
                        dt = crud.ExecQuery("SELECT SID,SERIAL# FROM V$SESSION WHERE SID = '" + sid + "' AND SERIAL# = '" + ser + "'");
                        if (dt.Rows.Count > 0)
                            crud.ExecNonQuery("ALTER SYSTEM KILL SESSION '" + sid + "," + ser + "' IMMEDIATE");
                    }
                }
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.Message);
            }



        }

        void bind_dataGridViewInValidUserLogin()
        {
            DataTable dtInvalid = new DataTable();
            string sqlInvalid = "Select * from VIEW_SESSION_KILL";
            dtInvalid = crud.ExecQuery(sqlInvalid);
            dataGridViewInValidUserLogin.DataSource = dtInvalid;
            dataGridViewInValidUserLogin.Columns[0].Width = 60;
            dataGridViewInValidUserLogin.Columns[1].Width = 60;
            dataGridViewInValidUserLogin.Columns[2].Width = 80;
        }
        private void btnAutoKill_Click(object sender, EventArgs e)
        {
            if (btnAutoKill.Text == "Start Auto Kill")
            {
                timerKill.Start();
                btnAutoKill.Text = "Stop Auto Kill";
                bind_dataGridViewInValidUserLogin();
                timer = false;
            }
            else
            {
                btnAutoKill.Text = "Start Auto Kill";
                timerKill.Stop();
                bind_dataGridViewInValidUserLogin();
                timer = true;
            }

        }
        public bool checkInsert(string username, string datetime)
        {
            string sqlDetail = "select USER_CODE,to_char(LOGIN_TIME,'YYYY/MM/DD HH24:MI:SS') Log_time  FROM USER_LOGIN_RECORD WHERE USER_CODE='" + username + "' and to_char(LOGIN_TIME,'YYYY/MM/DD HH24:MI:SS')='" + datetime + "'  ";
            DataTable dt = new DataTable();
            dt = crud.ExecQuery(sqlDetail);
            if (dt.Rows.Count > 0) return true;
            return false;
        }

        void Save()
        {
            try
            {
                DataTable dt = new DataTable();
                string sqlDetail = "select USERNAME,to_char(LOGON_TIME,'YYYY/MM/DD HH24:MI:SS') Log_time  FROM V$SESSION WHERE USERNAME NOT IN ('SYS','SYSTEM','SYSMAN','DBSNMP','SICL')AND PROGRAM LIKE 'frmweb%' order by LOGON_TIME desc";
                dt = crud.ExecQuery(sqlDetail);
                foreach (DataRow dr in dt.Rows)
                {
                    if (checkInsert(dr[0].ToString(), dr[1].ToString()) == false)
                    {
                        crud.ExecNonQuery("INSERT INTO USER_LOGIN_RECORD (USER_CODE, LOGIN_TIME,NO_OF_LOGIN) VALUES(q'[" + dr[0].ToString() + "]', to_date('" + dr[1] + "','YYYY/MM/DD HH24:MI:SS'),q'[" + lblCount.Text + "]')");
                    }
                }
                //  Msgbox.Show("New Record is inserted successfully.");
            }
            catch (Exception ex)
            {
                Msgbox.Show(ex.ToString());
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            Msgbox.Show("New Record saved successfully.");
        }

        private void timerAutoSave_Tick(object sender, EventArgs e)
        {
            Save();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            string sqlDetail = "select * from View_report_login";
            dt = crud.ExecQuery(sqlDetail);
            My_DataTable_Extensions.ExportToExcel(dt, "");

        }
        //int i = 0;
        private void fillChart()
        {
            //AddXY value in chart1 in series named as Salary 

            //DataTable dt = new DataTable();
            //string sqlDetail = "select NAME, max(Login) Login ,Max(NO) NO from View_report_login group by NAME";
            //dt = crud.ExecQuery(sqlDetail);

            //foreach (DataRow dr in dt.Rows) {
            // chart1.ChartAreas[0].AxisX.Maximum = i--;
            //if (i == 40) i = 0;
            chart1.Series["Log In"].Points.AddXY(DateTime.Now.ToString(), lblCount.Text);
            //   chart1.Series["Log In"].Points. = DateTime.Now.ToString();

            //  chart1. //= Color.Transparent;
            chart1.Series["Log In"].Color = System.Drawing.Color.Lime;
            chart1.Series["Log In"].Points[0].Color = System.Drawing.Color.FromArgb(50, 224, 0, 0);
            chart1.Series["Log In"].Points[chart1.Series["Log In"].Points.Count - 1].Color = System.Drawing.Color.FromArgb(30, 225, 128, 0);
            //chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
            chart1.ChartAreas[0].AxisY.MajorGrid.Interval = 5;
            //chart1.ChartAreas[0].AxisX.Maximum = i++;           
            // }
            //chart1.Series["Log In"].Points.AddXY("Ajay", "10000");
            //chart1.Series["Log In"].Points.AddXY("Ramesh", "8000");
            //chart1.Series["Log In"].Points.AddXY("Ankit", "7000");
            //chart1.Series["Log In"].Points.AddXY("Gurmeet", "10000");
            //chart1.Series["Log In"].Points.AddXY("Suresh", "8500");
            //chart title  
            //  chart1.Titles.Add("Log In Chart");
        }

        private void chart1_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {
            // Check selected chart element and set tooltip text for it
            switch (e.HitTestResult.ChartElementType)
            {
                case ChartElementType.DataPoint:
                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = string.Format("{0}\n{1}" + e.HitTestResult.Series.ToolTip.ToString(), dataPoint.XValue, dataPoint.YValues[0]);
                    break;



            }

        }

        private void dataGridViewCount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewInValidUserLogin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
