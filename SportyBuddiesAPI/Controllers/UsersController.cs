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
        public async Task<ActionResult<IEnumerable<UserWithoutSportsDto>>> GetUsers(string? name, string? searchQuery)
        {
            var users = await _sportyBuddiesRepository.GetUsersAsync(name,searchQuery);

            return Ok(_mapper.Map<IEnumerable<UserWithoutSportsDto>>(users));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId, bool includeSports = false)
        {
            var user = await _sportyBuddiesRepository.GetUserAsync(userId, includeSports);
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

        
    }
}