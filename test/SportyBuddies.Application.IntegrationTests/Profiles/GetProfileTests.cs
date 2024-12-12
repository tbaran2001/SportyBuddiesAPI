using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Profiles.Queries.GetProfile;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.IntegrationTests.Profiles;

public class GetProfileTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUser_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = Profile.Create(Guid.NewGuid());
        DbContext.Profiles.Add(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetProfileQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ProfileWithSportsResponse>();
    }
    
    [Fact]
    public async Task GetUser_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var query = new GetProfileQuery(Guid.NewGuid());
        
        // Act
        Func<Task> act = async () => await Sender.Send(query);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}