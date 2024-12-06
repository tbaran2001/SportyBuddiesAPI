using FluentAssertions;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Domain.UnitTests.Conversations;

public class CreateMessageTests
{
    [Fact]
    public void CreateMessage_WithValidData_ShouldCreateMessage()
    {
        // Arrange
        var conversationId = Guid.NewGuid();
        var senderId = Guid.NewGuid();
        var content = "Hello!";

        // Act
        var message = Message.Create(conversationId, senderId, content);

        // Assert
        message.ConversationId.Should().Be(conversationId);
        message.SenderId.Should().Be(senderId);
        message.Content.Should().Be(content);
    }
}