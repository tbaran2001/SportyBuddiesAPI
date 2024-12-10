namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(Stream file, string fileName);
}