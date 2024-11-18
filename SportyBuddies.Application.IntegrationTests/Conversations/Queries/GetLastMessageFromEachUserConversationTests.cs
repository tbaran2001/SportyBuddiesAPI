using FluentAssertions;
using SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachUserConversation;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Conversations.Queries;

public class GetLastMessageFromEachUserConversationTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetLastMessageFromEachUserConversation_ShouldReturnLastMessageFromEachUserConversation()
    {
        // Arrange
        var user1 = User.Create(Guid.NewGuid());
        var user2 = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user1);
        await DbContext.Users.AddAsync(user2);

        var buddy1 = Buddy.Create(user1, user2,DateTime.UtcNow);
        var buddy2 = Buddy.Create(user2, user1,DateTime.UtcNow);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);

        var conversation = Conversation.Create(user1.Id, new List<Guid> { user1.Id, user2.Id });
        await DbContext.Conversations.AddAsync(conversation);
        await DbContext.SaveChangesAsync();

        var message1 = Message.Create(conversation.Id, user1.Id, "Hello");
        var message2 = Message.Create(conversation.Id, user2.Id, "Hi");
        await DbContext.Messages.AddAsync(message1);
        await DbContext.Messages.AddAsync(message2);
        await DbContext.SaveChangesAsync();

        var query = new GetLastMessageFromEachUserConversationQuery(user1.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(1);
        result.Should().Contain(x => x.Content == "Hi");
    }
}