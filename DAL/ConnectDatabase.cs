using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnectDatabase
    {
        public string connectionString = "Data Source=LAPTOP-5I4BGSNV\\HOANGVU;Initial Catalog=DBProject.Net;Integrated Security=True";
        public SqlConnection connection = null;


    }
}
