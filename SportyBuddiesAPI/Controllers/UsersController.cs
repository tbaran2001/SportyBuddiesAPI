using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        const int MaxPageSize = 20;

        public UsersController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserWithoutSportsDto>>> GetUsers(string? name, string? searchQuery,
            int pageNumber = 1, int pageSize = 10, bool includeSports = true)
        {
            if (pageSize > MaxPageSize)
            {
                pageSize = MaxPageSize;
            }

            var (users, paginationMetaData) =
                await _sportyBuddiesRepository.GetUsersAsync(name, searchQuery, pageNumber, pageSize, includeSports);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            
            if (includeSports)
            {
                return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
            }

            return Ok(_mapper.Map<IEnumerable<UserWithoutSportsDto>>(users));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string userId, bool includeSports = true)
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

        [HttpPost]
        public async Task<ActionResult<UserWithoutSportsDto>> CreateUser(UserForCreationDto userForCreation)
        {
            var userEntity = _mapper.Map<Entities.User>(userForCreation);

            await _sportyBuddiesRepository.AddUserAsync(userEntity);
            await _sportyBuddiesRepository.SaveChangesAsync();

            var userToReturn = _mapper.Map<UserWithoutSportsDto>(userEntity);

            return CreatedAtRoute(nameof(GetUser), new { userId = userToReturn.Id }, userToReturn);
        }
    }
}