using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Conversations.Commands;

public class CreateConversationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateConversation_ShouldCreateConversation()
    {
        // Arrange
        var user1 = User.Create(Guid.NewGuid());
        var user2 = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user1);
        await DbContext.Users.AddAsync(user2);

        var buddy1 = Buddy.Create(user1, user2,DateTime.UtcNow);
        var buddy2 = Buddy.Create(user2, user1,DateTime.UtcNow);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        await DbContext.SaveChangesAsync();

        var command = new CreateConversationCommand(user1.Id, new List<Guid> { user1.Id, user2.Id });

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().BeOfType<CreateConversationResponse>();
        result.CreatorId.Should().Be(user1.Id);

        var conversation = await DbContext.Conversations.FindAsync(result.Id);
        conversation.Should().NotBeNull();
        conversation.CreatorId.Should().Be(user1.Id);
        conversation.Participants.Should().HaveCount(2);
        conversation.Participants.Should().Contain(p => p.UserId == user1.Id);
        conversation.Participants.Should().Contain(p => p.UserId == user2.Id);
    }

    [Fact]
    public async Task CreateConversation_ShouldThrowBadRequestException_WhenParticipantsAreNotBuddies()
    {
        // Arrange
        var user1 = User.Create(Guid.NewGuid());
        var user2 = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user1);
        await DbContext.Users.AddAsync(user2);
        await DbContext.SaveChangesAsync();

        var command = new CreateConversationCommand(user1.Id, new List<Guid> { user1.Id, user2.Id });

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Participants are not buddies");
    }

    [Fact]
    public async Task CreateConversation_ShouldThrowBadRequestException_WhenConversationAlreadyExists()
    {
        // Arrange
        var user1 = User.Create(Guid.NewGuid());
        var user2 = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user1);
        await DbContext.Users.AddAsync(user2);

        var buddy1 = Buddy.Create(user1, user2,DateTime.UtcNow);
        var buddy2 = Buddy.Create(user2, user1,DateTime.UtcNow);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);

        var conversation = Conversation.Create(user1.Id, new List<Guid> { user1.Id, user2.Id });
        await DbContext.Conversations.AddAsync(conversation);
        await DbContext.SaveChangesAsync();

        var command = new CreateConversationCommand(user1.Id, new List<Guid> { user1.Id, user2.Id });

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Conversation already exists");
    }
}