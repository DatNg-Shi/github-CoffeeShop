using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO_CoffeeShop
{
    public class DBUtilities
    {
        private static DBUtilities instance;
        private string strConnection = "server=SE130130;database=CoffeeShop;uid=sa;pwd=momasa123";
        public static DBUtilities Instance
        {
            get { if (instance == null) instance = new DBUtilities(); return DBUtilities.instance; }
            private set { DBUtilities.instance = value; }
        }
        
        private DBUtilities() { }
        public DataTable ExecuteQuery(string SQL, object[] parameter = null) // return table
        {
            SqlConnection cnn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            if (parameter != null)
            {
                string[] listPara = SQL.Split(' ');
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        cmd.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtAccount = new DataTable();
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }

                da.Fill(dtAccount);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cnn.Close();
            }
            return dtAccount;
        }

        public int ExecuteNonQuery(string SQL, object[] parameter = null) //so dong thanh cong
        {
            int result = 0;
            using (SqlConnection cnn = new SqlConnection(strConnection))
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, cnn);
                if (parameter != null)
                {
                    string[] listPara = SQL.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                result = cmd.ExecuteNonQuery();
                cnn.Close();
            }
            return result;
        }

        public object ExecuteScalar(string SQL, object[] parameter = null) //return so luong
        {
            object result = 0;
            using (SqlConnection cnn = new SqlConnection(strConnection))
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlCommand cmd = new SqlCommand(SQL, cnn);
                if (parameter != null)
                {
                    string[] listPara = SQL.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                result = cmd.ExecuteScalar();
                cnn.Close();
            }
            return result;
        }
    }
}
