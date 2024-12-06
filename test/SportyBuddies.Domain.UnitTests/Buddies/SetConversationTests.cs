using FluentAssertions;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.UnitTests.Buddies;

public class SetConversationTests
{
    [Fact]
    public void SetConversation_WithValidData_ShouldSetConversation()
    {
        // Arrange
        var (buddy1, buddy2) = Buddy.CreatePair(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
        var conversation = Conversation.CreateOneToOne(Guid.NewGuid(), Guid.NewGuid());

        // Act
        buddy1.SetConversation(conversation);

        // Assert
        buddy1.ConversationId.Should().Be(conversation.Id);
        buddy1.Conversation.Should().Be(conversation);
    }
}