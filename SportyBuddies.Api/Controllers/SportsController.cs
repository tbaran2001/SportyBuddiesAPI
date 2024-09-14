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
    public class SportsController : ApiController
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

            var sportsResult = await _mediator.Send(query);

            return sportsResult.Match(
                sports => Ok(_mapper.Map<IEnumerable<SportResponse>>(sports)),
                Problem);
        }

        [HttpGet("{sportId:guid}")]
        public async Task<IActionResult> GetSport(Guid sportId)
        {
            var query = new GetSportQuery(sportId);

            var sportResult = await _mediator.Send(query);

            return sportResult.Match(
                sport => Ok(_mapper.Map<SportResponse>(sport)),
                Problem);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSport(CreateSportRequest request)
        {
            var command = _mapper.Map<CreateSportCommand>(request);
            var createSportResult = await _mediator.Send(command);

            return createSportResult.Match(
                sport => CreatedAtAction(nameof(GetSport), new { sportId = sport.Id },
                    _mapper.Map<SportResponse>(sport)),
                Problem);
        }

        [HttpDelete("{sportId:guid}")]
        public async Task<IActionResult> DeleteSport(Guid sportId)
        {
            var command = new DeleteSportCommand(sportId);

            var deleteSportResult = await _mediator.Send(command);

            return deleteSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }
    }
}