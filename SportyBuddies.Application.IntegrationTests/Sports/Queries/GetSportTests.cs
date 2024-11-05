using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Application.Sports.Queries.GetSport;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.Sports.Queries;

public class GetSportTests: BaseIntegrationTest
{
    public GetSportTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetSport_ShouldReturnSport_WhenSportExists()
    {
        // Arrange
        var sport = Sport.Create("Football", "A sport played by two teams of eleven players on a rectangular field with goalposts at each end.");
        
        DbContext.Sports.Add(sport);
        await DbContext.SaveChangesAsync();

        var query = new GetSportQuery(sport.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(sport.Name);
        
        // Clean up
        DbContext.Sports.Remove(sport);
        await DbContext.SaveChangesAsync();
    }
    
    [Fact]
    public async Task GetSport_ShouldThrowException_WhenSportDoesNotExist()
    {
        // Arrange
        var query = new GetSportQuery(Guid.NewGuid());

        // Act
        Func<Task> act = async () => await Sender.Send(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}