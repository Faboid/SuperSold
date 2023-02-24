using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

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
        var products = await _productsHandler.QueryProductsBySellerUserName(userName);
        var listProducts = await products.Select(x => (Product)x).Skip((row ?? 0) * 10).Take(10).ToListAsyncSafe();
        return View(listProducts);

    }

}
