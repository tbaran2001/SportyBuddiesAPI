using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;
using SportyBuddies.Contracts.Matches;

namespace SportyBuddies.Api.Mappings;

public class MatchMappingProfile : Profile
{
    public MatchMappingProfile()
    {
        CreateMap<MatchDto, MatchResponse>();
        CreateMap<UpdateMatchRequest, UpdateMatchCommand>();
        CreateMap<UpdateMatchCommand, MatchDto>();
    }
}