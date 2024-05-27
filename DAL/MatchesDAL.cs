using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace DAL
{
    public class MatchesDAL
    {
        public bool AddData(string roundID, int seasonID, DateTime startDate, DateTime endDate)
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

                    // Các khung giờ thi đấu
                    List<TimeSpan> timeSlots = new List<TimeSpan>
                    {
                        new TimeSpan(2, 0 ,0),
                        new TimeSpan(18, 0, 0),
                        new TimeSpan(18, 30, 0),
                        new TimeSpan(19, 0, 0),
                        new TimeSpan(19, 30, 0),
                        new TimeSpan(20, 0, 0),
                        new TimeSpan(20, 30, 0),
                        new TimeSpan(21, 0, 0),
                        new TimeSpan(21, 30, 0),
                        new TimeSpan(22, 0, 0),
                        new TimeSpan(22, 30, 0),
                        new TimeSpan(23, 0, 0)
                    };

                    // Tính khoảng thời gian giữa các trận đấu
                    TimeSpan totalDuration = endDate - startDate;
                    TimeSpan interval = TimeSpan.FromTicks(totalDuration.Ticks / maxNumOfMatch);

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
                        string matchID = $"{roundID.Trim()}_{i.ToString().PadLeft(2, '0')}";

                        // Chọn ngẫu nhiên thời gian thi đấu
                        TimeSpan matchTimeSlot = timeSlots[random.Next(timeSlots.Count)];
                        DateTime matchDate = startDate.AddTicks(interval.Ticks * (i - 1));
                        DateTime matchTime = new DateTime(matchDate.Year, matchDate.Month, matchDate.Day, matchTimeSlot.Hours, matchTimeSlot.Minutes, matchTimeSlot.Seconds);

                        Match match = new Match
                        {
                            MatchID = matchID,
                            SeasonID = seasonID,
                            RoundID = roundID,
                            HomeID = homeClub.ClubID,
                            AwayID = awayClub.ClubID,
                            MatchName = matchName,
                            MatchTime = matchTime
                        };

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
                            join ssHomeClubID in db.SeasonClubs on m.HomeID equals ssHomeClubID.ClubID
                            join ssAwayClubID in db.SeasonClubs on m.AwayID equals ssAwayClubID.ClubID
                            join homeClub in db.Clubs on ssHomeClubID.ClubID equals homeClub.ClubID
                            join awayClub in db.Clubs on ssAwayClubID.ClubID equals awayClub.ClubID
                            where m.RoundID == roundID && m.SeasonID == seasonID && ssHomeClubID.SeasonID == seasonID && ssAwayClubID.SeasonID == seasonID
                            select new
                            {
                                matchID = m.MatchID,
                                roundID = m.RoundID,
                                seasonID = m.SeasonID,
                                homeID = m.HomeID,
                                awayID = m.AwayID,
                                matchName = m.MatchName,
                                matchTime = m.MatchTime,
                                homeClubName = homeClub.ClubName,
                                awayClubName = awayClub.ClubName,
                                homeClubLogo = homeClub.Logo,
                                awayClubLogo = awayClub.Logo,
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
                    match.MatchTime = item.matchTime;

                    match.SeasonClub = new SeasonClub 
                    { 
                        ClubID = item.homeID, 
                        Club = new Club 
                        { 
                            ClubID = item.homeID,
                            ClubName = item.homeClubName, 
                            Logo = item.homeClubLogo, 
                        } 
                    } ;

                    match.SeasonClub1 = new SeasonClub
                    {
                        ClubID = item.awayID,
                        Club = new Club
                        {
                            ClubID = item.awayID,
                            ClubName = item.awayClubName,
                            Logo = item.awayClubLogo,
                        }
                    };

                    matches.Add(match);
                }
            }
            return matches;
        }

        public Match GetLastestMatchInAMatchweek(string roundID)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                //var query = db.Matches.OrderBy(m => m.MatchTime && m.RoundID == roundID).FirstOrDefault();

                var query = from m in db.Matches
                            where m.RoundID == roundID
                            orderby m.MatchTime descending
                            select m;

                return query.FirstOrDefault();
            }
        }
    }
}
