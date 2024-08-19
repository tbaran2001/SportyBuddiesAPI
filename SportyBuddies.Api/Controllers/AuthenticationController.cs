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
            var result =
                _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

            var response = new AuthenticationResponse(result.Id, result.FirstName, result.LastName, result.Email,
                result.Token);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authenticationService.Login(request.Email, request.Password);

            var response = new AuthenticationResponse(result.Id, result.FirstName, result.LastName, result.Email,
                result.Token);

            return Ok(response);
        }
    }
}