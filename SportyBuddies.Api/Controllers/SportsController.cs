using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Sports.Commands;
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
            var command = new CreateSportCommand(request.SportType.ToString(), request.Name, request.Description,
                request.AdminId);

            var sportId = await _mediator.Send(command);

            var response = new SportResponse(sportId, request.SportType, request.Name, request.Description);

            return Ok(response);
        }
    }
}