using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RoundsBLL
    {
        RoundsDAL roundsDAL = new RoundsDAL();

        public List<Round> LoadDataBySeasonID(int seasonID)
        {
            return roundsDAL.LoadDataBySeasonID(seasonID);
        }

        public Round LoadDataByID(string roundID)
        {
            return roundsDAL.LoadDataByID(roundID);
        }

        public Match GetLastMatchInPreviousMatchweekByRoundNameAndSeasonID(int seasonID, string currentRoundName)
        {
            return roundsDAL.GetLastMatchInPreviousMatchweekByRoundNameAndSeasonID(seasonID, currentRoundName);
        }

        public Round GetPreviousRound(int seasonID, string previousMatchweekName)
        {
            return roundsDAL.GetPreviousRound(seasonID, previousMatchweekName);
        }
    }
}
