using FluentAssertions;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.UnitTests.Conversations;

public class CreateParticipantTests
{
    [Fact]
    public void CreateParticipant_WithValidData_ShouldCreateParticipant()
    {
        // Arrange
        var conversationId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var participant = Participant.Create(conversationId, userId);

        // Assert
        participant.ConversationId.Should().Be(conversationId);
        participant.UserId.Should().Be(userId);
    }
}