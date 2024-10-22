using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace SportyBuddies.Application.Users.Commands.UploadPhoto;

public record UploadPhotoCommand(Guid UserId, bool IsMain, IFormFile File) : IRequest<ErrorOr<string>>;