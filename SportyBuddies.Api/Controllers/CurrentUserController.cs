using System.IO.Compression;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Application.Buddies.Queries.GetUserBuddies;
using SportyBuddies.Application.Matches.Commands.UpdateMatch;
using SportyBuddies.Application.Matches.Queries.GetRandomMatch;
using SportyBuddies.Application.Matches.Queries.GetUserMatches;
using SportyBuddies.Application.Messages.Commands.SendMessage;
using SportyBuddies.Application.Messages.Queries.GetLastUserMessages;
using SportyBuddies.Application.Messages.Queries.GetUserMessages;
using SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;
using SportyBuddies.Application.Users.Commands.UpdateUser;
using SportyBuddies.Application.Users.Commands.UpdateUserPreferences;
using SportyBuddies.Application.Users.Commands.UploadPhoto;
using SportyBuddies.Application.Users.Queries.GetUser;
using SportyBuddies.Application.Users.Queries.GetUserMainPhoto;
using SportyBuddies.Application.Users.Queries.GetUserPhotos;
using SportyBuddies.Application.UserSports.Commands.AddUserSport;
using SportyBuddies.Application.UserSports.Commands.RemoveUserSport;
using SportyBuddies.Application.UserSports.Queries.GetUserSports;
using SportyBuddies.Contracts.Matches;
using SportyBuddies.Contracts.Messages;
using SportyBuddies.Contracts.Users;
using SportyBuddies.Identity.Models;
using Swipe = SportyBuddies.Domain.Matches.Swipe;
using Gender = SportyBuddies.Domain.Users.Gender;

namespace SportyBuddies.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CurrentUserController(UserManager<ApplicationUser> userManager, ISender mediator) : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserQuery(Guid.Parse(userId));

            var userResult = await mediator.Send(query);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCurrentUser(UpdateUserRequest userRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserCommand(Guid.Parse(userId), userRequest.Name, userRequest.Description,
                (Gender)userRequest.Gender, userRequest.DateOfBirth);

            var userResult = await mediator.Send(command);

            return userResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("sports")]
        public async Task<IActionResult> GetCurrentUserSports()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserSportsQuery(Guid.Parse(userId));

            var userSportsResult = await mediator.Send(query);

            return userSportsResult.Match(
                Ok,
                Problem);
        }

        [HttpPost("sports/{sportId}")]
        public async Task<IActionResult> AddSportToCurrentUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new AddUserSportCommand(Guid.Parse(userId), sportId);

            var userSportResult = await mediator.Send(command);

            return userSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpDelete("sports/{sportId}")]
        public async Task<IActionResult> RemoveSportFromCurrentUser(Guid sportId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new RemoveUserSportCommand(Guid.Parse(userId), sportId);

            var userSportResult = await mediator.Send(command);

            return userSportResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpGet("matches")]
        public async Task<IActionResult> GetCurrentUserMatches(bool includeUsers = false)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMatchesQuery(Guid.Parse(userId), includeUsers);

            var matchesResult = await mediator.Send(query);

            return matchesResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("matches/random")]
        public async Task<IActionResult> GetRandomMatch()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetRandomMatchQuery(Guid.Parse(userId));

            var matchResult = await mediator.Send(query);

            return matchResult.Match(
                Ok,
                Problem);
        }

        [HttpPut("matches/{matchId}")]
        public async Task<IActionResult> UpdateMatch(Guid matchId, UpdateMatchRequest matchRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateMatchCommand(matchId, (Swipe)matchRequest.Swipe);

            var matchResult = await mediator.Send(command);

            return matchResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpGet("buddies")]
        public async Task<IActionResult> GetCurrentUserBuddies(bool includeUsers = false)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserBuddiesQuery(Guid.Parse(userId), includeUsers);

            var buddiesResult = await mediator.Send(query);

            return buddiesResult.Match(
                Ok,
                Problem);
        }

        [HttpPost("messages/{recipientId}")]
        public async Task<IActionResult> SendMessage(Guid recipientId, SendMessageRequest messageRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new SendMessageCommand(Guid.Parse(userId), recipientId, messageRequest.Content);

            var messageResult = await mediator.Send(command);

            return messageResult.Match<IActionResult>(
                _ => NoContent(),
                Problem);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetUserMessages()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMessagesQuery(Guid.Parse(userId));

            var messagesResult = await mediator.Send(query);

            return messagesResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("messages/{buddyId}")]
        public async Task<IActionResult> GetUserMessagesByBuddyId(Guid buddyId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMessagesByBuddyIdQuery(Guid.Parse(userId), buddyId);

            var messagesResult = await mediator.Send(query);

            return messagesResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("messages/last")]
        public async Task<IActionResult> GetLastUserMessages()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetLastUserMessagesQuery(Guid.Parse(userId));

            var messagesResult = await mediator.Send(query);

            return messagesResult.Match(
                Ok,
                Problem);
        }

        [HttpPost("photos")]
        public async Task<IActionResult> UploadPhoto([FromForm] IFormFile file, bool isMain = false)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UploadPhotoCommand(Guid.Parse(userId), isMain, file);

            var photoResult = await mediator.Send(command);

            return photoResult.Match(
                Ok,
                Problem);
        }

        [HttpGet("photos/main")]
        public async Task<IActionResult> GetUserMainPhoto()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMainPhotoQuery(Guid.Parse(userId));

            var photoResult = await mediator.Send(query);

            var stream = System.IO.File.OpenRead(photoResult.Value);
            return File(stream, "image/jpeg");
        }

        [HttpGet("photos")]
        public async Task<IActionResult> GetUserPhotos()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserPhotosQuery(Guid.Parse(userId));

            var photosResult = await mediator.Send(query);

            var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var photo in photosResult.Value)
                {
                    var entry = archive.CreateEntry(Path.GetFileName(photo.Url));
                    using (var entryStream = entry.Open())
                    {
                        await using var fileStream = System.IO.File.OpenRead(photo.Url);
                        await fileStream.CopyToAsync(entryStream);
                    }
                }
            }

            zipStream.Seek(0, SeekOrigin.Begin);
            return File(zipStream, "application/zip", "photos.zip");
        }

        [HttpPut("preferences")]
        public async Task<IActionResult> UpdateUserPreferences(UpdateUserPreferencesRequest preferencesRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new UpdateUserPreferencesCommand(Guid.Parse(userId), preferencesRequest.MinAge,
                preferencesRequest.MaxAge, (Gender)preferencesRequest.Gender);

            var preferencesResult = await mediator.Send(command);

            return preferencesResult.Match(
                _ => NoContent(),
                Problem);
        }
    }
}