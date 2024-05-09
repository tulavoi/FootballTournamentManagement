using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace DAL
{
    public class ConnectDatabase
    {
        public string connectionString = "Data Source=LAPTOP-5I4BGSNV\\HOANGVU;Initial Catalog=DBProject.Net;Integrated Security=True";
        //public string connectionString = WebConfigurationManager.ConnectionStrings["DAL.Properties.Settings.DBProject_NetConnectionString"].ConnectionString;

        public SqlConnection connection = null;

        public void CloseConnect()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public void OpenConnect()
        {
            connection = new SqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public SqlCommand CreateCommand(string cmdText, CommandType storedProcedure)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = storedProcedure;
            cmd.CommandText = cmdText;
            cmd.Connection = connection;
            return cmd;
        }
    }
}
