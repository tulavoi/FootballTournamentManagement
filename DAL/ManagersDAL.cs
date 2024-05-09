using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class ManagersDAL
    {
        public List<Manager> LoadManagerByClubID(int clubID)
        {
            List<Manager> managers = new List<Manager>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from m in db.Managers
                            where m.ClubID == clubID
                            select new
                            {
                                managerID = m.ManagerID,
                                managerName = m.ManagerName,
                                image = m.Image,
                                country = m.Country,
                                dob = m.DOB,
                            };

                foreach (var item in query)
                {
                    Manager manager = new Manager();
                    manager.ManagerID = item.managerID;
                    manager.ManagerName = item.managerName;
                    manager.Image = item.image;
                    manager.Country = item.country;
                    manager.DOB = item.dob;

                    managers.Add(manager);
                }
            }

            return managers;
        }


        public bool AddData(Manager manager)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    db.Managers.InsertOnSubmit(manager);
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


        public bool DeleteData(int managerID)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.Managers.Where(m => m.ManagerID == managerID).FirstOrDefault();
                    if (query != null)
                    {
                        db.Managers.DeleteOnSubmit(query);
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

        public Manager LoadDataByManagerID(int managerID)
        {
            Manager manager = new Manager();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Managers.Where(m => m.ManagerID == managerID).FirstOrDefault();

                manager.ManagerID = query.ManagerID;
                manager.ManagerName = query.ManagerName;
                manager.ClubID = query.ClubID;
                manager.Image = query.Image;
                manager.Country = query.Country;
                manager.DOB = query.DOB;
            }
            return manager;
        }

        public bool EditData(Manager manager)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    int id = manager.ManagerID;
                    var query = db.Managers.Where(m => m.ManagerID == id).FirstOrDefault();
                    if (query != null)
                    {
                        query.ManagerName = manager.ManagerName;
                        query.ClubID = manager.ClubID;
                        query.Image = manager.Image;
                        query.Country = manager.Country;
                        query.DOB = manager.DOB;

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
