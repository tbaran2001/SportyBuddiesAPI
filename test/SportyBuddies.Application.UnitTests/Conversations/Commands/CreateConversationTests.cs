using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Authentication;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Buddies;
using SportyBuddies.Domain.Common;
using SportyBuddies.Domain.Common.Interfaces;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Conversations.Commands;

public class CreateConversationTests
{
    private readonly CreateConversationCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly IConversationService _conversationServiceMock;
    private readonly IUserContext _userContextMock;

    public CreateConversationTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ConversationMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _conversationServiceMock = Substitute.For<IConversationService>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userContextMock = Substitute.For<IUserContext>();
        _handler = new CreateConversationCommandHandler(_unitOfWorkMock, mapper, _conversationServiceMock,
            _userContextMock);
    }

    [Fact]
    public async Task Handle_Should_CreateConversation()
    {
        // Arrange
        var command = new CreateConversationCommand(Guid.NewGuid());

        var currentUser = new CurrentUser(Guid.NewGuid(), "", [], null);
        _userContextMock.GetCurrentUser().Returns(currentUser);

        var conversation = Conversation.CreateOneToOne(currentUser.Id, command.ParticipantId);
        _conversationServiceMock.CreateConversationAsync(currentUser.Id, command.ParticipantId).Returns(conversation);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreateConversationResponse>();
    }
}