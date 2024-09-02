using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Sports.Commands.CreateSport;
using SportyBuddies.Application.Sports.Commands.DeleteSport;
using SportyBuddies.Application.Sports.Queries.GetSport;
using SportyBuddies.Contracts.Sports;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ISender _mediator;

        public SportsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSport(CreateSportRequest request)
        {
            var command = new CreateSportCommand(request.Name, request.Description);
            var createSportResult = await _mediator.Send(command);

            return Ok(new SportResponse(createSportResult.Id, request.Name, request.Description));
        }

        [HttpGet("{sportId:guid}")]
        public async Task<IActionResult> GetSport(Guid sportId)
        {
            var query = new GetSportQuery(sportId);

            var sport = await _mediator.Send(query);

            return Ok(new SportResponse(sport.Id, sport.Name, sport.Description));
        }

        [HttpDelete("{sportId:guid}")]
        public async Task<IActionResult> DeleteSport(Guid sportId)
        {
            var command = new DeleteSportCommand(sportId);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}