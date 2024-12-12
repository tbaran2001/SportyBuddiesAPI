using FluentAssertions;
using NSubstitute;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Services;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Domain.UnitTests.Services.Matches;

public class FindMatchesToAddTests
{
    private readonly IMatchesRepository _matchesRepository;
    private readonly IProfilesRepository _profilesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MatchService _matchService;

    public FindMatchesToAddTests()
    {
        _matchesRepository = Substitute.For<IMatchesRepository>();
        _profilesRepository = Substitute.For<IProfilesRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _matchService = new MatchService(_profilesRepository, _matchesRepository, _unitOfWork);
    }

    [Fact]
    public async Task FindMatchesToAddAsync_WhenUserDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        _profilesRepository.GetProfileByIdWithSportsAsync(profileId).Returns((Profile)null);

        // Act
        Func<Task> act = async () => await _matchService.FindMatchesToAddAsync(profileId);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Profile not found");
    }

    [Fact]
    public async Task FindMatchesToAddAsync_WhenValidRequest_ShouldAddMatches()
    {
        // Arrange
        var profileId = Guid.NewGuid();
        var user = Profile.Create(Guid.NewGuid());
        user.AddSport(Sport.Create("Football", "Soccer"));
        _profilesRepository.GetProfileByIdWithSportsAsync(profileId).Returns(user);
        _profilesRepository.GetPotentialMatchesAsync(profileId, Arg.Any<IEnumerable<Guid>>()).Returns(new List<Profile>());

        // Act
        await _matchService.FindMatchesToAddAsync(profileId);

        // Assert
        await _matchesRepository.Received().AddMatchesAsync(Arg.Any<List<Match>>());
        await _unitOfWork.Received().CommitChangesAsync();
    }
}