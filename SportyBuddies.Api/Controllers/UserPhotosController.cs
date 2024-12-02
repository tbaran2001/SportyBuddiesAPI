using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Features.Users.Commands.UploadPhoto;
using SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;
using SportyBuddies.Infrastructure.Identity;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserPhotosController(UserManager<ApplicationUser> userManager, ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file, bool isMain = false)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UploadPhotoCommand(Guid.Parse(userId), isMain, file);

            var photoResult = await mediator.Send(command);

            return Ok(photoResult);
        }

        [HttpGet("Main")]
        public async Task<IActionResult> GetUserMainPhoto()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMainPhotoQuery(Guid.Parse(userId));

            var photoResult = await mediator.Send(query);

            var stream = System.IO.File.OpenRead(photoResult);
            return File(stream, "image/jpeg");
        }
    }
}
