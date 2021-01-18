using SimpleAPI.DTO.Interfaces;

namespace SimpleAPI.DTO
{
    public record StatDto : IMultiEntityRecord
    {
        public int Gameid { get; set; }
        public int Teamid { get; set; }
        public int Playerid { get; set; }
        public short Passingattempts { get; set; }
        public short Passingcompletions { get; set; }
        public short Passingyards { get; set; }
        public short Passingtds { get; set; }
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
        public bool IsDirty(object obj)
        {
            var original = (obj as StatDto);
            var updatedVersion = this with { Game = original.Game, Player = original.Player, Team = original.Team };
            return !(updatedVersion.Equals(this));
        }

        public GameDto Game { get; set; }
        public PlayerDto Player { get; set; }
        public TeamDto Team { get; set; }
    }
}
