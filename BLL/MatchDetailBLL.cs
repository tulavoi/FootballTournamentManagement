using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public MatchDetail GetMatchDetail(string matchID)
        {
            return matchDetailDAL.GetMatchDetail(matchID);
        }

        public bool EditData(MatchDetail matchDetail, DAL.Match match)
        {
            return matchDetailDAL.EditData(matchDetail, match);
        }
    }
}
