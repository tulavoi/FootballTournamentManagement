using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

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

        public Round LoadDataByID(string roundID)
        {
            Round round = new Round();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Rounds.Where(r => r.RoundID == roundID).FirstOrDefault();

                if (query != null)
                {
                    round.RoundID = query.RoundID;
                    round.SeasonID = query.SeasonID;
                    round.RoundName = query.RoundName;
                }
            }
            return round;
        }

        public Match GetLastMatchInPreviousMatchweekByRoundNameAndSeasonID(int seasonID, string currentRoundName)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                Round previousRound = new Round();
                if (currentRoundName == "Matchweek 1")
                { 
                    string roundIDInMatchweek1 = "MW1_1003";
                    var lastMatchInRound = from m in db.Matches
                                           join r in db.Rounds on m.RoundID equals r.RoundID
                                           where m.RoundID == roundIDInMatchweek1 && r.SeasonID == seasonID
                                           orderby m.MatchTime descending
                                           select m;
                    return lastMatchInRound.FirstOrDefault();
                }

                else
                    previousRound = GetPreviousRound(seasonID, currentRoundName);

                if (previousRound != null)
                {
                    var lastMatchInRound = from m in db.Matches
                                           join r in db.Rounds on m.RoundID equals r.RoundID
                                           where m.RoundID == previousRound.RoundID && r.SeasonID == seasonID
                                           orderby m.MatchTime descending
                                           select m;

                    return lastMatchInRound.FirstOrDefault();
                }
            }
            return null;
        }


        public Round GetPreviousRound(int seasonID, string currentRoundName)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                string[] part = currentRoundName.Split(' ');
                string prefix = part[0];
                int matchweekNum = Convert.ToInt32(part[1]);

                string roundName = prefix + " " + (matchweekNum - 1).ToString();

                var query = db.Rounds.Where(r => r.SeasonID == seasonID && r.RoundName == roundName).FirstOrDefault();
                return query;
            }
        }
    }
}
