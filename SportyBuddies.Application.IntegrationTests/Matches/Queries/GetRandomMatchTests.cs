﻿using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Application.Matches.Queries.GetRandomMatch;
using SportyBuddies.Domain.Matches;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Matches.Queries;

public class GetRandomMatchTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetRandomMatch_ShouldReturnMatch_WhenMatchExists()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        var matchedUser = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.Users.AddAsync(matchedUser);
        
        var match = Match.Create(user, matchedUser, DateTime.UtcNow);
        await DbContext.Matches.AddAsync(match);
        
        await DbContext.SaveChangesAsync();
        
        var query = new GetRandomMatchQuery(user.Id);
        
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
        var user = User.Create(Guid.NewGuid());
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetRandomMatchQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().BeNull();
    }
}