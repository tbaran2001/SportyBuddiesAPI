﻿using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;

namespace SportyBuddies.Application.Messages.Queries.GetUserMessages;

public record GetUserMessagesQuery(Guid UserId) : IRequest<ErrorOr<IEnumerable<MessageResponse>>>;