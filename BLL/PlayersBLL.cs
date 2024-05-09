using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class PlayersBLL
    {
        PlayersDAL playersDAL = new PlayersDAL();

        public List<Player> LoadDataByClubID(int clubID)
        {
            return playersDAL.LoadDataByClubID(clubID);
        }

        public bool AddData(Player player)
        {
            return playersDAL.AddData(player);
        }

        public bool DeleteData(int playerID)
        {
            return playersDAL.DeleteData(playerID);
        }

        public bool DeleteAllData(int clubID)
        {
            return playersDAL.DeleteAllData(clubID);
        }

        public Player LoadDataByPlayerID(int playerID)
        {
            return playersDAL.LoadDataByPlayerID(playerID);
        }

        public bool EditData(Player player)
        {
            return playersDAL.EditData(player);
        }

        public List<Player> SearchPlayer(string keyword, int clubID)
        {
            return playersDAL.SearchPlayer(keyword, clubID);
        }
    }
}
