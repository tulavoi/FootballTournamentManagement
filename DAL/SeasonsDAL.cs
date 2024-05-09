using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SeasonsDAL
    {
        public List<Season> LoadData()
        {
            List<Season> seasons = new List<Season>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from ss in db.Seasons
                            select ss;

                foreach (var item in query)
                {
                    Season ss = new Season();
                    ss.SeasonID = item.SeasonID;
                    ss.SeasonName = item.SeasonName;
                    ss.StartDate = item.StartDate;
                    ss.EndDate = item.EndDate;

                    seasons.Add(ss);
                }
            }
            return seasons;
        }
    }
}
