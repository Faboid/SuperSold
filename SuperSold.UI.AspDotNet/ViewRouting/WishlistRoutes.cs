using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;
namespace SuperSold.UI.AspDotNet.ViewRouting;

public class WishlistRoutes {

    private readonly IUrlHelper _urlHelper;

    public WishlistRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? Index() => _urlHelper.Action(nameof(WishlistController.Index), "Wishlist");
    public string? IndexPartial() => _urlHelper.Action("IndexPartial", "Wishlist");
    public string? AddToWishlist() => _urlHelper.Action("AddToWishlist", "Wishlist");

}