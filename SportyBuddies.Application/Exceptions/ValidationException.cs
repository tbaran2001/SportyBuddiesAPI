namespace SportyBuddies.Application.Exceptions;

public class ValidationException(IDictionary<string, string[]> errors)
    : Exception("Validation failed for one or more fields")
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}