using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Users.Commands.CreateUser;
using SportyBuddies.Application.Users.Commands.DeleteUser;
using SportyBuddies.Application.Users.Queries.GetUser;
using SportyBuddies.Application.Users.Queries.GetUsers;
using SportyBuddies.Contracts.Users;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMapper mapper, ISender mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers(bool includeSports = false)
        {
            var query = new GetUsersQuery(includeSports);

            var usersResult = await mediator.Send(query);

            return Ok(usersResult);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);

            var userResult = await mediator.Send(query);

            return Ok(userResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = mapper.Map<CreateUserCommand>(request);

            var createUserResult = await mediator.Send(command);

            return CreatedAtAction(nameof(GetUser), new { userId = createUserResult.Id }, createUserResult);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var command = new DeleteUserCommand(userId);

            await mediator.Send(command);

            return NoContent();
        }
    }
}