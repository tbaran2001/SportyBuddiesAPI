using AutoMapper;

namespace SportyBuddiesAPI.Profiles;

public class SportProfile : Profile
{
    public SportProfile()
    {
        CreateMap<Entities.Sport, Models.SportDto>();
        CreateMap<Models.CreateSportDto, Entities.Sport>();
        CreateMap<Models.UpdateSportDto, Entities.Sport>();
        CreateMap<Entities.Sport, Models.UpdateSportDto>();
    }
}