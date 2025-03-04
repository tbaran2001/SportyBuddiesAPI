﻿using Microsoft.AspNetCore.SignalR;

namespace SportyBuddies.Application.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(HubMessage message)
    {
        await Clients.Users(message.Participants.Select(p => p.ToString()).ToList()).ReceiveMessage(message);
    }
}