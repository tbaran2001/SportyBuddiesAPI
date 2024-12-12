using FluentAssertions;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Profiles.Events;
using SportyBuddies.Domain.UnitTests.Infrastructure;

namespace SportyBuddies.Domain.UnitTests.Profiles;

public class DeleteProfileTests:BaseTest
{
    [Fact]
    public void Delete_Should_RaiseUserDeletedDomainEvent()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());

        // Act
        user.Delete();

        // Assert
        var domainEvent = AssertDomainEventWasPublished<ProfileDeletedEvent>(user);

        domainEvent.ProfileId.Should().Be(user.Id);
    }
}