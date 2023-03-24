using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;
namespace SuperSold.UI.AspDotNet.ViewRouting;

public class WishlistRoutes {

    private readonly IUrlHelper _urlHelper;
    private const string _controllerName = "Wishlist";

    public WishlistRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? Index() => _urlHelper.Action(nameof(WishlistController.Index), _controllerName);
    public string? IndexPartial() => _urlHelper.Action("IndexPartial", _controllerName);
    public string? AddToWishlist() => _urlHelper.Action("AddToWishlist", _controllerName);
    public string? Remove() => _urlHelper.Action("Remove", _controllerName);

}