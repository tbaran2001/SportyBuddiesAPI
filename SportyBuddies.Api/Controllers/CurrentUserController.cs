using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Users.Queries.GetUser;
using SportyBuddies.Contracts.Users;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrentUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CurrentUserController(UserManager<ApplicationUser> userManager, ISender mediator, IMapper mapper)
        {
            _userManager = userManager;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            
            var query = new GetUserQuery(Guid.Parse(userId));
            
            var user = await _mediator.Send(query);
            
            return Ok(_mapper.Map<UserResponse>(user));
        }
    }
}
