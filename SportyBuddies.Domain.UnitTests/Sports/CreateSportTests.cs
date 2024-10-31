using FluentAssertions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestConstants;

namespace SportyBuddies.Domain.UnitTests.Sports;

public class CreateSportTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        var sport = Sport.Create(SportConstants.Name, SportConstants.Description);

        // Assert
        sport.Name.Should().Be(SportConstants.Name);
        sport.Description.Should().Be(SportConstants.Description);
    }
}