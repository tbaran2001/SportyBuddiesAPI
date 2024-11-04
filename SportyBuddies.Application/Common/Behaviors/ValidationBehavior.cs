using FluentValidation;
using MediatR;
using ValidationException = SportyBuddies.Application.Exceptions.ValidationException;

namespace SportyBuddies.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validator is null)
            return await next();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var errors = validationResult.Errors
            .GroupBy(x => x.PropertyName, x => x.ErrorMessage)
            .ToDictionary(x => x.Key, x => x.ToArray());

        if (errors.Any()) throw new ValidationException(errors);

        return await next();
    }
}