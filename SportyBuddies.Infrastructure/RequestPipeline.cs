using Microsoft.AspNetCore.Builder;
using SportyBuddies.Infrastructure.Middleware;

namespace SportyBuddies.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<EventualConsistencyMiddleware>();

        return builder;
    }
}