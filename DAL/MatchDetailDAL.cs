using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;

namespace DAL
{
    public class MatchDetailDAL
    {
        public MatchDetail LoadDataByMatchID(string matchID)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from md in db.MatchDetails
                            join pim in db.PlayersInMatches on md.MotmID equals pim.PlayerID
                            join p in db.Players on pim.PlayerID equals p.PlayerID
                            join r in db.Referees on md.RefereeID equals r.RefereeID
                            join m in db.Matches on md.MatchID equals m.MatchID
                            where md.MatchID == matchID
                            select new
                            {
                                MatchID = md.MatchID,
                                MotmID = md.MotmID,
                                MotmName = p.PlayerName,
                                MotmImg = p.Image,
                                RefereeID = md.RefereeID,
                                RefereeName = r.RefereeName,
                                HomeGoals = md.HomeGoals,
                                AwayGoals = md.AwayGoals,
                                HomeTactical = md.HomeTactical,
                                AwayTactical = md.AwayTactical,
                                MatchTime = m.MatchTime
                            };


                var item = query.FirstOrDefault();

                if (item != null)
                {
                    MatchDetail matchDetail = new MatchDetail();
                    matchDetail.MatchID = item.MatchID;
                    matchDetail.MotmID = item.MotmID;

                    matchDetail.PlayersInMatch = new PlayersInMatch
                    {
                        PlayerID = item.MotmID,
                        Player = new Player
                        {
                            PlayerName = item.MotmName,
                            Image = item.MotmImg
                        }
                    };

                    matchDetail.RefereeID = item.RefereeID;
                    matchDetail.Referee = new Referee { RefereeName = item.RefereeName };
                    matchDetail.HomeGoals = item.HomeGoals;
                    matchDetail.AwayGoals = item.AwayGoals;
                    matchDetail.HomeTactical = item.HomeTactical;
                    matchDetail.AwayTactical = item.AwayTactical;
                    matchDetail.Match = new Match { MatchTime = item.MatchTime };
                    return matchDetail;
                }
            }
            return null;
        }

        public MatchDetail GetMatchDetail(string matchID)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from md in db.MatchDetails
                            join r in db.Referees on md.RefereeID equals r.RefereeID
                            join m in db.Matches on md.MatchID equals m.MatchID
                            where md.MatchID == matchID
                            select new
                            {
                                matchID = md.MatchID,
                                motmID = md.MotmID,
                                matchTime = m.MatchTime,
                                refereeID = md.RefereeID,
                                refereeName = r.RefereeName,
                                homeGoals = md.HomeGoals,
                                awayGoals = md.AwayGoals,
                                homeTactic = md.HomeTactical,
                                awayTactic = md.AwayTactical
                            };

                var item = query.FirstOrDefault();
                if (item != null)
                {
                    MatchDetail matchDetail = new MatchDetail();
                    matchDetail.MatchID = item.matchID;
                    matchDetail.MotmID = item.motmID;
                    matchDetail.Match = new Match {MatchID = item.matchID, MatchTime = item.matchTime };
                    matchDetail.RefereeID = item.refereeID;
                    matchDetail.Referee = new Referee { RefereeID = item.refereeID, RefereeName = item.refereeName };
                    matchDetail.HomeGoals = item.homeGoals;
                    matchDetail.AwayGoals = item.awayGoals;
                    matchDetail.HomeTactical = item.homeTactic;
                    matchDetail.AwayTactical = item.awayTactic;

                    return matchDetail;
                }
            }
            return null;
        }

        public bool EditData(MatchDetail matchDetail, DateTime matchTime)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    string matchID = matchDetail.MatchID;
                    //EditMatchTimeOfMatch(matchID, matchTime);
                    var query = db.MatchDetails.Where(md => md.MatchID == matchID).FirstOrDefault();
                    if (query != null)
                    {
                        query.MatchID = matchDetail.MatchID;
                        query.MotmID = matchDetail.MotmID;
                        query.HomeGoals = matchDetail.HomeGoals;
                        query.AwayGoals = matchDetail.AwayGoals;
                        query.HomeTactical = matchDetail.HomeTactical;
                        query.AwayTactical = matchDetail.AwayTactical;
                        query.RefereeID = matchDetail.RefereeID;
                        query.Match.MatchTime = matchDetail.Match.MatchTime;

                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void EditMatchTimeOfMatch(string matchID, DateTime matchTime)
        {
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = db.Matches.Where(m => m.MatchID == matchID).FirstOrDefault();

            }

        }
    }
}
