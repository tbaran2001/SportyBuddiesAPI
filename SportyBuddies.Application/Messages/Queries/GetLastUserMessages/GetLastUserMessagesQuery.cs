﻿using ErrorOr;
using MediatR;
using SportyBuddies.Application.Common.DTOs.Message;

namespace SportyBuddies.Application.Messages.Queries.GetLastUserMessages;

public record GetLastUserMessagesQuery(Guid UserId) : IRequest<ErrorOr<IEnumerable<MessageResponse>>>;