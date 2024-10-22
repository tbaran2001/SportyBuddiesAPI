using AutoMapper;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Users.Commands.CreateUser;
using SportyBuddies.Application.Users.Commands.UpdateUser;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<User, UserResponse>();
        CreateMap<User, UserWithSportsResponse>();
    }
}