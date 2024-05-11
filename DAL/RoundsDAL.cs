using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoundsDAL
    {
        public List<Round> LoadDataBySeasonID(int seasonID)
        {
            List<Round> rounds = new List<Round>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from r in db.Rounds
                            where r.SeasonID == seasonID
                            orderby Convert.ToInt32(r.RoundName.Substring(10)) ascending
                            select r;

                foreach (var item in query)
                {
                    Round round = new Round();
                    round.RoundID = item.RoundID;
                    round.SeasonID = item.SeasonID;
                    round.RoundName = item.RoundName;

                    rounds.Add(round);
                }
            }
            return rounds;
        }

        public void CreateData(int seasonID)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                int numberOfRounds = 38;
                for (int i = 1; i <= numberOfRounds; i++)
                {
                    Round round = new Round
                    {
                        RoundID = $"MW{i}_{seasonID}", // Tạo RoundID dạng 'MW1_seasonID', 'MW2_seasonId'
                        RoundName = $"Matchweek {i}",
                        SeasonID = seasonID
                    };

                    db.Rounds.InsertOnSubmit(round);
                }

                db.SubmitChanges();
            }
        }
    }
}
