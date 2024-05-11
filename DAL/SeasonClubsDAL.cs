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

        public bool DeleteDataByClubID(int clubID, int seasonID)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.SeasonClubs.Where(c => c.ClubID == clubID && c.SeasonID == seasonID).FirstOrDefault();
                    if (query != null)
                    {
                        db.SeasonClubs.DeleteOnSubmit(query);
                        db.SubmitChanges();

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool AddData(int clubID, int seasonID)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    // Kiểm tra số lượng đội bóng trong season có đạt giới hạn chưa (max là 20 club)
                    bool canAdd = db.SeasonClubs.Count(sc => sc.SeasonID == seasonID) < 20;
                    if (canAdd)
                    {
                        SeasonClub sc = new SeasonClub { ClubID = clubID, SeasonID = seasonID };
                        db.SeasonClubs.InsertOnSubmit(sc);
                        db.SubmitChanges();

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
