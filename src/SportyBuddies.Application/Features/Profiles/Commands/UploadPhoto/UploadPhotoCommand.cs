using MediatR;

namespace SportyBuddies.Application.Features.Profiles.Commands.UploadPhoto;

public record UploadPhotoCommand(Stream File, string FileName) : IRequest<string>;