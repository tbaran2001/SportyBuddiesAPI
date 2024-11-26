using FluentAssertions;
using NSubstitute;
using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Conversations;
using SportyBuddies.Domain.Services;

namespace SportyBuddies.Domain.UnitTests.Services.Conversations;

public class CreateConversationTests
{
    private readonly IBuddiesRepository _buddiesRepository;
    private readonly IConversationsRepository _conversationsRepository;
    private readonly ConversationService _conversationService;

    public CreateConversationTests()
    {
        _buddiesRepository = Substitute.For<IBuddiesRepository>();
        _conversationsRepository = Substitute.For<IConversationsRepository>();
        _conversationService = new ConversationService(_buddiesRepository, _conversationsRepository);
    }

    [Fact]
    public async Task CreateConversationAsync_WhenParticipantsAreNotBuddies_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantId = Guid.NewGuid();
        _buddiesRepository.AreUsersAlreadyBuddiesAsync(creatorId, participantId).Returns(false);

        // Act
        Func<Task> act = async () => await _conversationService.CreateConversationAsync(creatorId, participantId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Participants are not buddies.");
    }

    [Fact]
    public async Task CreateConversationAsync_WhenConversationAlreadyExists_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantId = Guid.NewGuid();
        _buddiesRepository.AreUsersAlreadyBuddiesAsync(creatorId, participantId).Returns(true);
        _conversationsRepository.UsersHaveConversationAsync(creatorId, participantId).Returns(true);

        // Act
        Func<Task> act = async () => await _conversationService.CreateConversationAsync(creatorId, participantId);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Conversation already exists.");
    }

    [Fact]
    public async Task CreateConversationAsync_WhenValidRequest_ShouldCreateConversation()
    {
        // Arrange
        var creatorId = Guid.NewGuid();
        var participantId = Guid.NewGuid();
        _buddiesRepository.AreUsersAlreadyBuddiesAsync(creatorId, participantId).Returns(true);
        _conversationsRepository.UsersHaveConversationAsync(creatorId, participantId).Returns(false);

        // Act
        await _conversationService.CreateConversationAsync(creatorId, participantId);

        // Assert
        await _conversationsRepository.Received().AddAsync(Arg.Any<Conversation>());
    }
}