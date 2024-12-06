namespace SportyBuddies.Application.Exceptions;

public class NotFoundException(string name, string key) : Exception($"{name} ({key}) was not found");