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
    }
}
