using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SimpleAPI.DataAccess
{
    public partial class Stat
    {
        [Key, Column(Order = 0)]
        public int Gameid { get; set; }
        [Key, Column(Order = 1)]
        public int Teamid { get; set; }
        [Key, Column(Order = 2)]
        public int Playerid { get; set; }
        public short Passingattempts { get; set; }
        public short Passingcompletions { get; set; }
        public short Passingyards { get; set; }
        public short Interceptions { get; set; }
        public short Fumbles { get; set; }
        public short Receptions { get; set; }
        public short Targets { get; set; }
        public short Drops { get; set; }
        public short Receivingyards { get; set; }
        public short Receivingtds { get; set; }
        public short Rushingattempts { get; set; }
        public short Rushingyards { get; set; }
        public short Rushingtds { get; set; }
        public short Otheryards { get; set; }
        public short Othertds { get; set; }
        public short Twopointconversion { get; set; }
        public decimal Points { get; set; }
        public short? Rztarget { get; set; }
        public short? Rzrush { get; set; }
        public bool Started { get; set; }
        public short Snaps { get; set; }
        public short Passingtds { get; set; }

        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
