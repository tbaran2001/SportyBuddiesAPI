using MediatR;

namespace SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;

public record GetUserMainPhotoQuery() : IRequest<string>;