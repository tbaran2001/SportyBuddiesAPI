using AutoMapper;

namespace SportyBuddiesAPI.Profiles;

public class MatchProfile:Profile
{
    public MatchProfile()
    {
        CreateMap<Entities.Match, Models.MatchDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.MatchedUserId, opt => opt.MapFrom(src => src.MatchedUser.Id));
        CreateMap<Models.UpdateMatchDto, Entities.Match>();
    }
}