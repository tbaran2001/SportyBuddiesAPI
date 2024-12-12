using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Features.Profiles.Queries.GetProfiles;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.IntegrationTests.Profiles;

public class GetProfilesTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUsers_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users=new List<Profile>
        {
            Profile.Create(Guid.NewGuid()),
            Profile.Create(Guid.NewGuid())
        };
        DbContext.Profiles.AddRange(users);
        await DbContext.SaveChangesAsync();
        
        var query = new GetProfilesQuery();
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ProfileWithSportsResponse>>();
    }
    
    [Fact]
    public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var query = new GetProfilesQuery();
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}