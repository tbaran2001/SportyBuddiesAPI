using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Common.DTOs.Buddy;
using SportyBuddies.Application.Features.Buddies.Queries.GetUserBuddies;
using SportyBuddies.Infrastructure.Identity;

namespace SportyBuddies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuddiesController(UserManager<ApplicationUser> userManager, ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<BuddyResponse>>> GetUserBuddies()
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Unauthorized();

        var query = new GetUserBuddiesQuery(Guid.Parse(userId));

        var userBuddiesResult = await mediator.Send(query);

        return Ok(userBuddiesResult);
    }
}