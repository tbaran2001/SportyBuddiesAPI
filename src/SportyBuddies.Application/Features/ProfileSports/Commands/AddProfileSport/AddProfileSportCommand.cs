using MediatR;

namespace SportyBuddies.Application.Features.ProfileSports.Commands.AddProfileSport;

public record AddProfileSportCommand(Guid SportId) : IRequest;