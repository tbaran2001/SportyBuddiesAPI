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
    public class AccountController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository ??
                                       throw new ArgumentNullException(nameof(sportyBuddiesRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _signInManager = signInManager;
        }
    
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetMe()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }
        
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return NoContent();
        }
        
        
    }
}
