using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ClubsBLL
    {
        ClubsDAL clubsDAL = new ClubsDAL();

        public List<Club> LoadData()
        {
            return clubsDAL.LoadData();
        }

        public bool AddData(Club club)
        {
            return clubsDAL.AddData(club);
        }

        public bool DeleteData(int id)
        {
            return clubsDAL.DeleteData(id);
        }

        public bool EditData(Club clubEdit)
        {
            return clubsDAL.EditData(clubEdit);
        }

        public Club LoadDataByID(int clubID)
        {
            return clubsDAL.LoadDataByID(clubID);
        }
    }
}
