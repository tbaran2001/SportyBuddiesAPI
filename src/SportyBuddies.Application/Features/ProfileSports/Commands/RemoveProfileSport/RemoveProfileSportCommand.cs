using MediatR;

namespace SportyBuddies.Application.Features.ProfileSports.Commands.RemoveProfileSport;

public record RemoveProfileSportCommand(Guid SportId) : IRequest;