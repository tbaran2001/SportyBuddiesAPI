using AutoMapper;
using FluentAssertions;
using NSubstitute;
using SportyBuddies.Application.Common.DTOs.Message;
using SportyBuddies.Application.Mappings;
using SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;
using SportyBuddies.Domain.Messages;

namespace SportyBuddies.Application.UnitTests.Messages.Queries;

public class GetUserMessagesWithBuddyTests
{
    private readonly GetUserMessagesWithBuddyQuery _query = new(Guid.NewGuid(), Guid.NewGuid());
    private readonly GetUserMessagesWithBuddyQueryHandler _handler;
    private readonly IMessagesRepository _messagesRepositoryMock;
    
    public GetUserMessagesWithBuddyTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MessageMappingProfile>();
        });
        var mapper = configurationProvider.CreateMapper();
        
        _messagesRepositoryMock = Substitute.For<IMessagesRepository>();
        _handler = new GetUserMessagesWithBuddyQueryHandler(_messagesRepositoryMock, mapper);
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
        _messagesRepositoryMock.GetUserMessagesWithBuddyAsync(_query.UserId, _query.BuddyId).Returns(messages);
        
        // Act
        var result = await _handler.Handle(_query, CancellationToken.None);
        
        // Assert
        result.Should().BeOfType<List<MessageResponse>>();
    }
}