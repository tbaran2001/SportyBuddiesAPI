namespace SportyBuddies.Application.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(HubMessage message);
}