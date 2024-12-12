using AutoMapper;
using SportyBuddies.Api.Contracts.Profiles;
using SportyBuddies.Application.Features.Profiles.Commands.CreateProfile;

namespace SportyBuddies.Api.Mappings;

public class ProfileMappingProfile : AutoMapper.Profile
{
    public ProfileMappingProfile()
    {
        CreateMap<CreateProfileRequest, CreateProfileCommand>();
    }
}