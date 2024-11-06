using FluentAssertions;
using SportyBuddies.Application.Exceptions;
using SportyBuddies.Application.IntegrationTests.Infrastructure;
using SportyBuddies.Application.Sports.Commands.CreateSport;

namespace SportyBuddies.Application.IntegrationTests.Sports.Commands;

public class CreateSportTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateSport_ShouldCreateSport_WhenSportDoesNotExist()
    {
        // Arrange
        var command = new CreateSportCommand("Basketball",
            "A sport played by two teams of five players on a rectangular court with a hoop at each end.");

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
        var command = new CreateSportCommand("Football",
            "A sport played by two teams of eleven players on a rectangular field with goalposts at each end.");

        await Sender.Send(command);

        // Act
        Func<Task> act = async () => await Sender.Send(command);

        // Assert
        await act.Should().ThrowAsync<ConflictException>();
    }
}