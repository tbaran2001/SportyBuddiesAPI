using FluentAssertions;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.UnitTests.Profiles;

public class CreateProfileTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        var user= Profile.Create(Guid.NewGuid());
        
        user.Id.Should().NotBeEmpty();
    }
}