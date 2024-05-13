using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;

namespace DAL
{
    public class AccountsDAL
    {
        public bool CheckLogin(Account account)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Accounts.Where(a => a.Email == account.Email && a.Password == account.Password).FirstOrDefault();

                if (query != null)
                    return true;
                else
                    return false;
            }
        }
    }
}
