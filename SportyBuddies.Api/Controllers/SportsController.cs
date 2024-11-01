using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Sports.Queries.GetSports;
using SportyBuddies.Application.UserSports.Commands.AddUserSport;
using SportyBuddies.Application.UserSports.Commands.RemoveUserSport;
using SportyBuddies.Application.UserSports.Queries.GetUserSports;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SportsController(UserManager<ApplicationUser> userManager, ISender mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetSports()
        {
            var query = new GetSportsQuery();

            var sportsResult = await mediator.Send(query);

            return Ok(sportsResult);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserSports()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserSportsQuery(Guid.Parse(userId));

            var userSportsResult = await mediator.Send(query);

            return Ok(userSportsResult);
        }

        [HttpPost("{sportId:guid}")]
        public async Task<IActionResult> AddSportToUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new AddUserSportCommand(Guid.Parse(userId), sportId);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{sportId:guid}")]
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