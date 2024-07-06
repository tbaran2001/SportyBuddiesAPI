using AutoMapper;

namespace SportyBuddiesAPI.Profiles;

public class SportProfile : Profile
{
    public SportProfile()
    {
        CreateMap<Entities.Sport, Models.SportDto>();
        CreateMap<Models.SportForCreationDto, Entities.Sport>();
        CreateMap<Models.SportForUpdateDto, Entities.Sport>();
        CreateMap<Entities.Sport, Models.SportForUpdateDto>();
    }
}