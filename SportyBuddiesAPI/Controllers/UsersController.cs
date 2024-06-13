using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Models;
using SportyBuddiesAPI.Services;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;

        public UsersController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithoutSportsDto>>> GetUsers()
        {
            var users = await _sportyBuddiesRepository.GetUsersAsync();

            return Ok(_mapper.Map<IEnumerable<UserWithoutSportsDto>>(users));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id, bool includeSports = false)
        {
            var user = await _sportyBuddiesRepository.GetUserAsync(id, includeSports);
            if (user == null)
            {
                return NotFound();
            }

            if (includeSports)
            {
                return Ok(_mapper.Map<UserDto>(user));
            }

            return Ok(_mapper.Map<UserWithoutSportsDto>(user));
        }

        [HttpGet("{userId}/sports")]
        public async Task<ActionResult<IEnumerable<SportDto>>> GetUserSports(int userId)
        {
            if (!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound("User not found.");
            }
            
            var sports = await _sportyBuddiesRepository.GetUserSportsAsync(userId);
            
            return Ok(_mapper.Map<IEnumerable<SportDto>>(sports));
        }

        [HttpGet("{userId}/sports/{sportId}")]
        public async Task<ActionResult<SportDto>> GetUserSport(int userId, int sportId)
        {
            if(!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound("User not found.");
            }

            if (!await _sportyBuddiesRepository.SportExistsAsync(sportId))
            {
                return NotFound("Sport not found.");
            }
            
            var userSport = await _sportyBuddiesRepository.GetUserSportAsync(userId, sportId);
            if (userSport == null)
            {
                return NotFound("User does not have this sport.");
            }
            
            return Ok(_mapper.Map<SportDto>(userSport));
        }
        
        [HttpPost("{userId}/sports/{sportId}")]
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
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{userId}/sports/{sportId}")]
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
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}