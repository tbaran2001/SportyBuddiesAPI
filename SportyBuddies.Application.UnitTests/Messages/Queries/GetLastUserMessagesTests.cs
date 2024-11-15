using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Messages.Queries.GetLastUserMessages;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Domain.Messages;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.UnitTests.Messages.Queries;

public class GetLastUserMessagesTests
{
    private readonly GetLastUserMessagesQuery _query = new(Guid.NewGuid());
    private readonly GetLastUserMessagesQueryHandler _handler;
    private readonly IMessagesRepository _messagesRepositoryMock;

    public GetLastUserMessagesTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MessageMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();
        
        _messagesRepositoryMock = Substitute.For<IMessagesRepository>();
        _handler = new GetLastUserMessagesQueryHandler(_messagesRepositoryMock, mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnMessages_WhenValidRequest()
    {
        // Arrange
        var messages = new List<Message>
        {
            Message.Create(Guid.NewGuid(), _query.UserId, "Hello", DateTime.UtcNow),
            Message.Create(_query.UserId, Guid.NewGuid(), "Hi", DateTime.UtcNow)
        };
        _messagesRepositoryMock.GetLastUserMessagesAsync(_query.UserId).Returns(messages);

        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);

        // Assert
        result.Should().BeOfType<List<MessageResponse>>();
    }
}