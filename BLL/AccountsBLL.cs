using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AccountsBLL
    {
        AccountsDAL accountsDAL = new AccountsDAL();
        public bool CheckLogin(Account account)
        {
            return accountsDAL.CheckLogin(account);
        }
    }
}
