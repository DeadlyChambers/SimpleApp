using AutoMapper;
using SimpleAPI.Common.Enums;
using SimpleAPI.Common.Extensions;
using SimpleAPI.DataAccess;

namespace SimpleAPI.DTO.Profiles
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            //DTOs to Entity
            CreateMap<GameDto, Game>()
                .ForMember(dest => dest.Gameid, opt => opt.MapFrom(src => src.Id));
            CreateMap<TeamDto, Team>().ForMember(dest => dest.Conference, opt => opt.MapFrom(src => src.Conference.GetId()))
                .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division.GetId()))
                .ForMember(dest => dest.Teamid, opt => opt.MapFrom(src => src.Id)); 
            CreateMap<PlayerDto, Player>().ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions.GetId()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.GetId()))
                //I don't feel this game status should live directly on the player. It seems to specific and doesn't offer snapshotting
                .ForMember(dest => dest.PlayingStatus, opt => opt.MapFrom(src => src.Status.GetId()))
                .ForMember(dest => dest.Playerid, opt => opt.MapFrom(src => src.Id));


            //Entity to DTOs
            CreateMap<Game, GameDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Gameid));
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Conference, opt => opt.MapFrom(src => src.Conference.MappedToEnum<TeamConferences>()))
                .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division.MappedToEnum<TeamDivisions>()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Teamid));
            CreateMap<Player, PlayerDto>()
                //Need to do something about the position array property, possibly implement some sort of multiple select list and project to an array
                .ForMember(dest => dest.Positions, opt => opt.MapFrom(src => src.Positions[0].MappedToEnum<PlayerPosition>())) 
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.MappedToEnum<PlayerStatus>()))
                //I don't feel this game status should live directly on the player. It seems to specific and doesn't offer snapshotting
                .ForMember(dest => dest.PlayingStatus, opt => opt.MapFrom(src => src.Status.MappedToEnum<PlayerGameStatus>()))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Playerid));
            
            CreateMap<StatDto, Stat>();
            CreateMap<Stat, StatDto>();
        }
    }
}
