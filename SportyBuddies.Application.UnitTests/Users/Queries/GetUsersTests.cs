using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Features.Users.Queries.GetUsers;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Queries;

public class GetUsersTests
{
    private readonly GetUsersQuery _query= new();
    private readonly GetUsersQueryHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;

    public GetUsersTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<UserMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();
    
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _handler = new GetUsersQueryHandler(_usersRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnUsers()
    {
        // Arrange
        var users = new List<User>
        {
            User.Create(Guid.NewGuid()),
            User.Create(Guid.NewGuid())
        };
        _usersRepositoryMock.GetAllUsersAsync().Returns(users);
        
        // Act
        var result = await _handler.Handle(_query, default);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(users.Count);
    }

}