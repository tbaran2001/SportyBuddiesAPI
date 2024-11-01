using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Messages;
using SportyBuddies.Application.Messages.Commands.SendMessage;
using SportyBuddies.Application.Messages.Queries.GetLastUserMessages;
using SportyBuddies.Application.Messages.Queries.GetUserMessagesWithBuddy;
using SportyBuddies.Identity.Models;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class MessagesController(UserManager<ApplicationUser> userManager, ISender mediator) : ControllerBase
    {
        [HttpGet("LastMessages")]
        public async Task<IActionResult> GetLastUserMessages()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetLastUserMessagesQuery(Guid.Parse(userId));

            var messagesResult = await mediator.Send(query);

            return Ok(messagesResult);
        }
        
        [HttpPost("{recipientId}")]
        public async Task<IActionResult> SendMessage(Guid recipientId, SendMessageRequest messageRequest)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var command = new SendMessageCommand(Guid.Parse(userId), recipientId, messageRequest.Content);

            await mediator.Send(command);

            return NoContent();
        }
        
        [HttpGet("{buddyId}")]
        public async Task<IActionResult> GetUserMessagesByBuddyId(Guid buddyId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var query = new GetUserMessagesByBuddyIdQuery(Guid.Parse(userId), buddyId);

            var messagesResult = await mediator.Send(query);

            return Ok(messagesResult);
        }
    }
}