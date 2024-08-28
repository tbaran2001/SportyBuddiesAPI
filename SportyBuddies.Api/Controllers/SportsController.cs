using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Sports.Commands;
using SportyBuddies.Contracts.Sports;
using ErrorOr;

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
            var command = new CreateSportCommand(request.SportType.ToString(), request.Name, request.Description,
                request.AdminId);

            var createSportResult = await _mediator.Send(command);

            return createSportResult.Match(
                sport => Ok(new SportResponse(sport.Id, request.SportType, request.Name, request.Description)),
                error => Problem());
        }
    }
}