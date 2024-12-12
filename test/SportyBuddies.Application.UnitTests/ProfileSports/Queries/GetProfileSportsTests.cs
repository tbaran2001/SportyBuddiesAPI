using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.ProfileSports.Queries.GetProfileSports;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Sports;
using Profile = SportyBuddies.Domain.Profiles.Profile;

namespace SportyBuddies.Application.UnitTests.ProfileSports.Queries;

public class GetProfileSportsTests
{
    private readonly GetProfileSportsQuery _query = new();
    private readonly GetProfileSportsQueryHandler _handler;
    private readonly IProfilesRepository _profilesRepositoryMock;
    private readonly IUserContext _userContextMock;

    public GetProfileSportsTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<SportMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _profilesRepositoryMock = Substitute.For<IProfilesRepository>();
        _userContextMock = Substitute.For<IUserContext>();

        _handler = new GetProfileSportsQueryHandler(_profilesRepositoryMock, mapper, _userContextMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnListOfSports()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var sport1 = Sport.Create("Football", "Description");
        var sport2 = Sport.Create("Basketball", "Description");
        user.AddSport(sport1);
        user.AddSport(sport2);

        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(currentUser.Id).Returns(user);

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
        var user = Profile.Create(Guid.NewGuid());

        var currentUser = new CurrentUser(user.Id, "", []);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(currentUser.Id).Returns(user);

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

        _profilesRepositoryMock.GetProfileByIdWithSportsAsync(currentUser.Id).Returns((Profile?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}