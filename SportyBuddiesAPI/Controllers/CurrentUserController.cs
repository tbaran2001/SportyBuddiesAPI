using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Entities;
using SportyBuddiesAPI.Models;
using SportyBuddiesAPI.Services;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentUserController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CurrentUserController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _sportyBuddiesRepository.GetUserAsync(userId, true);
            return Ok(_mapper.Map<UserCurrentDto>(user));
        }

        [HttpPatch]
        [Authorize]
        public async Task<ActionResult<UserDto>> UpdateCurrentUser(JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var userEntity = await _sportyBuddiesRepository.GetUserAsync(userId, true);
            if (userEntity == null)
            {
                return NotFound();
            }

            var userToPatch = _mapper.Map<UserForUpdateDto>(userEntity);
            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userToPatch, userEntity);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("sports")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SportDto>>> GetUserSports()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var sports = await _sportyBuddiesRepository.GetUserSportsAsync(userId);
            return Ok(_mapper.Map<IEnumerable<SportDto>>(sports));
        }
        
        [HttpPost("sports/{sportId}")]
        [Authorize]
        public async Task<ActionResult> AddSportToUser(int sportId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!await _sportyBuddiesRepository.SportExistsAsync(sportId))
            {
                return NotFound();
            }

            if (await _sportyBuddiesRepository.HasSportAsync(userId, sportId))
            {
                return BadRequest("User already has this sport");
            }

            await _sportyBuddiesRepository.AddSportToUserAsync(userId, sportId);
            await _sportyBuddiesRepository.UpdateUserMatchesAsync(userId);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("sports/{sportId}")]
        [Authorize]
        public async Task<ActionResult> RemoveSportFromUser(int sportId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            if (!await _sportyBuddiesRepository.SportExistsAsync(sportId))
            {
                return NotFound();
            }

            if (!await _sportyBuddiesRepository.HasSportAsync(userId, sportId))
            {
                return BadRequest("User does not have this sport");
            }

            await _sportyBuddiesRepository.RemoveSportFromUserAsync(userId, sportId);
            await _sportyBuddiesRepository.UpdateUserMatchesAsync(userId);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpGet("matches")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetUserMatches()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var matches = await _sportyBuddiesRepository.GetUserMatchesAsync(userId);
            return Ok(_mapper.Map<IEnumerable<MatchDto>>(matches));
        }
        
        [HttpGet("matches/random")]
        [Authorize]
        public async Task<ActionResult<MatchDto>> GetRandomMatch()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var match = await _sportyBuddiesRepository.GetRandomMatchAsync(userId);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MatchDto>(match));
        }
        
        [HttpPut("matches/{matchId}")]
        [Authorize]
        public async Task<ActionResult> UpdateMatch(int matchId, MatchForUpdateDto matchForUpdateDto)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var match = await _sportyBuddiesRepository.GetMatchAsync(matchId);
            if (match == null)
            {
                return NotFound();
            }

            if (match.User.Id != userId)
            {
                return Unauthorized();
            }

            _mapper.Map(matchForUpdateDto, match);
            await _sportyBuddiesRepository.SaveChangesAsync();

            return NoContent();
        }
        
    }
}