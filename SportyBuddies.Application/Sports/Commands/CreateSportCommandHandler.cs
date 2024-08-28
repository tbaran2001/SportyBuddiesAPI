using MediatR;

namespace SportyBuddies.Application.Sports.Commands;

public class CreateSportCommandHandler : IRequestHandler<CreateSportCommand, Guid>
{
    public Task<Guid> Handle(CreateSportCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}