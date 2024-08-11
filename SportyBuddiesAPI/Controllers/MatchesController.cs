using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Models;
using SportyBuddiesAPI.Services;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;

        public MatchesController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatches()
        {
            var matches = await _sportyBuddiesRepository.GetMatchesAsync();
            
            return Ok(_mapper.Map<IEnumerable<MatchDto>>(matches));
        }
        
        [HttpGet("{matchId}")]
        public async Task<ActionResult<MatchDto>> GetMatch(int matchId)
        {
            var match = await _sportyBuddiesRepository.GetMatchAsync(matchId);
            
            if(match == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<MatchDto>(match));
        }
        
        [HttpGet("{userId}/{matchedUserId}")]
        public async Task<ActionResult<MatchDto>> GetMatch(string userId, string matchedUserId)
        {
            if(!await _sportyBuddiesRepository.UserExistsAsync(userId) ||
               !await _sportyBuddiesRepository.UserExistsAsync(matchedUserId))
            {
                return NotFound();
            }
            
            var match = await _sportyBuddiesRepository.GetMatchAsync(userId, matchedUserId);
            
            if(match == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<MatchDto>(match));
        }
        
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetUserMatches(string userId)
        {
            if(!await _sportyBuddiesRepository.UserExistsAsync(userId))
            {
                return NotFound();
            }
            
            var matches = await _sportyBuddiesRepository.GetUserMatchesAsync(userId);
            
            return Ok(_mapper.Map<IEnumerable<MatchDto>>(matches));
        }

        [HttpPut("{userId}/{matchedUserId}")]
        public async Task<ActionResult> UpdateMatch(string userId, string matchedUserId, MatchForUpdateDto matchForUpdate)
        {
            var matchEntity = await _sportyBuddiesRepository.GetMatchAsync(userId, matchedUserId);
            if (matchEntity == null)
            {
                return NotFound();
            }
            
            _mapper.Map(matchForUpdate, matchEntity);
            
            await _sportyBuddiesRepository.SaveChangesAsync();
            
            return NoContent();
        }

    }
}