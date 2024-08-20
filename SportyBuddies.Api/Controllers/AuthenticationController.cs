using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Services.Authentication;
using SportyBuddies.Contracts.Authentication;

namespace SportyBuddies.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult =
                _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email,
                authResult.Token);

            return Ok(authResult);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);

            var response = new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email,
                authResult.Token);

            return Ok(response);
        }
    }
}