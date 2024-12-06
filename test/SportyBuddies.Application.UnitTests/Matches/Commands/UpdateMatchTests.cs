using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Matches;

namespace SportyBuddies.Application.UnitTests.Matches.Commands;

public class UpdateMatchTests
{
    private readonly UpdateMatchCommand _command = new(Guid.NewGuid(), Swipe.Right);
    private readonly UpdateMatchCommandHandler _handler;
    private readonly IMatchesRepository _matchesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IMatchService _matchServiceMock;
    private readonly IBuddyService _buddyServiceMock;

    public UpdateMatchTests()
    {
        _matchesRepositoryMock = Substitute.For<IMatchesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _matchServiceMock = Substitute.For<IMatchService>();
        _buddyServiceMock = Substitute.For<IBuddyService>();

        _handler = new UpdateMatchCommandHandler(_matchesRepositoryMock, _unitOfWorkMock,
            _buddyServiceMock);
    }

    [Fact]
    public async Task Handle_WhenMatchDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns((Match)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenOppositeMatchDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        var (match1, match2) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns(match1);
        _matchesRepositoryMock.GetMatchByIdAsync(match1.OppositeMatchId).Returns((Match)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_WhenMatchExists_ShouldUpdateMatch()
    {
        // Arrange
        var (match1, match2) = Match.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns(match1);
        _matchesRepositoryMock.GetMatchByIdAsync(match1.OppositeMatchId).Returns(match2);

        // Act
        await _handler.Handle(_command, default);

        // Assert
        match1.Swipe.Should().Be(_command.Swipe);
    }
}