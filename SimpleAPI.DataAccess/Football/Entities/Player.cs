using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleAPI.DataAccess
{
    public partial class Player
    {
        public Player()
        {
            Stats = new HashSet<Stat>();
        }

        public int Playerid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public short Height { get; set; }
        public short Weight { get; set; }
        public string College { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime? Modifieddate { get; set; }
        public string[] Positions { get; set; }
        public string Status { get; set; }
        public short? DraftYear { get; set; }
        public DateTime Dob { get; set; }
        public string PlayingStatus { get; set; }

        public virtual ICollection<Stat> Stats { get; set; }
    }
}
