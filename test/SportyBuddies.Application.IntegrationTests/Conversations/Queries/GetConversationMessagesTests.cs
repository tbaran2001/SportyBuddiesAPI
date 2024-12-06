using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Queries.GetConversationMessages;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Conversations.Queries;

public class GetConversationMessagesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetConversationMessages_ShouldReturnMessages()
    {
        // Arrange
        var user1 = User.Create(CurrentUserId);
        var user2 = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user1);
        await DbContext.Users.AddAsync(user2);

        var (buddy1, buddy2) = Buddy.CreatePair(user1.Id, user2.Id, DateTime.UtcNow);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);

        var conversation = Conversation.CreateOneToOne(user1.Id, user2.Id);
        await DbContext.Conversations.AddAsync(conversation);
        await DbContext.SaveChangesAsync();

        var message1 = Message.Create(conversation.Id, user1.Id, "Hello");
        var message2 = Message.Create(conversation.Id, user2.Id, "Hi");
        await DbContext.Messages.AddAsync(message1);
        await DbContext.Messages.AddAsync(message2);
        await DbContext.SaveChangesAsync();

        var query = new GetConversationMessagesQuery(conversation.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(2);
        result.Should().Contain(x => x.Content == "Hello");
        result.Should().Contain(x => x.Content == "Hi");
    }

    [Fact]
    public async Task GetConversationMessages_ShouldThrowNotFoundException_WhenConversationDoesntExists()
    {
        // Arrange
        var query = new GetConversationMessagesQuery(CurrentUserId);

        // Act
        Func<Task> act = async () => await Sender.Send(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}