using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace Testing
{
    public class CRUD
    {
        string connString = ConfigurationManager.ConnectionStrings["Testing.Properties.Settings.ConnectionString"].ConnectionString;

        public DataTable ExecQuery(string sql)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
            }
            return dt;
        }

        public DataTable ExecQuery(OracleCommand cmd)
        {
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
            }

            return dt;
        }

        public void ExecNonQuery(string sql)
        {
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public void ExecNonQuery(OracleCommand cmd)
        {
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }


        public void ExecSP_NoOutPara(string spName, string[] spParaKeys, string[] spParaValues)
        {
            if (spParaKeys.Length != spParaValues.Length)
            {
                Msgbox.Show("Error Parameters in Function to call Stored Procedures!");
                return;
            }
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                for (int i = 0; i < spParaKeys.Length; i++)
                {
                    cmd.Parameters.Add(spParaKeys[i], OracleDbType.NVarchar2).Value = spParaValues[i];
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }

        public DataTable ExecSP_OutPara(string spName, string[] spParaKeys, string[] spParaValues)
        {
            if (spParaKeys.Length != spParaValues.Length)
            {
                Msgbox.Show("Error Parameters in Function to call Stored Procedures!");
                return null;
            }
            DataTable dt = new DataTable();
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                for (int i = 0; i < spParaKeys.Length; i++)
                {
                    cmd.Parameters.Add(spParaKeys[i], OracleDbType.NVarchar2).Value = spParaValues[i];
                }
                cmd.Parameters.Add("sp_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                dt.Load(cmd.ExecuteReader());
                cmd.Dispose();
            }
            return dt;
        }

        public string ExecFunc_String(string fnName, string[] fnParaKeys, string[] fnParaValues)
        {
            string result = string.Empty;
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = fnName;
                //return parameter from oracle function must be first index
                cmd.Parameters.Add("result", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.ReturnValue;
                for (int i = 0; i < fnParaKeys.Length; i++)
                {
                    cmd.Parameters.Add(fnParaKeys[i], OracleDbType.NVarchar2).Value = fnParaValues[i];
                }
                cmd.ExecuteNonQuery();
                result = cmd.Parameters["result"].Value.ToString();
                cmd.Dispose();
            }
            return result;
        }

        public string ExecFunc_String_New(string fnName, string[] fnParaKeys, string[] fnParaValues)
        {
            string result = string.Empty;
            using (OracleConnection con = new OracleConnection(connString))
            {
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = fnName;
                //return parameter from oracle function must be first index
                cmd.Parameters.Add("result", OracleDbType.Clob).Direction = ParameterDirection.ReturnValue;
                for (int i = 0; i < fnParaKeys.Length; i++)
                {
                    cmd.Parameters.Add(fnParaKeys[i], OracleDbType.NVarchar2).Value = fnParaValues[i];
                }
                cmd.ExecuteNonQuery();

                var clobValue = (OracleClob)cmd.Parameters["result"].Value;
                result = clobValue.Value.ToString();

                cmd.Dispose();
            }
            return result;
        }
    }

    //class CRUD
    //{
    //    //string connString = "User Id=sicl;Password=sicl;Data Source=192.168.110.241:1521/infolive";
    //    //string connString = "User Id=sicl;Password=sficlive19;Data Source=192.168.110.241:1521/infolive";
    //    string connString = ConfigurationManager.ConnectionStrings["Testing.Properties.Settings.ConnectionString"].ConnectionString;
      
    //    //con.ConnectionString = connString;
            

    //    public DataTable ExecQuery(string sql)
    //    {
    //        OracleConnection con = new OracleConnection(connString);
    //        con.Open();
    //        OracleCommand cmd = new OracleCommand();
    //        cmd.Connection = con;
    //        cmd.CommandType = CommandType.Text;
    //        cmd.CommandText = sql;
    //        DataTable dt = new DataTable();
    //        dt.Load(cmd.ExecuteReader());

    //        //cmd.Connection.Close();
    //        cmd.Connection.Dispose();
    //        con.Close();
    //        con.Dispose();
            
    //        return dt;
    //    }

    //    public DataTable ExecQuery(OracleCommand cmd)
    //    {
    //        OracleConnection con = new OracleConnection(connString);
    //        con.Open();
    //        cmd.Connection = con;
    //        cmd.CommandType = CommandType.Text;
    //        DataTable dt = new DataTable();
    //        dt.Load(cmd.ExecuteReader());

    //        //cmd.Connection.Close();
    //        cmd.Connection.Dispose();
    //        con.Close();
    //        con.Dispose();

    //        return dt;
    //    }

    //    public void ExecNonQuery(string sql)
    //    {
    //        //OracleConnection con = new OracleConnection();
    //        //con.ConnectionString = connString;
    //        OracleConnection con = new OracleConnection(connString);
    //        con.Open();
    //        OracleCommand cmd = new OracleCommand();
    //        cmd.Connection = con;
    //        cmd.CommandType = CommandType.Text;
    //        cmd.CommandText = sql;
    //        cmd.ExecuteNonQuery();

    //        //cmd.Connection.Close();
    //        cmd.Connection.Dispose();
    //        con.Close();
    //        con.Dispose();
    //    }

    //    public void ExecNonQuery(OracleCommand cmd)
    //    {
    //        OracleConnection con = new OracleConnection(connString);
    //        con.Open();
    //        cmd.Connection = con;
    //        cmd.CommandType = CommandType.Text;
    //        cmd.ExecuteNonQuery();

    //        cmd.Connection.Dispose();
    //        con.Close();
    //        con.Dispose();
    //    }


    //    public void ExecSP_NoOutPara(string spName, string[] spParaKeys, string[] spParaValues)
    //    {
    //        if (spParaKeys.Length != spParaValues.Length)
    //        {
    //            Msgbox.Show("Error Parameters in Function to call Stored Procedures!");
    //            return;
    //        }

    //        //OracleConnection con = new OracleConnection();
    //        //con.ConnectionString = connString;
    //        OracleConnection con = new OracleConnection(connString);
    //        con.Open();
    //        OracleCommand cmd = new OracleCommand();
    //        cmd.Connection = con;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = spName;

    //        for (int i = 0; i < spParaKeys.Length; i++)
    //        {
    //            cmd.Parameters.Add(spParaKeys[i], OracleDbType.NVarchar2).Value = spParaValues[i];
    //        }

    //        cmd.ExecuteNonQuery();

    //        //cmd.Connection.Close();
    //        cmd.Connection.Dispose();
    //        con.Close();
    //        con.Dispose();
    //    }

    //    public DataTable ExecSP_OutPara(string spName, string[] spParaKeys, string[] spParaValues)
    //    {
           
    //            if (spParaKeys.Length != spParaValues.Length)
    //            {
    //                Msgbox.Show("Error Parameters in Function to call Stored Procedures!");
    //                return null;
    //            }

    //            //OracleConnection con = new OracleConnection();
    //            //con.ConnectionString = connString;
    //            OracleConnection con = new OracleConnection(connString);
    //            con.Open();
    //            OracleCommand cmd = new OracleCommand();
    //            cmd.Connection = con;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = spName;

    //            for (int i = 0; i < spParaKeys.Length; i++)
    //            {
    //                cmd.Parameters.Add(spParaKeys[i], OracleDbType.NVarchar2).Value = spParaValues[i];
    //            }
    //            cmd.Parameters.Add("sp_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

    //            DataTable dt = new DataTable();
    //            dt.Load(cmd.ExecuteReader());

    //            //cmd.ExecuteNonQuery();
    //            //OracleDataReader dr = ((OracleRefCursor)cmd.Parameters["sp_result"].Value).GetDataReader();
    //            //DataTable dt = new DataTable();
    //            //OracleDataAdapter da = new OracleDataAdapter(cmd);
    //            //da.Fill(dt);
    //            //dt.Load(dr);

    //            //cmd.Connection.Close();
    //            cmd.Connection.Dispose();
    //            con.Close();
    //            con.Dispose();

                
           
    //        return dt;
    //    }

    //    public string ExecFunc_String(string fnName, string[] fnParaKeys, string[] fnParaValues)
    //    {
    //        //OracleConnection con = new OracleConnection();
    //        //con.ConnectionString = connString;
    //        OracleConnection con = new OracleConnection(connString);
    //        con.Open();
    //        OracleCommand cmd = new OracleCommand();
    //        cmd.Connection = con;
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.CommandText = fnName;
    //        //return parameter from oracle function must be first index
    //        cmd.Parameters.Add("result", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.ReturnValue;

    //        for (int i = 0; i < fnParaKeys.Length; i++)
    //        {
    //            cmd.Parameters.Add(fnParaKeys[i], OracleDbType.NVarchar2).Value = fnParaValues[i];
    //        }

    //        cmd.ExecuteNonQuery();
    //        string result = cmd.Parameters["result"].Value.ToString();
    //        cmd.Connection.Dispose();
    //        con.Close();
    //        con.Dispose();

    //        return result;
    //    }
    //}
}
