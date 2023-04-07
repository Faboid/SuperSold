using Microsoft.AspNetCore.Authentication;
using SuperSold.UI.AspDotNet.Constants;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Services;

public class AuthCookieService : IAuthCookieService {

    private readonly IHttpContextAccessor _http;
    private HttpContext Context => _http.HttpContext ?? throw new ArgumentNullException(nameof(_http.HttpContext), "The http context has not been properly injected.");

    public AuthCookieService(IHttpContextAccessor http) {
        _http = http;
    }

    public async Task Login(ClaimsPrincipal principal, AuthenticationProperties properties) => await Context!.SignInAsync(Cookies.Auth, principal, properties);
    public async Task Logout() => await Context.SignOutAsync(Cookies.Auth);
    public ClaimsPrincipal GetPrincipal() => Context.User;
    public AuthenticationProperties GetProperties() => Context.Features.Get<IAuthenticateResultFeature>()?.AuthenticateResult?.Properties ?? new();
}
