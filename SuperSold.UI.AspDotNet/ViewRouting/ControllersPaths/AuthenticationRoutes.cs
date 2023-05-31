using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class AuthenticationRoutes : BaseRoutes
{

    public AuthenticationRoutes(IUrlHelper urlHelper) : base(urlHelper, "Authentication") { }

    public string? SignUp() => BuildUrlToAction("SignUp");
    public string? Login() => BuildAbsoluteUrlToAction("Login");
    public string? Logout() => BuildUrlToAction("Logout");

}
