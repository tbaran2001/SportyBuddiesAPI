using Microsoft.AspNetCore.Http;

namespace SportyBuddies.Application.Common.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file);
}