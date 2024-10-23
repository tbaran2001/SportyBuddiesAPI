using MediatR;

namespace SportyBuddies.Application.Users.Queries.GetUserMainPhoto;

public record GetUserMainPhotoQuery(Guid UserId) : IRequest<string>;