using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;

namespace SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

public class WishlistRoutes : BaseRoutes
{

    public WishlistRoutes(IUrlHelper urlHelper) : base(urlHelper, "Wishlist") { }

    public string? IndexPartial() => BuildUrlToAction("IndexPartial");
    public string? AddToWishlist() => BuildUrlToAction("AddToWishlist");
    public string? Remove() => BuildUrlToAction("Remove");
    public string? MoveToCart() => BuildUrlToAction("MoveToCart");

}