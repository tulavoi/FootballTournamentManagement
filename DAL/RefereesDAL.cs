using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DAL
{
    public class RefereesDAL
    {
        public List<Referee> LoadData()
        {
            List<Referee> referees = new List<Referee>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from r in db.Referees
                            select r;

                foreach (var item in query)
                {
                    Referee referee = new Referee();
                    referee.RefereeID = item.RefereeID;
                    referee.RefereeName = item.RefereeName;
                    referee.DOB = item.DOB;
                    referee.Country = item.Country;

                    referees.Add(referee);
                }
            }
            return referees;
        }

        public Referee LoadDataByRefereeID(int refereeID)
        {
            Referee referee = new Referee();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Referees.Where(r => r.RefereeID == refereeID).FirstOrDefault();
                if (query != null)
                {
                    referee.RefereeName = query.RefereeName;
                    referee.DOB = query.DOB;
                    referee.Country = query.Country;

                    return referee;
                }

            }
            return null;
        }


        public bool AddData(Referee referee)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    db.Referees.InsertOnSubmit(referee);
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

        public bool DeleteData(int refereeID)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.Referees.Where(r => r.RefereeID == refereeID).FirstOrDefault();

                    if (query != null)
                    {
                        db.Referees.DeleteOnSubmit(query);
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

        public bool EditData(Referee referee)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    int id = referee.RefereeID;
                    var query = db.Referees.Where(r => r.RefereeID == id).FirstOrDefault();
                    if (query != null)
                    {
                        query.RefereeName = referee.RefereeName;
                        query.DOB = referee.DOB;
                        query.Country = referee.Country;

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
