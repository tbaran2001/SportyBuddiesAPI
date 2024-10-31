using FluentAssertions;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.UnitTests.Users;

public class CreateUserTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        var user= User.Create(Guid.NewGuid());
        
        user.Id.Should().NotBeEmpty();
    }
}