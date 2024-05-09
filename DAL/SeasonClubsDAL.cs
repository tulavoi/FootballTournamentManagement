using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SeasonClubsDAL
    {
        public List<Club> LoadDataBySeasonID(int seasonID){
            List<Club> clubs = new List<Club>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from clubSeason in db.SeasonClubs
                            join club in db.Clubs on clubSeason.ClubID equals club.ClubID
                            where clubSeason.SeasonID == seasonID
                            select club;

                foreach (var item in query)
                {
                    Club club = new Club();
                    club.ClubID = item.ClubID;
                    club.ClubName = item.ClubName;
                    club.Logo = item.Logo;

                    clubs.Add(club);
                }
            }

            return clubs;
        }
    }
}
