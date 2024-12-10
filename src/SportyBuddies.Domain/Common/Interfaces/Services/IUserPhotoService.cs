using SportyBuddies.Domain.Users;

namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IUserPhotoService
{
    Task<string> UploadAndAssignPhotoAsync(User user, Stream file, string fileName, bool isMain);
}