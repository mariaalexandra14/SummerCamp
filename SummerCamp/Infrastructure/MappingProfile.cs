using AutoMapper;
using SummerCamp.DataModels.Models;
using SummerCamp.Models;

namespace SummerCamp.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Coach, CoachViewModel>().ReverseMap();
            CreateMap<Team, TeamViewModel>().ReverseMap();
            CreateMap<Country, CountryViewModel>().ReverseMap();
            CreateMap<Player, PlayerViewModel>().ReverseMap();
            CreateMap<Sponsor, SponsorViewModel>().ReverseMap();
            CreateMap<Competition, CompetitionViewModel>().ReverseMap();
        }
    }
}
