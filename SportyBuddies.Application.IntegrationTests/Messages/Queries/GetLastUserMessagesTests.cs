using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Features.Messages.Queries.GetLastUserMessages;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Messages.Queries;

public class GetLastUserMessagesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetLastUserMessages_ShouldReturnMessages_WhenValidRequest()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser1 = User.Create(Guid.NewGuid());
        var matchedUser2 = User.Create(Guid.NewGuid());
        
        await DbContext.Users.AddAsync(user);
        await DbContext.Users.AddAsync(matchedUser1);
        await DbContext.Users.AddAsync(matchedUser2);
        
        await DbContext.SaveChangesAsync();
        
        var buddy1 = Buddy.Create(user, matchedUser1, DateTime.UtcNow);
        var buddy2 = Buddy.Create(user, matchedUser2, DateTime.UtcNow);
        
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        
        var message1 = Message.Create(user.Id, matchedUser1.Id, "Hello", DateTime.UtcNow);
        var message2 = Message.Create(matchedUser1.Id, user.Id, "Hi", DateTime.UtcNow);
        var message3 = Message.Create(user.Id, matchedUser2.Id, "Hello", DateTime.UtcNow);
        var message4 = Message.Create(matchedUser2.Id, user.Id, "Hi", DateTime.UtcNow);
        
        await DbContext.Messages.AddAsync(message1);
        await DbContext.Messages.AddAsync(message2);
        await DbContext.Messages.AddAsync(message3);
        await DbContext.Messages.AddAsync(message4);
        
        await DbContext.SaveChangesAsync();
        
        var query = new GetLastUserMessagesQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<MessageResponse>>();
        result.Count.Should().Be(2);
    }
    
    [Fact]
    public async Task GetLastUserMessages_ShouldReturnEmptyList_WhenNoMessages()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser1 = User.Create(Guid.NewGuid());
        var matchedUser2 = User.Create(Guid.NewGuid());
        
        await DbContext.Users.AddAsync(user);
        await DbContext.Users.AddAsync(matchedUser1);
        await DbContext.Users.AddAsync(matchedUser2);
        
        await DbContext.SaveChangesAsync();
        
        var buddy1 = Buddy.Create(user, matchedUser1, DateTime.UtcNow);
        var buddy2 = Buddy.Create(user, matchedUser2, DateTime.UtcNow);
        
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        
        await DbContext.SaveChangesAsync();
        
        var query = new GetLastUserMessagesQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeEmpty();
    }
}