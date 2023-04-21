using Microsoft.AspNetCore.Mvc;

namespace SuperSold.UI.AspDotNet.ViewRouting;

public class CartRoutes : BaseRoutes {

    public CartRoutes(IUrlHelper urlHelper) : base(urlHelper, "Cart") { }

    public string? Index() => BuildUrlToAction("Index");
    public string? IndexPartial() => BuildUrlToAction("IndexPartial");
    public string? AddToCart() => BuildUrlToAction("AddToCart");
    public string? Remove() => BuildUrlToAction("Remove");
    public string? MoveToWishlist() => BuildUrlToAction("MoveToWishlist");
    public string? Buy() => BuildUrlToAction("Buy");

}
