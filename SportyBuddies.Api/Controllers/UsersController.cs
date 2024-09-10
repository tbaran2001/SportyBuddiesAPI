using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Users.Commands.CreateUser;
using SportyBuddies.Application.Users.Commands.DeleteUser;
using SportyBuddies.Application.Users.Queries;
using SportyBuddies.Application.Users.Queries.GetUser;
using SportyBuddies.Application.Users.Queries.GetUsers;
using SportyBuddies.Contracts.Users;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
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
            
            var users = await _mediator.Send(query);
            
            return Ok(_mapper.Map<IEnumerable<UserResponse>>(users));
        }
        
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);
            
            var user = await _mediator.Send(query);
            
            return Ok(_mapper.Map<UserResponse>(user));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            
            var createUserResult = await _mediator.Send(command);
            
            return Ok(_mapper.Map<UserResponse>(createUserResult));
        }
        
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var command = new DeleteUserCommand(userId);
            
            await _mediator.Send(command);
            
            return NoContent();
        }
    }
}
