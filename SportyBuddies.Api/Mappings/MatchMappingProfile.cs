using AutoMapper;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;
using SportyBuddies.Contracts.Matches;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Api.Mappings;

public class MatchMappingProfile : Profile
{
    public MatchMappingProfile()
    {
        CreateMap<Match, MatchResponse>();
        CreateMap<UpdateMatchRequest, UpdateMatchCommand>();
        CreateMap<UpdateMatchCommand, Match>();
    }
}