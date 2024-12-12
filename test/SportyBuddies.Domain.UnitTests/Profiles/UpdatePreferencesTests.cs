using FluentAssertions;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.UnitTests.TestData;

namespace SportyBuddies.Domain.UnitTests.Profiles;

public class UpdatePreferencesTests
{
    [Fact]
    public void UpdatePreferences_Should_SetPreferencesPropertyValue()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        var preferences = Preferences.Create(
            PreferencesData.MinAge,
            PreferencesData.MaxAge,
            PreferencesData.MaxDistance,
            PreferencesData.Gender);

        // Act
        user.UpdatePreferences(preferences);

        // Assert
        user.Preferences.Should().Be(preferences);
    }
}