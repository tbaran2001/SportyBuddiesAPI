using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ErrorsController(ProblemDetailsFactory problemDetailsFactory)
    {
        _problemDetailsFactory = problemDetailsFactory;
    }

    [Route("/errors")]
    public IActionResult Errors()
    {
        var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandler.Error;

        var problem = new ProblemDetails
        {
            Instance = exceptionHandler.Path,
            Detail = exception.Message
        };

        switch (exception)
        {
            case BadRequestException badRequestException:
                problem.Status = (int)HttpStatusCode.BadRequest;
                break;
            case NotFoundException notFound:
                problem.Status = (int)HttpStatusCode.NotFound;
                break;
            case ValidationException validationException:
                foreach (var validationError in validationException.Errors)
                    problem.Extensions.Add(validationError.Key, validationError.Value);

                problem.Status = (int)HttpStatusCode.BadRequest;
                break;
            default:
                problem.Status = (int)HttpStatusCode.InternalServerError;
                break;
        }

        return MyProblem(problem);
    }

    public ObjectResult MyProblem(ProblemDetails problem)
    {
        ProblemDetails problemDetails;
        problemDetails = ProblemDetailsFactory.CreateProblemDetails(HttpContext, problem.Status);

        problem.Type = problemDetails.Type;
        problem.Title = problemDetails.Title;
        return new ObjectResult(problem)
        {
            StatusCode = problem.Status
        };
    }
}