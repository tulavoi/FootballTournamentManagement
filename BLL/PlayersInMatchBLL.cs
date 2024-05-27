using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public List<PlayersInMatch> LoadAllPlayerInMatch(string matchID)
        {
            return playersInMatchDAL.LoadAllPlayerInMatch(matchID);
        }

        public bool AddData(List<PlayersInMatch> players)
        {
            return playersInMatchDAL.AddData(players);
        }

        public PlayersInMatch Get1PlayerInMatch(string matchID, int playerID)
        {
            return playersInMatchDAL.Get1PlayerInMatch(matchID, playerID);
        }

        public bool EditData(PlayersInMatch playersInMatch)
        {
            return playersInMatchDAL.EditData(playersInMatch);
        }

        public bool DeleteData(int playerID, string selectedMatchID)
        {
            return playersInMatchDAL.DeleteData(playerID, selectedMatchID);

        }
    }
}
