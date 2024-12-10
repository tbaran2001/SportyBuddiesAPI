using SportyBuddies.Domain.Common.Interfaces.Repositories;
using SportyBuddies.Domain.Common.Interfaces.Services;
using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Services;

public class UserPhotoService(IBlobStorageService blobStorageService, IUsersRepository usersRepository)
    : IUserPhotoService
{
    public async Task<string> UploadAndAssignPhotoAsync(User user, Stream file, string fileName, bool isMain)
    {
        if (isMain && user.MainPhoto != null)
        {
            var response= await blobStorageService.DeleteBlobAsync(user.MainPhoto.Url);
            if (!response)
                throw new Exception("Error deleting main photo");
            usersRepository.DeletePhotoAsync(user.MainPhoto);
            user.RemoveMainPhoto();
        }

        var userFolder= user.Id.ToString();
        var fullFileName = $"{userFolder}/{fileName}";

        var photoUrl = await blobStorageService.UploadToBlobAsync(file, fullFileName);

        var userPhoto = UserPhoto.Create(user, photoUrl, isMain);
        user.AddPhoto(userPhoto);

        await usersRepository.AddPhotoAsync(userPhoto);

        return photoUrl;
    }
}