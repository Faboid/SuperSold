using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class AuthenticationRoutes {

    private readonly IUrlHelper _urlHelper;

    public AuthenticationRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? SignUp() => _urlHelper.Action("SignUp", "Authentication");
    public string? Login() => _urlHelper.Action("Login", "Authentication");
    public string? Logout() => _urlHelper.Action("Logout", "Authentication");

}
