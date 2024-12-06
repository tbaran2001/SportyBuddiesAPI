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
        var participantId = Guid.NewGuid();

        // Act
        var conversation = Conversation.CreateOneToOne(creatorId, participantId);

        // Assert
        conversation.CreatorId.Should().Be(creatorId);
        conversation.Participants.Should().HaveCount(2);
        conversation.Participants.Should().ContainSingle(p => p.UserId == creatorId);
        conversation.Participants.Should().ContainSingle(p => p.UserId == participantId);
    }
}