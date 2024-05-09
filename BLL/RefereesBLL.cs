using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RefereesBLL
    {
        RefereesDAL refereesDAL = new RefereesDAL();
        public List<Referee> LoadData()
        {
            return refereesDAL.LoadData();
        }

        public bool AddData(Referee referee)
        {
            return refereesDAL.AddData(referee);
        }

        public bool DeleteData(int refereeID)
        {
            return refereesDAL.DeleteData(refereeID);
        }

        public Referee LoadDataByRefereeID(int refereeID)
        {
            return refereesDAL.LoadDataByRefereeID(refereeID);
        }

        public bool EditData(Referee referee)
        {
            return refereesDAL.EditData(referee);
        }
    }
}
