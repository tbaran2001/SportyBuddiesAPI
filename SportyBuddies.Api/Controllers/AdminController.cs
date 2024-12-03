using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Sports;
using SportyBuddies.Application.Common.DTOs.Sport;
using SportyBuddies.Application.Common.DTOs.User;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddSport(CreateSportRequest request)
        {
            var command = new CreateSportCommand(request.Name, request.Description);
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("Sports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SportResponse>>> GetSports()
        {
            var query = new GetSportsQuery();
            var sports = await mediator.Send(query);
            return Ok(sports);
        }

        [HttpGet("Users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserWithSportsResponse>>> GetUsers()
        {
            var query = new GetUsersQuery();
            var users = await mediator.Send(query);
            return Ok(users);
        }

        /*[HttpPost("AddRandomSportsToUsers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddRandomSportsToUsers()
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
        }*/
    }
}