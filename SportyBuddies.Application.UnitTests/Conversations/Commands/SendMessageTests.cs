using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Commands.SendMessage;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Conversations;

namespace SportyBuddies.Application.UnitTests.Conversations.Commands;

public class SendMessageTests
{
    private readonly SendMessageCommand _command= new(Guid.NewGuid(), Guid.NewGuid(), "Hello");
    private readonly SendMessageCommandHandler _handler;
    private readonly IConversationsRepository _conversationsRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IHubContext<ChatHub, IChatClient> _hubContextMock;

    public SendMessageTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ConversationMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _conversationsRepositoryMock = Substitute.For<IConversationsRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _hubContextMock = Substitute.For<IHubContext<ChatHub, IChatClient>>();
        _handler = new SendMessageCommandHandler(_conversationsRepositoryMock,_hubContextMock, _unitOfWorkMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnMessage_WhenMessageIsSent()
    {
        // Arrange
        var conversation = Conversation.Create(_command.UserId, new List<Guid> { _command.UserId, Guid.NewGuid() });
        _conversationsRepositoryMock.GetByIdAsync(_command.ConversationId).Returns(conversation);

        // Act
        var result = await _handler.Handle(_command, default);

        // Assert
        result.Should().BeOfType<MessageResponse>();
        result.SenderId.Should().Be(_command.UserId);

        await _conversationsRepositoryMock.Received(1).AddMessageAsync(Arg.Any<Message>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenConversationDoesNotExist()
    {
        // Arrange
        _conversationsRepositoryMock.GetByIdAsync(_command.ConversationId).Returns((Conversation)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, default);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}