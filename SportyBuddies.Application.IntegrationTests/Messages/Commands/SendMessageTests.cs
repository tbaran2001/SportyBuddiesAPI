using FluentAssertions;
using SportyBuddies.Application.Features.Messages.Commands.SendMessage;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Messages.Commands;

public class SendMessageTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task SendMessage_ShouldSendMessage_WhenMessageIsValid()
    {
        // Arrange
        var sender = User.Create(Guid.NewGuid());
        var recipient = User.Create(Guid.NewGuid());
        var buddy1=Buddy.Create(sender,recipient,DateTime.UtcNow);
        var buddy2=Buddy.Create(recipient,sender,DateTime.UtcNow);
        
        await DbContext.Users.AddAsync(sender);
        await DbContext.Users.AddAsync(recipient);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        
        await DbContext.SaveChangesAsync();
        
        var command = new SendMessageCommand(sender.Id, recipient.Id, "Hello, how are you?");
        
        // Act
        await Sender.Send(command);
        
        // Assert
        var message = DbContext.Messages.FirstOrDefault();
        message.Should().NotBeNull();
        message.SenderId.Should().Be(sender.Id);
        message.RecipientId.Should().Be(recipient.Id);
    }
}