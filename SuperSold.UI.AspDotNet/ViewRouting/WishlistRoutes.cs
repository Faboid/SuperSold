using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;
namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class WishlistRoutes {

    public static string? WishlistIndex(this IUrlHelper urlHelper) => urlHelper.Action(nameof(WishlistController.Index), "Wishlist");
    public static string? WishlistIndexPartial(this IUrlHelper urlHelper) => urlHelper.Action("IndexPartial", "Wishlist");
    public static string? WishlistAddToWishlist(this IUrlHelper urlHelper) => urlHelper.Action("AddToWishlist", "Wishlist");

}
