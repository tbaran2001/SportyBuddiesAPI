using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Sports;
using SportyBuddies.Application.Features.Sports.Commands.CreateSport;
using SportyBuddies.Application.Features.Sports.Queries.GetSports;
using SportyBuddies.Application.Features.Users.Queries.GetUsers;
using SportyBuddies.Application.Features.UserSports.Commands.AddUserSport;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController(ISender mediator) : ControllerBase
    {
        [HttpPost("Sports")]
        public async Task<IActionResult> AddSport(CreateSportRequest request)
        {
            var command = new CreateSportCommand(request.Name, request.Description);
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("Sports")]
        public async Task<IActionResult> GetSports()
        {
            var query = new GetSportsQuery();
            var sports = await mediator.Send(query);
            return Ok(sports);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();
            var users = await mediator.Send(query);
            return Ok(users);
        }

        [HttpPost("AddRandomSportsToUsers")]
        public async Task<IActionResult> AddRandomSportsToUsers()
        {
            var usersQuery = new GetUsersQuery();
            var users = await mediator.Send(usersQuery);

            var sportsQuery = new GetSportsQuery();
            var sports = await mediator.Send(sportsQuery);

            var random = new Random();

            foreach (var user in users)
            {
                var assignedSports = new HashSet<Guid>();
                for (int i = 0; i < 5; i++)
                {
                    Guid sportId;
                    do
                    {
                        sportId = sports[random.Next(sports.Count)].Id;
                    } while (assignedSports.Contains(sportId));

                    assignedSports.Add(sportId);

                    var command = new AddUserSportCommand(user.Id, sportId);
                    await mediator.Send(command);
                }
            }

            return NoContent();
        }
    }
}