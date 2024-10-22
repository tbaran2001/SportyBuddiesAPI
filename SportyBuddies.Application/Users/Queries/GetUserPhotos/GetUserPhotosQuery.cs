using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs;

namespace SportyBuddies.Application.Users.Queries.GetUserPhotos;

public record GetUserPhotosQuery(Guid UserId) : IRequest<ErrorOr<List<UserPhotoResponse>>>;