using System;
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
                    playerInMatch.Player = new Player { PlayerID = item.PlayerID, PlayerName = item.PlayerName, Image = item.Image, Number = item.Number };
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

        public List<PlayersInMatch> LoadAllPlayerInMatch(string matchID)
        {
            List<PlayersInMatch> players = new List<PlayersInMatch>();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from pim in db.PlayersInMatches
                            join p in db.Players on pim.PlayerID equals p.PlayerID
                            where pim.MatchID == matchID
                            select new
                            {
                                playerID = pim.PlayerID,
                                playerName = p.PlayerName,
                                image = p.Image,
                                number = p.Number,
                                position = p.Position
                            };
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        PlayersInMatch player = new PlayersInMatch();
                        player.PlayerID = player.PlayerID;
                        player.Player = new Player
                        {
                            PlayerID = item.playerID,
                            PlayerName = item.playerName,
                            Image = item.image,
                            Position = item.position,
                            Number = item.number
                        };

                        players.Add(player);
                    }
                    return players;
                }
            }
            return null;
        }

        public bool AddData(List<PlayersInMatch> playersInMatch)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    db.PlayersInMatches.InsertAllOnSubmit(playersInMatch);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public PlayersInMatch Get1PlayerInMatch(string matchID, int playerID)
        {
            PlayersInMatch playersInMatch = new PlayersInMatch();
            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from pim in db.PlayersInMatches
                            join p in db.Players on pim.PlayerID equals p.PlayerID
                            where pim.MatchID == matchID && pim.PlayerID == playerID
                            select new
                            {
                                MatchID = matchID,
                                PlayerID = pim.PlayerID,
                                PlayerName = p.PlayerName,
                                Position = pim.Position,
                                Goals = pim.Goal,
                                OwnGoals = pim.OwnGoal,
                                YellowCards = pim.YellowCard,
                                RedCards = pim.RedCard,
                                IsCaptain = pim.IsCaptain
                            };

                var item = query.FirstOrDefault();
                if (item != null)
                {
                    playersInMatch.Match = new Match
                    {
                        MatchID = item.MatchID
                    };

                    playersInMatch.Player = new Player
                    {
                        PlayerID = item.PlayerID,
                        PlayerName = item.PlayerName,
                    };
                    playersInMatch.Position = item.Position;
                    playersInMatch.Goal = item.Goals;
                    playersInMatch.OwnGoal = item.OwnGoals;
                    playersInMatch.YellowCard = item.YellowCards;
                    playersInMatch.RedCard = item.RedCards;
                    playersInMatch.IsCaptain = item.IsCaptain;

                    return playersInMatch;
                }
            }
            return null;
        }

        public bool EditData(PlayersInMatch playersInMatch)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.PlayersInMatches.Where(pim => pim.PlayerID == playersInMatch.Player.PlayerID && pim.MatchID == playersInMatch.MatchID).FirstOrDefault();

                    if (query != null)
                    {
                        query.Position = playersInMatch.Position;
                        query.Goal = playersInMatch.Goal;
                        query.OwnGoal = playersInMatch.OwnGoal;
                        query.YellowCard = playersInMatch.YellowCard;
                        query.RedCard = playersInMatch.RedCard;
                        query.IsCaptain = playersInMatch.IsCaptain;
                        //db.PlayersInMatches.DeleteOnSubmit(query);

                        db.SubmitChanges();

                        return true;
                    }



                    //db.PlayersInMatches.InsertOnSubmit(playersInMatch);

                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeleteData(int playerID, string matchID)
        {
            try
            {
                using (DBProjetDataContext db = new DBProjetDataContext())
                {
                    var query = db.PlayersInMatches.Where(pim => pim.PlayerID == playerID && pim.MatchID == matchID).FirstOrDefault();
                    if (query != null)
                    {
                        db.PlayersInMatches.DeleteOnSubmit(query);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public List<PlayerInMatchDTO> LoadPlayerInMatchDTO(string matchID, int isHome)
        {
            List<PlayerInMatchDTO> playerMatchDTOs = new List<PlayerInMatchDTO>();

            using (DBProjetDataContext db = new DBProjetDataContext())
            {
                var query = from pim in db.PlayersInMatches
                            where pim.MatchID == matchID && pim.IsHomeTeam == isHome
                            join p in db.Players on pim.PlayerID equals p.PlayerID
                            select new PlayerInMatchDTO
                            {
                                MatchID = pim.MatchID,
                                PlayerID = pim.PlayerID,
                                PlayerName = p.PlayerName,
                                Image = p.Image,
                                Number = (int)p.Number,
                                IsHomeTeam = pim.IsHomeTeam,
                                Position = pim.Position,
                                Goal = (int)pim.Goal,
                                YellowCard = (int)pim.YellowCard,
                                RedCard = (int)pim.RedCard,
                                OwnGoal = (int)pim.OwnGoal,
                                IsCaptain = pim.IsCaptain
                            };

                playerMatchDTOs = query.ToList();
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
            var sortedPlayerMatchDTOs = playerMatchDTOs.OrderBy(p => positionOrder.ContainsKey(p.Position) ? positionOrder[p.Position] : int.MaxValue).ToList();

            // Return the sorted list
            return sortedPlayerMatchDTOs;
        }
    }
}
