using AutoMapper;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Contracts.Sports;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Api.Mappings;

public class SportMappingProfile : Profile
{
    public SportMappingProfile()
    {
        CreateMap<CreateSportRequest, CreateSportCommand>();
        CreateMap<Sport, SportResponse>();
    }
}