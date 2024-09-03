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
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public SportsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSports()
        {
            var query = new GetSportsQuery();

            var sports = await _mediator.Send(query);

            return Ok(_mapper.Map<IEnumerable<SportResponse>>(sports));
        }

        [HttpGet("{sportId:guid}")]
        public async Task<IActionResult> GetSport(Guid sportId)
        {
            var query = new GetSportQuery(sportId);

            var sport = await _mediator.Send(query);

            return Ok(_mapper.Map<SportResponse>(sport));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSport(CreateSportRequest request)
        {
            var command = _mapper.Map<CreateSportCommand>(request);
            var createSportResult = await _mediator.Send(command);

            return Ok(_mapper.Map<SportResponse>(createSportResult));
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