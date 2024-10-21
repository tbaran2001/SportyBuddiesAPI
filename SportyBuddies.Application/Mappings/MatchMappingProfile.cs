using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Mappings;

public class MatchMappingProfile : Profile
{
    public MatchMappingProfile()
    {
        CreateMap<Match, MatchResponse>();
        CreateMap<Match, MatchWithUsersResponse>();
        CreateMap<Match, RandomMatchResponse>();
        CreateMap<UpdateMatchCommand, Match>();
    }
}