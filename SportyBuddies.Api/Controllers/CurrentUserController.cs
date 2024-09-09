using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Matches.Queries.GetUserMatches;
using SportyBuddies.Application.Users.Commands.UpdateUser;
using SportyBuddies.Application.Users.Queries.GetUser;
using SportyBuddies.Application.UserSports.Commands.AddUserSport;
using SportyBuddies.Application.UserSports.Commands.RemoveUserSport;
using SportyBuddies.Application.UserSports.Queries.GetUserSports;
using SportyBuddies.Contracts.Matches;
using SportyBuddies.Contracts.Sports;
using SportyBuddies.Contracts.Users;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrentUserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserController(UserManager<ApplicationUser> userManager, ISender mediator, IMapper mapper)
        {
            _userManager = userManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetUserQuery(Guid.Parse(userId));

            var user = await _mediator.Send(query);

            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new UpdateUserCommand(Guid.Parse(userId), userRequest.Name, userRequest.Description);

            var user = await _mediator.Send(command);

            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpGet("sports")]
        public async Task<IActionResult> GetCurrentUserSports()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetUserSportsQuery(Guid.Parse(userId));

            var userSports = await _mediator.Send(query);

            return Ok(_mapper.Map<IEnumerable<SportResponse>>(userSports));
        }

        [HttpPost("sports/{sportId}")]
        public async Task<IActionResult> AddSportToCurrentUser(Guid sportId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new AddUserSportCommand(Guid.Parse(userId), sportId);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("sports/{sportId}")]
        public async Task<IActionResult> RemoveSportFromCurrentUser(Guid sportId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new RemoveUserSportCommand(Guid.Parse(userId), sportId);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("matches")]
        public async Task<IActionResult> GetCurrentUserMatches()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMatchesQuery(Guid.Parse(userId));

            var matches = await _mediator.Send(query);

            return Ok(_mapper.Map<IEnumerable<MatchResponse>>(matches));
        }
    }
}