using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class StadiumsBLL
    {
        StadiumsDAL stadiumsDAL = new StadiumsDAL();

        public Stadium LoadDataByClubID(int clubID)
        {
            return stadiumsDAL.LoadDataByClubID(clubID);
        }

        public bool AddData(Stadium stadium)
        {
            return stadiumsDAL.AddData(stadium);
        }

        public bool DeleteData(string stadiumID)
        {
            return stadiumsDAL.DeleteData(stadiumID);
        }

        public bool EditData(Stadium stadium)
        {
            return stadiumsDAL.EditData(stadium);
        }
    }
}
