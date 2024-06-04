using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PlayerInMatchDTO
    {
        public string MatchID { get; set; }
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string Image { get; set; }
        public int Number { get; set; }
        public int IsHomeTeam { get; set; }
        public string Position { get; set; }
        public int Goal { get; set; }
        public int YellowCard { get; set; }
        public int RedCard { get; set; }
        public int OwnGoal { get; set; }
        public int IsCaptain { get; set; }
    }
}
