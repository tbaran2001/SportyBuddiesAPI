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
using SportyBuddies.Contracts.Users;
using SportyBuddies.Domain.MatchAggregate.ValueObjects;
using SportyBuddies.Domain.SportAggregate.ValueObjects;
using SportyBuddies.Domain.UserAggregate.ValueObjects;
using SportyBuddies.Identity.Models;
using Swipe = SportyBuddies.Domain.MatchAggregate.Swipe;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CurrentUserController(UserManager<ApplicationUser> userManager, ISender mediator) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserQuery(UserId.Create(Guid.Parse(userId)));

            var userResult = await mediator.Send(query);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserCommand(UserId.Create(Guid.Parse(userId)), userRequest.Name,
                userRequest.Description);

            var userResult = await mediator.Send(command);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("sports")]
        public async Task<IActionResult> GetCurrentUserSports()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserSportsQuery(UserId.Create(Guid.Parse(userId)));

            var userSportsResult = await mediator.Send(query);

            return userSportsResult.Match(
                Ok,
                Problem);
        }

        [HttpPost("sports/{sportId}")]
        public async Task<IActionResult> AddSportToCurrentUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new AddUserSportCommand(UserId.Create(Guid.Parse(userId)), SportId.Create(sportId));

            var userSportResult = await mediator.Send(command);

            return userSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpDelete("sports/{sportId}")]
        public async Task<IActionResult> RemoveSportFromCurrentUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new RemoveUserSportCommand(UserId.Create(Guid.Parse(userId)), SportId.Create(sportId));

            var userSportResult = await mediator.Send(command);

            return userSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpGet("matches")]
        public async Task<IActionResult> GetCurrentUserMatches()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMatchesQuery(UserId.Create(Guid.Parse(userId)));

            var matchesResult = await mediator.Send(query);

            return matchesResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("matches/random")]
        public async Task<IActionResult> GetRandomMatch()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetRandomMatchQuery(UserId.Create(Guid.Parse(userId)));

            var matchResult = await mediator.Send(query);

            return matchResult.Match(
                Ok,
                Problem);
        }

        [HttpPut("matches/{matchId}")]
        public async Task<IActionResult> UpdateMatch(Guid matchId, UpdateMatchRequest matchRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateMatchCommand(MatchId.Create(matchId), (Swipe)matchRequest.Swipe);

            var matchResult = await mediator.Send(command);

            return matchResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }
    }
}