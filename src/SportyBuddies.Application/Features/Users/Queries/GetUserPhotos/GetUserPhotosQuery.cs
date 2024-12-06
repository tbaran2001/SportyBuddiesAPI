using MediatR;
using SportyBuddies.Application.Common.DTOs.User;

namespace SportyBuddies.Application.Features.Users.Queries.GetUserPhotos;

public record GetUserPhotosQuery(Guid UserId) : IRequest<List<UserPhotoResponse>>;