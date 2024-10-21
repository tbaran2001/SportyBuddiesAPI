using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace SportyBuddies.Application.Users.Commands.UploadPhoto;

public record UploadPhotoCommand(Guid UserId, IFormFile File) : IRequest<ErrorOr<string>>;