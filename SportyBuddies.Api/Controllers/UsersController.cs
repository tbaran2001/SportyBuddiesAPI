using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Features.Users.Commands.DeleteUser;
using SportyBuddies.Application.Features.Users.Commands.UpdateUser;
using SportyBuddies.Application.Features.Users.Commands.UpdateUserPreferences;
using SportyBuddies.Application.Features.Users.Queries.GetUser;
using SportyBuddies.Infrastructure.Identity;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserWithSportsResponse>> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);

            var userResult = await mediator.Send(query);

            return Ok(userResult);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserWithSportsResponse>> GetCurrentUser()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserQuery(Guid.Parse(userId));

            var userResult = await mediator.Send(query);

            return Ok(userResult);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserCommand(Guid.Parse(userId), userRequest.Name, userRequest.Description,
                userRequest.Gender, userRequest.DateOfBirth);

            var userResult = await mediator.Send(command);

            return Ok(userResult);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCurrentUser()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new DeleteUserCommand(Guid.Parse(userId));

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut("Preferences")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUserPreferences(UpdateUserPreferencesRequest preferencesRequest)
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