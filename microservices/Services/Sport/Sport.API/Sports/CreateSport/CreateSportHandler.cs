namespace Sport.API.Sports.CreateSport;

public record CreateSportCommand(string Name, string Description) : ICommand<CreateSportResult>;

public record CreateSportResult(Guid Id);

internal class CreateSportCommandHandler(IDocumentSession session) : ICommandHandler<CreateSportCommand, CreateSportResult>
{
    public async Task<CreateSportResult> Handle(CreateSportCommand command, CancellationToken cancellationToken)
    {
        var sport = new Models.Sport
        {
            Name = command.Name,
            Description = command.Description
        };

        session.Store(sport);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateSportResult(sport.Id);
    }
}