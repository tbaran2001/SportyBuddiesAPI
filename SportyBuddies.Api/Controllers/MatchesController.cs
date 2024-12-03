using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Matches;
using SportyBuddies.Application.Common.DTOs.Match;
using SportyBuddies.Application.Features.Matches.Commands.UpdateMatch;
using SportyBuddies.Application.Features.Matches.Queries.GetRandomMatch;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MatchesController(ISender mediator) : ControllerBase
    {
        [HttpGet("Random")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MatchResponse>> GetRandomMatch()
        {
            var query = new GetRandomMatchQuery();
            var matchResult = await mediator.Send(query);

            return Ok(matchResult);
        }
        
        [HttpPut("{matchId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateMatch(Guid matchId, UpdateMatchRequest matchRequest)
        {
            var command = new UpdateMatchCommand(matchId, matchRequest.Swipe);

            await mediator.Send(command);

            return NoContent();
        }
    }
}
