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

        var (match1, match2) = Match.CreatePair(user.Id, matchedUser.Id, DateTime.UtcNow);
        await DbContext.Matches.AddAsync(match1);
        await DbContext.Matches.AddAsync(match2);

        await DbContext.SaveChangesAsync();

        var command = new UpdateMatchCommand(match1.Id, swipe);

        // Act
        await Sender.Send(command);

        // Assert
        match1.Swipe.Should().Be(swipe);
        match1.SwipeDateTime.Should().NotBeNull();
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