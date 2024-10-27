using AutoMapper;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Application.Common.DTOs;
using SportyBuddies.Application.Users.Commands.CreateUser;

namespace SportyBuddies.Api.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>();
    }
}