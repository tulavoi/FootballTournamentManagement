using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DAL
{
    public class StadiumsDAL
    {
        public Stadium LoadDataByClubID(int clubID)
        {
            Stadium stadium = new Stadium();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Stadiums.Where(s => s.ClubID == clubID).FirstOrDefault();

                if (query != null)
                {
                    stadium.StadiumID = query.StadiumID;
                    stadium.StadiumName = query.StadiumName;
                    stadium.Image = query.Image;
                    stadium.Size = query.Size;
                    stadium.Capacity = query.Capacity;
                    stadium.Location = query.Location;
                    stadium.BuiltTime = query.BuiltTime;
                    return stadium;
                }
                else
                    return null;
            }
        }

        public bool AddData(Stadium stadium)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    db.Stadiums.InsertOnSubmit(stadium);
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

        public bool DeleteData(string stadiumID)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.Stadiums.Where(s => s.StadiumID == stadiumID).FirstOrDefault();
                    db.SubmitChanges();
                    if (query != null)
                    {
                        db.Stadiums.DeleteOnSubmit(query);
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

        public bool EditData(Stadium stadium)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    string stadiumID = stadium.StadiumID;

                    var query = db.Stadiums.Where(s => s.StadiumID == stadiumID).FirstOrDefault();
                    if (query != null)
                    {
                        query.StadiumID = stadium.StadiumID;
                        query.StadiumName = stadium.StadiumName;
                        query.ClubID = stadium.ClubID;
                        query.Image = stadium.Image;
                        query.Size = stadium.Size;
                        query.Capacity = stadium.Capacity;
                        query.BuiltTime = stadium.BuiltTime;

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
