using Microsoft.AspNetCore.Mvc;
using SuperSold.UI.AspDotNet.Controllers;
using SuperSold.UI.AspDotNet.ViewRouting.ControllersPaths;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public static class RoutesBuilder {

    public static ProfileRoutes Profile(this IUrlHelper urlHelper) => new(urlHelper);
    public static CartRoutes Cart(this IUrlHelper urlHelper) => new(urlHelper);
    public static WishlistRoutes Wishlist(this IUrlHelper urlHelper) => new(urlHelper);
    public static AuthenticationRoutes Authentication(this IUrlHelper urlHelper) => new(urlHelper);
    public static HomeRoutes Home(this IUrlHelper urlHelper) => new(urlHelper);
    public static ProductsRoutes Products(this IUrlHelper urlHelper) => new(urlHelper);
    public static SavedRelationshipsRoutes SavedRelationships(this IUrlHelper urlHelper) => new(urlHelper);
    public static PurchaseRoutes Purchase(this IUrlHelper urlHelper) => new(urlHelper);
    public static RollbacksRoutes Rollbacks(this IUrlHelper urlHelper) => new(urlHelper);

}