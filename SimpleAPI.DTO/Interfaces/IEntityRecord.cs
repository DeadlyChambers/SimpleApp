using System;

namespace SimpleAPI.DTO.Interfaces
{
    public interface ICheckDirty
    {
        public bool IsDirty(object obj);
    }

    public interface IMultiEntityRecord : ICheckDirty
    {
        public int Gameid { get; set; }
        public int Teamid { get; set; }
        public int Playerid { get; set; }

        /// <summary>
        /// Game|Team|Plaery
        /// </summary>
        public string CompositeId { get { return $"{Gameid}|{Teamid}|{Playerid}"; } }
    }

    public interface IEntityRecord : ICheckDirty
    {
        public int Id { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime? Modifieddate { get; set; }
    }
}
