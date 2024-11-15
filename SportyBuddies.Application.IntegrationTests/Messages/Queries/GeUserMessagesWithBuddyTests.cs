using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Features.Messages.Queries.GetUserMessagesWithBuddy;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Messages.Queries;

public class GeUserMessagesWithBuddyTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUserMessagesWithBuddy_ShouldReturnMessages_WhenValidRequest()
    {
        // Arrange
        var sender = User.Create(Guid.NewGuid());
        var recipient = User.Create(Guid.NewGuid());
        var buddy1 = Buddy.Create(sender, recipient, DateTime.UtcNow);
        var buddy2 = Buddy.Create(recipient, sender, DateTime.UtcNow);
        
        await DbContext.Users.AddAsync(sender);
        await DbContext.Users.AddAsync(recipient);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        
        var message1 = Message.Create(sender.Id, recipient.Id, "Hello", DateTime.UtcNow);
        var message2 = Message.Create(recipient.Id, sender.Id, "Hi", DateTime.UtcNow);
        
        await DbContext.Messages.AddAsync(message1);
        await DbContext.Messages.AddAsync(message2);
        
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserMessagesWithBuddyQuery(sender.Id, recipient.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<MessageResponse>>();
        result.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task GetUserMessagesWithBuddy_ShouldReturnEmptyList_WhenNoMessages()
    {
        // Arrange
        var sender = User.Create(Guid.NewGuid());
        var recipient = User.Create(Guid.NewGuid());
        var buddy1 = Buddy.Create(sender, recipient, DateTime.UtcNow);
        var buddy2 = Buddy.Create(recipient, sender, DateTime.UtcNow);
        
        await DbContext.Users.AddAsync(sender);
        await DbContext.Users.AddAsync(recipient);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserMessagesWithBuddyQuery(sender.Id, recipient.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeEmpty();
    }
}