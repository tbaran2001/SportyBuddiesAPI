using BuildingBlocks.CQRS;

namespace Sport.API.Sports.GetSport;

public record GetSportByIdQuery(Guid Id): IQuery<GetSportByIdResult>;
public record GetSportByIdResult(Models.Sport Sport);

internal class GetSportByIdQueryHandler: IQueryHandler<GetSportByIdQuery, GetSportByIdResult>
{
    public async Task<GetSportByIdResult> Handle(GetSportByIdQuery query, CancellationToken cancellationToken)
    {
        var sport = new Models.Sport
        {
            Id = query.Id,
            Name = "Football",
            Description = "Football is a family of team sports that involve, to varying degrees, kicking a ball to score a goal."
        };

        return new GetSportByIdResult(sport);
    }
}