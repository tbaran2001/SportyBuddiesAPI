using FluentAssertions;
using Microsoft.AspNetCore.SignalR;
using NSubstitute;
using SportyBuddies.Application.Common.Interfaces;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Hubs;
using SportyBuddies.Application.Messages.Commands.SendMessage;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Messages.Commands;

public class SendMessageTests
{
    private readonly SendMessageCommand _command = new(Guid.NewGuid(), Guid.NewGuid(), "Hello");
    private readonly SendMessageCommandHandler _handler;
    private readonly IMessagesRepository _messagesRepositoryMock;
    private readonly IUsersRepository _usersRepositoryMock;
    private readonly IHubContext<ChatHub, IChatClient> _hubContextMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IDateTimeProvider _dateTimeProviderMock;

    public SendMessageTests()
    {
        _messagesRepositoryMock = Substitute.For<IMessagesRepository>();
        _usersRepositoryMock = Substitute.For<IUsersRepository>();
        _hubContextMock = Substitute.For<IHubContext<ChatHub, IChatClient>>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _dateTimeProviderMock = Substitute.For<IDateTimeProvider>();
        _handler = new SendMessageCommandHandler(_messagesRepositoryMock, _usersRepositoryMock, _hubContextMock,
            _unitOfWorkMock, _dateTimeProviderMock);
    }

    [Fact]
    public async Task Handle_ShouldSendMessage_WhenValidRequest()
    {
        // Arrange
        _usersRepositoryMock.UserExistsAsync(_command.SenderId).Returns(true);
        _usersRepositoryMock.UserExistsAsync(_command.RecipientId).Returns(true);
        _dateTimeProviderMock.UtcNow.Returns(DateTime.UtcNow);

        // Act
        await _handler.Handle(_command, CancellationToken.None);

        // Assert
        await _messagesRepositoryMock.Received(1).AddMessageAsync(Arg.Any<Message>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
        await _hubContextMock.Clients.User(_command.RecipientId.ToString())
            .Received(1)
            .ReceiveMessage(Arg.Any<HubMessage>());
        await _hubContextMock.Clients.User(_command.SenderId.ToString())
            .Received(1)
            .ReceiveMessage(Arg.Any<HubMessage>());
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenSenderDoesNotExist()
    {
        // Arrange
        _usersRepositoryMock.UserExistsAsync(_command.SenderId).Returns(false);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenRecipientDoesNotExist()
    {
        // Arrange
        _usersRepositoryMock.UserExistsAsync(_command.SenderId).Returns(true);
        _usersRepositoryMock.UserExistsAsync(_command.RecipientId).Returns(false);

        // Act
        Func<Task> act = async () => await _handler.Handle(_command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}