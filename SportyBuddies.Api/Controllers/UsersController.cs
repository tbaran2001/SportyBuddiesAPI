using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Application.Users.Commands.DeleteUser;
using SportyBuddies.Application.Users.Commands.UpdateUser;
using SportyBuddies.Application.Users.Commands.UpdateUserPreferences;
using SportyBuddies.Application.Users.Queries.GetUser;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class UsersController(UserManager<ApplicationUser> userManager, IMapper mapper, ISender mediator)
        : ControllerBase
    {
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);

            var userResult = await mediator.Send(query);

            return Ok(userResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserQuery(Guid.Parse(userId));

            var userResult = await mediator.Send(query);

            return Ok(userResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserCommand(Guid.Parse(userId), userRequest.Name, userRequest.Description,
                userRequest.Gender, userRequest.DateOfBirth);

            var userResult = await mediator.Send(command);

            return Ok(userResult);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCurrentUser(Guid userId)
        {
            var command = new DeleteUserCommand(userId);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut("Preferences")]
        public async Task<IActionResult> UpdateUserPreferences(UpdateUserPreferencesRequest preferencesRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserPreferencesCommand(Guid.Parse(userId), preferencesRequest.MinAge,
                preferencesRequest.MaxAge, preferencesRequest.MaxDistance, preferencesRequest.Gender);

            await mediator.Send(command);

            return NoContent();
        }
    }
}