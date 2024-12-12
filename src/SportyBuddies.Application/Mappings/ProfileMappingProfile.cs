using AutoMapper;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Features.Profiles.Commands.CreateProfile;
using SportyBuddies.Application.Features.Profiles.Commands.UpdateProfile;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.Mappings;

public class ProfileMappingProfile : AutoMapper.Profile
{
    public ProfileMappingProfile()
    {
        CreateMap<CreateProfileCommand, Profile>();
        CreateMap<UpdateProfileCommand, Profile>();
        CreateMap<Profile, ProfileResponse>();
        CreateMap<Profile, ProfileWithSportsResponse>();
    }
}