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
    public class UsersController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public UsersController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();

            var usersResult = await _mediator.Send(query);

            return usersResult.Match(
                users => Ok(_mapper.Map<IEnumerable<UserResponse>>(users)),
                Problem);
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);

            var userResult = await _mediator.Send(query);

            return userResult.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                Problem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);

            var createUserResult = await _mediator.Send(command);

            return createUserResult.Match(
                user => Ok(_mapper.Map<UserResponse>(user)),
                Problem);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var command = new DeleteUserCommand(userId);

            var deleteUserResult = await _mediator.Send(command);

            return deleteUserResult.Match<IActionResult>(
                _ => Ok(),
                Problem);
        }
    }
}