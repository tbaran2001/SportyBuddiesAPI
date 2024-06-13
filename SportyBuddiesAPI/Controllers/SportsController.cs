using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Models;
using SportyBuddiesAPI.Services;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;

        public SportsController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDto>>> GetSports()
        {
            var sports = await _sportyBuddiesRepository.GetSportsAsync();

            return Ok(_mapper.Map<IEnumerable<SportDto>>(sports));
        }

        [HttpGet("{sportId}", Name = "GetSport")]
        public async Task<ActionResult<SportDto>> GetSport(int sportId)
        {
            var sport = await _sportyBuddiesRepository.GetSportAsync(sportId);
            if (sport == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<SportDto>(sport));
        }

        [HttpPost]
        public async Task<ActionResult<SportDto>> CreateSport(CreateSportDto sport)
        {
            var sportEntity = _mapper.Map<Entities.Sport>(sport);

            await _sportyBuddiesRepository.AddSportAsync(sportEntity);

            await _sportyBuddiesRepository.SaveChangesAsync();

            var sportToReturn = _mapper.Map<SportDto>(sportEntity);

            return CreatedAtRoute(nameof(GetSport), new {sportId = sportToReturn.Id}, sportToReturn);
        }

        [HttpPut("{sportId}")]
        public async Task<ActionResult> UpdateSport(int sportId, UpdateSportDto sport)
        {
            var sportEntity = await _sportyBuddiesRepository.GetSportAsync(sportId);
            if (sportEntity == null)
            {
                return NotFound();
            }
            
            _mapper.Map(sport, sportEntity);

            await _sportyBuddiesRepository.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpPatch("{sportId}")]
        public async Task<ActionResult> PartiallyUpdateSport(int sportId, JsonPatchDocument<UpdateSportDto> patchDocument)
        {
            var sportEntity = await _sportyBuddiesRepository.GetSportAsync(sportId);
            if (sportEntity == null)
            {
                return NotFound();
            }
            
            var sportToPatch = _mapper.Map<UpdateSportDto>(sportEntity);
            
            patchDocument.ApplyTo(sportToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if(!TryValidateModel(sportToPatch))
            {
                return ValidationProblem(ModelState);
            }
            
            _mapper.Map(sportToPatch, sportEntity);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{sportId}")]
        public async Task<ActionResult> DeleteSport(int sportId)
        {
            var sportEntity = await _sportyBuddiesRepository.GetSportAsync(sportId);
            if (sportEntity == null)
            {
                return NotFound();
            }
            
            _sportyBuddiesRepository.DeleteSport(sportEntity);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
        
    }
}