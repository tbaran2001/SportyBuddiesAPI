using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using SportyBuddies.Domain.Common.Interfaces.Services;

namespace SportyBuddies.Infrastructure.BlobStorage;

public class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

    public async Task<string> UploadToBlobAsync(Stream file, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ProfilePicturesContainer);

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(file, true);

        return $"{fileName}";
    }

    public async Task<bool> DeleteBlobAsync(string blobUrl)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ProfilePicturesContainer);

        var blobClient = containerClient.GetBlobClient(blobUrl);

        var response= await blobClient.DeleteIfExistsAsync();

        return response.Value;
    }
}