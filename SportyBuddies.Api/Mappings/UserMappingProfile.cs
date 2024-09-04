using AutoMapper;
using SportyBuddies.Application.Users.Commands.CreateUser;
using SportyBuddies.Contracts.Users;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Api.Mappings;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<User, UserResponse>();
    }
}