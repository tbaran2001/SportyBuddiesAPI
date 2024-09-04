using AutoMapper;
using SportyBuddies.Application.Users.Commands.CreateUser;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Mappings;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
    }
}