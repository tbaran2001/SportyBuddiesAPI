using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Matches;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;
using SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;
using SportyBuddies.Infrastructure.Identity;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MatchesController(UserManager<ApplicationUser> userManager, ISender mediator) : ControllerBase
    {
        [HttpGet("Random")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MatchResponse>> GetRandomMatch()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetRandomMatchQuery(Guid.Parse(userId));
            var matchResult = await mediator.Send(query);

            return Ok(matchResult);
        }
        
        [HttpPut("{matchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateMatch(Guid matchId, UpdateMatchRequest matchRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateMatchCommand(matchId, matchRequest.Swipe);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
