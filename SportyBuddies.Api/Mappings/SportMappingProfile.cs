using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Contracts.Sports;

namespace SportyBuddies.Api.Mappings;

public class SportMappingProfile : Profile
{
    public SportMappingProfile()
    {
        CreateMap<CreateSportRequest, CreateSportCommand>();
        CreateMap<SportDto, SportResponse>();
    }
}