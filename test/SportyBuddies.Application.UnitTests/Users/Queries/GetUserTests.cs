using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Queries.GetUser;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Users.Queries;

public class GetUserTests
{
    private readonly GetUserQuery _query= new(Guid.NewGuid());
    private readonly GetUserQueryHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;

    public GetUserTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<UserMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();
    
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _handler = new GetUserQueryHandler(_usersRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = User.Create(_query.UserId);
        _usersRepositoryMock.GetUserByIdWithSportsAsync(_query.UserId).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeOfType<UserWithSportsResponse>();
        result.Id.Should().Be(user.Id);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        _usersRepositoryMock.GetUserByIdWithSportsAsync(_query.UserId).Returns((User?)null);

        // Act
        var act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}