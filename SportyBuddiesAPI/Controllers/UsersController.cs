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
        public ActionResult<UserDto> GetUser(int id)
        {
            var user = SportyBuddiesDataStore.Current.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("{userId}/sports")]
        public ActionResult<IEnumerable<SportDto>> GetUserSports(int userId)
        {
            var user = SportyBuddiesDataStore.Current.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var userSports = SportyBuddiesDataStore.Current.UserSports.Where(us => us.UserId == userId).ToList();
            var sports = new List<SportDto>();
            foreach (var userSport in userSports)
            {
                var sport = SportyBuddiesDataStore.Current.Sports.FirstOrDefault(s => s.Id == userSport.SportId);
                if (sport != null)
                {
                    sports.Add(sport);
                }
            }

            return Ok(sports);
        }

        [HttpGet("{userId}/sports/{sportId}")]
        public ActionResult<SportDto> GetUserSport(int userId, int sportId)
        {
            var user = SportyBuddiesDataStore.Current.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var userSport =
                SportyBuddiesDataStore.Current.UserSports.FirstOrDefault(us =>
                    us.UserId == userId && us.SportId == sportId);
            if (userSport == null)
            {
                return NotFound();
            }

            var sport = SportyBuddiesDataStore.Current.Sports.FirstOrDefault(s => s.Id == sportId);
            if (sport == null)
            {
                return NotFound();
            }

            return Ok(sport);
        }
    }
}