using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Sports.Commands.DeleteSport;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.Sports.Commands;

public class DeleteSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task DeleteSport_ShouldDeleteSport_WhenSportExists()
    {
        // Arrange
        var sport = Sport.Create("Basketball",
            "A sport played by two teams of five players on a rectangular court with a hoop at each end.");
        DbContext.Sports.Add(sport);
        await DbContext.SaveChangesAsync();
        
        var deleteCommand = new DeleteSportCommand(sport.Id);
        
        // Act
        await Sender.Send(deleteCommand);
        
        // Assert
        DbContext.Sports.Should().NotContain(s => s.Id == sport.Id);
    }
    
    [Fact]
    public async Task DeleteSport_ShouldThrowException_WhenSportDoesNotExist()
    {
        // Arrange
        var deleteCommand = new DeleteSportCommand(Guid.NewGuid());
        
        // Act
        Func<Task> act = async () => await Sender.Send(deleteCommand);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}