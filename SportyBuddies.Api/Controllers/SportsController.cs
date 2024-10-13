using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Application.Sports.Commands.DeleteSport;
using SportyBuddies.Application.Sports.Queries.GetSport;
using SportyBuddies.Application.Sports.Queries.GetSports;
using SportyBuddies.Contracts.Sports;
using SportyBuddies.Domain.SportAggregate.ValueObjects;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    public class SportsController(ISender mediator, IMapper mapper) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSports()
        {
            var query = new GetSportsQuery();

            var sportsResult = await mediator.Send(query);

            return sportsResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("{sportId:guid}")]
        public async Task<IActionResult> GetSport(Guid sportId)
        {
            var query = new GetSportQuery(SportId.Create(sportId));

            var sportResult = await mediator.Send(query);

            return sportResult.Match(
                Ok,
                Problem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSport(CreateSportRequest request)
        {
            var command = mapper.Map<CreateSportCommand>(request);
            var createSportResult = await mediator.Send(command);

            return createSportResult.Match(
                sport => CreatedAtAction(nameof(GetSport), new { sportId = sport.Id }, sport),
                Problem);
        }

        [HttpDelete("{sportId:guid}")]
        public async Task<IActionResult> DeleteSport(Guid sportId)
        {
            var command = new DeleteSportCommand(SportId.Create(sportId));

            var deleteSportResult = await mediator.Send(command);

            return deleteSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }
    }
}