using AutoMapper;
using SportyBuddies.Contracts.Matches;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Api.Mappings;

public class MatchMappingProfile : Profile
{
    public MatchMappingProfile()
    {
        CreateMap<Match, MatchResponse>();
    }
}