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
    [Route("api/[controller]")]
    public class UsersController(IMapper mapper, ISender mediator) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();

            var usersResult = await mediator.Send(query);

            return usersResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);

            var userResult = await mediator.Send(query);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = mapper.Map<CreateUserCommand>(request);

            var createUserResult = await mediator.Send(command);

            return createUserResult.Match(
                user => CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user),
                Problem);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var command = new DeleteUserCommand(userId);

            var deleteUserResult = await mediator.Send(command);

            return deleteUserResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }
    }
}