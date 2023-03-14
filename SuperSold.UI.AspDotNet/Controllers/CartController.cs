using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using SuperSold.UI.AspDotNet.ViewRouting;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class CartController : Controller {

    private readonly IWishlistHandler _wishlistHandler;
    private readonly ICartHandler _cartHandler;

    public CartController(IWishlistHandler wishlistHandler, ICartHandler cartHandler) {
        _wishlistHandler = wishlistHandler;
        _cartHandler = cartHandler;
    }

    [HttpGet]
    public IActionResult WishList() => View();

    [HttpGet]
    public async Task<IActionResult> WishListPartial(int page = 0) {

        var userId = User.GetIdentity();
        var products = await _wishlistHandler.QueryWishlistedProductsByUserId(userId)
            .SkipToPage(page, 3)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        return this.ProductListPartialView(PartialViewNames.WishlistProductRowPartial, products);
    }

    [HttpGet]
    [Route("/Cart/View")]
    public IActionResult ViewCart() => View();

    [HttpGet]
    public async Task<IActionResult> ViewCartPartial(int page = 0) {

        var userId = User.GetIdentity();
        var products = await _cartHandler.QueryCartedProductsByUserId(userId)
            .SkipToPage(page, 3)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        return this.ProductListPartialView(PartialViewNames.CartProductRowPartial, products);
    }

    [HttpGet]
    public IActionResult Buy() {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _cartHandler.AddToCart(userId, productId);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(AddToCart), null),
            alreadyexists => Conflict()
        );
    }

    [HttpPost]
    public async Task<IActionResult> AddToWishlist(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _wishlistHandler.WishlistProduct(userId, productId);

        return result.Match<IActionResult>(
            success => CreatedAtAction(nameof(AddToWishlist), null),
            alreadyexists => BadRequest()
        );
        
    }

}
