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
    public class AuthController : ControllerBase
    {
        private readonly ISportyBuddiesRepository _sportyBuddiesRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(ISportyBuddiesRepository sportyBuddiesRepository, IMapper mapper,
            UserManager<User> userManager, SignInManager<User> signInManager)
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
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _sportyBuddiesRepository.GetUserAsync(userId, true);
            return Ok(_mapper.Map<UserCurrentDto>(user));
        }

        [HttpPatch("me")]
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
    }
}