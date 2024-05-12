using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MatchDTO
    {
        public string MatchID { get; set; }
        public int? SeasonID { get; set; }
        public string RoundID { get; set; }
        public int HomeID { get; set; }
        public int AwayID { get; set; }
        public string MatchName { get; set; }
        public string HomeClubName { get; set; }
        public string AwayClubName { get; set; }
        public string HomeClubLogo { get; set; }
        public string AwayClubLogo { get; set; }
    }
}
