using AutoMapper;
using SportyBuddies.Api.Contracts.Matches;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;

namespace SportyBuddies.Api.Mappings;

public class MatchMappingProfile : Profile
{
    public MatchMappingProfile()
    {
        CreateMap<UpdateMatchRequest, UpdateMatchCommand>();
    }
}