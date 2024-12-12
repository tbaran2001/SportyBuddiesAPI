using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Profiles;
using SportyBuddies.Application.Common.DTOs.Profile;
using SportyBuddies.Application.Features.Profiles.Commands.DeleteProfile;
using SportyBuddies.Application.Features.Profiles.Commands.UpdateProfile;
using SportyBuddies.Application.Features.Profiles.Commands.UpdateProfilePreferences;
using SportyBuddies.Application.Features.Profiles.Commands.UploadPhoto;
using SportyBuddies.Application.Features.Profiles.Queries.GetCurrentProfile;
using SportyBuddies.Application.Features.Profiles.Queries.GetProfile;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProfilesController(ISender mediator)
        : ControllerBase
    {
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileWithSportsResponse>> GetCurrentProfile()
        {
            var query = new GetCurrentProfileQuery();

            var profileResult = await mediator.Send(query);

            return Ok(profileResult);
        }

        [HttpGet("{profileId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileWithSportsResponse>> GetProfile(Guid profileId)
        {
            var query = new GetProfileQuery(profileId);

            var profileResult = await mediator.Send(query);

            return Ok(profileResult);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProfileResponse>> UpdateCurrentProfile(UpdateProfileRequest profileRequest)
        {
            var command = new UpdateProfileCommand(profileRequest.Name, profileRequest.Description,
                profileRequest.Gender, profileRequest.DateOfBirth);

            var profileResult = await mediator.Send(command);

            return Ok(profileResult);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteCurrentProfile()
        {
            var command = new DeleteProfileCommand();

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut("Preferences")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateProfilePreferences(UpdateProfilePreferencesRequest preferencesRequest)
        {
            var command = new UpdateProfilePreferencesCommand(preferencesRequest.MinAge,
                preferencesRequest.MaxAge, preferencesRequest.MaxDistance, preferencesRequest.Gender);

            await mediator.Send(command);

            return NoContent();
        }

        [HttpPost("Photos")]
        public async Task<IActionResult> UploadPhoto([FromForm]IFormFile file)
        {
            await using var stream = file.OpenReadStream();

            var command = new UploadPhotoCommand(stream, file.FileName);

            var result = await mediator.Send(command);

            return Ok(result);
        }
    }
}