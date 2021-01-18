using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleAPI.DataAccess
{
    public partial class Game
    {
        public Game()
        {
            Stats = new HashSet<Stat>();
        }

        public int Gameid { get; set; }
        public int Hometeamid { get; set; }
        public int Awayteamid { get; set; }
        public DateTime Gamedate { get; set; }
        public short Homescore { get; set; }
        public short Awayscore { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime? Modifieddate { get; set; }

        public virtual Team Awayteam { get; set; }
        public virtual Team Hometeam { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
    }
}
