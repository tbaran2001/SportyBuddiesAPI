using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SportyBuddies.Application.Common.Services;

namespace SportyBuddies.Infrastructure.Services;

public class FileStorageService(IWebHostEnvironment webHostEnvironment) : IFileStorageService
{
    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var uploadsFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolderPath)) Directory.CreateDirectory(uploadsFolderPath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolderPath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return fileName;
    }
}