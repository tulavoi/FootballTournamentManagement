using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;

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
				//throw ex;
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

        public List<Match> GetDataByRoundID(string roundID, int seasonID)
        {
            List<Match> matches = new List<Match>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from m in db.Matches
                            join homeClub in db.Clubs on m.HomeID equals homeClub.ClubID
                            join awayClub in db.Clubs on m.AwayID equals awayClub.ClubID
                            where m.RoundID == roundID && m.SeasonID == seasonID
                            select new
                            {
                                matchID = m.MatchID,
                                roundID = m.RoundID,
                                seasonID = m.SeasonID,
                                homeID = m.HomeID,
                                awayID = m.AwayID,
                                matchName = m.MatchName,
                                homeClubName = homeClub.ClubName,
                                awayClubName = awayClub.ClubName,
                                homeClubLogo = homeClub.Logo,
                                awayClubLogo = awayClub.Logo
                            };

                foreach (var item in query)
                {
                    Match match = new Match();
                    match.MatchID = item.matchID;
                    match.RoundID = item.roundID;
                    match.SeasonID = item.seasonID;
                    match.HomeID = item.homeID;
                    match.AwayID = item.awayID;
                    match.MatchName = item.matchName;

                    match.Club = new Club { ClubName = item.homeClubName, Logo = item.homeClubLogo };
                    match.Club1 = new Club { ClubName = item.awayClubName, Logo = item.awayClubLogo };

                    matches.Add(match);
                }
            }
            return matches;
        }
    }
}
