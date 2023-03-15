using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class CartRoutes {

    public static string? CartIndex(this IUrlHelper urlHelper) => urlHelper.Action("Index", "Cart");
    public static string? CartIndexPartial(this IUrlHelper urlHelper) => urlHelper.Action("IndexPartial", "Cart");
    public static string? CartAddToCart(this IUrlHelper urlHelper) => urlHelper.Action("AddToCart", "Cart");
    public static string? CartBuy(this IUrlHelper urlHelper) => urlHelper.Action("Buy", "Cart");

}
