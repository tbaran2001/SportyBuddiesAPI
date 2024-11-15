using MediatR;

namespace SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;

public record GetUserMainPhotoQuery(Guid UserId) : IRequest<string>;