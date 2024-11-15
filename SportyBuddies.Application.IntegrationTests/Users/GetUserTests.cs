using FluentAssertions;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Users.Queries.GetUser;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.Users;

public class GetUserTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetUser_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid());
        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();
        
        var query = new GetUserQuery(user.Id);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserWithSportsResponse>();
    }
    
    [Fact]
    public async Task GetUser_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var query = new GetUserQuery(Guid.NewGuid());
        
        // Act
        Func<Task> act = async () => await Sender.Send(query);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}