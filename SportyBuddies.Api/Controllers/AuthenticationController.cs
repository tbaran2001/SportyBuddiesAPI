using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Contracts.Authentication;
using ErrorOr;
using MediatR;
using SportyBuddies.Application.Authentication.Commands.Register;
using SportyBuddies.Application.Authentication.Common;
using SportyBuddies.Application.Authentication.Queries.Login;
using SportyBuddies.Domain.Common.Errors;

namespace SportyBuddies.Api.Controllers
{
    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;

        public AuthenticationController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            return authResult.Match(
                result => Ok(MapAuthResult(result)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var authResult = await _mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }

            return authResult.Match(
                result => Ok(MapAuthResult(result)),
                errors => Problem(errors));
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName,
                authResult.User.Email, authResult.Token);
        }
    }
}