using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.Features.Sports.Commands.CreateSport;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Domain.Sports;

namespace SportyBuddies.Application.IntegrationTests.Sports.Commands;

public class CreateSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateSport_ShouldCreateSport_WhenSportDoesNotExist()
    {
        // Arrange
        var command = new CreateSportCommand("Test",
            "Test description");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
    }

    [Fact]
    public async Task CreateSport_ShouldThrowException_WhenSportAlreadyExists()
    {
        // Arrange
        var command = new CreateSportCommand("Test1",
            "Test description");

        await Sender.Send(command);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<ConflictException>();
    }
}