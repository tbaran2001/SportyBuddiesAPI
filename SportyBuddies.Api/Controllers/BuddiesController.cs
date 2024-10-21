using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Buddies.Queries.GetUserBuddies;

namespace SportyBuddies.Api.Controllers;

[Route("api/[controller]")]
public class BuddiesController(ISender mediator, IMapper mapper) : ApiController
{
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserBuddies(Guid userId, bool includeUsers = false)
    {
        var query = new GetUserBuddiesQuery(userId, includeUsers);

        var buddiesWithUsersResult = await mediator.Send(query);

        return buddiesWithUsersResult.Match(
            Ok,
            Problem);
    }
}