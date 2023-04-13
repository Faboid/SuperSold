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
    public static SavedRelationshipsRoutes SavedRelationships(this IUrlHelper urlHelper) => new(urlHelper);
    public static PurchaseRoutes Purchase(this IUrlHelper urlHelper) => new(urlHelper);

}

public class SavedRelationshipsRoutes {

    private readonly IUrlHelper _urlHelper;
    private const string _controllerName = "SavedRelationships";

    public SavedRelationshipsRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? ModifyQuantity() => _urlHelper.Action("ModifyQuantity", _controllerName);

}