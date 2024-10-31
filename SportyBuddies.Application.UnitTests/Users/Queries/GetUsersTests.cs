using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Users.Queries.GetUsers;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Queries;

public class GetUsersTests
{
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
    public async Task Handle_ShouldReturnUserResponse_WhenIncludeSportsIsFalse()
    {
        // Arrange
        GetUsersQuery query= new GetUsersQuery(false);
        var users = new List<User> { User.Create(Guid.NewGuid()), User.Create(Guid.NewGuid()) };
        _usersRepositoryMock.GetAllUsersAsync(query.IncludeSports).Returns(users);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<List<UserResponse>>();
    }
    
    [Fact]
    public async Task Handle_ShouldReturnUserWithSportsResponse_WhenIncludeSportsIsTrue()
    {
        // Arrange
        GetUsersQuery query= new GetUsersQuery(true);
        var users = new List<User> { User.Create(Guid.NewGuid()), User.Create(Guid.NewGuid()) };
        _usersRepositoryMock.GetAllUsersAsync(query.IncludeSports).Returns(users);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<List<UserWithSportsResponse>>();
    }
}