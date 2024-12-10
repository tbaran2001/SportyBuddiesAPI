using MediatR;

namespace SportyBuddies.Application.Features.Users.Commands.UploadPhoto;

public record UploadPhotoCommand(Stream File, string FileName) : IRequest<string>;