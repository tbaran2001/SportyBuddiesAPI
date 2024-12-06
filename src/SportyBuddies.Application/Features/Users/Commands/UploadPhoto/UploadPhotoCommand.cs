using MediatR;
using Microsoft.AspNetCore.Http;

namespace SportyBuddies.Application.Features.Users.Commands.UploadPhoto;

public record UploadPhotoCommand(bool IsMain, IFormFile File) : IRequest<string>;