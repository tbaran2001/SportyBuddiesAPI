using Microsoft.AspNetCore.Http;

namespace SportyBuddies.Application.Common.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Guid userId, IFormFile file, Guid photoId);
}