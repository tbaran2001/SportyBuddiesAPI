namespace SportyBuddies.Domain.Common.Interfaces.Services;

public interface IBlobStorageService
{
    Task<string> UploadToBlobAsync(Stream file, string fileName);
    Task<bool> DeleteBlobAsync(string blobUrl);
    string? GetBlobSasUrl(string? blobUrl);
}