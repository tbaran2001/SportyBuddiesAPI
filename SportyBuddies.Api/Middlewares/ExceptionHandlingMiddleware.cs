using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SportyBuddies.Application.Exceptions;

namespace SportyBuddies.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        ProblemDetails problemDetails;
        switch (exception)
        {
            case BadRequestException badRequestException:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.BadRequest, detail: badRequestException.Message);
                break;
            case ConflictException conflictException:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.Conflict, detail: conflictException.Message);
                break;
            case ForbiddenException forbiddenException:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.Forbidden, detail: forbiddenException.Message);
                break;
            case NotFoundException notFoundException:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.NotFound, detail: notFoundException.Message);
                break;
            case UnauthorizedException unauthorizedException:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.Unauthorized, detail: unauthorizedException.Message);
                break;
            case ValidationException validationException:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.BadRequest, detail: validationException.Message);
                foreach (var error in validationException.Errors)
                    problemDetails.Extensions.Add(error.Key, error.Value);
                break;
            default:
                problemDetails = problemDetailsFactory.CreateProblemDetails(httpContext,
                    (int)HttpStatusCode.InternalServerError, detail: exception.Message);
                break;
        }

        var result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

        httpContext.Response.ContentType = "application/problem+json";
        await httpContext.Response.WriteAsJsonAsync(result.Value);
    }
}