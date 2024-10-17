using Microsoft.AspNetCore.SignalR;

namespace SportyBuddies.Application.Hubs;

public class ChatHub : Hub<IChatClient>
{
    public async Task SendMessage(HubMessage message)
    {
        await Clients.User(message.RecipientId.ToString()).ReceiveMessage(message);
        await Clients.User(message.SenderId.ToString()).ReceiveMessage(message);
    }
}