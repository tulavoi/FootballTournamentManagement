using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
