using AutoMapper;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Users.Commands.CreateUser;
using SportyBuddies.Contracts.Users;

namespace SportyBuddies.Api.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
        CreateMap<UserDto, UserResponse>();
    }
}