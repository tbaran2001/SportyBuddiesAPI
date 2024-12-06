namespace SportyBuddies.Application.Authentication;

public interface IUserContext
{
    CurrentUser GetCurrentUser();
}