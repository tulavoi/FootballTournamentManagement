using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PlayersInMatchBLL
    {
        PlayersInMatchDAL playersInMatchDAL = new PlayersInMatchDAL();
        public List<PlayersInMatch> LoadPlayerInMatch(string matchID, int isHome)
        {
            return playersInMatchDAL.LoadPlayerInMatch(matchID, isHome);
        }
    }
}
