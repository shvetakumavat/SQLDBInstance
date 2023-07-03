using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Collections;
using System.Data;


namespace STOCK_SYS
{
    public class SqlDBInstance
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        SqlParameter prm_return,prm_values,sql_datatable;
        DataTable dt;

        List<SqlParameter> prmlst;


        public SqlDBInstance()
        {
            con = new SqlConnection();
            con.ConnectionString = getConnectionString();
            con.Close();
        }
       
        private static String  getConnectionString()
        {
            return "Data Source=RAHUL-PC\\SQLEXPRESS;Initial Catalog=STOCK_SYS;Integrated Security=True";
           //return Properties.Settings.Default.ConnectionString.ToString();
           // return "";
        }

        public int generateCommand(string cmdName,CommandType type = CommandType.Text)
        {
            int i;
            try
            {
                cmd = new SqlCommand();
                con.Open();
                lock (cmdName)
                {
                    cmd.Connection = con;
                    cmd.CommandText = cmdName;
                    cmd.CommandType =type;
                    i = cmd.ExecuteNonQuery();
                    return i;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        public int generateCommand(string prName, Hashtable ht)
        {
            int i = 0;
            try
            {
                cmd = new SqlCommand();
                prm_return = new SqlParameter("@id", SqlDbType.Int);
                prm_return.Direction = ParameterDirection.ReturnValue;
                con.Open();
                cmd.Connection = con;
                lock (prName)
                {
                    cmd.CommandText = prName;
                    ICollection keys = ht.Keys;
                    cmd.Parameters.Add(prm_return);
                    foreach (String k in keys)
                    {
                        cmd.Parameters.AddWithValue("@" + k, ht[k]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    i = Convert.ToInt32(prm_return.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception  : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return i;
        }

        /// <summary>
        /// this function is use for passing a datatable as parameter to insert all data of datatale to sql server 
        /// </summary>
        /// <param name="prName">procedure name</param>
        /// <param name="dtBhavcopy">datatabke for data insertion</param>
        /// <returns></returns>
        public int generateCommand(string prName,string prmDtName ,DataTable dtBhavcopy)
        {
            try
            {
                int i = 0;
                lock(prName)
                {
                    cmd = new SqlCommand();

                    sql_datatable = new SqlParameter();
                    sql_datatable.ParameterName =prmDtName;
                    sql_datatable.SqlDbType = SqlDbType.Structured;
                    sql_datatable.Value = dtBhavcopy;

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = prName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(sql_datatable);
                    i = cmd.ExecuteNonQuery();
                    return i;
                }
                

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        public DataTable getDataTable(string prName, Hashtable ht)
        {
            cmd = new SqlCommand();
            sda = new SqlDataAdapter();
            dt = new DataTable();

            try
            {
                con.Open();
                cmd.Connection = con;
                lock (prName)
                {
                    cmd.CommandText = prName;
                    ICollection keys = ht.Keys;
                    foreach (String k in keys)
                    {
                        cmd.Parameters.AddWithValue("@" + k, ht[k]);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public DataTable getProcedureDataTable(string prName)
        {
            cmd = new SqlCommand();
            sda = new SqlDataAdapter();
            dt = new DataTable();

            try
            {
                con.Open();
                cmd.Connection = con;
                lock (prName)
                {
                    cmd.CommandText = prName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }




        public DataTable getDataTable(string cmdName)
        {
            cmd = new SqlCommand();
            sda = new SqlDataAdapter();
            dt = new DataTable();

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = cmdName;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 180;
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception  : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public string getCountTable(string prName)
        {
            string cnt;
            cmd = new SqlCommand();
            sda = new SqlDataAdapter();
            try
            {
                con.Open();
                cmd.Connection = con;
                lock (prName)
                {
                    cmd.CommandText = prName;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 180;
                    cnt = cmd.ExecuteScalar().ToString();
                    return cnt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public string getScalarValue(string functionName, Hashtable ht)
        {
            string value;
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = functionName;
                prm_return = new SqlParameter("@prBal", SqlDbType.Float);
                prm_return.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(prm_return);
                ICollection keys = ht.Keys;
                foreach (String k in keys)
                {
                    cmd.Parameters.AddWithValue("@" + k, ht[k]);
                }
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteScalar();
                value = prm_return.Value.ToString();
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception : " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
