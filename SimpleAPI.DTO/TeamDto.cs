using SimpleAPI.Common.Enums;
using SimpleAPI.DTO.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.DTO
{
    public record TeamDto  : IEntityRecord, ISelectAsItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EnumDataType(typeof(TeamConferences))]
        public TeamConferences Conference { get; set; }
        [EnumDataType(typeof(TeamDivisions))]
        public TeamDivisions Division { get; set; }
        public string Location { get; set; }
        public IList<GameDto> GameAwayteams { get; set; }
        public IList<GameDto> GameHometeams { get; set; }
        public IList<StatDto> Stats { get; set; }
        public DateTime Createddate { get; set; }
        public DateTime? Modifieddate { get; set; }

        public KeyValuePair<string, string> AsKeyValuePair()
        {
            return new KeyValuePair<string, string>(Id.ToString(), this.ToString());
        }

        public bool IsDirty(object obj)
        {
            var original = (obj as TeamDto);
            return !this.Id.Equals(original.Id) ||
                !this.Name.Equals(original.Name) ||
                !this.Conference.Equals(original.Conference) ||
                !this.Division.Equals(original.Division) ||
                !this.Location.Equals(original.Location);
        }

        /// <summary>
        /// Location Name
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Location} {Name}";
        }
    }
}
