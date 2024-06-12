using AutoMapper;

namespace SportyBuddiesAPI.Profiles;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<Entities.User, Models.UserWithoutSportsDto>();

    }
}