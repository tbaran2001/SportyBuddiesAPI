using AutoMapper;
using FluentAssertions;
using NSubstitute;
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

    public CreateConversationTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ConversationMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _conversationServiceMock = Substitute.For<IConversationService>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateConversationCommandHandler(_unitOfWorkMock, mapper, _conversationServiceMock);
    }

    [Fact]
    public async Task Handle_Should_CreateConversation()
    {
        // Arrange
        var command = new CreateConversationCommand(Guid.NewGuid(), Guid.NewGuid());
        var conversation = Conversation.CreateOneToOne(command.CreatorId, command.ParticipantId);
        _conversationServiceMock.CreateConversationAsync(command.CreatorId, command.ParticipantId).Returns(conversation);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<CreateConversationResponse>();
    }
}