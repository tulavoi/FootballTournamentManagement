using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SeasonsBLL
    {
        SeasonsDAL seasonsDAL = new SeasonsDAL();
        public List<Season> LoadData()
        {
            return seasonsDAL.LoadData();
        }

        public bool AddData(Season season)
        {
            return seasonsDAL.AddData(season);
        }
    }
}
