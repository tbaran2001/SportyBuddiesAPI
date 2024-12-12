using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.ProfileSports.Commands.AddProfileSport;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Profiles;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.ProfileSports.Commands;

public class AddProfileSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task AddUserSport_ShouldAddSportToUser()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Profiles.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();
        
        var command = new AddProfileSportCommand(sport.Id);

        // Act
        await Sender.Send(command);

        // Assert
        user.Sports.Should().Contain(s => s.Id == sport.Id);
    }
    
    [Fact]
    public async Task AddUserSport_ShouldThrowException_WhenUserAlreadyHasSport()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        var sport = Sport.Create("Football", "Football description");
        user.AddSport(sport);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();
        
        var command = new AddProfileSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Profile already has this sport");
    }
    
    [Fact]
    public async Task AddUserSport_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var sport = Sport.Create("Football", "Football description");
        await DbContext.Sports.AddAsync(sport);
        await DbContext.SaveChangesAsync();
        
        var command = new AddProfileSportCommand(sport.Id);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async Task AddUserSport_ShouldThrowNotFoundException_WhenSportDoesNotExist()
    {
        // Arrange
        var user = Profile.Create(CurrentUserId);
        await DbContext.Profiles.AddAsync(user);
        await DbContext.SaveChangesAsync();
        
        var command = new AddProfileSportCommand(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}