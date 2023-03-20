using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
public class WishlistController : Controller {

    private readonly IWishlistHandler _wishlistHandler;

    public WishlistController(IWishlistHandler wishlistHandler) {
        _wishlistHandler = wishlistHandler;
    }

    [HttpGet]
    public IActionResult Index() {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page = 0) {

        var userId = User.GetIdentity();
        var products = await _wishlistHandler.QueryWishlistedProductsByUserId(userId)
            .SkipToPage(page, 3)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        return this.ProductListPartialView(PartialViewNames.WishlistRow, products);
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _wishlistHandler.WishlistProduct(userId, productId);

        return result.Match<IActionResult>(
            success => RedirectToAction(nameof(Index)),
            alreadyexists => RedirectToAction(nameof(Index))
        );

    }

}
