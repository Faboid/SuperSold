using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;
using System.Security.Claims;

namespace SuperSold.UI.AspDotNet.Controllers;
public class ProductsController : Controller {

    private readonly IProductsHandler _productsHandler;

    public ProductsController(IProductsHandler productsHandler) {
        _productsHandler = productsHandler;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyProducts(int? row) {

        var userName = User.Identity!.Name!;

        var listProducts = await _productsHandler
            .QueryProductsBySellerUserName(userName)
            .Select(x => (Product)x)
            .Skip((row ?? 0) * 10)
            .Take(10)
            .ToListAsyncSafe();

        return View(listProducts);

    }

    [HttpGet]
    [Authorize]
    public IActionResult Publish() => View();

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Publish(Product product) {

        if(!ModelState.IsValid) {
            return View();
        }

        var user = User.Identity!.Name!;
        var sellerId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        product.Id = Guid.NewGuid();
        product.SellerId = Guid.Parse(sellerId);

        var result = await _productsHandler.CreateProduct(product, user);

        return result.Match<IActionResult>(
            success => Redirect("MyProducts"),
            alreadyExists => {
                ViewBag.Message = "There has been a duplication error. It's suggested to reload the page and re-enter the values.";
                //todo - this should never happen. Add logging and consider returning a 500 error
                return View();
            }
        );

    }

}
