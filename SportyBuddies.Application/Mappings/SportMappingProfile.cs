using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Domain.SportAggregate;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Application.Mappings;

public class SportMappingProfile : Profile
{
    public SportMappingProfile()
    {
        CreateMap<CreateSportCommand, Sport>();
        CreateMap<Sport, SportResponse>().ReverseMap();

        CreateMap<SportId, Guid>().ConvertUsing(src => src.Value);
    }
}