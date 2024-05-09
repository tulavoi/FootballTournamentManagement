using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SeasonBLL
    {
        SeasonsDAL seasonsDAL = new SeasonsDAL();
        public List<Season> LoadData()
        {
            return seasonsDAL.LoadData();
        }
    }
}
