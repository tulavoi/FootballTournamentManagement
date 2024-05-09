using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SeasonClubsBLL
    {
        SeasonClubsDAL ssClubsDAL = new SeasonClubsDAL();

        public List<Club> LoadDataBySeasonID(int seasonID)
        {
            return ssClubsDAL.LoadDataBySeasonID(seasonID);
        }
    }
}
