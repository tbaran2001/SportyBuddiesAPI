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
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Conversations.Commands;

public class CreateConversationTests
{
    private readonly CreateConversationCommandHandler _handler;
    private readonly IConversationsRepository _conversationsRepositoryMock;
    private readonly IBuddiesRepository _buddiesRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public CreateConversationTests()
    {
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<ConversationMappingProfile>(); });
        var mapper = configurationProvider.CreateMapper();

        _conversationsRepositoryMock = Substitute.For<IConversationsRepository>();
        _buddiesRepositoryMock = Substitute.For<IBuddiesRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _handler = new CreateConversationCommandHandler(_conversationsRepositoryMock,_buddiesRepositoryMock, _unitOfWorkMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnConversation_WhenConversationIsCreated()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantIds = new List<Guid> { creatorId, Guid.NewGuid() };
        CreateConversationCommand command = new(creatorId, participantIds);

        _conversationsRepositoryMock.AreParticipantsBuddiesAsync(Arg.Any<ICollection<Guid>>()).Returns(true);
        _conversationsRepositoryMock.UsersHaveConversationAsync(Arg.Any<ICollection<Guid>>()).Returns(false);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().BeOfType<CreateConversationResponse>();
        result.CreatorId.Should().Be(command.CreatorId);

        await _conversationsRepositoryMock.Received(1).AddAsync(Arg.Any<Conversation>());
        await _unitOfWorkMock.Received(1).CommitChangesAsync();
    }

    [Fact]
    public async Task Handle_ShouldThrowBadRequestException_WhenParticipantsAreNotBuddies()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantIds = new List<Guid> { creatorId, Guid.NewGuid() };
        CreateConversationCommand command = new(creatorId, participantIds);

        _conversationsRepositoryMock.AreParticipantsBuddiesAsync(Arg.Any<ICollection<Guid>>()).Returns(false);

        // Act
        var act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Participants are not buddies");
    }

    [Fact]
    public async Task Handle_ShouldThrowBadRequestException_WhenUsersHaveConversation()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantIds = new List<Guid> { creatorId, Guid.NewGuid() };
        CreateConversationCommand command = new(creatorId, participantIds);

        _conversationsRepositoryMock.AreParticipantsBuddiesAsync(Arg.Any<ICollection<Guid>>()).Returns(true);
        _conversationsRepositoryMock.UsersHaveConversationAsync(Arg.Any<ICollection<Guid>>()).Returns(true);

        // Act
        var act = async () => await _handler.Handle(command, default);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage("Conversation already exists");
    }
}