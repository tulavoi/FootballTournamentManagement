using System.Data;
using System.Data.SqlClient;

namespace GUI
{
    public class SqlHelper
    {
        SqlConnection cn;
        public SqlHelper(string connectionString)
        {
            cn = new SqlConnection(connectionString);
        }

        public bool IsConnect
        {
            get
            {
                if (cn.State == ConnectionState.Open)
                    cn.Open();
                return true;
            }

        }
    }
}
