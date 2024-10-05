using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;
using SportyBuddies.Application.Matches.Queries.GetRandomMatch;
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
using Swipe = SportyBuddies.Domain.Matches.Swipe;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CurrentUserController : ApiController
    {
        private readonly ISender _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public CurrentUserController(UserManager<ApplicationUser> userManager, ISender mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserQuery(Guid.Parse(userId));

            var userResult = await _mediator.Send(query);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserCommand(Guid.Parse(userId), userRequest.Name, userRequest.Description);

            var userResult = await _mediator.Send(command);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("sports")]
        public async Task<IActionResult> GetCurrentUserSports()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserSportsQuery(Guid.Parse(userId));

            var userSportsResult = await _mediator.Send(query);

            return userSportsResult.Match(
                Ok,
                Problem);
        }

        [HttpPost("sports/{sportId}")]
        public async Task<IActionResult> AddSportToCurrentUser(Guid sportId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new AddUserSportCommand(Guid.Parse(userId), sportId);

            var userSportResult = await _mediator.Send(command);

            return userSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpDelete("sports/{sportId}")]
        public async Task<IActionResult> RemoveSportFromCurrentUser(Guid sportId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new RemoveUserSportCommand(Guid.Parse(userId), sportId);

            var userSportResult = await _mediator.Send(command);

            return userSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpGet("matches")]
        public async Task<IActionResult> GetCurrentUserMatches()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMatchesQuery(Guid.Parse(userId));

            var matchesResult = await _mediator.Send(query);

            return matchesResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("matches/random")]
        public async Task<IActionResult> GetRandomMatch()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetRandomMatchQuery(Guid.Parse(userId));

            var matchResult = await _mediator.Send(query);

            return matchResult.Match(
                Ok,
                Problem);
        }

        [HttpPut("matches/{matchId}")]
        public async Task<IActionResult> UpdateMatch(Guid matchId, UpdateMatchRequest matchRequest)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateMatchCommand(matchId, (Swipe)matchRequest.Swipe);

            var matchResult = await _mediator.Send(command);

            return matchResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }
    }
}