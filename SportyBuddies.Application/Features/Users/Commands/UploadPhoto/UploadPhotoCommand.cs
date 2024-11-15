using MediatR;
using Microsoft.AspNetCore.Http;

namespace SportyBuddies.Application.Features.Users.Commands.UploadPhoto;

public record UploadPhotoCommand(Guid UserId, bool IsMain, IFormFile File) : IRequest<string>;