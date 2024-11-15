using AutoMapper;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Features.Users.Commands.CreateUser;
using SportyBuddies.Application.Features.Users.Commands.UpdateUser;
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