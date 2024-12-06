using FluentAssertions;
using NSubstitute;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Services;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Services.Matches;

public class FindMatchesToAddTests
{
    private readonly IMatchesRepository _matchesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly MatchService _matchService;

    public FindMatchesToAddTests()
    {
        _matchesRepository = Substitute.For<IMatchesRepository>();
        _usersRepository = Substitute.For<IUsersRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _matchService = new MatchService(_usersRepository, _matchesRepository, _unitOfWork);
    }

    [Fact]
    public async Task FindMatchesToAddAsync_WhenUserDoesNotExist_ShouldThrowException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _usersRepository.GetUserByIdWithSportsAsync(userId).Returns((User)null);

        // Act
        Func<Task> act = async () => await _matchService.FindMatchesToAddAsync(userId);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User not found");
    }

    [Fact]
    public async Task FindMatchesToAddAsync_WhenValidRequest_ShouldAddMatches()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = User.Create(Guid.NewGuid());
        user.AddSport(Sport.Create("Football", "Soccer"));
        _usersRepository.GetUserByIdWithSportsAsync(userId).Returns(user);
        _usersRepository.GetPotentialMatchesAsync(userId, Arg.Any<IEnumerable<Guid>>()).Returns(new List<User>());

        // Act
        await _matchService.FindMatchesToAddAsync(userId);

        // Assert
        await _matchesRepository.Received().AddMatchesAsync(Arg.Any<List<Match>>());
        await _unitOfWork.Received().CommitChangesAsync();
    }
}