using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Application.IntegrationTests.Matches.Queries;

public class GetRandomMatchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetRandomMatch_ShouldReturnMatch_WhenMatchExists()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        var matchedUser = Profile.Create(Guid.NewGuid());
        await DbContext.Profiles.AddAsync(user);
        await DbContext.Profiles.AddAsync(matchedUser);
        
        var (match, _) = Match.CreatePair(user.Id, matchedUser.Id, DateTime.UtcNow);
        await DbContext.Matches.AddAsync(match);
        
        await DbContext.SaveChangesAsync();
        
        var query = new GetRandomMatchQuery();
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeOfType<RandomMatchResponse>();
        result.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetRandomMatch_ShouldReturnNull_WhenMatchDoesNotExist()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetRandomMatchQuery();
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeNull();
    }
}