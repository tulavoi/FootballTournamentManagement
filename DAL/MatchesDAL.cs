using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MatchesDAL
    {
        public bool AddData(string roundID, int seasonID)
        {
			try
			{
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    if (!ValidNumberOfMatchInOneRound(roundID))
                        return false;

                    // Lấy danh sách các đội chưa được chọn
                    var unselectedClubs = db.SeasonClubs
                                            .Where(sc => sc.SeasonID == seasonID && !db.Matches.Any(m => m.RoundID == roundID && (m.HomeID == sc.ClubID || m.AwayID == sc.ClubID)))
                                            .ToList();

                    int maxNumOfMatch = 10;

                    // Lặp để tạo 10 trận đấu mới
                    var random = new Random();
                    for (int i = 1; i <= maxNumOfMatch; i++)
                    {
                        // Lấy ngẫu nhiên đội chủ nhà từ danh sách đội chưa được chọn
                        var homeClub = unselectedClubs[random.Next(unselectedClubs.Count)];
                        unselectedClubs.Remove(homeClub); // Loại bỏ đội đã chọn để không chọn lại trong các lượt sau

                        // Lấy ngẫu nhiên đội khách từ danh sách đội chưa được chọn
                        var awayClub = unselectedClubs[random.Next(unselectedClubs.Count)];
                        unselectedClubs.Remove(awayClub);

                        // Lấy tên của đội chủ nhà và đội khách
                        var homeName = db.Clubs.FirstOrDefault(c => c.ClubID == homeClub.ClubID)?.ClubName;
                        var awayName = db.Clubs.FirstOrDefault(c => c.ClubID == awayClub.ClubID)?.ClubName;

                        // Tạo match name
                        string matchName = $"{homeName} - {awayName}";

                        // Tạo matchID
                        string matchID = $"{roundID}_{i.ToString().PadLeft(2, '0')}";

                        Match match = new Match();
                        match.MatchID = matchID;
                        match.SeasonID = seasonID;
                        match.RoundID = roundID;
                        match.HomeID = homeClub.ClubID;
                        match.AwayID = awayClub.ClubID;
                        match.MatchName = matchName;

                        db.Matches.InsertOnSubmit(match);
                    }
                    db.SubmitChanges();
                    return true;
                }
            }
			catch (Exception ex)
			{
				throw ex;
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool ValidNumberOfMatchInOneRound(string roundID)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                int numOfMatch = db.Matches.Where(m => m.RoundID == roundID).Count();

                return numOfMatch < 10;
            }
        }
    }
}
