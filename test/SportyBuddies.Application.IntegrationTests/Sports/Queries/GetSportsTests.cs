using FluentAssertions;
using SportyBuddies.Application.Features.Sports.Queries.GetSports;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.Sports.Queries;

public class GetSportsTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task GetSports_ShouldReturnSports_WhenSportsExist()
    {
        // Arrange
        var sports= new List<Sport>
        {
            Sport.Create("Football", "A sport played by two teams of eleven players on a rectangular field with goalposts at each end."),
            Sport.Create("Basketball", "A sport played by two teams of five players on a rectangular court with a hoop at each end.")
        };
        
        DbContext.Sports.AddRange(sports);
        await DbContext.SaveChangesAsync();

        var query = new GetSportsQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
        result.Should().Contain(s => s.Name == "Football");
        result.Should().Contain(s => s.Name == "Basketball");
        
        // Clean up
        DbContext.Sports.RemoveRange(sports);
        await DbContext.SaveChangesAsync();
    }
    
    [Fact]
    public async Task GetSports_ShouldReturnEmptyList_WhenNoSportsExist()
    {
        // Arrange
        var query = new GetSportsQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeEmpty();
    }
}