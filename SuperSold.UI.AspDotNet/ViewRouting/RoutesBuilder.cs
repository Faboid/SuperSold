using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class RoutesBuilder {

    public static ProfileRoutes Profile(this IUrlHelper urlHelper) => new(urlHelper);
    public static CartRoutes Cart(this IUrlHelper urlHelper) => new(urlHelper);
    public static WishlistRoutes Wishlist(this IUrlHelper urlHelper) => new(urlHelper);
    public static AuthenticationRoutes Authentication(this IUrlHelper urlHelper) => new(urlHelper);
    public static HomeRoutes Home(this IUrlHelper urlHelper) => new(urlHelper);
    public static ProductsRoutes Products(this IUrlHelper urlHelper) => new(urlHelper);

}

public class ProfileRoutes {

    private readonly IUrlHelper _urlHelper;
    private const string _controllerName = "Profile";

    public ProfileRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? Index() => _urlHelper.Action("Index", _controllerName);
    public string? IsUsernameUnique() => _urlHelper.Action("IsUsernameUnique", _controllerName);
    public string? RenameAccount() => _urlHelper.Action("RenameAccount", _controllerName);
    public string? ChangeEmail() => _urlHelper.Action("ChangeEmail", _controllerName);
    public string? ChangePassword() => _urlHelper.Action("ChangePassword", _controllerName);
    public string? DeleteAccount() => _urlHelper.Action("DeleteAccount", _controllerName);

}