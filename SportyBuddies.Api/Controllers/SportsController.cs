using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Features.Sports.Queries.GetSports;
using SportyBuddies.Application.Features.UserSports.Commands.AddUserSport;
using SportyBuddies.Application.Features.UserSports.Commands.RemoveUserSport;
using SportyBuddies.Application.Features.UserSports.Queries.GetUserSports;
using SportyBuddies.Infrastructure.Identity;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SportsController(UserManager<ApplicationUser> userManager, ISender mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SportResponse>>> GetSports()
        {
            var query = new GetSportsQuery();

            var sportsResult = await mediator.Send(query);

            return Ok(sportsResult);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SportResponse>>> GetUserSports()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserSportsQuery(Guid.Parse(userId));

            var userSportsResult = await mediator.Send(query);

            return Ok(userSportsResult);
        }

        [HttpPost("{sportId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddSportToUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new AddUserSportCommand(Guid.Parse(userId), sportId);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{sportId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveSportFromUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new RemoveUserSportCommand(Guid.Parse(userId), sportId);

            await mediator.Send(command);

            return NoContent();
        }
    }
}