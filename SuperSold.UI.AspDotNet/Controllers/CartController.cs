using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;

[Authorize]
[AutoValidateAntiforgeryToken]
public class CartController : Controller {

    private readonly IProductsHandler _productsHandler;

    public CartController(IProductsHandler productsHandler) {
        _productsHandler = productsHandler;
    }

    [HttpGet]
    public IActionResult WishList(int? page = 0) {
        return View();
    }

    [HttpGet]
    [Route("/Cart/View")]
    public IActionResult ViewCart(int? page = 0) {
        return View();
    }

    [HttpGet]
    public IActionResult Buy() {
        return View();
    }

    [HttpPost]
    public Task<IActionResult> AddToCart(Product product) {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> AddToWishlist(Product product) {
        throw new NotImplementedException();
    }

}
