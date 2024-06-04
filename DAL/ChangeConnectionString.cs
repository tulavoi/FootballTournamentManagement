using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ChangeConnectionString
    {
        private static string connectionString;

        public static void setConnectionString(string connectionStr)
        {
            connectionString = connectionStr;
        }

        public static string getConnectionString()
        {
            return connectionString;
        }
    }
}
