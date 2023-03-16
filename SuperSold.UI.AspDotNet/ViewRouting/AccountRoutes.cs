using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class AccountRoutes {

    private readonly IUrlHelper _urlHelper;

    public AccountRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? SignUp() => _urlHelper.Action("SignUp", "Account");
    public string? Login() => _urlHelper.Action("Login", "Account");
    public string? Logout() => _urlHelper.Action("Logout", "Account");

}
