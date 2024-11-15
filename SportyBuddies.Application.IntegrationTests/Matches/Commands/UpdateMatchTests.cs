using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Matches.Commands;

public class UpdateMatchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Theory]
    [InlineData(Swipe.Left)]
    [InlineData(Swipe.Right)]
    public async Task UpdateMatch_ShouldUpdateMatch_WhenValidRequest(Swipe swipe)
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.Users.AddAsync(matchedUser);

        var match = Match.Create(user, matchedUser, DateTime.UtcNow);
        await DbContext.Matches.AddAsync(match);

        await DbContext.SaveChangesAsync();

        var command = new UpdateMatchCommand(match.Id, swipe);

        // Act
        await Sender.Send(command);

        // Assert
        match.Swipe.Should().Be(swipe);
        match.SwipeDateTime.Should().NotBeNull();
    }
    
    [Fact]
    public async Task UpdateMatch_ShouldThrowNotFoundException_WhenMatchDoesntExist()
    {
        // Arrange
        var command = new UpdateMatchCommand(Guid.NewGuid(), Swipe.Left);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}