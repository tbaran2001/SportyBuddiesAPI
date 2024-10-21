using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Application.Users.Commands.UploadPhoto;

public class UploadPhotoCommandHandler(IFileStorageService fileStorageService)
    : IRequestHandler<UploadPhotoCommand, ErrorOr<string>>
{
    public async Task<ErrorOr<string>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
    {
        var url = await fileStorageService.SaveFileAsync(request.File);
        return url;
    }
}