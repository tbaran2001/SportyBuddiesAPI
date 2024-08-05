using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Entities;
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
        private readonly UserManager<User> _userManager;
        const int maxPageSize = 20;

        public UsersController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper,
            UserManager<User> userManager)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithoutSportsDto>>> GetUsers(string? name, string? searchQuery,
            int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var (users, paginationMetaData) =
                await _sportyBuddiesRepository.GetUsersAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

            return Ok(_mapper.Map<IEnumerable<UserWithoutSportsDto>>(users));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(string userId, bool includeSports = false)
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