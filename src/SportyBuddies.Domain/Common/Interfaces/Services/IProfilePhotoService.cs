using SportyBuddies.Domain.Profiles;

namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IProfilePhotoService
{
    Task<string> UploadAndAssignPhotoAsync(Profile profile, Stream file, string fileName, bool isMain);
}