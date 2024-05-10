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
    }
}
