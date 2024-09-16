using MediatR;
using Shouldly;
using SportyBuddies.Application.SubcutaneousTests.Common;
using TestCommon.Sports;

namespace SportyBuddies.Application.SubcutaneousTests.Sports.Commands;

[Collection(MediatorFactoryCollection.CollectionName)]
public class CreateSportTests(MediatorFactory mediatorFactory)
{
    private readonly IMediator _mediator = mediatorFactory.CreateMediator();

    [Fact]
    public async void CreateSport_WhenValidCommand_ShouldCreateSport()
    {
        // Arrange
        var createSportCommand = SportCommandFactory.CreateCreateSportCommand();

        // Act
        var createSportResult = await _mediator.Send(createSportCommand);

        // Assert
        createSportResult.IsError.ShouldBe(false);
        createSportResult.Value.Name.ShouldBe(createSportCommand.Name);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(200)]
    public async void CreateSport_WhenCommandContainsInvalidData_ShouldReturnValidationError(int sportNameLength)
    {
        var sportName = new string('a', sportNameLength);
        var createSportCommand = SportCommandFactory.CreateCreateSportCommand(sportName);

        var createSportResult = await _mediator.Send(createSportCommand);

        createSportResult.IsError.ShouldBe(true);
        createSportResult.FirstError.Code.ShouldBe("Name");
    }
}