using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Features.Users.Commands.UploadPhoto;
using SportyBuddies.Application.Features.Users.Queries.GetUserMainPhoto;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserPhotosController(ISender mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file, bool isMain = false)
        {
            var command = new UploadPhotoCommand(isMain, file);

            var photoResult = await mediator.Send(command);

            return Ok(photoResult);
        }

        [HttpGet("Main")]
        public async Task<IActionResult> GetUserMainPhoto()
        {
            var query = new GetUserMainPhotoQuery();

            var photoResult = await mediator.Send(query);

            var stream = System.IO.File.OpenRead(photoResult);
            return File(stream, "image/jpeg");
        }
    }
}