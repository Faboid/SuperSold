using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class RoutesBuilder {

    public static CartRoutes Cart(this IUrlHelper urlHelper) => new(urlHelper);
    public static WishlistRoutes Wishlist(this IUrlHelper urlHelper) => new(urlHelper);
    public static AccountRoutes Account(this IUrlHelper urlHelper) => new(urlHelper);
    public static HomeRoutes Home(this IUrlHelper urlHelper) => new(urlHelper);
    public static ProductsRoutes Products(this IUrlHelper urlHelper) => new(urlHelper);

}