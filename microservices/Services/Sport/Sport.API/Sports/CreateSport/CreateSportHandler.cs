using BuildingBlocks.CQRS;

namespace Sport.API.Sports.CreateSport;

public record CreateSportCommand(string Name, string Description) : ICommand<CreateSportResult>;

public record CreateSportResult(Guid Id);

public class CreateSportCommandHandler : ICommandHandler<CreateSportCommand, CreateSportResult>
{
    public async Task<CreateSportResult> Handle(CreateSportCommand command, CancellationToken cancellationToken)
    {
        var sport = new Models.Sport
        {
            Name = command.Name,
            Description = command.Description
        };

        return new CreateSportResult(Guid.NewGuid());
    }
}