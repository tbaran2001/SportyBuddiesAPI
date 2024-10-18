using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.Mappings;

public class MatchMappingProfile : Profile
{
    public MatchMappingProfile()
    {
        CreateMap<Match, MatchResponse>()
            .ConstructUsing((src, ctx) => new MatchResponse(
                src.Id,
                ctx.Mapper.Map<UserWithSportsResponse>(src.User),
                ctx.Mapper.Map<UserWithSportsResponse>(src.MatchedUser),
                src.MatchDateTime,
                src.Swipe,
                src.SwipeDateTime
            ));
        CreateMap<UpdateMatchCommand, Match>();
    }
}