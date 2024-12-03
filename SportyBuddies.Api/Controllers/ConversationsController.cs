using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportyBuddies.Api.Contracts.Conversations;
using SportyBuddies.Api.Contracts.Messages;
using SportyBuddies.Application.Common.DTOs.Conversation;
using SportyBuddies.Application.Features.Conversations.Commands.CreateConversation;
using SportyBuddies.Application.Features.Conversations.Commands.SendMessage;
using SportyBuddies.Application.Features.Conversations.Queries.GetConversation;
using SportyBuddies.Application.Features.Conversations.Queries.GetConversationMessages;
using SportyBuddies.Application.Features.Conversations.Queries.GetLastMessageFromEachUserConversation;

namespace SportyBuddies.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ConversationsController(ISender mediator)
        : ControllerBase
    {
        [HttpGet("{conversationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ConversationResponse>> GetConversation(Guid conversationId)
        {
            var query = new GetConversationQuery(conversationId);
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CreateConversationResponse>> CreateConversation(
            CreateConversationRequest request)
        {
            var command = new CreateConversationCommand(request.ParticipantId);
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPost("{conversationId}/Messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<MessageResponse>> SendMessage(Guid conversationId, SendMessageRequest request)
        {
            var command = new SendMessageCommand(conversationId, request.Content);
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{conversationId}/Messages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetConversationMessages(Guid conversationId)
        {
            var query = new GetConversationMessagesQuery(conversationId);
            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("LastMessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetLastMessageFromEachUserConversation()
        {
            var query = new GetLastMessageFromEachUserConversationQuery();
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}