using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddiesAPI.Entities;
using SportyBuddiesAPI.Models;
using SportyBuddiesAPI.Services;

namespace SportyBuddiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _sportyBuddiesRepository = sportyBuddiesRepository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId=_userManager.GetUserId(User);
            if (userId== null)
            {
                return Unauthorized();
            }
            var user = await _sportyBuddiesRepository.GetUserAsync(userId,true);
            return Ok(_mapper.Map<UserCurrentDto>(user));
        }
    }
}
