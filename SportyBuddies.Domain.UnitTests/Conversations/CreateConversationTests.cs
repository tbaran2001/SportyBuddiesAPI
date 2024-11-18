using FluentAssertions;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.UnitTests.Conversations;

public class CreateConversationTests
{
    [Fact]
    public void CreateConversation_WithValidData_ShouldCreateConversation()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantIds = new List<Guid> { creatorId, Guid.NewGuid() };

        // Act
        var conversation = Conversation.Create(creatorId, participantIds);

        // Assert
        conversation.CreatorId.Should().Be(creatorId);
        conversation.Participants.Should().HaveCount(2);
        conversation.Participants.First().UserId.Should().Be(creatorId);
        conversation.Messages.Should().BeEmpty();
    }
}