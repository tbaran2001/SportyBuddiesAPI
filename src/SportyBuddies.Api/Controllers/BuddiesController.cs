using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetProfileBuddies;

namespace SportyBuddies.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BuddiesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BuddyResponse>>> GetProfileBuddies()
    {
        var query = new GetProfileBuddiesQuery();

        var profileBuddiesResult = await mediator.Send(query);

        return Ok(profileBuddiesResult);
    }
}