using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Services;

public class UserPhotoService(IBlobStorageService blobStorageService, IUsersRepository usersRepository)
    : IUserPhotoService
{
    public async Task<string> UploadAndAssignPhotoAsync(User user, Stream file, string fileName, bool isMain)
    {
        if (isMain && user.MainPhotoUrl != null)
        {
            var response= await blobStorageService.DeleteBlobAsync(user.MainPhotoUrl);
            if (!response)
                throw new Exception("Error deleting main photo");
            user.RemoveMainPhoto();
        }

        var userFolder= user.Id.ToString();
        var fullFileName = $"{userFolder}/{fileName}";

        var photoUrl = await blobStorageService.UploadToBlobAsync(file, fullFileName);

        user.AddMainPhoto(photoUrl);

        return photoUrl;
    }
}