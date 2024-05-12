using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MatchesBLL
    {
        MatchesDAL matchesDAL = new MatchesDAL();
        public bool AddData(string roundID, int seasonID)
        {
            return matchesDAL.AddData(roundID, seasonID);
        }
    }
}
