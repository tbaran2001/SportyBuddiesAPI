using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Queries.GetConversationMessages;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.UnitTests.Conversations.Queries;

public class GetConversationMessagesTests
{
    private readonly GetConversationMessagesQuery _query = new(Guid.NewGuid());
    private readonly GetConversationMessagesQueryHandler _handler;
    private readonly IConversationsRepository _conversationsRepositoryMock;

    public GetConversationMessagesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ConversationMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _conversationsRepositoryMock = Substitute.For<IConversationsRepository>();
        _handler = new GetConversationMessagesQueryHandler(_conversationsRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnMessages_WhenConversationExists()
    {
        // Arrange
        var conversation = Conversation.CreateOneToOne(Guid.NewGuid(), Guid.NewGuid());
        var message1 = Message.Create(conversation.Id, conversation.Participants.First().UserId, "Hello");
        var message2 = Message.Create(conversation.Id, conversation.Participants.Last().UserId, "Hi");
        conversation.Messages.Add(message1);
        conversation.Messages.Add(message2);
        _conversationsRepositoryMock.GetConversationWithMessagesAsync(_query.ConversationId).Returns(conversation);

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeOfType<List<MessageResponse>>();
        result.Count().Should().Be(2);
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenConversationDoesNotExist()
    {
        // Arrange
        _conversationsRepositoryMock.GetConversationWithMessagesAsync(_query.ConversationId).Returns((Conversation)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_query, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}