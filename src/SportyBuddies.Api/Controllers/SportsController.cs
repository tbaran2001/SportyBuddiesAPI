using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Features.ProfileSports.Commands.AddProfileSport;
using SportyBuddies.Application.Features.ProfileSports.Commands.RemoveProfileSport;
using SportyBuddies.Application.Features.ProfileSports.Queries.GetProfileSports;
using SportyBuddies.Application.Features.Sports.Queries.GetSports;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SportsController(ISender mediator) : ControllerBase
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
        public async Task<ActionResult<IEnumerable<SportResponse>>> GetProfileSports()
        {
            var query = new GetProfileSportsQuery();

            var profileSportsResult = await mediator.Send(query);

            return Ok(profileSportsResult);
        }

        [HttpPost("{sportId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddSportToProfile(Guid sportId)
        {
            var command = new AddProfileSportCommand(sportId);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{sportId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveSportFromProfile(Guid sportId)
        {
            var command = new RemoveProfileSportCommand(sportId);

            await mediator.Send(command);

            return NoContent();
        }
    }
}