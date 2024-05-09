using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ManagersBLL
    {
        ManagersDAL managersDAL = new ManagersDAL();
        public List<Manager> LoadManagerByClubID(int clubID)
        {
            return managersDAL.LoadManagerByClubID(clubID);
        }

        public bool AddData(Manager manager)
        {
            return managersDAL.AddData(manager);
        }

        public bool DeleteData(int managerID)
        {
            return managersDAL.DeleteData(managerID);
        }

        public Manager LoadDataByManagerID(int managerID)
        {
            return managersDAL.LoadDataByManagerID(managerID);
        }

        public bool EditData(Manager manager)
        {
            return managersDAL.EditData(manager);
        }
    }
}
