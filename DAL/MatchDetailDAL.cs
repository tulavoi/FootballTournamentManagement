using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;

namespace DAL
{
    public class MatchDetailDAL
    {
        public MatchDetail LoadDataByMatchID(string matchID)
        {
            MatchDetail matchDetail = new MatchDetail();
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
                }
            }
            return matchDetail;
        }
    }
}
