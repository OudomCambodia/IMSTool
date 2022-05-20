using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Testing
{
    class DBS11SqlCrud
    {
        public SqlConnection connection;

        //~DBS11SqlCrud() {
        //    CloseConnection();
        //}

        public SqlConnection CreateConnection() //create mysql connection
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBS11SQLConnectionString"].ConnectionString;
            connection = new SqlConnection(connectionString);

            return connection;
        }

        private bool OpenConnection() //connect open
        {
            try
            {
                connection = CreateConnection();
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private bool CloseConnection() //conne ction close
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void Executing(string query) //execute sql query
        {

            try
            {
                if (OpenConnection())
                {
                    //create command and assign the query and connection from the constructor
                    SqlCommand cmd = new SqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Executing(SqlCommand cmd)
        {
            try
            {
                if (OpenConnection())
                {
                    cmd.Connection = connection;
                    cmd.ExecuteNonQuery();
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet LoadData(string Query) //get data using sql query
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection())
                {

                    SqlCommand cmd = new SqlCommand(Query, connection);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }

                    CloseConnection();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet LoadData(SqlCommand cmd) //get data using sql query
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection())
                {

                    cmd.Connection = connection;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(ds);
                    }

                    CloseConnection();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DataSet ExucteSP(Dictionary<string, string> Params, string spName) // execute sql stored procedure 
        {
            try
            {
                DataSet ds = new DataSet();
                if (OpenConnection()) //open connection
                {
                    using (SqlCommand cmd = new SqlCommand(spName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (KeyValuePair<string, string> item in Params)
                        {
                            //Console.WriteLine("Key: {0}, Value: {1}", item.Key, item.Value);
                            cmd.Parameters.AddWithValue("@" + item.Key, item.Value);


                        }
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(ds);
                        }
                    }
                    CloseConnection(); //close connection
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ExecuteMySql(string string_name, params object[] variables)
        {
            OpenConnection();
            try
            {
                SqlCommand com = new SqlCommand(); //SqlCommand();
                com.Connection = connection;
                com.CommandText = string_name;
                com.CommandType = System.Data.CommandType.StoredProcedure;
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
            CloseConnection();
        }


        public DataTable ExecuteMySqlOutPara(string string_name, params object[] variables)
        {
            OpenConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand com = new SqlCommand(); //SqlCommand();
                com.Connection = connection;
                com.CommandText = string_name;
                com.CommandType = System.Data.CommandType.StoredProcedure;
                for (int i = 0; i < variables.Length - 1; i += 2)
                {
                    SqlParameter p = new SqlParameter(variables[i].ToString(), variables[i + 1]); //SqlParameter
                    com.Parameters.Add(p);
                }

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                com.Connection.Dispose();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Error ExecuteMySql");
            }
            CloseConnection();
            return dt;
        }
    }
}
