using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.Services;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Matches.Commands;

public class UpdateMatchTests
{
    private readonly UpdateMatchCommand _command= new(Guid.NewGuid(), Swipe.Right);
    private readonly UpdateMatchCommandHandler _handler;
    private readonly IMatchesRepository _matchesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IMatchService _matchingServiceMock;

    public UpdateMatchTests()
    {
        _matchesRepositoryMock = Substitute.For<IMatchesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _matchingServiceMock = Substitute.For<IMatchService>();
        _handler = new UpdateMatchCommandHandler(_matchesRepositoryMock, _unitOfWorkMock, _matchingServiceMock);
    }

    [Fact]
    public async Task Handle_ShouldUpdateMatchSwipe()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var match = Match.Create(user, matchedUser,DateTime.UtcNow);
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns(match);

        // Act
        await _handler.Handle(_command, CancellationToken.None);
        
        // Assert
        match.Swipe.Should().Be(_command.Swipe);
        
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }
    
    [Fact]
    public async Task Handle_ShouldCreateBuddyRelationship_WhenSwipeIsRight()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var match = Match.Create(user, matchedUser,DateTime.UtcNow);
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns(match);

        // Act
        await _handler.Handle(_command, CancellationToken.None);
        
        // Assert
        await _matchingServiceMock.Received(1).CreateBuddyRelationshipAsync(match.Id);
    }
    
    [Fact]
    public async Task Handle_ShouldThrowBadRequestException_WhenUsersAreAlreadyBuddies()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        var match = Match.Create(user, matchedUser,DateTime.UtcNow);
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns(match);
        _matchingServiceMock.AreUsersBuddiesAsync(user.Id, matchedUser.Id).Returns(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Users are already buddies");
    }
    
    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenMatchDoesNotExist()
    {
        // Arrange
        _matchesRepositoryMock.GetMatchByIdAsync(_command.MatchId).Returns((Match?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}