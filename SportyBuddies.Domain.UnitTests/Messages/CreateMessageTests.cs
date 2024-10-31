using FluentAssertions;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Domain.UnitTests.Messages;

public class CreateMessageTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Arrange
        var senderId = Guid.NewGuid();
        var recipientId = Guid.NewGuid();
        var content = "Hello";
        var utcNow = DateTime.UtcNow;
        
        // Act
        var message = Message.Create(senderId, recipientId, content, utcNow);
        
        // Assert
        message.SenderId.Should().Be(senderId);
        message.RecipientId.Should().Be(recipientId);
        message.Content.Should().Be(content);
        message.TimeSent.Should().Be(utcNow);
    }
}