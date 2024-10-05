// File: SportyBuddies.Api.IntegrationTests/Helpers/AuthHelper.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportyBuddies.Identity.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SportyBuddies.Api.IntegrationTests.Helpers;

public static class AuthHelper
{
    public static async Task<HttpClient> AuthenticateUserAsync(HttpClient client, IServiceProvider services, string userName, string password)
    {
        using var scope = services.CreateScope();
        var scopedServices = scope.ServiceProvider;
        var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
        var signInManager = scopedServices.GetRequiredService<SignInManager<ApplicationUser>>();
        var httpContextAccessor = scopedServices.GetRequiredService<IHttpContextAccessor>();
        var authenticationService = scopedServices.GetRequiredService<IAuthenticationService>();

        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            user = new ApplicationUser { UserName = userName, Email = $"{userName}@example.com" };
            await userManager.CreateAsync(user, password);
        }

        var httpContext = new DefaultHttpContext { RequestServices = scopedServices };
        httpContextAccessor.HttpContext = httpContext;

        await signInManager.SignInAsync(user, isPersistent: false);

        var authCookie = httpContext.Response.Headers["Set-Cookie"].ToString();
        client.DefaultRequestHeaders.Add("Cookie", authCookie);

        return client;
    }
}