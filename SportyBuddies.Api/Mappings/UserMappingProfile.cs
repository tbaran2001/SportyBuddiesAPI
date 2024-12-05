using AutoMapper;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Application.Features.Users.Commands.CreateUser;

namespace SportyBuddies.Api.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}