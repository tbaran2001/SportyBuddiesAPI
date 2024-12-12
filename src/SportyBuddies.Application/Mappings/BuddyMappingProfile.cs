using AutoMapper;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Domain.Buddies;

namespace SportyBuddies.Application.Mappings;

public class BuddyMappingProfile : Profile
{
    public BuddyMappingProfile()
    {
        CreateMap<Buddy, BuddyResponse>();
        CreateMap<Buddy, BuddyWithProfilesResponse>();
    }
}