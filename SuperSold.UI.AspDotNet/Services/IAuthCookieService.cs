using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Services;

public interface IAuthCookieService {

    Task Login(ClaimsPrincipal principal, AuthenticationProperties properties);
    Task Logout();

    ClaimsPrincipal GetPrincipal();
    AuthenticationProperties GetProperties();

}
