using FluentAssertions;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.UnitTests.TestData;

namespace SportyBuddies.Domain.UnitTests.Sports;

public class CreateSportTests
{
    [Fact]
    public void Create_Should_SetPropertyValue()
    {
        // Act
        var sport = Sport.Create(SportData.Name, SportData.Description);

        // Assert
        sport.Name.Should().Be(SportData.Name);
        sport.Description.Should().Be(SportData.Description);
    }
}