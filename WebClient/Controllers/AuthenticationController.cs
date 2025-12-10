using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebClient.Controllers;

public static class AuthenticationController
{
    public static RouteGroupBuilder AddAuthenticationController(this WebApplication app)
    {
        var group = app.MapGroup("/api/authentication");

        group.MapPost("/login", async ([FromForm] string username, [FromForm] string password, HttpContext httpContext) =>
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });
            var result = await client.PostAsync("https://localhost:54915/api/user/Login", content);

            if (result != null && result.IsSuccessStatusCode)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "User")
                };
                var claimsIdentity = new ClaimsIdentity(claims, "cookie");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                var authenticationProperties = new AuthenticationProperties() { IsPersistent = true };
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return Results.Redirect("/");
            }

            return Results.Unauthorized();
        }).DisableAntiforgery();

        return group;
    }
}
