﻿using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.IntegrationTests.Conversations.Commands;

public class CreateConversationTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateConversation_ShouldCreateConversation()
    {
        // Arrange
        var user1 = Profile.Create(CurrentUserId);
        var user2 = Profile.Create(Guid.NewGuid());
        await DbContext.Profiles.AddAsync(user1);
        await DbContext.Profiles.AddAsync(user2);

        var (buddy1, buddy2) = Buddy.CreatePair(user1.Id, user2.Id, DateTime.UtcNow);
        await DbContext.Buddies.AddAsync(buddy1);
        await DbContext.Buddies.AddAsync(buddy2);
        await DbContext.SaveChangesAsync();

        var command = new CreateConversationCommand(user2.Id);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().BeOfType<CreateConversationResponse>();
        result.CreatorId.Should().Be(user1.Id);

        var conversation = await DbContext.Conversations.FindAsync(result.Id);
        conversation.Should().NotBeNull();
        conversation.CreatorId.Should().Be(user1.Id);
        conversation.Participants.Should().HaveCount(2);
        conversation.Participants.Should().Contain(p => p.ProfileId == user1.Id);
        conversation.Participants.Should().Contain(p => p.ProfileId == user2.Id);
    }
}