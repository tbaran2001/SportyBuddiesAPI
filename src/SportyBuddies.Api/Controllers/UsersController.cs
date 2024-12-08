using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Users;
using SportyBuddies.Application.Common.DTOs.User;
using SportyBuddies.Application.Features.Users.Commands.DeleteUser;
using SportyBuddies.Application.Features.Users.Commands.UpdateUser;
using SportyBuddies.Application.Features.Users.Commands.UpdateUserPreferences;
using SportyBuddies.Application.Features.Users.Queries.GetCurrentUser;
using SportyBuddies.Application.Features.Users.Queries.GetUser;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController(ISender mediator)
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
            var query = new GetCurrentUserQuery();

            var userResult = await mediator.Send(query);

            return Ok(userResult);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserResponse>> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var command = new UpdateUserCommand(userRequest.Name, userRequest.Description,
                userRequest.Gender, userRequest.DateOfBirth);

            var userResult = await mediator.Send(command);

            return Ok(userResult);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCurrentUser()
        {
            var command = new DeleteUserCommand();

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut("Preferences")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateUserPreferences(UpdateUserPreferencesRequest preferencesRequest)
        {
            var command = new UpdateUserPreferencesCommand(preferencesRequest.MinAge,
                preferencesRequest.MaxAge, preferencesRequest.MaxDistance, preferencesRequest.Gender);

            await mediator.Send(command);

            return NoContent();
        }
    }
}