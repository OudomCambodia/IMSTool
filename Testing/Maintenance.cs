using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Testing
{
    public static class Maintenance
    {
        static string connString = ConfigurationManager.ConnectionStrings["Testing.Properties.Settings.ConnectionString"].ConnectionString;
        public static bool Check()
        {
            DataTable dt = new DataTable();
            //using (OracleConnection con = new OracleConnection(connString))
            //{
            //    con.Open();
            //    OracleCommand cmd = new OracleCommand();
            //    cmd.Connection = con;
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = "SELECT PASSWORD FROM USER_PRINT_SYSTEM WHERE USER_CODE = 'MAINT'";
            //    dt.Load(cmd.ExecuteReader());
            //    cmd.Dispose();
            //}
            CRUD crud = new CRUD();
            dt = crud.ExecQuery("SELECT PASSWORD FROM USER_PRINT_SYSTEM WHERE USER_CODE = 'MAINT'");
            string result = dt.Rows[0][0].ToString();
            if (result == "OFF")
                return false;
            else if (result == "ON")
                return true;
            return false;
        }
    }
}
