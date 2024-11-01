using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Buddies.Queries.GetUserBuddies;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BuddiesController(UserManager<ApplicationUser> userManager, ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUserBuddies()
    {
        var userId = userManager.GetUserId(User);
        if (userId == null) return Unauthorized();

        var query = new GetUserBuddiesQuery(Guid.Parse(userId));

        var userBuddiesResult = await mediator.Send(query);

        return Ok(userBuddiesResult);
    }
}