using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.UserSports.Commands.AddUserSport;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Sports;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Application.IntegrationTests.UserSports.Commands;

public class AddUserSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task AddUserSport_ShouldAddSportToUser()
    {
        // Arrange
        var user = User.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Users.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();
        
        var command = new AddUserSportCommand(sport.Id);

        // Act
        await Sender.Send(command);

        // Assert
        user.Sports.Should().Contain(s => s.Id == sport.Id);
    }
    
    [Fact]
    public async Task AddUserSport_ShouldThrowException_WhenUserAlreadyHasSport()
    {
        // Arrange
        var user = User.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        user.AddSport(sport);
        await DbContext.Users.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();
        
        var command = new AddUserSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("User already has this sport");
    }
    
    [Fact]
    public async Task AddUserSport_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();
        
        var command = new AddUserSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task AddUserSport_ShouldThrowNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        var user = User.Create(CurrentUserId);
        await DbContext.Users.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var command = new AddUserSportCommand(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}