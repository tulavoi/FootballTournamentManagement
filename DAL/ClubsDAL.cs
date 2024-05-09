using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ClubsDAL
    {
        public List<Club> LoadData()
        {
            List<Club> clubs = new List<Club>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from club in db.Clubs
                            where club.ClubID != 1026
                            select club;

                foreach (var item in query)
                {
                    Club club = new Club();
                    club.ClubID = item.ClubID;
                    club.Logo = item.Logo;
                    club.ClubName = item.ClubName;

                    clubs.Add(club);
                }
            }

            return clubs;
        }

        public Club LoadDataByID(int clubID)
        {
            Club club = new Club();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Clubs.Where(cl => cl.ClubID == clubID).FirstOrDefault();

                club.ClubID = query.ClubID;
                club.Logo = query.Logo;
                club.ClubName = query.ClubName;
            }

            return club;
        }

        public bool AddData(Club club)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    db.Clubs.InsertOnSubmit(club);
                    db.SubmitChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool EditData(Club clubEdit)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    int id = clubEdit.ClubID;
                    var query = db.Clubs.Where(club => club.ClubID == id).FirstOrDefault();
                    if (query != null)
                    {
                        query.ClubName = clubEdit.ClubName;
                        query.Logo = clubEdit.Logo;
                        db.SubmitChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteData(int id)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.Clubs.Where(club => club.ClubID == id).FirstOrDefault();
                    if (query != null)
                    {
                        db.Clubs.DeleteOnSubmit(query);
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
