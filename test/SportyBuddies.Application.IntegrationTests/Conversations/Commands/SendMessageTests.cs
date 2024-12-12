using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Commands.SendMessage;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.IntegrationTests.Conversations.Commands;

public class SendMessageTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task SendMessage_ShouldSendMessage()
    {
        // Arrange
        var user1 = Profile.Create(CurrentUserId);
        var user2 = Profile.Create(Guid.NewGuid());
        await DbContext.Profiles.AddAsync(user1);
        await DbContext.Profiles.AddAsync(user2);

        var (buddy1, buddy2) = Buddy.CreatePair(user1.Id, user2.Id, DateTime.UtcNow);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);

        var conversation = Conversation.CreateOneToOne(user1.Id, user2.Id);
        await DbContext.Conversations.AddAsync(conversation);
        await DbContext.SaveChangesAsync();

        var command = new SendMessageCommand(conversation.Id, "Hello");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().NotBeNull();
        result.SenderId.Should().Be(user1.Id);
        result.Content.Should().Be("Hello");
    }

    [Fact]
    public async Task SendMessage_ShouldThrowNotFoundException_WhenDoesntExists()
    {
        // Arrange
        var user1 = Profile.Create(CurrentUserId);
        await DbContext.Profiles.AddAsync(user1);
        await DbContext.SaveChangesAsync();

        var command = new SendMessageCommand(Guid.NewGuid(), "Hello");

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}