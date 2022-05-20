using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Testing
{
    public class MyDB : IDisposable
    {
        private SqlConnection conn = new SqlConnection(); //SqlConnection();
        public string connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString"].ConnectionString;
        //public string connectionString = "Data Source=FIPNHMID01;Initial Catalog=Card11;User ID=sa;Password=Forte@1234;";
        //public string connectionString = "Data Source=192.168.110.246;Initial Catalog=Card11;User ID=sa;Password=Forte@1234;";
        #region "Properties"
        public string ConnectionString { get; set; }
        #endregion
        #region "Constructors"
        /// <summary>
        /// MyDB default constructor
        /// </summary>
        public MyDB() { }

        /// <summary>
        /// MyDB Constructor with connectionString parameter
        /// </summary>
        /// <param name="connectionString">Database connection string. Ex. "Server=localhost;Database=db_name;Uid=u_name;Pwd=u_pwd; MinimumPoolSize=1;maximumpoolsize=1;"</param>
        public MyDB(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
        #endregion
        #region "Procedures"
        /// <summary>
        /// openConnection: open MySqlConnection
        /// </summary>
        private void openConneciton()
        {
            try
            {
                conn.ConnectionString = connectionString;
                if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error OpenConnection");
            }
        }

        /// <summary>
        /// closeConnection: close mysqlConnection;
        /// </summary>
        private void closeConnection()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open) conn.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error CloseConnection");
            }
        }

        /// <summary>
        /// ExecuteMySql: Insert, Delete, or Update of a single record
        /// </summary>
        /// <param name="string_name">Sql String or Stored Procedure</param>
        /// <param name="variables">ParamArrays of Field name followed by its value. E.g. "name","Sok","sex","Male"...</param>
        public void ExecuteMySql(string string_name, params object[] variables)
        {
            openConneciton();
            try
            {
                SqlCommand com = new SqlCommand(); //SqlCommand();
                com.Connection = conn;
                com.CommandText = string_name;
                if (string_name.Contains("sp_"))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                }
                for (int i = 0; i < variables.Length - 1; i += 2)
                {
                    SqlParameter p = new SqlParameter(variables[i].ToString(), variables[i + 1]); //SqlParameter
                    com.Parameters.Add(p);
                }
                com.ExecuteNonQuery();
                com.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error ExecuteMySql");
            }
            closeConnection();
        }

        /// <summary>
        /// ExecuteMySqL:Isert, Delete, or Update a table referecing by DataTable
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="string_name">Sql String or Stored Procedure</param>
        /// <param name="variables">ParamArrays of Field name followed by its value. E.g. "name","Sok","sex","Male"...</param>

        public void ExecuteMySql(ref DataTable dt, string string_name, params object[] variables)
        {
            openConneciton();
            try
            {
                SqlCommand com = new SqlCommand(); //SqlCommand();
                com.Connection = conn;
                com.CommandText = string_name;
                if (string_name.Contains("sp_"))
                {
                    com.CommandType = CommandType.StoredProcedure;
                }
                for (int i = 0; i < variables.Length - 1; i += 2)
                {
                    SqlParameter p = new SqlParameter(variables[i].ToString(), variables[i + 1]); //SqlParameter;
                    com.Parameters.Add(p);
                }
                SqlDataAdapter da = new SqlDataAdapter(com); //SqlDataAdapter;
                da.Update(dt);
                com.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error ExecuteMySql Ref DataTable");
            }
            closeConnection();
        }
        #endregion

        #region "Functions"
        /// <summary>
        /// getDataTable: return DataTable for use.
        /// </summary>
        /// <param name="string_name">Sql String or Stored Procedure</param>
        /// <param name="variables">ParamArrays of Field name followed by its value. E.g. "name","Sok","sex","Male"...</param>
        /// <returns></returns>
        public DataTable getDataTable(string string_name, params object[] variables)
        {
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand(); //SqlCommand();
            openConneciton();
            com.Connection = conn;
            com.CommandText = string_name;
            if (string_name.Contains("sp_"))
            {
                com.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i < variables.Length - 1; i += 2)
            {
                SqlParameter p = new SqlParameter(variables[i].ToString(), variables[i + 1]); //SqlParameter;
                com.Parameters.Add(p);
            }
            SqlDataAdapter da = new SqlDataAdapter(com); //SqlDataAdapter;
            da.Fill(dt);
            com.Dispose();
            closeConnection();

            return dt;
        }



        #endregion

        /// <summary>
        /// ExecuteMySqlAndGetValue: Insert, Delete, or Update and provide its ID to use for another purpose.
        /// </summary>
        /// <param name="string_name">Sql String or Stored Procedure</param>
        /// <param name="variables">ParamArrays of Field name followed by its value. E.g. "name","Sok","sex","Male"...</param>
        /// <returns></returns>
        public object ExecuteMySqlAndGetValue(string string_name, params object[] variables)
        {
            object myVal;
            openConneciton();

            SqlCommand com = new SqlCommand(); //SqlCommand();
            com.Connection = conn;
            com.CommandText = string_name;
            if (string_name.Contains("sp_"))
            {
                com.CommandType = CommandType.StoredProcedure;
            }
            for (int i = 0; i < variables.Length - 1; i += 2)
            {
                SqlParameter p = new SqlParameter(variables[i].ToString(), variables[i + 1]); //SqlParameter;
                com.Parameters.Add(p);
            }

            myVal = com.ExecuteScalar();
            com.Dispose();
            closeConnection();
            return myVal;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    conn.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~MyDB()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

        public DataTable ExecQuery(string sql)
        {
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand(); //SqlCommand();
            openConneciton();
            com.Connection = conn;
            com.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(com); //SqlDataAdapter;
            da.Fill(dt);
            com.Dispose();
            closeConnection();

            return dt;
        }

        public DataTable ExecQuery(SqlCommand com)
        {
            DataTable dt = new DataTable();
            openConneciton();
            com.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(com); //SqlDataAdapter;
            da.Fill(dt);
            com.Dispose();
            closeConnection();

            return dt;
        }
    }
}
