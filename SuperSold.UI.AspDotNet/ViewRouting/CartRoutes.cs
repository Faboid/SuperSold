using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class CartRoutes {

    private readonly IUrlHelper _urlHelper;

    public CartRoutes(IUrlHelper urlHelper) {
        _urlHelper = urlHelper;
    }

    public string? Index() => _urlHelper.Action("Index", "Cart");
    public string? IndexPartial() => _urlHelper.Action("IndexPartial", "Cart");
    public string? AddToCart() => _urlHelper.Action("AddToCart", "Cart");
    public string? Remove() => _urlHelper.Action("Remove", "Cart");
    public string? MoveToWishlist() => _urlHelper.Action("MoveToWishlist", "Cart");
    public string? Buy() => _urlHelper.Action("Buy", "Cart");

}
