using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Application.Users.Queries.GetUsers;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Users;

public class GetUsersTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUsers_ShouldReturnUsers_WhenUsersExist()
    {
        // Arrange
        var users=new List<User>
        {
            User.Create(Guid.NewGuid()),
            User.Create(Guid.NewGuid())
        };
        DbContext.Users.AddRange(users);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUsersQuery();
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<UserWithSportsResponse>>();
    }
    
    [Fact]
    public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        // Arrange
        var query = new GetUsersQuery();
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}