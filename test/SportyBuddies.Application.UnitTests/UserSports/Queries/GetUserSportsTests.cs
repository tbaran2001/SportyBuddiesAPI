using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.UserSports.Queries.GetUserSports;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.UserSports.Queries;

public class GetUserSportsTests
{
    private readonly GetUserSportsQuery _query = new();
    private readonly GetUserSportsQueryHandler _handler;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IUserContext _userContextMock;

    public GetUserSportsTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new GetUserSportsQueryHandler(_usersRepositoryMock, mapper, _userContextMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnListOfSports()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var sport1 = Sport.Create("Football", "Description");
        var sport2 = Sport.Create("Basketball", "Description");
        user.AddSport(sport1);
        user.AddSport(sport2);

        _usersRepositoryMock.GetUserByIdWithSportsAsync(currentUser.Id).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().HaveCount(2);
        result[0].Name.Should().Be("Football");
        result[1].Name.Should().Be("Basketball");
        result.Should().BeOfType<List<SportResponse>>();
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_WhenNoSports()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdWithSportsAsync(currentUser.Id).Returns(user);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeEmpty();
        result.Should().BeOfType<List<SportResponse>>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenUserNotFound()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _usersRepositoryMock.GetUserByIdWithSportsAsync(currentUser.Id).Returns((User?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}