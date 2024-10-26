using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Infrastructure.Services;

public class FileStorageService(IWebHostEnvironment webHostEnvironment)
    : IFileStorageService
{
    public async Task<string> SaveFileAsync(Guid userId, IFormFile file, Guid photoId)
    {
        var uploadsFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads", userId.ToString());
        if (!Directory.Exists(uploadsFolderPath))
            Directory.CreateDirectory(uploadsFolderPath);

        var fileName = $"{photoId}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolderPath, fileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filePath;
    }
}