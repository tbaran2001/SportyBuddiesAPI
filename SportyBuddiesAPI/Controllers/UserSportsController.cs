using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Models;
using SportyBuddiesAPI.Services;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSportsController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;


        public UserSportsController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<SportDto>>> GetUserSports(int userId)
        {
            if (!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound("User not found.");
            }
            
            var sports = await _sportyBuddiesRepository.GetUserSportsAsync(userId);
            
            return Ok(_mapper.Map<IEnumerable<SportDto>>(sports));
        }
        
        [HttpGet("{userId}/{sportId}")]
        public async Task<ActionResult<SportDto>> GetUserSport(int userId, int sportId)
        {
            if (!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound("User not found.");
            }
            
            var sport = await _sportyBuddiesRepository.GetUserSportAsync(userId, sportId);
            if (sport == null)
            {
                return NotFound("Sport not found.");
            }
            
            return Ok(_mapper.Map<SportDto>(sport));
        }
        
        [HttpPost("{userId}/{sportId}")]
        public async Task<ActionResult> AddSportToUser(int userId, int sportId)
        {
            if (!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound("User not found.");
            }

            if (!await _sportyBuddiesRepository.SportExistsAsync(sportId))
            {
                return NotFound("Sport not found.");
            }
            
            if (await _sportyBuddiesRepository.HasSportAsync(userId, sportId))
            {
                return BadRequest("User already has this sport.");
            }

            await _sportyBuddiesRepository.AddSportToUserAsync(userId, sportId);
            await _sportyBuddiesRepository.UpdateUserMatchesAsync(userId);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{userId}/{sportId}")]
        public async Task<ActionResult> RemoveSportFromUser(int userId, int sportId)
        {
            if (!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound("User not found.");
            }

            if (!await _sportyBuddiesRepository.SportExistsAsync(sportId))
            {
                return NotFound("Sport not found.");
            }
            
            if (!await _sportyBuddiesRepository.HasSportAsync(userId, sportId))
            {
                return BadRequest("User does not have this sport.");
            }

            await _sportyBuddiesRepository.RemoveSportFromUserAsync(userId, sportId);
            await _sportyBuddiesRepository.UpdateUserMatchesAsync(userId);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
