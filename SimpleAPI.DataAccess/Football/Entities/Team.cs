using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleAPI.DataAccess
{
    public partial class Team
    {
        public Team()
        {
            GameAwayteams = new HashSet<Game>();
            GameHometeams = new HashSet<Game>();
            Stats = new HashSet<Stat>();
        }

        public int Teamid { get; set; }
        public string Name { get; set; }
        public string Conference { get; set; }
        public string Division { get; set; }
        public string Location { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime? Modifieddate { get; set; }

        public virtual ICollection<Game> GameAwayteams { get; set; }
        public virtual ICollection<Game> GameHometeams { get; set; }
        public virtual ICollection<Stat> Stats { get; set; }
    }
}
