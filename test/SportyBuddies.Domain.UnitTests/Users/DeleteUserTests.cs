using FluentAssertions;
using SportyBuddies.Domain.UnitTests.Infrastructure;
using SportyBuddies.Domain.Users;
using SportyBuddies.Domain.Users.Events;

namespace SportyBuddies.Domain.UnitTests.Users;

public class DeleteUserTests:BaseTest
{
    [Fact]
    public void Delete_Should_RaiseUserDeletedDomainEvent()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());

        // Act
        user.Delete();

        // Assert
        var domainEvent = AssertDomainEventWasPublished<UserDeletedEvent>(user);

        domainEvent.UserId.Should().Be(user.Id);
    }
}