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

        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        var listProducts = await _productsHandler
            .QueryProductsBySellerId(Guid.Parse(userId))
            .Select(x => (Product)x)
            .SkipToPage(row, 10)
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

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Delete(Product model) {

        if(!ModelState.IsValid) {
            return RedirectToAction("MyProducts");
        }

        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        if(userId != model.SellerId.ToString()) {
            return new UnauthorizedResult();
        }

        var result = await _productsHandler.DeleteProduct(model.Id);
        return result.Match<IActionResult>(
            success => RedirectToAction("MyProducts"),
            notFound => NotFound()
        );

    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Edit(Guid id) {

        var product = await _productsHandler.GetProduct(id);

        if(product.TryPickT1(out var notFound, out var model)) {
            return NotFound();
        }

        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        if(model.IdSellerAccount.ToString() != userId) {
            return new UnauthorizedResult();
        }

        return View((Product)model);

    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Edit(Product model) {

        if(!ModelState.IsValid) {
            return View(model);
        }
        
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        if(model.SellerId.ToString() != userId) {
            return new UnauthorizedResult();
        }

        var result = await _productsHandler.EditProduct(model.Id, model);
        return result.Match<IActionResult>(
            success => RedirectToAction("MyProducts"),
            notFound => NotFound()
        );

    }

}
