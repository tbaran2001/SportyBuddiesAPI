using AutoMapper;
using SportyBuddies.Api.Contracts.Sports;
using SportyBuddies.Application.Features.Sports.Commands.CreateSport;

namespace SportyBuddies.Api.Mappings;

public class SportMappingProfile : Profile
{
    public SportMappingProfile()
    {
        CreateMap<CreateSportRequest, CreateSportCommand>();
    }
}