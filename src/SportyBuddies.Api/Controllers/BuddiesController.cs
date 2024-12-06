using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;

namespace SportyBuddies.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BuddiesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BuddyResponse>>> GetUserBuddies()
    {
        var query = new GetUserBuddiesQuery();

        var userBuddiesResult = await mediator.Send(query);

        return Ok(userBuddiesResult);
    }
}