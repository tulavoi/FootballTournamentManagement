using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MatchDetailBLL
    {
        MatchDetailDAL matchDetailDAL = new MatchDetailDAL();

        public MatchDetail LoadDataByMatchID(string matchID)
        {
            return matchDetailDAL.LoadDataByMatchID(matchID);
        }
    }
}
