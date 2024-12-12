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
        var profileId = Guid.NewGuid();

        // Act
        var participant = Participant.Create(conversationId, profileId);

        // Assert
        participant.ConversationId.Should().Be(conversationId);
        participant.ProfileId.Should().Be(profileId);
    }
}