using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class PlayersInMatchDAL
    {
        public List<PlayersInMatch> LoadPlayerInMatch(string matchID, int isHome)
        {
            List<PlayersInMatch> playersInMatch = new List<PlayersInMatch>();

            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from pim in db.PlayersInMatches
                            where pim.MatchID == matchID && pim.IsHomeTeam == isHome
                            join p in db.Players on pim.PlayerID equals p.PlayerID
                            select new
                            {
                                PlayerID = pim.PlayerID,
                                PlayerName = p.PlayerName,
                                Image = p.Image,
                                Number = p.Number,
                                IsHomeTeam = pim.IsHomeTeam,
                                Position = pim.Position,
                                Goal = pim.Goal,
                                YellowCard = pim.YellowCard,
                                RedCard = pim.RedCard,
                                OwnGoal = pim.OwnGoal,
                                IsCaptain = pim.IsCaptain
                            };

                foreach (var item in query)
                {
                    PlayersInMatch playerInMatch = new PlayersInMatch();
                    playerInMatch.PlayerID = item.PlayerID;
                    playerInMatch.Player = new Player { PlayerName = item.PlayerName, Image = item.Image, Number = item.Number };
                    playerInMatch.IsHomeTeam = item.IsHomeTeam;
                    playerInMatch.Position = item.Position;
                    playerInMatch.Goal = item.Goal;
                    playerInMatch.YellowCard = item.YellowCard;
                    playerInMatch.RedCard = item.RedCard;
                    playerInMatch.OwnGoal = item.OwnGoal;
                    playerInMatch.IsCaptain = item.IsCaptain;

                    playersInMatch.Add(playerInMatch);
                }
            }

            var positionOrder = new Dictionary<string, int>
            {
                { "Goalkeeper", 1 },
                { "Defender", 2 },
                { "Midfielder", 3 },
                { "Forward", 4 },
                { "Substitute", 5 } // Assuming 'Substitute' is a valid position
            };

            // Sort the list based on the defined order
            var sortedPlayersInMatch = playersInMatch.OrderBy(p => positionOrder.ContainsKey(p.Position) ? positionOrder[p.Position] : int.MaxValue).ToList();

            // Return or use the sorted list
            return sortedPlayersInMatch;
        }
    }
}
