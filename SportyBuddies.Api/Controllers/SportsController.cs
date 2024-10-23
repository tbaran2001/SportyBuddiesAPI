using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Application.Sports.Commands.DeleteSport;
using SportyBuddies.Application.Sports.Queries.GetSport;
using SportyBuddies.Application.Sports.Queries.GetSports;
using SportyBuddies.Contracts.Sports;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    public class SportsController(ISender mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetSports()
        {
            var query = new GetSportsQuery();

            var sportsResult = await mediator.Send(query);

            return Ok(sportsResult);
        }

        [HttpGet("{sportId:guid}")]
        public async Task<IActionResult> GetSport(Guid sportId)
        {
            var query = new GetSportQuery(sportId);

            var sportResult = await mediator.Send(query);

            return Ok(sportResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSport(CreateSportRequest request)
        {
            var command = mapper.Map<CreateSportCommand>(request);
            var createSportResult = await mediator.Send(command);

            return CreatedAtAction(nameof(GetSport), new { sportId = createSportResult.Id }, createSportResult);
        }

        [HttpDelete("{sportId:guid}")]
        public async Task<IActionResult> DeleteSport(Guid sportId)
        {
            var command = new DeleteSportCommand(sportId);

            await mediator.Send(command);

            return NoContent();
        }
    }
}