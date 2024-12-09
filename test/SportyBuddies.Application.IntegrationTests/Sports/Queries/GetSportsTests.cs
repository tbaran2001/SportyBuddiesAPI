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
        DbContext.Sports.RemoveRange(DbContext.Sports);
        await DbContext.SaveChangesAsync();
        var sports= new List<Sport>
        {
            Sport.Create("Test1", "Test description"),
            Sport.Create("Test2", "Test description")
        };
        
        DbContext.Sports.AddRange(sports);
        await DbContext.SaveChangesAsync();

        var query = new GetSportsQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
        result.Should().Contain(s => s.Name == "Test1");
        result.Should().Contain(s => s.Name == "Test2");
        
        // Clean up
        DbContext.Sports.RemoveRange(sports);
        await DbContext.SaveChangesAsync();
    }
    
    [Fact]
    public async Task GetSports_ShouldReturnEmptyList_WhenNoSportsExist()
    {
        // Arrange
        DbContext.Sports.RemoveRange(DbContext.Sports);
        await DbContext.SaveChangesAsync();
        var query = new GetSportsQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.Should().BeEmpty();
    }
}