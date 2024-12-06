using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachUserConversation;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.UnitTests.Conversations.Queries;

public class GetLastMessageFromEachUserConversationTests
{
    private readonly GetLastMessageFromEachUserConversationQuery _query = new();
    private readonly GetLastMessageFromEachUserConversationQueryHandler _handler;
    private readonly IConversationsRepository _conversationsRepositoryMock;
    private readonly IUserContext _userContextMock;

    public GetLastMessageFromEachUserConversationTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ConversationMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _conversationsRepositoryMock = Substitute.For<IConversationsRepository>();
        _userContextMock = Substitute.For<IUserContext>();
        _handler = new GetLastMessageFromEachUserConversationQueryHandler(_conversationsRepositoryMock, mapper,
            _userContextMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnMessages_WhenConversationExists()
    {
        // Arrange
        var currentUser = new CurrentUser(Guid.NewGuid(), "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var conversation = Conversation.CreateOneToOne(currentUser.Id, Guid.NewGuid());
        var message1 = Message.Create(conversation.Id, conversation.Participants.First().UserId, "Hello");
        var message2 = Message.Create(conversation.Id, conversation.Participants.Last().UserId, "Hi");
        conversation.Messages.Add(message1);
        conversation.Messages.Add(message2);
        _conversationsRepositoryMock.GetLastMessageFromEachUserConversationAsync(currentUser.Id)
            .Returns(new List<Message> { message1, message2 });

        // Act
        var result = await _handler.Handle(_query, default);

        // Assert
        result.Should().BeOfType<List<MessageResponse>>();
        result.Count().Should().Be(2);
    }
}