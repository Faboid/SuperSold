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

    private readonly ICartHandler _cartHandler;

    public CartController(ICartHandler cartHandler) {
        _cartHandler = cartHandler;
    }

    [HttpGet]
    [Route("/Cart/View")]
    public IActionResult MainRoute() => View("View");

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> IndexPartial(int page = 0) {

        var userId = User.GetIdentity();
        var products = await _cartHandler.QueryCartedProductsByUserId(userId)
            .SkipToPage(page, 3)
            .Select(x => (Product)x)
            .ToListAsyncSafe();

        return this.ProductListPartialView(PartialViewNames.MyCartRow, products);
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
            success => RedirectToAction(nameof(Index)),
            alreadyexists => RedirectToAction(nameof(Index))
        );
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid productId) {

        var userId = User.GetIdentity();
        var result = await _cartHandler.RemoveFromCart(userId, productId);
        return result.Match<IActionResult>(
            success => Ok(),
            notfound => NotFound()
        );

    }

}
