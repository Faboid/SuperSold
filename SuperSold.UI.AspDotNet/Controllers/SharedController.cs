using Microsoft.AspNetCore.Mvc;
using SuperSold.Data.DBInteractions;
using SuperSold.UI.AspDotNet.Extensions;
using SuperSold.UI.AspDotNet.Models;

namespace SuperSold.UI.AspDotNet.Controllers;
public class SharedController : Controller {

    private readonly IProductsHandler _productsHandler;

    public SharedController(IProductsHandler productsHandler) {
        _productsHandler = productsHandler;
    }

    [HttpGet]
    [RequireHttps]
    public async Task<IActionResult>  RenderProductList(string rowPartialName) {
        var products = await _productsHandler.QueryAllProducts().Take(3).Select(x => (Product)x).ToListAsyncSafe();
        return PartialView("_ProductListPartialView", (rowPartialName, (ICollection<Product>)products));
    }

}
